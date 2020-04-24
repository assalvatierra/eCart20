-- initialize Status tables
insert into UserStatus("Name") values ('Active'),('Inactive');
insert into StoreStatus("Name") values ('Active'),('Inactive');
insert into StorePickupStatus("Name") values ('Active'),('Inactive');
insert into CartItemStatus("Name") values ('Active'),('Cancelled');
insert into CartStatus("Name") values ('Active'),('Submitted'),('Processing'),('Ready'),('Delivered');

-- initialize cities
insert into MasterCities("Name") values ('Davao'),('Tagum'),('Digos');

-- initialize areas
insert into MasterAreas("MasterCityId","Name") values (1,'Matina'),(1,'Agdao'),(1,'Buhangin'),(1,'Maa'),(1,'Mintal');


-- store categories
insert into StoreCategories("Name","SortOrder") values('Grocery',1),('Convenience',2),('General',3);


-- item category groups
insert into ItemCatGroups("Name","SortOrder") values('Canned Goods',1),('Beverages',2),('Snacks',3),('Personal Care',4),
('Baby Products',5),('Households',6),('Cooking',7),('School Supplies',8);

-- item categories
insert into ItemCategories("ItemCatGroupId","Name","SortOrder") values
(1,'Cornedbeef',1),(1,'Beefloaf',2),(1,'Sardines',3),(1,'Tuna',4),
(2,'Softdrinks',1),(1,'Energy Drink',2),(1,'Alcoholic',3),
(3,'Chips',1),(3,'Bread',2),(3,'Biscuit',3),(3,'Others',4),
(4,'Bath Soap',1),(4,'Shampoo',2),(4,'Others',3);
