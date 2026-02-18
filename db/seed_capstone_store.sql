INSERT INTO users (first_name, last_name, user_name, user_email, user_address)
VALUES
    -- (customers)
    ('Albert', 'Einstein', 'aeinstein', 'albert.einstein@example.com', 'Hollandsch Diep 1, Capelle'),
    ('Marie', 'Curie', 'mcurie', 'marie.curie@example.com', 'Radiumstraat 12, Rotterdam'),
    ('Isaac', 'Newton', 'inewton', 'isaac.newton@example.com', 'Apple Tree Lane 3, London'),
    ('Nikola', 'Tesla', 'ntesla', 'nikola.tesla@example.com', 'AC Boulevard 7, New York'),
    ('Charles', 'Darwin', 'cdarwin', 'charles.darwin@example.com', 'Evolution Road 9, Cambridge'),
    ('Alan', 'Turing', 'aturing', 'alan.turing@example.com', 'Enigma Street 11, Manchester'),
    ('Galileo', 'Galilei', 'ggalilei', 'galileo.galilei@example.com', 'Astronomy Street 1, Pisa'),
    ('Nicolaus', 'Copernicus', 'ncopernicus', 'nicolaus.copernicus@example.com', 'Heliocentric Way 3, Torun'),
    ('Michael', 'Faraday', 'mfaraday', 'michael.faraday@example.com', 'Electromagnetism Lane 7, London'),
    ('Max', 'Planck', 'mplanck', 'max.planck@example.com', 'Quantum Road 42, Kiel'),
    ('Ada', 'Lovelace', 'alovelace', 'ada.lovelace@example.com', 'Analytical Engine St 5, London'),
    ('Grace', 'Hopper', 'ghopper', 'grace.hopper@example.com', 'Compiler Avenue 8, Arlington'),

    -- (admins)
    ('George', 'Washington', 'gwashington', 'george.washington@usa.gov', 'Mount Vernon, Virginia'),
    ('Abraham', 'Lincoln', 'alincoln', 'abraham.lincoln@usa.gov', 'Springfield, Illinois'),
    ('Franklin', 'Roosevelt', 'froosevelt', 'franklin.roosevelt@usa.gov', 'Hyde Park, New York');

INSERT INTO customer (user_id, age)
VALUES
    (1, 76),   -- Albert Einstein
    (2, 66),   -- Marie Curie
    (3, 84),   -- Isaac Newton
    (4, 86),   -- Nikola Tesla
    (5, 73),   -- Charles Darwin
    (6, 41),   -- Alan Turing
    (7, 77),   -- Galileo Galilei
    (8, 70),   -- Nicolaus Copernicus
    (9, 76),   -- Michael Faraday
    (10, 89),  -- Max Planck
    (11, 36),  -- Ada Lovelace
    (12, 85);  -- Grace Hopper

INSERT INTO admin (user_id)
VALUES
    (13),  -- George Washington
    (14),  -- Abraham Lincoln
    (15);  -- Franklin Roosevelt

INSERT INTO product_catalogue (product_name, description, product_price, quantity_in_stock)
VALUES
    ('Amoxicillin', 'Broad-spectrum antibiotic, oral use', 12.50, 120),
    ('Doxycycline', 'Antibiotic for bacterial infections', 9.95, 80),
    ('Azithromycin', 'Macrolide antibiotic for various infections', 18.75, 60),
    ('Ciprofloxacin', 'Fluoroquinolone antibiotic', 22.40, 45),
    ('Clarithromycin', 'Antibiotic for respiratory infections', 19.90, 50),
    ('Cetirizine', 'Antihistamine for allergy relief', 4.99, 200),
    ('Loratadine', 'Non-drowsy antihistamine', 5.49, 180),
    ('Desloratadine', 'Long-acting antihistamine', 6.75, 150),
    ('Fexofenadine', 'Antihistamine for hay fever', 7.95, 140),
    ('Levocetirizine', 'Antihistamine for allergic rhinitis', 6.25, 160);

INSERT INTO orders (customer_id, order_date, order_status)
VALUES
    (1, '2024-01-10', 'CART'),
    (2, '2024-01-12', 'SUBMITTED'),
    (3, '2024-01-15', 'PROCESSED'),
    (4, '2024-01-18', 'CANCELLED'),
    (5, '2024-01-20', 'CART'),
    (6, '2024-01-22', 'SUBMITTED'),
    (7, '2024-01-25', 'PROCESSED'),
    (8, '2024-01-27', 'CART'),
    (9, '2024-01-28', 'PROCESSED'),
    (10, '2024-01-30', 'SUBMITTED');

INSERT INTO order_details (order_id, product_id, amount, total_price)
VALUES
    -- Order 1 (Albert Einstein)
    (1, 6, 2, 9.98),     -- Cetirizine (2 × 4.99)
    (1, 1, 1, 12.50),   -- Amoxicillin

    -- Order 2 (Marie Curie)
    (2, 7, 1, 5.49),    -- Loratadine
    (2, 3, 1, 18.75),   -- Azithromycin

    -- Order 3 (Isaac Newton)
    (3, 2, 1, 9.95),    -- Doxycycline

    -- Order 4 (Nikola Tesla – cancelled)
    (4, 4, 1, 22.40),   -- Ciprofloxacin

    -- Order 5 (Charles Darwin)
    (5, 6, 3, 14.97),   -- Cetirizine (3 × 4.99)
    (5, 8, 1, 6.75),    -- Desloratadine

    -- Order 6 (Alan Turing)
    (6, 9, 1, 7.95),    -- Fexofenadine

    -- Order 7 (Galileo Galilei)
    (7, 10, 2, 12.50),  -- Levocetirizine (2 × 6.25)

    -- Order 8 (Nicolaus Copernicus)
    (8, 5, 1, 19.90),   -- Clarithromycin

    -- Order 9 (Michael Faraday)
    (9, 1, 1, 12.50),   -- Amoxicillin
    (9, 7, 1, 5.49),    -- Loratadine

    -- Order 10 (Max Planck)
    (10, 6, 1, 4.99);   -- Cetirizine
