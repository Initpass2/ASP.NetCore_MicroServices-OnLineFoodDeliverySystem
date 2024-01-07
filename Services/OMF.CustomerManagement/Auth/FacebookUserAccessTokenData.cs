using Newtonsoft.Json;

namespace OMF.CustomerManagement.Auth
{
    public class FacebookUserAccessTokenData
    {
        [JsonProperty("app_id")]
        public long AppId { get; set; }
        public string Type { get; set; }
        public string Application { get; set; }
        [JsonProperty("expires_at")]
        public long ExpiresAt { get; set; }
        [JsonProperty("is_valid")]
        public bool IsValid { get; set; }
        [JsonProperty("user_id")]
        public long UserId { get; set; }
    }

    public class FacebookUserAccessTokenValidation
    {
        [JsonProperty]
        public FacebookUserAccessTokenData Data { get; set; }
    }
}
