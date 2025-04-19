CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;
CREATE TABLE "Customers" (
    "CustomerId" INTEGER NOT NULL CONSTRAINT "PK_Customers" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Phone" TEXT NOT NULL,
    "Address" TEXT NOT NULL
);

CREATE TABLE "Products" (
    "ProductId" INTEGER NOT NULL CONSTRAINT "PK_Products" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Category" TEXT NOT NULL,
    "Volume" TEXT NOT NULL,
    "Company" TEXT NOT NULL
);

CREATE TABLE "Orders" (
    "OrderId" INTEGER NOT NULL CONSTRAINT "PK_Orders" PRIMARY KEY AUTOINCREMENT,
    "CustomerId" INTEGER NOT NULL,
    "OrderDateAd" TEXT NOT NULL,
    "OrderDateBs" TEXT NOT NULL,
    "TotalAmount" decimal(18,2) NOT NULL,
    "PendingAmount" decimal(18,2) NOT NULL,
    CONSTRAINT "FK_Orders_Customers_CustomerId" FOREIGN KEY ("CustomerId") REFERENCES "Customers" ("CustomerId") ON DELETE RESTRICT
);

CREATE TABLE "InventoryStocks" (
    "StockId" INTEGER NOT NULL CONSTRAINT "PK_InventoryStocks" PRIMARY KEY AUTOINCREMENT,
    "ProductId" INTEGER NOT NULL,
    "PurchaseDate" TEXT NOT NULL,
    "Quantity" INTEGER NOT NULL,
    "UnitCost" decimal(18,2) NOT NULL,
    CONSTRAINT "FK_InventoryStocks_Products_ProductId" FOREIGN KEY ("ProductId") REFERENCES "Products" ("ProductId") ON DELETE RESTRICT
);

CREATE TABLE "OrderItems" (
    "ItemId" INTEGER NOT NULL CONSTRAINT "PK_OrderItems" PRIMARY KEY AUTOINCREMENT,
    "OrderId" INTEGER NOT NULL,
    "ProductId" INTEGER NOT NULL,
    "Quantity" INTEGER NOT NULL,
    "PaintShade" TEXT NOT NULL,
    "ExciseDuty" decimal(18,2) NOT NULL,
    "Vat" decimal(18,2) NOT NULL,
    CONSTRAINT "FK_OrderItems_Orders_OrderId" FOREIGN KEY ("OrderId") REFERENCES "Orders" ("OrderId") ON DELETE CASCADE,
    CONSTRAINT "FK_OrderItems_Products_ProductId" FOREIGN KEY ("ProductId") REFERENCES "Products" ("ProductId") ON DELETE RESTRICT
);

CREATE TABLE "Payments" (
    "PaymentId" INTEGER NOT NULL CONSTRAINT "PK_Payments" PRIMARY KEY AUTOINCREMENT,
    "OrderId" INTEGER NOT NULL,
    "AmountPaid" decimal(18,2) NOT NULL,
    "PaymentDate" TEXT NOT NULL,
    CONSTRAINT "FK_Payments_Orders_OrderId" FOREIGN KEY ("OrderId") REFERENCES "Orders" ("OrderId") ON DELETE CASCADE
);

CREATE UNIQUE INDEX "IX_Customer_Phone_Unique" ON "Customers" ("Phone");

CREATE INDEX "IX_Inventory_PurchaseDate" ON "InventoryStocks" ("PurchaseDate");

CREATE INDEX "IX_InventoryStocks_ProductId" ON "InventoryStocks" ("ProductId");

CREATE INDEX "IX_OrderItems_OrderId" ON "OrderItems" ("OrderId");

CREATE INDEX "IX_OrderItems_ProductId" ON "OrderItems" ("ProductId");

CREATE INDEX "IX_Order_Date" ON "Orders" ("OrderDateAd");

CREATE INDEX "IX_Orders_CustomerId" ON "Orders" ("CustomerId");

CREATE INDEX "IX_Payment_Date" ON "Payments" ("PaymentDate");

CREATE INDEX "IX_Payments_OrderId" ON "Payments" ("OrderId");

CREATE INDEX "IX_Product_Category_Volume" ON "Products" ("Category", "Volume");

CREATE INDEX "IX_Product_Company" ON "Products" ("Company");

CREATE INDEX "IX_Product_Name" ON "Products" ("Name");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250415041434_Initial', '9.0.4');

COMMIT;

