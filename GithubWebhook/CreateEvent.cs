namespace GithubWebhook
{

    using Newtonsoft.Json;

    public partial class CreateEvent
    {
        [JsonProperty("ref")]
        public string Ref { get; set; }

        [JsonProperty("ref_type")]
        public string RefType { get; set; }

        [JsonProperty("master_branch")]
        public string MasterBranch { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("pusher_type")]
        public string PusherType { get; set; }

        [JsonProperty("repository")]
        public Repository Repository { get; set; }

        [JsonProperty("sender")]
        public User Sender { get; set; }
    }
}
