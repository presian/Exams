namespace ListAllTeamNames
{
    using System;

    using Football.Models;
    class ListAllTeamNames
    {
        static void Main()
        {
            using (var db = new FootballEntities())
            {
                foreach (var team in db.Teams)
                {
                    Console.WriteLine(team.TeamName);
                }
            }
        }
    }
}
