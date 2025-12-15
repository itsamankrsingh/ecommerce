insert into master.Tb_Categories(name,display_order) values ('Action',1);


insert into master.Tb_Categories(name,display_order) values ('Sci/Fi',2);
insert into master.Tb_Categories(name,display_order) values ('History',3);


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
