using System;
using System.Drawing;
using System.Linq;
using Findwise.Configuration;
using Findwise.InstallerModule;
using Microsoft.Office.Server.Search.Administration;
using Findwise.Sharepoint.SolutionInstaller;
using System.Collections.Generic;
using static ManagedPropertiesCreator.Configuration;
using ManagedPropertiesCreator.Properties;
using System.ComponentModel;

namespace ManagedPropertiesCreator
{
    [Category(ModuleCategoryNames.SharepointSearch)]
    public class ManagedPropertiesCreator : InstallerModuleBase
    {
        public override string Name => "Managed Properties Creator";

        public override System.Drawing.Image Icon => Resources.if_accessories_text_editor_15334;

        private Configuration myConfiguration = new Configuration();
        public override ConfigurationBase Configuration { get => myConfiguration; set => myConfiguration = value as Configuration; }

        public override void CheckStatus()
        {
            Status = InstallerModuleStatus.Refreshing;
            try
            {
                var content = SearchApplicationContent(myConfiguration.SearchApplicationName);
                var owner = new SearchObjectOwner(SearchObjectLevel.Ssa);

                var existedManagedProperty = ExistedManagedProperty(myConfiguration.SearchApplicationName, myConfiguration.ManagedProperties);

                Status = existedManagedProperty.Count() == myConfiguration.ManagedProperties.Count() && IsManagedPropertyHaveAllCrawledProperties(existedManagedProperty) ? InstallerModuleStatus.Installed : InstallerModuleStatus.NotInstalled;
            }
            catch (Exception ex)
            {
                Status = InstallerModuleStatus.Error;
                LogError(ex);
                throw;
            }
        }

        public override void Install()
        {
            var content = SearchApplicationContent(myConfiguration.SearchApplicationName);
            var owner = new SearchObjectOwner(SearchObjectLevel.Ssa);

            var managedProperties = content.SearchApplication.GetManagedProperties();
            var notInstalledManagedProperty = myConfiguration.ManagedProperties.Where(s => !managedProperties.Any(my => s.Name == my.Name));
            var installedManagedProperty = myConfiguration.ManagedProperties.Where(s => managedProperties.Any(my => s.Name == my.Name));

            try
            {
                if (notInstalledManagedProperty != null)
                {
                    foreach (var mp in notInstalledManagedProperty)
                    {
                        var managedProperty = content.SearchApplication.CreateManagedProperty(mp.Name, mp.PropertyType, owner);
                        managedProperty.Retrievable = mp.Retrievable;
                        managedProperty.Queryable = mp.Queryable;
                        managedProperty.SafeForAnonymous = mp.Safe;
                        managedProperty.Searchable = mp.Searchable;
                        managedProperty.Refinable = mp.Refinable;
                        managedProperty.Sortable = mp.Sort;
                        managedProperty.SortableType = mp.Sortable;
                        managedProperty.HasMultipleValues = mp.Multivalue;
                        managedProperty.CompanyExtraction = mp.CompanyExtraction;
                        managedProperty.CompleteMatching = mp.CompleteMatching;
                        managedProperty.TokenNormalization = mp.TokenNormalization;

                        if (mp.Alias != null) content.SearchApplication.SetManagedPropertyAliases(managedProperty, mp.Alias, owner);
                        if (mp.Description != null) managedProperty.Description = mp.Description;


                        if (mp.CustomEntityExtractionConfiguration is WordExtraction)
                        {
                            managedProperty.WordExtractionCustom1 = mp.CustomEntityExtractionConfiguration.WordExtractionCustom1;
                            managedProperty.WordExtractionCustom2 = mp.CustomEntityExtractionConfiguration.WordExtractionCustom2;
                            managedProperty.WordExtractionCustom3 = mp.CustomEntityExtractionConfiguration.WordExtractionCustom3;
                            managedProperty.WordExtractionCustom4 = mp.CustomEntityExtractionConfiguration.WordExtractionCustom4;
                            managedProperty.WordExtractionCustom5 = mp.CustomEntityExtractionConfiguration.WordExtractionCustom5;
                            managedProperty.WordPartExtractionCustom1 = mp.CustomEntityExtractionConfiguration.WordPartExtractionCustom1;
                            managedProperty.WordPartExtractionCustom2 = mp.CustomEntityExtractionConfiguration.WordPartExtractionCustom2;
                            managedProperty.WordPartExtractionCustom3 = mp.CustomEntityExtractionConfiguration.WordPartExtractionCustom3;
                            managedProperty.WordPartExtractionCustom4 = mp.CustomEntityExtractionConfiguration.WordPartExtractionCustom4;
                            managedProperty.WordPartExtractionCustom5 = mp.CustomEntityExtractionConfiguration.WordPartExtractionCustom5;
                            managedProperty.WordExactExtractionCustom = mp.CustomEntityExtractionConfiguration.WordExactExtractionCustom;
                            managedProperty.WordPartExactExtractionCustom = mp.CustomEntityExtractionConfiguration.WordPartExactExtractionCustom;
                        }

                        content.SearchApplication.UpdateManagedProperty(managedProperty, owner);

                        if (mp.Properties != null)
                            SetCrawledPropertyInManagedProperty(content, owner, mp, managedProperty);
                    }
                }
                else if (installedManagedProperty != null)
                {
                    foreach (var mp in installedManagedProperty)
                    {
                        var managedProperty = content.SearchApplication.GetManagedProperty(mp.Name, owner);

                        if (mp.Properties != null)
                            SetCrawledPropertyInManagedProperty(content, owner, mp, managedProperty);
                    }
                }
            }
            catch (Exception ex)
            {
                Status = InstallerModuleStatus.Error;
                LogError(ex);
                throw;
            }
        }

        public override void Uninstall()
        {
            var content = SearchApplicationContent(myConfiguration.SearchApplicationName);
            var owner = new SearchObjectOwner(SearchObjectLevel.Ssa);

            var existedManagedProperty = ExistedManagedProperty(myConfiguration.SearchApplicationName, myConfiguration.ManagedProperties);
            try
            {
                foreach (var managedProperty in existedManagedProperty)
                {
                    content.SearchApplication.DeleteManagedProperty(managedProperty, owner);
                }
            }
            catch (Exception ex)
            {
                Status = InstallerModuleStatus.Error;
                LogError(ex);
                throw;
            }
        }

        private bool IsManagedPropertyHaveAllCrawledProperties(IEnumerable<ManagedPropertyInfo> existedManagedProperty)
        {
            bool result = true;
            var content = SearchApplicationContent(myConfiguration.SearchApplicationName);
            var owner = new SearchObjectOwner(SearchObjectLevel.Ssa);

            var mappedCrawledProperties = new List<CrawledPropertyInfo>();
            myConfiguration.ManagedProperties.ToList().ForEach(mpd =>
            {
                ManagedPropertyInfo emi = existedManagedProperty.FirstOrDefault(element => element.Name == mpd.Name);
                if (emi != null)
                {
                    mappedCrawledProperties = content.SearchApplication.GetMappedCrawledProperties(emi, owner);
                    IEnumerable<string> mcpName = mappedCrawledProperties.Select(mcp => mcp.Name);
                    result &= mpd.Properties != null ? mpd.Properties.All(el => mcpName.Contains(el)) : true;
                }
            });

            return result;
        }

        private static IEnumerable<ManagedPropertyInfo> ExistedManagedProperty(string searchApplicationName, ManagedPropertyDefinition[] managedPropertyDefinition)
        {
            var content = SearchApplicationContent(searchApplicationName);

            var allManagedProperties = content.SearchApplication.GetManagedProperties();
            return allManagedProperties.Where(s => managedPropertyDefinition.Any(my => s.Name == my.Name));
        }

        private List<MappingInfo> GetMappingsInfoList(Content content, SearchObjectOwner owner, ManagedPropertyDefinition mp, ManagedPropertyInfo managedProperty)
        {
            var mpcList = new List<MappingInfo>();

            CategoryInfoCollection categories = content.SearchApplication.GetAllCategories(owner);
            var categoriesNames = categories.Where(x => x.Name != null).Select(y => y.Name);
            var allCrawledProperties = new List<CrawledPropertyInfo>();

            foreach (var categoryName in categoriesNames)
            {
                allCrawledProperties.AddRange(content.SearchApplication.GetAllCrawledProperties(null, categoryName, 0, owner));
            }

            if (mp.Properties != null)
            {
                foreach (var property in mp.Properties)
                {
                    try
                    {
                        var categoryName = allCrawledProperties.Where(x=> x.Name == property).Select(y=> y.CategoryName).FirstOrDefault();
                        var categoryDetails = GetCategoryDetails(content, owner, categoryName);

                        var cp = content.SearchApplication.GetCrawledProperty(categoryDetails.CategoryPropset, property, 0, false, owner);

                        if (cp != null)
                        {
                            cp.CategoryName = categoryDetails.CategoryName;
                            cp.Name = property;
                            cp.Propset = categoryDetails.CategoryPropset;
                            cp.IsMappedToContents = false;
                            cp.IsNameEnum = false;
                            content.SearchApplication.UpdateCrawledProperty(cp, owner);
                        }

                        MappingInfo mapping = new MappingInfo()
                        {
                            CrawledPropertyName = cp.Name,
                            CrawledPropset = cp.Propset,
                            ManagedPid = managedProperty.Pid
                        };

                        mpcList.Add(mapping);
                    }
                    catch (Exception ex)
                    {
                        Status = InstallerModuleStatus.Error;
                        LogError(ex);
                        throw;
                    }
                }
            }
            return mpcList;
        }

        private void SetCrawledPropertyInManagedProperty(Content content, SearchObjectOwner owner, ManagedPropertyDefinition mp, ManagedPropertyInfo managedProperty)
        {
            List<MappingInfo> mpcList = GetMappingsInfoList(content, owner, mp, managedProperty);
            content.SearchApplication.SetManagedPropertyMappings(managedProperty, mpcList, owner);
        }

        private CategoryDetails GetCategoryDetails(Content content, SearchObjectOwner owner, string categoryName)
        {
            CategoryDetails categoryDetails = new CategoryDetails();

            var categoryCrawledProperty = content.SearchApplication.GetAllCrawledProperties(null, categoryName, 0, owner).First();

            categoryDetails.CategoryPropset = categoryCrawledProperty.Propset;
            categoryDetails.CategoryName = categoryCrawledProperty.CategoryName;

            return categoryDetails;
        }

        private static Content SearchApplicationContent(string searchApplicationName)
        {
            var context = SearchContext.GetContext(searchApplicationName);
            return new Content(context);
        }
    }
}
