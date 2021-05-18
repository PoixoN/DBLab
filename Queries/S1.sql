SELECT Countries.name FROM Countries
WHERE Countries.id IN
(SELECT Developers.CountryId
FROM Developers
WHERE Developers.id IN
  (SELECT Games.developerId
   FROM Games
   WHERE Games.statusId IN
   (SELECT Statuses.id
     FROM Statuses
     WHERE Statuses.name = X)));
