using System;
using System.Drawing;
using System.Linq;
using Findwise.Configuration;
using Findwise.InstallerModule;
using Microsoft.Office.Server.Search.Administration;
using Findwise.Sharepoint.SolutionInstaller;
using System.Collections.Generic;

namespace ManagedPropertiesCreator
{
    class ManagedPropertiesCreator : InstallerModuleBase
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
                SearchServiceApplication ssa = content.SearchApplication;
                var owner = new SearchObjectOwner(SearchObjectLevel.Ssa);

                var managedProperties = content.SearchApplication.GetManagedProperties();
                var managedProperty = managedProperties.Where(s => myConfiguration.ManagedPropertyDefinitions.Any(my => s.Name == my.Name));

                Status = managedProperty.Count() == myConfiguration.ManagedPropertyDefinitions.Count() ? InstallerModuleStatus.Installed : InstallerModuleStatus.NotInstalled;
            }
            catch
            {
                Status = InstallerModuleStatus.Error;
                throw;
            }
        }

        public override void Install()
        {
            var content = SearchApplicationContent(myConfiguration.SearchApplicationName);
            SearchServiceApplication ssa = content.SearchApplication;
            var owner = new SearchObjectOwner(SearchObjectLevel.Ssa);

            var managedProperties = content.SearchApplication.GetManagedProperties();
            var notInstallManagedProperty = myConfiguration.ManagedPropertyDefinitions.Where(s => !managedProperties.Any(my => s.Name == my.Name));

            var managedProperty = new ManagedPropertyInfo();

            if (notInstallManagedProperty != null)
            {
                foreach (var item in notInstallManagedProperty)
                {
                    managedProperty = content.SearchApplication.CreateManagedProperty(item.Name, ManagedDataType.Text, owner);
                    managedProperty.Retrievable = item.Retrieve;
                    managedProperty.Queryable = item.Query;
                    managedProperty.SafeForAnonymous = item.Safe;

                    managedProperty.Searchable = item.Search;
                    managedProperty.Refinable = item.Refine;
                    managedProperty.Sortable = item.Sort;
                    managedProperty.HasMultipleValues = item.Multivalue;

                    content.SearchApplication.UpdateManagedProperty(managedProperty, owner);

                    var categories = content.SearchApplication.GetAllCategories(owner);
                    var businessDataCategory = categories.First(c => c.Name == "Business Data");
                    var businessCrawledProperty = content.SearchApplication.GetAllCrawledProperties(null, businessDataCategory.Name, 1, owner).First();
                    var businessCategoryPropset = businessCrawledProperty.Propset;
                    var businessCategoryName = businessCrawledProperty.CategoryName;

                    var cps = new string[] { };
                    var crawledProperties = new List<CrawledPropertyInfo>();

                    foreach (var property in item.Properties)
                    {
                        cps.ToList().Add(property);
                        crawledProperties.Add(content.SearchApplication.GetCrawledProperty(businessCategoryPropset, property, 0, false, owner));
                    }
                }
            }
        }

        public override void Uninstall()
        {
            throw new NotImplementedException();
        }

        private static Content SearchApplicationContent(string searchApplicationName)
        {
            var context = SearchContext.GetContext(searchApplicationName);
            return new Content(context);
        }
    }
}
