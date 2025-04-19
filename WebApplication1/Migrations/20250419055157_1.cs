using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    Volume = table.Column<int>(type: "INTEGER", nullable: false),
                    Company = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ShadeSlug = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    WholesalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RetailPrice = table.Column<decimal>(type: "TEXT", nullable: false, computedColumnSql: "ROUND(WholesalePrice + (WholesalePrice * 0.07) + ((WholesalePrice + (WholesalePrice * 0.07)) * 0.13), 2)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateAd = table.Column<DateTime>(type: "date", nullable: false),
                    DateBs = table.Column<string>(type: "Text", maxLength: 20, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PendingAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryStocks",
                columns: table => new
                {
                    BatchId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    PurchaseDateAd = table.Column<DateTime>(type: "date", nullable: false),
                    PurchaseDateBs = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    CostPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryStocks", x => x.BatchId);
                    table.ForeignKey(
                        name: "FK_InventoryStocks_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExciseDuty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDateAd = table.Column<DateTime>(type: "date", nullable: false),
                    PaymentDateBs = table.Column<string>(type: "TEXT", nullable: false),
                    IsAdvance = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "Kathmandu", "John Doe", "9841000001" },
                    { 2, "Lalitpur", "Anita Sharma", "9841000002" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Category", "Company", "Name", "ShadeSlug", "Volume", "WholesalePrice" },
                values: new object[,]
                {
                    { 1, 1, "Asian Paints", "Premium Paint", "royal-blue", 4, 800.00m },
                    { 2, 2, "Berger", "Wall Putty", "white", 1005, 500.00m },
                    { 3, 3, "Nerolac", "Smart Care Polish", "clear", 1001, 300.00m }
                });

            migrationBuilder.InsertData(
                table: "InventoryStocks",
                columns: new[] { "BatchId", "CostPrice", "ProductId", "PurchaseDateAd", "PurchaseDateBs", "Quantity" },
                values: new object[,]
                {
                    { 1, 0m, 1, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2081-01-18", 100 },
                    { 2, 480.00m, 2, new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "2081-01-22", 50 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CustomerId", "DateAd", "DateBs", "PendingAmount", "TotalAmount" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "2081-01-27", 0m, 1000.00m },
                    { 2, 2, new DateTime(2025, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "2081-01-29", 0.00m, 500.00m }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "ItemId", "ExciseDuty", "OrderId", "ProductId", "Quantity", "UnitPrice", "VAT" },
                values: new object[,]
                {
                    { 1, 56.00m, 1, 1, 1, 800.00m, 117.04m },
                    { 2, 35.00m, 1, 2, 1, 500.00m, 73.15m },
                    { 3, 35.00m, 2, 2, 1, 500.00m, 73.15m }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "Amount", "IsAdvance", "OrderId", "PaymentDateAd", "PaymentDateBs" },
                values: new object[,]
                {
                    { 1, 1000.00m, false, 1, new DateTime(2025, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "2081-01-28" },
                    { 2, 500.00m, true, 2, new DateTime(2025, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "2081-02-01" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Name",
                table: "Customers",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Phone",
                table: "Customers",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryStocks_ProductId",
                table: "InventoryStocks",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryStocks_PurchaseDateAd",
                table: "InventoryStocks",
                column: "PurchaseDateAd");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryStocks_PurchaseDateBs",
                table: "InventoryStocks",
                column: "PurchaseDateBs");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DateAd",
                table: "Orders",
                column: "DateAd");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DateBs",
                table: "Orders",
                column: "DateBs");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentDateAd",
                table: "Payments",
                column: "PaymentDateAd");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentDateBs",
                table: "Payments",
                column: "PaymentDateBs");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Category",
                table: "Products",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Company",
                table: "Products",
                column: "Company");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ShadeSlug",
                table: "Products",
                column: "ShadeSlug");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryStocks");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
