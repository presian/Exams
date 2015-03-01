--1--
GO
CREATE TABLE Monasteries(
Id INT PRIMARY KEY IDENTITY,
Name NVARCHAR(MAX),
CountryCode char(2) foreign key references Countries(CountryCode)
)

--2--
GO
INSERT INTO Monasteries(Name, CountryCode) VALUES
('Rila Monastery “St. Ivan of Rila”', 'BG'), 
('Bachkovo Monastery “Virgin Mary”', 'BG'),
('Troyan Monastery “Holy Mother''s Assumption”', 'BG'),
('Kopan Monastery', 'NP'),
('Thrangu Tashi Yangtse Monastery', 'NP'),
('Shechen Tennyi Dargyeling Monastery', 'NP'),
('Benchen Monastery', 'NP'),
('Southern Shaolin Monastery', 'CN'),
('Dabei Monastery', 'CN'),
('Wa Sau Toi', 'CN'),
('Lhunshigyia Monastery', 'CN'),
('Rakya Monastery', 'CN'),
('Monasteries of Meteora', 'GR'),
('The Holy Monastery of Stavronikita', 'GR'),
('Taung Kalat Monastery', 'MM'),
('Pa-Auk Forest Monastery', 'MM'),
('Taktsang Palphug Monastery', 'BT'),
('Sümela Monastery', 'TR')

--3--
GO
ALTER TABLE Countries
ADD IsDeleted BIT default 0

--4--
GO
UPDATE Countries
SET IsDeleted = 1
WHERE CountryCode in (SELECT
	c.CountryCode
FROM Countries c
JOIN CountriesRivers cr
ON c.CountryCode = cr.CountryCode
JOIN Rivers r
ON cr.RiverId = r.Id
GROUP BY c.CountryCode
HAVING COUNT(r.Id) > 3)

--5--
SELECT 
	m.Name AS Monastery,
	c.CountryName AS Country
FROM Monasteries m
JOIN Countries c
ON m.CountryCode = c.CountryCode
WHERE c.IsDeleted IS NULL
ORDER BY m.Name