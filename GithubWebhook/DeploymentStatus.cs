﻿namespace GithubWebhook
{

    using Newtonsoft.Json;

    public partial class DeploymentStatus
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("creator")]
        public User Creator { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("target_url")]
        public string TargetUrl { get; set; }

        [JsonProperty("created_at")]
        public System.DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public System.DateTime UpdatedAt { get; set; }

        [JsonProperty("deployment_url")]
        public string DeploymentUrl { get; set; }

        [JsonProperty("repository_url")]
        public string RepositoryUrl { get; set; }
    }
}
