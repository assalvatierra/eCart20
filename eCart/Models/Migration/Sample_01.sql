-- sample data for testing

-- create stores --
insert into StoreDetails([LoginId],[Name],[Address],[Remarks],[StoreStatusId],[StoreCategoryId],[MasterCityId],[MasterAreaId]) 
values	(1, 'NCCC Mall', 'Matina Crossing, Davao City','', 1, 3, 1, 1),
		(2, 'SM Mall', 'Ecoland, Davao City','', 1, 3, 1, 1),
		(3, 'Gaisano Mall', 'Sta Ana, Davao City','', 1, 3, 2, 1);

-- store pickup locations --
insert into StorePickupPoints([StoreDetailId],[Address],[Remarks],[StorePickupStatusId])
values  (1,'Matina Crossing, Davao City','NA',1),
		(2,'Ecoland, Davao City','NA',1),
		(3,'Sta Ana, Davao City','NA',1),
		--second address --
		(1,'Buhangin, Davao City','NA',1),	
		(2,'Lanang, Davao City','NA',1),	
		(3,'Toril, Davao City','NA',1);

insert into StorePickupPartners([StorePickupPointId],[StoreDetailId])
values	(1,1),(2,2),(3,3),
		(4,1),(5,2),(6,3);

-- item Master --
insert into ItemMasters([Name])
values	('Loaf Bread'), ('Rice 5 kilos'), ('Sardines'), ('Chicken'), ('Beef'), ('Pork'), ('CornBeef Canned'), ('Cenutry Tuna Canned'), ('Milk'), ('Coca-Cola 1 Liter'),
		('Colgate TootPaste'),('Safguard Soap'),('Clear Shampoo'),('Clear Shampoo'),('Clear Shampoo');

insert into ItemMasterCategories([ItemCategoryId],[ItemMasterId])
values (14, 1),(14, 2),(3, 3),(14,4),(14,5),(14,6),(1,7),(4,8),(14,9),(5,10);

-- create items for store --
insert into StoreItems([ItemMasterId], [StoreDetailId], [UnitPrice]) 
values	( 1, 1, 15.00 ), ( 2, 1, 52.00 ), ( 3, 1, 18.00 ), ( 4, 1, 120.00 ),( 5, 1, 155.00 ), ( 7, 1, 140.00 ), ( 8, 1, 18.00 ), ( 9, 1, 25.00 ), ( 10, 1, 45.00 ), ( 11, 1, 75.00 ),
		( 1, 2, 15.75 ), ( 2, 2, 50.25 ), ( 3, 2, 18.25 ), ( 4, 2, 125.00 ),( 5, 2, 165.00 ), 
		( 7, 3, 135.00 ), ( 8, 3, 19.75 ), ( 9, 3, 27.25 ), ( 10, 3, 42.25 ), ( 11, 3, 72.50 );

insert into UserDetails([UserId], [Name], [Address], [Email], [Mobile], [Remarks], [UserStatusId], [MasterCityId], [MasterAreaId] )
values (1, 'Admin', 'Address', 'Email', 'Mobile', 'Admin User', 1, 1, 1);