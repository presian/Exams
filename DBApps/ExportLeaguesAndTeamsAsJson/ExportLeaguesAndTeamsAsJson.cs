namespace ExportLeaguesAndTeamsAsJson
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Newtonsoft.Json;

    using Football.Models;
    using Helper;

    class ExportLeaguesAndTeamsAsJson
    {
        static void Main()
        {
            var exportList = new List<JsonExport>();
            using (var db = new FootballEntities())
            {
                foreach (var league in db.Leagues
                    .OrderBy(l => l.LeagueName)
                    .Select(l => new
                    {
                        l.LeagueName,
                        Teams = l.Teams
                            .OrderBy(t => t.TeamName)
                            .Select(t => t.TeamName)
                    }))
                {
                    var currentLeague = new JsonExport
                    {
                        LeagueName = league.LeagueName,
                        TeamNames = league.Teams.ToList()
                    };
                    exportList.Add(currentLeague);
                }
            }

            File.WriteAllText(Utility.ExportFolder + "leagues-and-teams.json", JsonConvert.SerializeObject(exportList, Formatting.Indented));
        }
    }
}
