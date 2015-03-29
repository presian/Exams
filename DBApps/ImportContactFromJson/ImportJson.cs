namespace ImportContactFromJson
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    class ImportJson
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "position")]
        public string Position { get; set; }

        [JsonProperty(PropertyName = "company")]
        public string Company { get; set; }

        [JsonProperty(PropertyName = "emails")]
        public virtual List<string> Emails { get; set; }

        [JsonProperty(PropertyName = "phones")]
        public virtual List<string> Phones { get; set; }

        [JsonProperty(PropertyName = "site")]
        public string Site { get; set; }

        [JsonProperty(PropertyName = "notes")]
        public string Note { get; set; }
    }
}
