namespace GithubWebhook
{

    using Newtonsoft.Json;

    public partial class InstallationEvent
    {
        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("installation")]
        public Installation Installation { get; set; }

        [JsonProperty("sender")]
        public User Sender { get; set; }
    }
}
