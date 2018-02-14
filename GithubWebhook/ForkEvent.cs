namespace GithubWebhook
{

    using Newtonsoft.Json;

    public partial class ForkEvent
    {
        [JsonProperty("forkee")]
        public Forkee Forkee { get; set; }

        [JsonProperty("repository")]
        public Repository Repository { get; set; }

        [JsonProperty("sender")]
        public User Sender { get; set; }
    }
}
