using Structurizr;

namespace modelc4_project.Layers {
    internal class C2 {
        public C2(Workspace workspace) {
            var coursePlatformSystem = new C4Builder(workspace)
                .SelectSoftwareSystem("Course Platform")
                .AddContainer("Course API", "Course platform core domain with endpoints", ".NET Core 5, WebApi")
                .AddContainer("Customer Web Page", "Sell and delivering courses for customers", "Angular 9, TypeScript", "WebPage")
                .AddContainer("Web Admin Panel", "Pane for courses management", ".NET Core 3.1, Razor Page", "WebPage")
                .AddContainer("Postgres Database", "Database course_db for whole application data", "RDB, Postgres 9.3", "Database")
                .ContainerUses("Course API", "Postgres Database", "Uses", "ORM")
                .ContainerUsesSofwareSystem("Course API", "TPay", "Requesting payments", "HTTPS/REST")
                .ContainerUsesSofwareSystem("Course API", "iFirma", "Generate invoice", "HTTPS/POST REST")
                .ContainerUsesSofwareSystem("Course API", "SMTP", "Send invoice", "SMTP")
                .SoftwareSystemUsesContainer("TPay", "Course API", "Respons", "HTTPS/REST")
                .PersonUsesContainer("Customer", "Customer Web Page", "Buy and pass the courses")
                .PersonUsesContainer("Admin", "Web Admin Panel", "Manages")
                .ContainerUses("Customer Web Page", "Course API", "Uses", "HTTPS/REST")
                .ContainerUses("Web Admin Panel", "Course API", "Uses", "HTTPS/REST")
                .AddStyle(new ElementStyle(Tags.Container) {
                    FontSize = 30,
                        Background = "#FB9D4B"
                })
                .AddStyle(new ElementStyle("Database") {
                    Background = "#808080",
                        Shape = Shape.Cylinder
                })
                .AddStyle(new ElementStyle("WebPage") {
                    Shape = Shape.WebBrowser
                }).GetSoftwareSystem("Course Platform");

            var containerView = workspace.Views.CreateContainerView(coursePlatformSystem, "Containers", null);
            containerView.PaperSize = PaperSize.A4_Landscape;
            containerView.EnableAutomaticLayout(RankDirection.LeftRight, 200, 200, 200, true);
            containerView.AddAllElements();
        }
    }
}