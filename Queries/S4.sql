SELECT MAX(Games.price)
FROM Games
WHERE Games.developerId IN
  (SELECT Developers.id
   FROM Developers
   WHERE Developers.name = X);