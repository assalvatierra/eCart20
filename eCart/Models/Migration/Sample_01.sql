-- sample data for testing

-- create stores --
insert into StoreDetails([LoginId],[Name],[Address],[Remarks],[StoreStatusId],[StoreCategoryId],[MasterCityId],[MasterAreaId]) 
values	(1, 'NCCC Centerpoint', 'Matina Crossing, Davao City','', 1, 3, 1, 1),
		(2, 'SM SuperMarket', 'Ecoland, Davao City','', 1, 3, 1, 1),
		(3, 'Gaisano Mall', 'Ecoland, Davao City','', 1, 3, 2, 1);

-- item Master --
insert into ItemMasters([Name])
values	('Loaf Bread'), ('Rice'), ('Sardines'), ('Chicken'), ('Beef'), ('Pork'), ('CornBeef Canned'), ('Cenutry Tuna Canned'), ('Milk'), ('Coca-Cola 1 Liter'),
		('Colgate TootPaste'),('Safguard Soap'),('Clear Shampoo'),('Clear Shampoo'),('Clear Shampoo');

-- create items for store --
insert into StoreItems([ItemMasterId], [StoreDetailId], [UnitPrice]) 
values	( 1, 2, 15 ), ( 2, 2, 52 ), ( 3, 2, 18 ), ( 4, 2, 120 ),( 5, 2, 155 ), ( 7, 2, 140 ), ( 8, 2, 18 ), ( 9, 2, 25 ), ( 10, 2, 45 ), ( 11, 2, 75 );
