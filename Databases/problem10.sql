SELECT
	cu.CurrencyCode,
	cu.Description AS [Currency],
	COUNT(c.CurrencyCode) AS [NumberOfCountries]
FROM Currencies cu
LEFT JOIN Countries c
	ON cu.CurrencyCode = c.CurrencyCode
GROUP BY	cu.CurrencyCode,
			cu.Description
ORDER BY NumberOfCountries DESC, Currency