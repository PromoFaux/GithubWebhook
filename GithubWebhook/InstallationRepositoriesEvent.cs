namespace GithubWebhook
{

    using Newtonsoft.Json;

    public partial class InstallationRepositoriesEvent
    {
        /// <summary>
        /// Can be "added" or "removed"
        /// </summary>
        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("installation")]
        public Installation Installation { get; set; }

        /// <summary>
        /// Can be "selected" or "all"
        /// </summary>
        [JsonProperty("repository_selection")]
        public string RepositorySelection { get; set; }

        [JsonProperty("repositories_added")]
        public Repository[] RepositoriesAdded { get; set; }

        [JsonProperty("repositories_removed")]
        public Repository[] RepositoriesRemoved { get; set; }

        [JsonProperty("sender")]
        public User Sender { get; set; }
    }
}
