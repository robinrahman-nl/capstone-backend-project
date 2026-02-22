SELECT
 o.order_id,
 c.customer_id,
 c.user_id,
 c.age

FROM
orders o
JOIN
customer c
ON o.customer_id = c.customer_id;