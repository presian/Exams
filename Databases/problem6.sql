SELECT
	p.PeakName,
	m.MountainRange AS Mountain,
	c.CountryName,
	cont.ContinentName
FROM Peaks p
JOIN Mountains m
	ON p.MountainId = m.Id
JOIN MountainsCountries mc
	ON m.Id = mc.MountainId
JOIN Countries c
	ON mc.CountryCode = c.CountryCode
JOIN Continents cont
	ON c.ContinentCode = cont.ContinentCode
ORDER BY p.PeakName, c.CountryName