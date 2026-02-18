-- -- • As a customer, I want to see all the items currently in my shopping cart.
-- SELECT * FROM
-- order_details od JOIN orders o
-- ON od.order_id = o.order_id
-- WHERE o.order_id = 1
--     AND order_status = 'CART';


-- -- As an administrator, I want to see all incoming (submitted) orders with details.
-- SELECT 
--     o.order_id,
--     o.customer_id,
--     o.order_date,
--     o.order_status,
--     od.product_id,
--     od.amount,
--     od.total_price
-- FROM orders o
-- JOIN order_details od 
--     ON o.order_id = od.order_id
-- WHERE o.order_status = 'SUBMITTED'
-- ORDER BY o.order_date DESC;


-- INSERT INTO product_catalogue (product_name, description, product_price, quantity_in_stock)
-- VALUES 
--     ('Test_name', 'Test_description', 12.50, 120)
-- ;

SELECT * FROM users;

SELECT user_id, first_name, last_name, user_name, user_email, user_address
FROM users;
