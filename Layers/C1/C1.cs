using Structurizr;

namespace modelc4_project.Layers.C1 {
    internal class C1 {
        public C1(Workspace workspace) {
            var coursePlatformSystem = new C1Builder(workspace)
                .AddPerson("Admin", "Configuring platform, generating coupons")
                .AddPerson("Customer", "Using course platform, buy on platform")
                .AddSoftwareSystem("Course Platform", "Sell and delivery courses to customers")
                .AddSoftwareSystem("TPay", "Handle payments", "External")
                .AddSoftwareSystem("iFirma", "Generate invoices", "External")
                .AddSoftwareSystem("SMTP", "Send emails", "External")
                .PersonUses("Customer", "Course Platform", "Buy and pass the courses")
                .PersonUses("Admin", "Course Platform", "Manages")
                .SoftwareSystemUses("Course Platform", "TPay", "Request payments")
                .SoftwareSystemUses("Course Platform", "iFirma", "Generate invoices")
                .SoftwareSystemUses("Course Platform", "SMTP", "Send emails")
                .SoftwareSystemDelivers("SMTP", "Customer", "Send emails")
                .SoftwareSystemUses("TPay", "Course Platform", "Responses")
                .AddStyle(new ElementStyle(Tags.SoftwareSystem) {
                    FontSize = 30,
                        Background = "#FB9D4B"
                })
                .AddStyle(new ElementStyle("External") {
                    FontSize = 30,
                        Background = "#FFCEE0"
                })
                .AddStyle(new ElementStyle(Tags.Person) {
                    FontSize = 30,
                        Background = "#FFF9B1",
                        Shape = Shape.Person
                }).GetSoftwareSystem("Course Platform");

            var contextView = workspace.Views.CreateSystemContextView(
                coursePlatformSystem,
                "Course Platform Context View",
                "System Context diagram for Course Platform.");

            contextView.PaperSize = PaperSize.A4_Portrait;
            contextView.EnableAutomaticLayout(RankDirection.RightLeft, 300, 50, 50, true);            
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();
        }
    }
}