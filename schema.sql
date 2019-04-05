CREATE DATABASE PizzaHub_7

GO

USE PizzaHub_7

CREATE TABLE City
(
	ID INT IDENTITY NOT NULL,	--Identity will auto-number new entries starting at 1
	[Name] NVARCHAR(50),
	Latitude DECIMAL(9,6),
	Longitude DECIMAL(9,6),
	CONSTRAINT PK_City PRIMARY KEY (ID)	--The Constraint is an additional schema rule.  Give these meaningful names
)

INSERT INTO City(Name, Latitude, Longitude) VALUES
('Chicago', 41, -87),
('Indianapolis', 46, -86),
('Milwaukee', 43, -87)

--To represent the 1-to-many relationship between cities and restaurants, I add City Name to
--restaurant
CREATE TABLE Restaurant
(
	ID INT IDENTITY NOT NULL,
	[Name] NVARCHAR(100),
	PhoneNumber NVARCHAR(100),
	CityID INT,
	CONSTRAINT PK_Restaurant PRIMARY KEY (ID),
	CONSTRAINT FK_Restaurant_City FOREIGN KEY (CityID) REFERENCES City(ID) ON DELETE CASCADE
)

INSERT INTO Restaurant (Name, PhoneNumber, CityID) VALUES
('Pizza Castle', '555-5432', (SELECT TOP 1 ID FROM City WHERE Name = 'Chicago')),
('Pie Hole', '555-5678', (SELECT TOP 1 ID FROM City WHERE Name = 'Indianapolis')),
('Upper Crust', '555-6767', (SELECT TOP 1 ID FROM City WHERE Name = 'Milwaukee'))


CREATE TABLE Styles
(
	ID INT IDENTITY NOT NULL,
	[Name] NVARCHAR(100),
	CONSTRAINT PK_Styles PRIMARY KEY (ID)
)

INSERT INTO Styles([Name]) VALUES
('Thin Crust'), 
('Thick Crust'), 
('Stuffed')


CREATE TABLE Sizes
(
	ID INT IDENTITY NOT NULL,
	[Name] NVARCHAR(100),
	CONSTRAINT PK_Sizes PRIMARY KEY (ID)
)

INSERT INTO Sizes ([Name]) VALUES 
('12"'), 
('14"'), 
('16"'), 
('18"')


CREATE TABLE Pizza
(
	RestaurantID INT NOT NULL,
	SizeID INT,
	StyleID INT,
	Price MONEY,
	CONSTRAINT PK_Pizza PRIMARY KEY (RestaurantID, SizeID, StyleID),
	CONSTRAINT FK_Pizza_Restaurant FOREIGN KEY (RestaurantID) REFERENCES Restaurant(ID) ON DELETE CASCADE,
	CONSTRAINT FK_Pizza_Size FOREIGN KEY (SizeID) REFERENCES Sizes(ID),
	CONSTRAINT FK_Pizza_Style FOREIGN KEY (StyleID) REFERENCES Styles(ID)
)


INSERT INTO Pizza (RestaurantID, SizeID, StyleID, Price) VALUES
((SELECT TOP 1 ID FROM Restaurant WHERE Name = 'Pizza Castle'),(SELECT TOP 1 ID FROM Sizes WHERE Name = '12"'), (SELECT TOP 1 ID FROM Styles WHERE Name = 'Thin Crust'), 16.99),
((SELECT TOP 1 ID FROM Restaurant WHERE Name = 'Pizza Castle'),(SELECT TOP 1 ID FROM Sizes WHERE Name = '14"'), (SELECT TOP 1 ID FROM Styles WHERE Name = 'Thin Crust'), 17.99),
((SELECT TOP 1 ID FROM Restaurant WHERE Name = 'Pizza Castle'),(SELECT TOP 1 ID FROM Sizes WHERE Name = '16"'), (SELECT TOP 1 ID FROM Styles WHERE Name = 'Thin Crust'), 18.99),
((SELECT TOP 1 ID FROM Restaurant WHERE Name = 'Pizza Castle'),(SELECT TOP 1 ID FROM Sizes WHERE Name = '14"'), (SELECT TOP 1 ID FROM Styles WHERE Name = 'Thick Crust'), 18.99),
((SELECT TOP 1 ID FROM Restaurant WHERE Name = 'Pizza Castle'),(SELECT TOP 1 ID FROM Sizes WHERE Name = '16"'), (SELECT TOP 1 ID FROM Styles WHERE Name = 'Thick Crust'), 19.99),
((SELECT TOP 1 ID FROM Restaurant WHERE Name = 'Pizza Castle'),(SELECT TOP 1 ID FROM Sizes WHERE Name = '18"'), (SELECT TOP 1 ID FROM Styles WHERE Name = 'Thick Crust'), 20.99),

((SELECT TOP 1 ID FROM Restaurant WHERE Name = 'Pie Hole'),(SELECT TOP 1 ID FROM Sizes WHERE Name = '12"'), (SELECT TOP 1 ID FROM Styles WHERE Name = 'Thin Crust'), 13.99),
((SELECT TOP 1 ID FROM Restaurant WHERE Name = 'Pie Hole'),(SELECT TOP 1 ID FROM Sizes WHERE Name = '14"'), (SELECT TOP 1 ID FROM Styles WHERE Name = 'Thin Crust'), 15.99),
((SELECT TOP 1 ID FROM Restaurant WHERE Name = 'Pie Hole'),(SELECT TOP 1 ID FROM Sizes WHERE Name = '16"'), (SELECT TOP 1 ID FROM Styles WHERE Name = 'Thin Crust'), 16.99),
((SELECT TOP 1 ID FROM Restaurant WHERE Name = 'Pie Hole'),(SELECT TOP 1 ID FROM Sizes WHERE Name = '18"'), (SELECT TOP 1 ID FROM Styles WHERE Name = 'Thin Crust'), 17.99),

((SELECT TOP 1 ID FROM Restaurant WHERE Name = 'Upper Crust'),(SELECT TOP 1 ID FROM Sizes WHERE Name = '14"'), (SELECT TOP 1 ID FROM Styles WHERE Name = 'Thin Crust'), 18.99),
((SELECT TOP 1 ID FROM Restaurant WHERE Name = 'Upper Crust'),(SELECT TOP 1 ID FROM Sizes WHERE Name = '16"'), (SELECT TOP 1 ID FROM Styles WHERE Name = 'Thin Crust'), 19.99),
((SELECT TOP 1 ID FROM Restaurant WHERE Name = 'Upper Crust'),(SELECT TOP 1 ID FROM Sizes WHERE Name = '16"'), (SELECT TOP 1 ID FROM Styles WHERE Name = 'Stuffed'), 20.99),
((SELECT TOP 1 ID FROM Restaurant WHERE Name = 'Upper Crust'),(SELECT TOP 1 ID FROM Sizes WHERE Name = '18"'), (SELECT TOP 1 ID FROM Styles WHERE Name = 'Stuffed'), 22.99)


CREATE TABLE Toppings
(
	ID INT IDENTITY,
	[Name] NVARCHAR(100),
	CONSTRAINT PK_Toppings PRIMARY KEY (ID)
)

INSERT INTO Toppings([Name]) VALUES
('Cheese'), 
('Sausage'), 
('Pepperoni'), 
('Green Pepper'), 
('Black Olives'), 
('Corn'), 
('Bacon'), 
('Anchovy'), 
('Mushroom'), 
('Artichoke Hearts'), 
('Onion')

CREATE TABLE RestaurantToppings
(
	RestaurantID INT NOT NULL,
	ToppingID INT NOT NULL,
	Price MONEY
	CONSTRAINT PK_PizzaToppings PRIMARY KEY (RestaurantID, ToppingID),
	CONSTRAINT FK_RestaurantToppings_Restaurant FOREIGN KEY (RestaurantID) REFERENCES Restaurant(ID) ON DELETE CASCADE,
	CONSTRAINT FK_RestaurantToppings_Topping FOREIGN KEY (ToppingID) REFERENCES Toppings(ID) ON DELETE CASCADE
)

INSERT INTO RestaurantToppings (RestaurantID, ToppingID, Price)
SELECT Restaurant.ID, Toppings.ID, 0.25 FROM Restaurant, Toppings

CREATE TABLE CustomerOrder
(
	ID INT IDENTITY NOT NULL,
	CreatedDate DATETIME,
	ContactPhoneNumber NVARCHAR(100),
	DeliveryStreet NVARCHAR(100),
	DeliveryCity NVARCHAR(100),
	DeliveryState NVARCHAR(100),
	DeliveryZipCode NVARCHAR(20),
	SpecialInstructions NVARCHAR(MAX),
	CONSTRAINT PK_CustomerOrder PRIMARY KEY (ID)
)

CREATE TABLE CustomerOrderPizzas
(
	CustomerOrderID INT NOT NULL,
	LineItemID INT IDENTITY NOT NULL,
	RestaurantID INT NOT NULL,
	StyleID INT NULL,
	SizeID INT NULL,
	OrderPrice MONEY --Even though I have price on pizza, I don't want to change price
	CONSTRAINT PK_CustomerOrderPizzas PRIMARY KEY (CustomerOrderID, LineItemID),
	CONSTRAINT FK_CustomerOrderPizzas_CustomerORder FOREIGN KEY (CustomerOrderID) REFERENCES CustomerOrder(ID) ON DELETE CASCADE,
	CONSTRAINT FK_CustomerOrderPizzas_RestaurantID FOREIGN KEY (RestaurantID) REFERENCES Restaurant(ID) ON DELETE CASCADE,
	CONSTRAINT FK_CustomerOrderPizzas_StyleID FOREIGN KEY (StyleID) REFERENCES Styles(ID) ON DELETE SET NULL,
	CONSTRAINT FK_CustomerOrderPizzas_SizeID FOREIGN KEY (SizeID) REFERENCES Sizes(ID) ON DELETE SET NULL


)

CREATE TABLE CustomerOrderPizzaToppings
(
	CustomerOrderID INT NOT NULL,
	LineItemID INT NOT NULL,
	ToppingID INT NOT NULL,
	CONSTRAINT PK_CustomerOrderPizzaToppings PRIMARY KEY (LineItemID, ToppingID),
	CONSTRAINT FK_CustomerOrderPizzaToppings_CustomerOrderPizzas FOREIGN KEY (CustomerOrderID, LineItemID) REFERENCES CustomerOrderPizzas(CustomerOrderID, LineItemID),
	CONSTRAINT FK_CustomerOrderPizzaToppings_Toppings FOREIGN KEY (ToppingID) REFERENCES Toppings(ID)
)
