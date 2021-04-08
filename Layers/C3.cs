using Structurizr;

namespace modelc4_project.Layers {
    internal class C3 {
        public C3(Workspace workspace) {
            var courseAPIContainer = new C4Builder(workspace)
                .SelectSoftwareSystem("Course Platform")
                .SelectContainer("Course API")
                .AddComponent("Product Controller", "Handle products requests.", ".NET Core 5, WebApi", "Controller")
                .AddComponent("Offer Controller", "Handle offers requests.", ".NET Core 5, WebApi", "Controller")
                .AddComponent("Payment Controller", "Handle payments responses.", ".NET Core 5, WebApi", "Controller")
                .AddComponent("Order Controller", "Handle orders requests.", ".NET Core 5, WebApi", "Controller")
                .AddComponent("Invoice Controller", "Handle generate invoice requests.", ".NET Core 5, WebApi", "Controller")
                .AddComponent("Discount Controller", "Handle discount requests.", ".NET Core 5, WebApi", "Controller")
                .AddComponent("Order", "Handle orders logic. Uses: Database Prostgres Container to read/write [ORM].", ".NET Core 5, ClassLib")
                .AddComponent("Payment", "Handle payments logic.", ".NET Core 5, ClassLib")
                .AddComponent("Offer", "Handle offers logic.", ".NET Core 5, ClassLib")
                .AddComponent("Email", "Handle sending emails logic.", ".NET Core 5, ClassLib")
                .AddComponent("Invoice", "Handle invoice logic.", ".NET Core 5, ClassLib")
                .AddComponent("Discount", "Handle discount logic.", ".NET Core 5, ClassLib")
                .AddComponent("Delivery", "Handle delivery logic.", ".NET Core 5, ClassLib")
                .AddComponent("Sell", "Handle selling logic.", ".NET Core 5, ClassLib")
                .ContainerUsesComponent("Web Admin Panel", "Product Controller", "Uses", "[HTTPS/REST]")
                .ContainerUsesComponent("Web Admin Panel", "Offer Controller", "Uses", "[HTTPS/REST]")
                .ComponentUsesComponent("Offer Controller", "Offer", "Uses", "Internal")
                .ComponentUsesComponent("Product Controller", "Offer", "Uses", "Internal")
                .ComponentUsesContainer("Offer", "Postgres Database", "Read/write from", "[DB/ORM]")
                .ContainerUsesComponent("Web Admin Panel", "Discount Controller", "Uses", "[HTTPS/REST]")
                .ContainerUsesComponent("Customer Web Page", "Discount Controller", "Uses", "[HTTPS/REST]")
                .ComponentUsesComponent("Discount Controller", "Discount", "Uses", "Internal")
                .ComponentUsesContainer("Discount", "Postgres Database", "Read/write from", "[DB/ORM]")
                .ContainerUsesComponent("Customer Web Page", "Order Controller", "Uses", "[HTTPS/REST]")
                .ComponentUsesComponent("Order Controller", "Order", "Uses", "Internal")
                .ComponentUsesContainer("Order", "Postgres Database", "Read/write from", "[DB/ORM]")
                .ComponentUsesComponent("Order", "Order", "Payment", "Internal")
                .ComponentUsesSoftwareSystem("Payment", "TPay", "Requesting payments", "[HTTPS/REST]")
                .SoftwareSystemUsesComponent("TPay", "Payment Controller", "Responses", "[HTTPS/REST]")
                .ComponentUsesComponent("Payment Controller", "Payment", "Uses", "Internal")
                .ComponentUsesComponent("Payment", "Sell", "Payment Success", "Internal")
                .ComponentUsesComponent("Sell", "Delivery", "Uses", "Internal")
                .ComponentUsesComponent("Sell", "Invoice", "Generate invoice", "Internal")
                .ComponentUsesSoftwareSystem("Invoice", "iFirma", "Generate invoice", "[HTTPS/POST REST]")
                .SoftwareSystemUsesComponent("iFirma", "Invoice Controller", "Responses", "[HTTPS/POST REST]")
                .ComponentUsesComponent("Invoice Controller", "Invoice", "Uses", "Internal")
                .ComponentUsesComponent("Invoice", "Email", "Send emails", "Internal")
                .ComponentUsesSoftwareSystem("Email", "SMTP", "Sending emails", "[SMTP]")
                .ComponentUsesContainer("Payment", "Postgres Database", "Read/write from", "[DB/ORM]")
                .ComponentUsesContainer("Sell", "Postgres Database", "Read/write from", "[DB/ORM]")
                .ComponentUsesContainer("Delivery", "Postgres Database", "Read/write from", "[DB/ORM]")
                .AddStyle(new ElementStyle(Tags.Component) {
                    Background = "#FB9D4B",
                        Shape = Shape.Hexagon
                })
                .AddStyle(new ElementStyle("Controller") {
                    Shape = Shape.Component
                })
                .AddStyle(new RelationshipStyle(Tags.Relationship) {
                    Routing = Routing.Curved
                })
                .AddStyle(new RelationshipStyle("Internal") {
                    Dashed = false,
                        Color = "#8FD14F",
                        Routing = Routing.Curved
                })
                .GetContainer("Course API");

            var componentView = workspace.Views.CreateComponentView(courseAPIContainer, "Components", null);
            componentView.PaperSize = PaperSize.A2_Landscape;
            componentView.EnableAutomaticLayout(RankDirection.LeftRight, 300, 100, 100, true);
            componentView.AddAllElements();
        }
    }
}