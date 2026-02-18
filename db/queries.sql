SELECT 
    c.customer_id,
    u.first_name,
    u.last_name,
    c.age
FROM 
    customer c
JOIN 
    users u 
    ON 
    c.user_id = u.user_id;

-- SELECT * FROM admin;