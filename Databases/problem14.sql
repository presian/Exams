SELECT
	c.CountryName AS Country,
	ISNULL((SELECT
		pe.PeakName
	FROM Peaks pe
	WHERE pe.Elevation = (SELECT
		MAX(pp.Elevation)
	FROM Countries cc
	LEFT JOIN MountainsCountries mmcc
		ON cc.CountryCode = mmcc.CountryCode
	LEFT JOIN Mountains mm
		ON mmcc.MountainId = mm.Id
	LEFT JOIN Peaks pp
		ON mm.Id = pp.MountainId
		WHERE cc.CountryName = c.CountryName
		)), '(no highest peak)') AS [Highest Peak Name],
	ISNULL(MAX(p.Elevation), 0) AS [Highest Peak Elevation],
	ISNULL((SELECT mm.MountainRange
FROM Mountains mm
WHERE mm.Id = (SELECT
		pe.MountainId
	FROM Peaks pe
	WHERE pe.Elevation = (SELECT
		MAX(pp.Elevation)
	FROM Countries cc
	LEFT JOIN MountainsCountries mmcc
		ON cc.CountryCode = mmcc.CountryCode
	LEFT JOIN Mountains mm
		ON mmcc.MountainId = mm.Id
	LEFT JOIN Peaks pp
		ON mm.Id = pp.MountainId
		WHERE cc.CountryName = c.CountryName
		))), '(no mountain)') AS Mountain
FROM Countries c
LEFT JOIN MountainsCountries mc
	ON c.CountryCode = mc.CountryCode
LEFT JOIN Mountains m
	ON mc.MountainId = m.Id
LEFT JOIN Peaks p
	ON m.Id = p.MountainId
GROUP BY c.CountryName
ORDER BY c.CountryName, [Highest Peak Name]

--works--
--SELECT
--		pe.PeakName
--	FROM Peaks pe
--	WHERE pe.Elevation = (SELECT
--		MAX(pp.Elevation)
--	FROM Countries cc
--	LEFT JOIN MountainsCountries mmcc
--		ON cc.CountryCode = mmcc.CountryCode
--	LEFT JOIN Mountains mm
--		ON mmcc.MountainId = mm.Id
--	LEFT JOIN Peaks pp
--		ON mm.Id = pp.MountainId
--		WHERE cc.CountryName = 'Bulgaria'
--		)

--works--
--(SELECT mm.MountainRange
--FROM Mountains mm
--WHERE mm.Id = (SELECT
--		pe.MountainId
--	FROM Peaks pe
--	WHERE pe.Elevation = (SELECT
--		MAX(pp.Elevation)
--	FROM Countries cc
--	LEFT JOIN MountainsCountries mmcc
--		ON cc.CountryCode = mmcc.CountryCode
--	LEFT JOIN Mountains mm
--		ON mmcc.MountainId = mm.Id
--	LEFT JOIN Peaks pp
--		ON mm.Id = pp.MountainId
--		WHERE cc.CountryName = 'Bulgaria'
--		)))