SELECT C.email
FROM Customers C
WHERE C.lastName != Y
AND NOT EXISTS
    ((SELECT Orders.gameId
      FROM Orders
      WHERE Orders.customerId = C.id)
     EXCEPT
     (SELECT Orders.gameId
      FROM Orders
      WHERE Orders.customerId IN
          (SELECT Customers.id
           FROM Customers
           WHERE Customers.email = Y)))
AND NOT EXISTS
    ((SELECT Orders.gameId
      FROM Orders
      WHERE Orders.customerId IN
  	      (SELECT Customers.id
           FROM Customers
           WHERE Customers.email = Y))
     EXCEPT
     (SELECT Orders.gameId
      FROM Orders
      WHERE Orders.customerId = C.id));
