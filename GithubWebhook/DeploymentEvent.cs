
namespace GithubWebhook
{

    using Newtonsoft.Json;

    public partial class DeploymentEvent
    {
        [JsonProperty("deployment")]
        public Deployment Deployment { get; set; }

        [JsonProperty("repository")]
        public Repository Repository { get; set; }

        [JsonProperty("sender")]
        public User Sender { get; set; }
    }
}
