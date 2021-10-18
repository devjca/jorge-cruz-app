using Newtonsoft.Json;
using System;


namespace Entities
{
    public class RequestDto
    {
        [JsonProperty("projectioId")]
        public int ProjectioId { get; set; }

        [JsonProperty("startProjection")]
        public string StartProjection { get; set; }

        [JsonProperty("endProjection")]
        public string EndProjection { get; set; }
    }
}
