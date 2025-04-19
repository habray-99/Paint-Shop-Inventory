-- Tables
CREATE TABLE Products (
    ProductId INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Category INTEGER NOT NULL CHECK (Category IN (1, 2, 3)),
    Volume INTEGER NOT NULL,
    Company TEXT NOT NULL,
    ShadeSlug TEXT NOT NULL,
    WholesalePrice DECIMAL(18,2) NOT NULL CHECK (WholesalePrice > 0),
    RetailPrice DECIMAL(18,2) GENERATED ALWAYS AS (
        ROUND(WholesalePrice +
             (WholesalePrice * 0.07) +
             ((WholesalePrice + (WholesalePrice * 0.07)) * 0.13, 2)
    ) STORED
);

CREATE TABLE InventoryStock (
    BatchId INTEGER PRIMARY KEY AUTOINCREMENT,
    ProductId INTEGER NOT NULL,
    Quantity INTEGER NOT NULL CHECK (Quantity > 0),
    PurchaseDateAD TEXT NOT NULL,
    PurchaseDateBS TEXT NOT NULL,
    CostPrice DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

CREATE TABLE Customers (
    CustomerId INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Phone TEXT,
    Address TEXT
);

CREATE TABLE Orders (
    OrderId INTEGER PRIMARY KEY AUTOINCREMENT,
    CustomerId INTEGER NOT NULL,
    DateAD TEXT NOT NULL,
    DateBS TEXT NOT NULL,
    TotalAmount DECIMAL(18,2) NOT NULL,
    PendingAmount DECIMAL(18,2) DEFAULT 0,
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);

CREATE TABLE OrderItems (
    ItemId INTEGER PRIMARY KEY AUTOINCREMENT,
    OrderId INTEGER NOT NULL,
    ProductId INTEGER NOT NULL,
    Quantity INTEGER NOT NULL CHECK (Quantity > 0),
    UnitPrice DECIMAL(18,2) NOT NULL,
    ExciseTax DECIMAL(18,2) NOT NULL,
    VAT DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

CREATE TABLE Payments (
    PaymentId INTEGER PRIMARY KEY AUTOINCREMENT,
    OrderId INTEGER NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    PaymentDate TEXT NOT NULL,
    IsAdvance INTEGER DEFAULT 0,
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId)
);

-- Indexes
CREATE INDEX IX_Products_ShadeSlug ON Products(ShadeSlug);
CREATE INDEX IX_Products_Company ON Products(Company);
CREATE INDEX IX_Products_Category ON Products(Category);
CREATE INDEX IX_InventoryStock_ProductId ON InventoryStock(ProductId);
CREATE INDEX IX_InventoryStock_PurchaseDateAD ON InventoryStock(PurchaseDateAD);
CREATE INDEX IX_Customers_Phone ON Customers(Phone);
CREATE INDEX IX_Customers_Name ON Customers(Name);
CREATE INDEX IX_Orders_CustomerId ON Orders(CustomerId);
CREATE INDEX IX_Orders_DateAD ON Orders(DateAD);
CREATE INDEX IX_Orders_DateBS ON Orders(DateBS);
CREATE INDEX IX_OrderItems_OrderId ON OrderItems(OrderId);
CREATE INDEX IX_OrderItems_ProductId ON OrderItems(ProductId);
CREATE INDEX IX_Payments_OrderId ON Payments(OrderId);
CREATE INDEX IX_Payments_PaymentDate ON Payments(PaymentDate);

-- View
CREATE VIEW ProductDisplay AS
SELECT
    p.ProductId,
    p.Name,
    CASE p.Category
        WHEN 1 THEN 'Paint'
        WHEN 2 THEN 'Putty'
        WHEN 3 THEN 'Smart Care'
    END AS CategoryName,
    CASE
        WHEN p.Category = 1 THEN
            CASE p.Volume
                WHEN 500 THEN '500ML' WHEN 200 THEN '200ML' WHEN 100 THEN '100ML' WHEN 50 THEN '50ML'
                WHEN 1 THEN '1L' WHEN 4 THEN '4L' WHEN 10 THEN '10L' WHEN 20 THEN '20L' WHEN 40 THEN '40L'
            END
        WHEN p.Category = 2 THEN
            CASE p.Volume
                WHEN 1001 THEN '1Kg' WHEN 1005 THEN '5Kg' WHEN 1010 THEN '10Kg' WHEN 1020 THEN '20Kg' WHEN 1040 THEN '40Kg'
            END
        WHEN p.Category = 3 THEN
            CASE p.Volume
                WHEN 500 THEN '500ML' WHEN 200 THEN '200ML' WHEN 100 THEN '100ML' WHEN 50 THEN '50ML'
                WHEN 1 THEN '1L' WHEN 4 THEN '4L' WHEN 10 THEN '10L' WHEN 20 THEN '20L' WHEN 40 THEN '40L'
                WHEN 1001 THEN '1Kg/L' WHEN 1005 THEN '5Kg/L' WHEN 1010 THEN '10Kg/L' WHEN 1020 THEN '20Kg/L' WHEN 1040 THEN '40Kg/L'
            END
    END AS VolumeDisplay,
    p.Company,
    p.ShadeSlug,
    p.WholesalePrice,
    p.RetailPrice
FROM Products p;
