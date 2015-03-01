SELECT
	p.PeakName,
	r.RiverName,
	LOWER(SUBSTRING(p.PeakName, 1, LEN(p.PeakName) - 1)) + LOWER(r.RiverName) AS Mix
FROM	Rivers r,
		Peaks p
WHERE LOWER(SUBSTRING(p.PeakName, LEN(p.PeakName), 1)) = LOWER(SUBSTRING(r.RiverName, 1, 1))
ORDER BY Mix