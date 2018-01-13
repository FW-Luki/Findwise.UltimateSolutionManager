using Findwise.Sharepoint.SolutionInstaller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Findwise.Configuration;
using Microsoft.Office.Server.Search.Administration;
using Microsoft.Office.Server.Search.Administration.Query;
using Findwise.InstallerModule;

namespace ResultSourceCreator
{
    public class ResultSourceCreator : InstallerModuleBase
    {
        public override string Name => "Result Source Creator";

        public override System.Drawing.Image Icon => null;

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

                Status = resultSource != null ? InstallerModuleStatus.Installed : InstallerModuleStatus.NotInstalled;
            }
            catch
            {
                Status = InstallerModuleStatus.Error;
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
                    resultSource.ProviderId = fm.ListProviders()["Local SharePoint Provider"].Id;
                }

                resultSource.CreateQueryTransform(myConfiguration.Query);
                resultSource.Commit();
            }
            catch
            {
                Status = InstallerModuleStatus.Error;
            }
        }

        public override void Uninstall()
        {
            throw new NotImplementedException();
        }
        private Content SearchApplicationContent(string searchApplicationName)
        {
            string ssaName = searchApplicationName;
            SearchContext context = SearchContext.GetContext(ssaName);
            Content content = new Content(context);

            return content;
        }
        private Source GetResultSource()
        {
            var content = SearchApplicationContent(myConfiguration.SearchApplicationName);
            SearchServiceApplication ssa = content.SearchApplication;
            fm = new FederationManager(ssa);
            owner = new SearchObjectOwner(SearchObjectLevel.Ssa);

            var resultSource = fm.GetSourceByName(myConfiguration.ResultSourceName, owner);

            return resultSource;
        }
    }
}
