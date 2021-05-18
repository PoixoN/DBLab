SELECT Countries.name
FROM Countries
WHERE Countries.id IN
	(SELECT Developers.countryId
	 FROM Developers
	 WHERE Developers.id IN
		(SELECT D.id
		 FROM Developers D
		 WHERE NOT EXISTS
	 		((SELECT Software.price
			  FROM Software
		      WHERE Software.developerId = K)
		     EXCEPT
		     (SELECT Software.price
		      FROM Software
		      WHERE Software.developerId = D.id AND Software.developerId != K))));