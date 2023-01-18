using Newtonsoft.Json;

namespace DigiFamily.Models
{
    public class APIToken
    {
        public string Access_Token { get; set; }
        public string Token_Type { get; set; }
        public int Expires_In { get; set; }
        public string refresh_token { get; set; }
        public string UserName { get; set; }
        [JsonProperty(".issued")]
        public string Issued { get; set; }
        [JsonProperty(".expires")]
        public string Expires { get; set; }
        [JsonProperty("as:client_id")]
        public string client_id { get; set; }
    }

    public class ErrorResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }
    }
}
