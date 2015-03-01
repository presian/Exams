SELECT
	cont.ContinentName,
	SUM(CONVERT(BIGINT, c.AreaInSqKm)) AS CountriesArea,
	SUM(CONVERT(BIGINT, c.Population)) AS CountriesPopulation
FROM Continents cont
JOIN Countries c
	ON cont.ContinentCode = c.ContinentCode
GROUP BY cont.ContinentName
ORDER BY CountriesPopulation DESC