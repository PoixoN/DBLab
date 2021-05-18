SELECT Statuses.name
FROM Statuses
WHERE Statuses.id NOT IN
  (SELECT Games.statusId
   FROM Games
   WHERE Games.developerId IN
   (SELECT Developers.id
     FROM Developers
     WHERE Developers.name = X));
