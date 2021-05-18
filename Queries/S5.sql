SELECT Developers.name
FROM Developers
WHERE Developers.id IN
	(SELECT Games.developerId
	 FROM Games
	 WHERE Games.price >= X);
