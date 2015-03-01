--1--
UPDATE Countries
SET CountryName = 'Burma'
WHERE CountryName = 'Myanmar'

--2--
INSERT INTO Monasteries(Name, CountryCode)
VALUES('Hanga Abbey',(SELECT CountryCode FROM Countries WHERE CountryName = 'Tanzania'))

--3-- 
INSERT INTO Monasteries(Name, CountryCode)
VALUES('Myin-Tin-Daik', (SELECT CountryCode FROM Countries WHERE CountryName = 'Myanmar'))

--4--
SELECT
	co.ContinentName,
	c.CountryName,
	COUNT(m.Id) AS MonasteriesCount
FROM Continents co
LEFT JOIN Countries c
	ON co.ContinentCode = c.ContinentCode
LEFT JOIN Monasteries m
	ON c.CountryCode = m.CountryCode
WHERE c.IsDeleted IS NULL
GROUP BY c.CountryName, co.ContinentName
ORDER BY MonasteriesCount DESC, c.CountryName