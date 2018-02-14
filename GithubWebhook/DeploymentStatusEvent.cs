
namespace GithubWebhook
{

    using Newtonsoft.Json;

    public partial class DeploymentStatusEvent
    {
        [JsonProperty("deployment_status")]
        public DeploymentStatus DeploymentStatus { get; set; }

        [JsonProperty("deployment")]
        public Deployment Deployment { get; set; }

        [JsonProperty("repository")]
        public Repository Repository { get; set; }

        [JsonProperty("sender")]
        public User Sender { get; set; }
    }
}
