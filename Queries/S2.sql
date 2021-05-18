SELECT Customers.lastName
FROM Customers
WHERE Customers.id IN
	(SELECT Orders.customerId
	 FROM Orders
	 WHERE Orders.gameId IN
	 	(SELECT Games.id
		 FROM Games
		 WHERE Games.developerId IN
		 	(SELECT Developers.id
			 FROM Developers
			 WHERE Developers.name = X)));