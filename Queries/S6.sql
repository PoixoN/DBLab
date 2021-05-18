SELECT Customers.firstName
FROM Customers
WHERE Customers.id IN
(SELECT Orders.customerId
	 FROM Orders
	 WHERE Orders.gameId IN
	 	(SELECT Games.id
		 FROM Games
		 WHERE Games.price > Y
		 AND Games.genreId IN
		 	(SELECT Genres.id
			 FROM Genres
			 WHERE Genres.name = X)));