namespace ImportLeaguesAndTeamsFromXML
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.XPath;

    using Football.Models;
    using Helper;

    class ImportLeaguesAndTeamsFromXml
    {
        static void Main()
        {
            var doc = XDocument.Load(Utility.ImportFolder + "leagues-and-teams.xml");
            var leagues = doc.XPathSelectElements("/leagues-and-teams/league");
            var db = new FootballEntities();
            var index = 0;
            foreach (var league in leagues)
            {
                index += 1;
                try
                {
                    var leagueName = GetLeagueName(league);
                    Console.WriteLine("Processing league #" + index + " ...");
                    var currentLeague = AddNewLeague(leagueName, db);
                    GetTeams(league, db, currentLeague);
                    Console.WriteLine();

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private static League AddNewLeague(string leagueName, FootballEntities db)
        {
            if (leagueName != null)
            {
                var league = db.Leagues.FirstOrDefault(l => l.LeagueName == leagueName);
                if (league == null)
                {
                    league = new League
                    {
                        LeagueName = leagueName
                    };

                    db.Leagues.Add(league);
                    Console.WriteLine("Created league: " + leagueName);
                }
                else
                {
                    Console.WriteLine("Existing league: " + leagueName);
                }

                return league;
            }

            return null;
        }

        private static void GetTeams(XElement league, FootballEntities db, League currentLeague)
        {
            var teamsTag = league.Element("teams");
            if (teamsTag != null)
            {
                foreach (var team in teamsTag.Descendants("team"))
                {
                    var name = team.Attribute("name").Value;
                    string country = null;
                    if (team.Attribute("country") != null)
                    {
                        country = team.Attribute("country").Value;
                    }

                    var currentTeam = db.Teams
                        .FirstOrDefault(t => t.TeamName == name
                                             && t.Country.CountryName == country);

                    if (currentTeam == null)
                    {
                        var currentCountry =
                            db.Countries.FirstOrDefault(c => c.CountryName == country);
                        currentTeam = new Team
                        {
                            TeamName = name,
                            Country = currentCountry,
                            Leagues = new Collection<League>
                            {
                               currentLeague 
                            }
                        };

                        Console.WriteLine("Created team: {0} ({1})", 
                            currentTeam.TeamName,
                            currentTeam.Country == null ? "no country" : currentTeam.Country.CountryName);

                        if (currentLeague != null)
                        {
                            currentTeam.Leagues.Add(currentLeague);
                            Console.WriteLine("Added team to league: {0} to league {1}",
                                currentTeam.TeamName,
                                currentLeague.LeagueName);
                        }

                        db.Teams.Add(currentTeam);
                    }
                    else
                    {
                        Console.WriteLine("Existing team: {0} ({1})",
                            currentTeam.TeamName,
                            currentTeam.Country == null ? "no country" : currentTeam.Country.CountryName);
                        if (currentLeague != null)
                        {
                            if (currentTeam.Leagues.Any(l => l.LeagueName == currentLeague.LeagueName))
                            {
                                Console.WriteLine("Existing team in league: {0} belongs to {1}",
                                currentTeam.TeamName,
                                currentLeague.LeagueName);
                            }
                            else
                            {
                                Console.WriteLine("Added team to league: {0} to league {1}",
                                currentTeam.TeamName,
                                currentLeague.LeagueName);
                            }

                            currentTeam.Leagues.Add(currentLeague);
                        }
                    }
                }
            }
        }

        private static string GetLeagueName(XElement league)
        {
            if (league.Element("league-name") != null)
            {
                return league.Element("league-name").Value;
            }

            return null;
        }
    }
}
