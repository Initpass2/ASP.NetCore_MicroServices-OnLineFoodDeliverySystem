using Newtonsoft.Json;

namespace OMF.CustomerManagement.Auth
{
    public class FacebookAuthSettings
    {
        [JsonProperty]
        public string AppId { get; set; }
        [JsonProperty]
        public string AppSecret { get; set; }
    }
}
