SELECT 
	r.RiverName AS River,
	COUNT(c.CountryCode) AS [Countries Count]
FROM Rivers r
join CountriesRivers cr
ON r.Id = cr.RiverId
JOIN Countries c
ON cr.CountryCode = c.CountryCode
GROUP BY r.RiverName
HAVING COUNT(c.CountryCode) >= 3