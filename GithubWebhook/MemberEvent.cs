namespace GithubWebhook
{

    using Newtonsoft.Json;

    public partial class MemberEvent
    {
        /// <summary>
        /// Can be one of "added", "deleted", or "edited"
        /// </summary>
        [JsonProperty("action")]
        public string Action { get; set; }

        //TODO: [JsonProperty("changes")]

        [JsonProperty("member")]
        public User Member { get; set; }

        [JsonProperty("repository")]
        public Repository Repository { get; set; }

        [JsonProperty("sender")]
        public User Sender { get; set; }
    }
}
