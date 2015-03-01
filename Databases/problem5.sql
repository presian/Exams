SELECT
	p.PeakName,
	m.MountainRange AS [Mountain],
	p.Elevation
FROM Peaks p
LEFT JOIN Mountains m
ON p.MountainId = m.Id
ORDER BY p.Elevation DESC