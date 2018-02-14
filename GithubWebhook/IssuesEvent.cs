namespace GithubWebhook
{

    using Newtonsoft.Json;

    public partial class IssuesEvent
    {
        /// <summary>
        /// Can be "assigned", "unassigned", "labeled", "unlabeled", "opened", "edited", "milestoned", "demilestoned", "closed", or "reopened"
        /// </summary>
        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("issue")]
        public Issue Issue { get; set; }

        //TODO: [JsonProperty("changes")] (Have not found documentation)

        [JsonProperty("repository")]
        public Repository Repository { get; set; }

        [JsonProperty("sender")]
        public User Sender { get; set; }
    }
}
