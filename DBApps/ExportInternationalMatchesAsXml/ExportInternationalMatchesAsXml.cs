namespace ExportInternationalMatchesAsXml
{
    using System.Linq;
    using System.Xml.Linq;

    using Football.Models;
    using Helper;

    class ExportInternationalMatchesAsXml
    {
        static void Main()
        {
            var matches = new XElement("matches");
            using (var db = new FootballEntities())
            {
                foreach (var internationalMatch in db.InternationalMatches
                    .OrderBy(i => i.MatchDate)
                    .ThenBy(i => i.HomeCountry.CountryName)
                    .ThenBy(i => i.AwayCountry.CountryName)
                    .Select(i => new
                    {
                        i.MatchDate,
                        HomeCountryName = i.HomeCountry.CountryName,
                        i.HomeCountryCode,
                        i.HomeGoals,
                        AwayCountryName = i.AwayCountry.CountryName,
                        i.AwayCountryCode,
                        i.AwayGoals,
                        i.League.LeagueName
                    }))
                {
                    var matchTag = new XElement("match");
                    if (internationalMatch.MatchDate != null)
                    {
                        var date = internationalMatch.MatchDate.Value.ToString("dd-MMMM-yyyy");

                        if (internationalMatch.MatchDate.Value.ToLongTimeString() != "00:00:00")
                        {
                            date = date + " " + internationalMatch.MatchDate.Value.ToString("hh:mm");
                        }

                        matchTag.SetAttributeValue("date", date);
                    }

                    var homeCountryTag = new XElement("home-country");
                    homeCountryTag.SetValue(internationalMatch.HomeCountryName);
                    homeCountryTag.SetAttributeValue("code", internationalMatch.HomeCountryCode);
                    matchTag.Add(homeCountryTag);

                    var awayCountyTag = new XElement("away-country");
                    awayCountyTag.SetValue(internationalMatch.AwayCountryName);
                    awayCountyTag.SetAttributeValue("code", internationalMatch.AwayCountryCode);
                    matchTag.Add(awayCountyTag);

                    if (internationalMatch.HomeGoals != null && internationalMatch.AwayGoals != null)
                    {
                        var scoreTag = new XElement("score");
                        scoreTag.SetValue(internationalMatch.HomeGoals + "-" + internationalMatch.AwayGoals);
                        matchTag.Add(scoreTag);
                    }

                    if (internationalMatch.LeagueName != null)
                    {
                        var leagNameTag = new XElement("league");
                        leagNameTag.SetValue(internationalMatch.LeagueName);
                        matchTag.Add(leagNameTag);
                    }

                    matches.Add(matchTag);
                }
            }

            matches.Save(Utility.ExportFolder + "international-matches.xml");
        }
    }
}
