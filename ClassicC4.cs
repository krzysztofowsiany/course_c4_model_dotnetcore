using System;
using Structurizr;
using Structurizr.Api;

namespace modelc4_project
{
    internal class ClassicC4
    {
        private ModelC4Config _config;
        private Workspace _workspace;

        public ClassicC4(ModelC4Config config)
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

            styles.Add(new ElementStyle(Tags.Container) {
                FontSize = 30,
                Background = "#FB9D4B"
            });

            styles.Add(new ElementStyle("Database") {
                Background = "#808080",
                Shape = Shape.Cylinder
            });

            styles.Add(new ElementStyle("WebPage") {
                Shape = Shape.WebBrowser
            });

            styles.Add(new ElementStyle(Tags.Component) {
                Background = "#FB9D4B",
                Shape = Shape.Hexagon
            });
            
            styles.Add(new ElementStyle("Controller") {                
                Shape = Shape.Component
            });

            styles.Add(new RelationshipStyle("Internal") {
               Dashed = false,
               Color = "#8FD14F",
               Routing = Routing.Orthogonal
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


            var courseAPIContainer = coursePlatformSystem.AddContainer("Course API", "Course platform core domain with endpoints", ".NET Core 5, WebApi");
            var customerWebPageContainer = coursePlatformSystem.AddContainer("Customer Web Page", "Sell and delivering courses for customers", "Angular 9, TypeScript");
            customerWebPageContainer.AddTags("WebPage");
            var webAdminPanelContainer = coursePlatformSystem.AddContainer("Web Admin Panel", "Pane for courses management", ".NET Core 3.1, Razor Page");
            webAdminPanelContainer.AddTags("WebPage");
            var databaseContainer = coursePlatformSystem.AddContainer("Postgres Database", "Database course_db for whole application data", "RDB, Postgres 9.3");
            databaseContainer.AddTags("Database");


            courseAPIContainer.Uses(databaseContainer, "Uses", "ORM");
            courseAPIContainer.Uses(tpayPlatformSystem, "Requesting payments", "HTTPS/REST");
            courseAPIContainer.Uses(ifirmaPlatformSystem, "Generate invoice", "HTTPS/POST REST");
            courseAPIContainer.Uses(smtpPlatformSystem, "Send invoice", "SMTP");
            tpayPlatformSystem.Uses(courseAPIContainer, "Respons", "HTTPS/REST");

            admin.Uses(webAdminPanelContainer, "Manages");
            customer.Uses(customerWebPageContainer, "Buy and pass the course");
            webAdminPanelContainer.Uses(courseAPIContainer, "Uses", "HTTPS/REST");
            customerWebPageContainer.Uses(courseAPIContainer, "Uses", "HTTPS/REST");

            var containerView = _workspace.Views.CreateContainerView(coursePlatformSystem, "Containers", null);
            containerView.PaperSize = PaperSize.A4_Portrait;
            containerView.EnableAutomaticLayout(RankDirection.TopBottom, 300, 50, 50, true);
            containerView.AddAllElements();     


            var productController = courseAPIContainer.AddComponent("Product Controller","Handle products requests.",".NET Core 5, WebApi");
            productController.AddTags("Controller");
            var offerController = courseAPIContainer.AddComponent("Offer Controller","Handle offers requests.",".NET Core 5, WebApi");          
            offerController.AddTags("Controller");
            var offerComponent = courseAPIContainer.AddComponent("Offer","Handle products logic.",".NET Core 5, ClassLib");
            productController.Uses(offerComponent, "Uses").AddTags("Internal");
            offerController.Uses(offerComponent, "Uses").AddTags("Internal");
            offerComponent.Uses(databaseContainer, "Read/write from", "[DB/ORM]");

            webAdminPanelContainer.Uses(productController, "Uses", "[HTTPS/REST]");
            webAdminPanelContainer.Uses(offerController, "Uses", "[HTTPS/REST]");

            
            
            var orderController = courseAPIContainer.AddComponent("Order Controller","Handle orders requests.",".NET Core 5, WebApi");
            orderController.AddTags("Controller");
            var orderComponent = courseAPIContainer.AddComponent("Order","Handle orders logic. Uses: Database Prostgres Container to read/write [ORM].", ".NET Core 5, ClassLib");
            var paymentComponent = courseAPIContainer.AddComponent("Payment","Handle payments logic.",".NET Core 5, ClassLib");            
            var paymentController = courseAPIContainer.AddComponent("Payment Controller","Handle payments responses.",".NET Core 5, WebApi");
            paymentController.AddTags("Controller");

            customerWebPageContainer.Uses(orderController, "Uses","[HTTPS/REST]");
            orderController.Uses(orderComponent, "Uses").AddTags("Internal");
            orderComponent.Uses(paymentComponent, "Request payments").AddTags("Internal");
            paymentController.Uses(paymentComponent, "Uses").AddTags("Internal");

            paymentComponent.Uses(tpayPlatformSystem, "Requesting payments", "[HTTPS/REST]");            
            tpayPlatformSystem.Uses(paymentController, "Responses", "[HTTPS/REST]");


            var componentView = _workspace.Views.CreateComponentView(courseAPIContainer, "Components", null);
            componentView.PaperSize = PaperSize.A2_Landscape;
            componentView.EnableAutomaticLayout(RankDirection.TopBottom, 300, 50, 50, true);
            componentView.AddAllElements();
        }

        internal void Publish()
        {
            var structrizrClient = new StructurizrClient(_config.ApiKey, _config.ApiSecret);

            structrizrClient.PutWorkspace(_config.WorkspaceId, _workspace);
        }
    }
}