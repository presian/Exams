namespace ExportLeaguesAndTeamsAsJson
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class JsonExport
    {
        public JsonExport()
        {
            this.TeamNames = new List<string>();
        }

        [JsonProperty(PropertyName = "leagueName")]
        public string LeagueName { get; set; }

        [JsonProperty(PropertyName = "teams")]
        public List<string> TeamNames { get; set; }  
    }
}
