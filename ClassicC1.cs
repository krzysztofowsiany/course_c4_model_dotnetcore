using System;
using Structurizr;
using Structurizr.Api;

namespace modelc4_project
{
    internal class ClassicC1
    {
        private ModelC4Config _config;
        private Workspace _workspace;

        public ClassicC1(ModelC4Config config)
        {
            _config = config;

            Prepare();
            Stylize();
        }

        private void Stylize()
        {
            var styles = _workspace.Views.Configuration.Styles;

            styles.Add(new ElementStyle(Tags.SoftwareSystem) {
                FontSize = 30,
                Background = "#FB9D4B"
            });

             styles.Add(new ElementStyle("External") {
                FontSize = 30,
                Background = "#FFCEE0"
            });

            styles.Add(new ElementStyle(Tags.Person) {
                FontSize = 30,
                Background = "#FFF9B1",
                Shape = Shape.Person
            });
        }

        private void Prepare()
        {
            _workspace = new Workspace("Course Platform", "Sell and delivery courses");
            var customer = _workspace.Model.AddPerson("Customer", "Using course platform, buy on platform");
            var admin = _workspace.Model.AddPerson("Admin", "Configuring platform, generating coupons");

            var coursePlatformSystem = _workspace.Model.AddSoftwareSystem("Course Platform", "Sell and delivery courses to customers");
            var tpayPlatformSystem = _workspace.Model.AddSoftwareSystem("TPay", "Handle payments");
            tpayPlatformSystem.AddTags("External");

            var ifirmaPlatformSystem = _workspace.Model.AddSoftwareSystem("iFirma", "Generate invoices");
            ifirmaPlatformSystem.AddTags("External");

            var smtpPlatformSystem = _workspace.Model.AddSoftwareSystem("SMTP", "Send emails");
            smtpPlatformSystem.AddTags("External");

            admin.Uses(coursePlatformSystem, "Manages");
            customer.Uses(coursePlatformSystem, "Buy and pass the courses");

            coursePlatformSystem.Uses(tpayPlatformSystem, "Request payments");
            coursePlatformSystem.Uses(ifirmaPlatformSystem, "Generate invoice");
            coursePlatformSystem.Uses(smtpPlatformSystem, "Send invoices");

            tpayPlatformSystem.Uses(coursePlatformSystem, "Responses");
            smtpPlatformSystem.Delivers(customer, "Send emails");

            var contextView = _workspace.Views.CreateSystemContextView(coursePlatformSystem, "Course Platform Context View", "System Context diagram for Course Platform.");
            contextView.PaperSize = PaperSize.A4_Portrait;
            contextView.EnableAutomaticLayout(RankDirection.RightLeft, 300, 50, 50, true);
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();            

        }

        internal void Publish()
        {
            var structrizrClient = new StructurizrClient(_config.ApiKey, _config.ApiSecret);

            structrizrClient.PutWorkspace(_config.WorkspaceId, _workspace);
        }
    }
}