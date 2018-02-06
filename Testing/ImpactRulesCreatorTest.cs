using System;
using ImpactRulesCreator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class ImpactRulesCreatorTest
    {
        //[TestMethod]
        public void InstallTest()
        {
            var module = new ImpactRulesCreator.ImpactRulesCreator()
            {
                Configuration = new Configuration()
                {
                    Site = "http://findwise.com",
                    Behavior = Microsoft.Office.Server.Search.Administration.SiteHitRuleBehavior.DelayBetweenRequests
                }
            };
            module.CheckStatus();
            if (module.Status != Findwise.Sharepoint.SolutionInstaller.InstallerModuleStatus.NotInstalled) Assert.Fail("Module already installed");
            module.Install();

        }
    }
}
