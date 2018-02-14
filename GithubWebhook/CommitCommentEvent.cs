namespace GithubWebhook
{

    using Newtonsoft.Json;

    public partial class CommitCommentEvent
    {
        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("comment")]
        public Comment Comment { get; set; }

        [JsonProperty("repository")]
        public Repository Repository { get; set; }

        [JsonProperty("sender")]
        public User Sender { get; set; }
    }
}
