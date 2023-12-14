CREATE DATABASE ProductInventoryDB;
USE ProductInventoryDB;

CREATE TABLE Products (
    ProductId INT PRIMARY KEY,
    ProductName NVARCHAR(MAX),
    Price FLOAT,
    Quantity INT,
    MfDate DATE,
    ExpDate DATE
);


INSERT INTO Products (ProductId, ProductName, Price, Quantity, MfDate, ExpDate)
VALUES
    (1, 'Bread', 2.99, 50, '2023-01-01', '2023-12-31'),
    (2, 'Maggi', 1.99, 100, '2023-02-01', '2024-02-01'),
    (3, 'Cooldrink', 3.49, 30, '2023-03-01', '2023-12-31'),
    (4, 'Chocolate', 4.99, 20, '2023-04-01', '2024-04-01');
	

	select * from Products




