using Findwise.Sharepoint.SolutionInstaller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.Configuration;
using Microsoft.Office.Server.Search.Administration;
using Microsoft.Office.Server.Search.Administration.Query;
using ResultSourceCreator.Properties;
using Findwise.InstallerModule;
using System.Security;
using System.ComponentModel;
using Microsoft.SharePoint;

namespace ResultSourceCreator
{
    [Category(ModuleCategoryNames.SharepointSearch)]
    public class ResultSourceCreator : InstallerModuleBase
    {
        public override string Name => "Result Source Creator";

        public override System.Drawing.Image Icon => Resources.if_Configuration_2202267;

        private Configuration myConfiguration = new Configuration();
        public override ConfigurationBase Configuration { get => myConfiguration; set => myConfiguration = value as Configuration; }

        private FederationManager fm;
        private SearchObjectOwner owner;

        public override void CheckStatus()
        {
            Status = InstallerModuleStatus.Refreshing;
            try
            {
                var resultSource = GetResultSource();
                if (myConfiguration.TypeConfiguration == null)
                {
                    throw new ArgumentNullException("The type cannot be null.");
                }

                Status = resultSource != null ? InstallerModuleStatus.Installed : InstallerModuleStatus.NotInstalled;
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
            Status = InstallerModuleStatus.Installing;
            try
            {
                var resultSource = GetResultSource();

                if (resultSource == null)
                {
                    resultSource = fm.CreateSource(owner);
                    resultSource.Name = myConfiguration.ResultSourceName;
                    if (myConfiguration.Description != null) resultSource.Description = myConfiguration.Description;
                    resultSource.ProviderId = fm.ListProviders()[myConfiguration.TypeConfiguration.Provider].Id;
                }
                resultSource.CreateQueryTransform(myConfiguration.Query);
                resultSource.Commit();
                if (myConfiguration.SetAsDefault) fm.UpdateDefaultSource(resultSource.Id, owner);
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
            Status = InstallerModuleStatus.Uninstalling;
            try
            {
                var resultSource = GetResultSource();

                if (resultSource != null)
                    fm.RemoveSource(resultSource);
            }
            catch (Exception ex)
            {
                Status = InstallerModuleStatus.Error;
                LogError(ex);
                throw;
            }
        }
        private Content SearchApplicationContent(string searchApplicationName)
        {
            SearchContext context = SearchContext.GetContext(searchApplicationName);
            return new Content(context);

        }
        private Source GetResultSource()
        {
            var content = SearchApplicationContent(myConfiguration.SearchApplicationName);
            SearchServiceApplication ssa = content.SearchApplication;
            fm = new FederationManager(ssa);
            owner = new SearchObjectOwner(SearchObjectLevel.Ssa);

            return fm.GetSourceByName(myConfiguration.ResultSourceName, owner);
        }
    }
}
