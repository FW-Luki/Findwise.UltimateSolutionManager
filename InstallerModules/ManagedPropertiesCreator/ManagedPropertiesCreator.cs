﻿using System;
using System.Drawing;
using System.Linq;
using Findwise.Configuration;
using Findwise.InstallerModule;
using Microsoft.Office.Server.Search.Administration;
using Findwise.Sharepoint.SolutionInstaller;
using System.Collections.Generic;
using static ManagedPropertiesCreator.Configuration;

namespace ManagedPropertiesCreator
{
    public class ManagedPropertiesCreator : InstallerModuleBase
    {
        public override string Name => "Managed Properties Creator";

        public override System.Drawing.Image Icon => null;

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
                        var managedProperty = content.SearchApplication.CreateManagedProperty(mp.Name, (ManagedDataType)mp.PropertyType, owner);
                        managedProperty.Retrievable = mp.Retrieve;
                        managedProperty.Queryable = mp.Query;
                        managedProperty.SafeForAnonymous = mp.Safe;
                        managedProperty.Searchable = mp.Search;
                        managedProperty.Refinable = mp.Refine;
                        managedProperty.Sortable = mp.Sort;
                        managedProperty.HasMultipleValues = mp.Multivalue;

                        content.SearchApplication.UpdateManagedProperty(managedProperty, owner);

                        SetCrawledPropertyInManagedProperty(content, owner, mp, managedProperty);
                    }
                }
                else if (installedManagedProperty != null)
                {
                    foreach (var mp in installedManagedProperty)
                    {
                        var managedProperty = content.SearchApplication.GetManagedProperty(mp.Name, owner);

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

        private CategoryDetails GetCategoryDetails(Content content, SearchObjectOwner owner, ManagedPropertyDefinition mp)
        {
            CategoryDetails categoryDetails = new CategoryDetails();

            CategoryInfoCollection categories = content.SearchApplication.GetAllCategories(owner);
            var categoryNameCrawledProperties = myConfiguration.ManagedProperties.Where(my => my.Name == mp.Name).Select(x => x.CrawledPropertiesCategory).First();
            var categoryInfo = categories.First(c => c.Name == categoryNameCrawledProperties);
            var categoryCrawledProperty = content.SearchApplication.GetAllCrawledProperties(null, categoryInfo.Name, 1, owner).First();

            categoryDetails.CategoryPropset = categoryCrawledProperty.Propset;
            categoryDetails.CategoryName = categoryCrawledProperty.CategoryName;

            return categoryDetails;
        }

        private List<MappingInfo> GetMappingsInfoList(Content content, SearchObjectOwner owner, CategoryDetails categoryDetails, ManagedPropertyDefinition mp, ManagedPropertyInfo managedProperty)
        {
            var mpcList = new List<MappingInfo>();

            if (mp.Properties != null)
            {
                foreach (var property in mp.Properties)
                {
                    try
                    {
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
            CategoryDetails categoryDetails = GetCategoryDetails(content, owner, mp);
            List<MappingInfo> mpcList = GetMappingsInfoList(content, owner, categoryDetails, mp, managedProperty);
            content.SearchApplication.SetManagedPropertyMappings(managedProperty, mpcList, owner);
        }

        private static Content SearchApplicationContent(string searchApplicationName)
        {
            var context = SearchContext.GetContext(searchApplicationName);
            return new Content(context);
        }
    }
}
    