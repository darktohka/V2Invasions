using System;
using Newtonsoft.Json;

namespace V2Invasions
{
    public class Invasion {

        [JsonProperty("asOf")]
        private long asOf;

        [JsonProperty("type")]
        private string type;

        [JsonProperty("progress")]
        private string progress;

        public long GetAsOf() {
            return asOf;
        }

        public string GetCogType() {
            return type;
        }

        public string GetProgress() {
            return progress;
        }
    }
}
