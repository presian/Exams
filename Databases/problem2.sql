SELECT TOP 30
	c.CountryName,
	c.Population
FROM Countries c
WHERE c.ContinentCode = (SELECT ContinentCode FROM Continents WHERE ContinentName = 'Europe')
ORDER BY c.Population DESC