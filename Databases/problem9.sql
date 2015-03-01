SELECT
	c.CountryName,
	cont.ContinentName,
	COUNT(r.Id) AS [RiversCount],
	ISNULL(SUM(r.Length), 0) AS [TotalLength]
FROM Countries c
LEFT JOIN Continents cont
	ON c.ContinentCode = cont.ContinentCode
LEFT JOIN CountriesRivers cr
	ON c.CountryCode = cr.CountryCode
LEFT JOIN Rivers r
	ON cr.RiverId = r.Id
GROUP BY	c.CountryName,
			cont.ContinentName
ORDER BY RiversCount DESC, TotalLength DESC, c.CountryName