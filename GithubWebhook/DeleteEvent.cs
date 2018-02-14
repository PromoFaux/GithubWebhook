namespace GithubWebhook
{

    using Newtonsoft.Json;

    public partial class DeleteEvent
    {
        [JsonProperty("ref")]
        public string Ref { get; set; }

        /// <summary>
        /// Can be "branch" or "tag"
        /// </summary>
        [JsonProperty("ref_type")]
        public string RefType { get; set; }

        [JsonProperty("pusher_type")]
        public string PusherType { get; set; }

        [JsonProperty("repository")]
        public Repository Repository { get; set; }

        [JsonProperty("sender")]
        public User Sender { get; set; }
    }
}
