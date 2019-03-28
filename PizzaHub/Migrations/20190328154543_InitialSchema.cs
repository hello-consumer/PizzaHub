using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaHub.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(9, 6)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(9, 6)", nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CustomerOrder",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ContactPhoneNumber = table.Column<string>(maxLength: 100, nullable: true),
                    DeliveryStreet = table.Column<string>(maxLength: 100, nullable: true),
                    DeliveryCity = table.Column<string>(maxLength: 100, nullable: true),
                    DeliveryState = table.Column<string>(maxLength: 100, nullable: true),
                    DeliveryZipCode = table.Column<string>(maxLength: 20, nullable: true),
                    SpecialInstructions = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrder", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Styles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Styles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Toppings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toppings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Restaurant",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 100, nullable: true),
                    CityID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurant", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Restaurant_City",
                        column: x => x.CityID,
                        principalTable: "City",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerOrderPizzas",
                columns: table => new
                {
                    CustomerOrderID = table.Column<int>(nullable: false),
                    LineItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RestaurantID = table.Column<int>(nullable: false),
                    StyleID = table.Column<int>(nullable: true),
                    SizeID = table.Column<int>(nullable: true),
                    OrderPrice = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrderPizzas", x => new { x.CustomerOrderID, x.LineItemID });
                    table.ForeignKey(
                        name: "FK_CustomerOrderPizzas_CustomerORder",
                        column: x => x.CustomerOrderID,
                        principalTable: "CustomerOrder",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerOrderPizzas_RestaurantID",
                        column: x => x.RestaurantID,
                        principalTable: "Restaurant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerOrderPizzas_SizeID",
                        column: x => x.SizeID,
                        principalTable: "Sizes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CustomerOrderPizzas_StyleID",
                        column: x => x.StyleID,
                        principalTable: "Styles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Pizza",
                columns: table => new
                {
                    RestaurantID = table.Column<int>(nullable: false),
                    SizeID = table.Column<int>(nullable: false),
                    StyleID = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pizza", x => new { x.RestaurantID, x.SizeID, x.StyleID });
                    table.ForeignKey(
                        name: "FK_Pizza_Restaurant",
                        column: x => x.RestaurantID,
                        principalTable: "Restaurant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pizza_Size",
                        column: x => x.SizeID,
                        principalTable: "Sizes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pizza_Style",
                        column: x => x.StyleID,
                        principalTable: "Styles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantToppings",
                columns: table => new
                {
                    RestaurantID = table.Column<int>(nullable: false),
                    ToppingID = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaToppings", x => new { x.RestaurantID, x.ToppingID });
                    table.ForeignKey(
                        name: "FK_RestaurantToppings_Restaurant",
                        column: x => x.RestaurantID,
                        principalTable: "Restaurant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestaurantToppings_Topping",
                        column: x => x.ToppingID,
                        principalTable: "Toppings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerOrderPizzaToppings",
                columns: table => new
                {
                    LineItemID = table.Column<int>(nullable: false),
                    ToppingID = table.Column<int>(nullable: false),
                    CustomerOrderID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrderPizzaToppings", x => new { x.LineItemID, x.ToppingID });
                    table.ForeignKey(
                        name: "FK_CustomerOrderPizzaToppings_Toppings",
                        column: x => x.ToppingID,
                        principalTable: "Toppings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerOrderPizzaToppings_CustomerOrderPizzas",
                        columns: x => new { x.CustomerOrderID, x.LineItemID },
                        principalTable: "CustomerOrderPizzas",
                        principalColumns: new[] { "CustomerOrderID", "LineItemID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderPizzas_RestaurantID",
                table: "CustomerOrderPizzas",
                column: "RestaurantID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderPizzas_SizeID",
                table: "CustomerOrderPizzas",
                column: "SizeID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderPizzas_StyleID",
                table: "CustomerOrderPizzas",
                column: "StyleID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderPizzaToppings_ToppingID",
                table: "CustomerOrderPizzaToppings",
                column: "ToppingID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderPizzaToppings_CustomerOrderID_LineItemID",
                table: "CustomerOrderPizzaToppings",
                columns: new[] { "CustomerOrderID", "LineItemID" });

            migrationBuilder.CreateIndex(
                name: "IX_Pizza_SizeID",
                table: "Pizza",
                column: "SizeID");

            migrationBuilder.CreateIndex(
                name: "IX_Pizza_StyleID",
                table: "Pizza",
                column: "StyleID");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_CityID",
                table: "Restaurant",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantToppings_ToppingID",
                table: "RestaurantToppings",
                column: "ToppingID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerOrderPizzaToppings");

            migrationBuilder.DropTable(
                name: "Pizza");

            migrationBuilder.DropTable(
                name: "RestaurantToppings");

            migrationBuilder.DropTable(
                name: "CustomerOrderPizzas");

            migrationBuilder.DropTable(
                name: "Toppings");

            migrationBuilder.DropTable(
                name: "CustomerOrder");

            migrationBuilder.DropTable(
                name: "Restaurant");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "Styles");

            migrationBuilder.DropTable(
                name: "City");
        }
    }
}
