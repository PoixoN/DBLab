SELECT C.firstName, C.email
FROM Customers C
WHERE C.lastname = Y
AND NOT EXISTS
	((SELECT Orders.gameId
	  FROM Orders
	  WHERE Orders.customerId = C.id)
	 EXCEPT
	 (SELECT Games.id
	  FROM Games))
AND NOT EXISTS
	((SELECT Games.id
	  FROM Games)
	 EXCEPT
	 (SELECT Orders.gameId
	  FROM Orders
	  WHERE Orders.customerId = C.id));
