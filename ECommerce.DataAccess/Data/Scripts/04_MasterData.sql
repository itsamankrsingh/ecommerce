insert into master.Tb_Categories(name,display_order) values ('Action',1);


insert into master.Tb_Categories(name,display_order) values ('Sci/Fi',2);
insert into master.Tb_Categories(name,display_order) values ('History',3);
INSERT INTO master.tb_categories (name, display_order) VALUES ('Software Engineering', 7);


INSERT INTO master.tb_products
(title, description, isbn, author, list_price, price, price50, price100)
VALUES
(
    'Clean Code',
    'A Handbook of Agile Software Craftsmanship',
    '978-0132350884',
    'Robert C. Martin',
    999.00,
    850.00,
    800.00,
    750.00
),
(
    'The Pragmatic Programmer',
    'Your Journey to Mastery',
    '978-0201616224',
    'Andrew Hunt, David Thomas',
    1200.00,
    1000.00,
    950.00,
    900.00
),
(
    'Design Patterns',
    'Elements of Reusable Object-Oriented Software',
    '978-0201633610',
    'Erich Gamma et al.',
    1500.00,
    1300.00,
    1200.00,
    1100.00
);


update master.tb_products set category_id = 5;


INSERT INTO master.tb_products
(category_id, title, description, isbn, author, list_price, price, price50, price100)
VALUES
(
    (SELECT id FROM master.tb_categories WHERE name = 'Action'),
    'The Bourne Identity',
    'A fast-paced action thriller novel',
    '978-0441172719',
    'Robert Ludlum',
    799.00,
    650.00,
    600.00,
    550.00
),
(
    (SELECT id FROM master.tb_categories WHERE name = 'Sci/Fi'),
    'Dune',
    'Epic science fiction saga set on Arrakis',
    '978-0441013593',
    'Frank Herbert',
    999.00,
    850.00,
    800.00,
    750.00
),
(
    (SELECT id FROM master.tb_categories WHERE name = 'History'),
    'Sapiens',
    'A brief history of humankind',
    '978-0062316097',
    'Yuval Noah Harari',
    1199.00,
    1000.00,
    950.00,
    900.00
);

INSERT INTO master.tb_companies
(name, address, city, state, postalcode, phonenumber)
VALUES
(
    'Tata Consultancy Services',
    'TCS House, Raveline Street, Fort',
    'Mumbai',
    'Maharashtra',
    '400001',
    '+91-22-67789999'
),
(
    'Infosys Limited',
    'Electronics City Phase 1',
    'Bengaluru',
    'Karnataka',
    '560100',
    '+91-80-28520261'
),
(
    'Wipro Technologies',
    'Sarjapur Road',
    'Bengaluru',
    'Karnataka',
    '560035',
    '+91-80-46880000'
),
(
    'Reliance Industries Ltd',
    'Maker Chambers IV, Nariman Point',
    'Mumbai',
    'Maharashtra',
    '400021',
    '+91-22-35555000'
),
(
    'HDFC Bank Ltd',
    'HDFC Bank House, Senapati Bapat Marg',
    'Mumbai',
    'Maharashtra',
    '400013',
    '+91-22-39760000'
),
(
    'ICICI Bank Ltd',
    'ICICI Bank Towers, Bandra Kurla Complex',
    'Mumbai',
    'Maharashtra',
    '400051',
    '+91-22-26531414'
);
