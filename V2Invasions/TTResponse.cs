using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace V2Invasions
{
    public class TTResponse
    {
        [JsonProperty("lastUpdated")]
        private long lastUpdated;

        [JsonProperty("invasions")]
        private Dictionary<String, Invasion> invasions;

        public long GetLastUpdated()
        {
            return lastUpdated;
        }

        public Dictionary<String, Invasion> GetInvasions()
        {
            return invasions;
        }
    }
}