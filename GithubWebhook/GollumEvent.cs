
namespace GithubWebhook
{

    using Newtonsoft.Json;

    public partial class GollumEvent
    {
        [JsonProperty("pages")]
        public Page[] Pages { get; set; }

        [JsonProperty("repository")]
        public Repository Repository { get; set; }

        [JsonProperty("sender")]
        public User Sender { get; set; }
    }
}
