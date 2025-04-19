-- Products table with enum-like values
CREATE TABLE Products (
    ProductID INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Category INTEGER NOT NULL CHECK (Category IN (1, 2, 3)), -- 1=Paint, 2=Putty, 3=SmartCare
    Volume INTEGER NOT NULL CHECK (
        Volume IN (500, 200, 100, 50, 1, 4, 10, 20, 40) OR
        (Category = 2 AND Volume IN (1, 5, 10, 20, 40)) -- Putty only allows kg volumes
    ),
    Company TEXT NOT NULL,
    ShadeSlug TEXT NOT NULL,
    WholesalePrice REAL NOT NULL CHECK (WholesalePrice > 0),
    RetailPrice REAL GENERATED ALWAYS AS (
        ROUND(WholesalePrice + 
             (WholesalePrice * 0.07) +  -- Excise
             ((WholesalePrice + (WholesalePrice * 0.07)) * 0.13), 2)  -- VAT
    ) STORED
);

-- Customers table remains unchanged
CREATE TABLE Customers (
    CustomerID INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Phone TEXT,
    Address TEXT
);

-- Inventory stock with volume constraints
CREATE TABLE InventoryStock (
    BatchID INTEGER PRIMARY KEY AUTOINCREMENT,
    ProductID INTEGER NOT NULL,
    Quantity INTEGER NOT NULL CHECK (Quantity > 0),
    PurchaseDateAD TEXT NOT NULL, -- ISO8601 format (YYYY-MM-DD)
    PurchaseDateBS TEXT NOT NULL, -- Nepali date format
    CostPrice REAL NOT NULL CHECK (CostPrice > 0),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- Orders table
CREATE TABLE Orders (
    OrderID INTEGER PRIMARY KEY AUTOINCREMENT,
    CustomerID INTEGER NOT NULL,
    DateAD TEXT NOT NULL,
    DateBS TEXT NOT NULL,
    TotalAmount REAL NOT NULL,
    PendingAmount REAL DEFAULT 0,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);

-- Order items with volume validation
CREATE TABLE OrderItems (
    ItemID INTEGER PRIMARY KEY AUTOINCREMENT,
    OrderID INTEGER NOT NULL,
    ProductID INTEGER NOT NULL,
    Quantity INTEGER NOT NULL CHECK (Quantity > 0),
    UnitPrice REAL NOT NULL,
    ExciseTax REAL NOT NULL,
    VAT REAL NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- Payments table
CREATE TABLE Payments (
    PaymentID INTEGER PRIMARY KEY AUTOINCREMENT,
    OrderID INTEGER NOT NULL,
    Amount REAL NOT NULL,
    PaymentDate TEXT NOT NULL,
    IsAdvance INTEGER DEFAULT 0, -- 0=false, 1=true
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
);

-- View for product display names
CREATE VIEW ProductDisplay AS
SELECT 
    ProductID,
    Name,
    CASE Category
        WHEN 1 THEN 'Paint'
        WHEN 2 THEN 'Putty'
        WHEN 3 THEN 'Smart Care'
    END AS CategoryName,
    CASE 
        WHEN Category = 1 THEN -- Paint volumes
            CASE Volume
                WHEN 500 THEN '500ML'
                WHEN 200 THEN '200ML'
                WHEN 100 THEN '100ML'
                WHEN 50 THEN '50ML'
                WHEN 1 THEN '1L'
                WHEN 4 THEN '4L'
                WHEN 10 THEN '10L'
                WHEN 20 THEN '20L'
                WHEN 40 THEN '40L'
            END
        WHEN Category = 2 THEN -- Putty volumes
            CASE Volume
                WHEN 1 THEN '1Kg'
                WHEN 5 THEN '5Kg'
                WHEN 10 THEN '10Kg'
                WHEN 20 THEN '20Kg'
                WHEN 40 THEN '40Kg'
            END
        ELSE 'N/A' -- For Smart Care products
    END AS VolumeDisplay,
    Company,
    ShadeSlug,
    WholesalePrice,
    RetailPrice
FROM Products;