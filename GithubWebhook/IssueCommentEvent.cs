﻿namespace GithubWebhook
{

    using Newtonsoft.Json;

    public partial class IssueCommentEvent
    {
        /// <summary>
        /// Can be "created", "edited", or "deleted"
        /// </summary>
        [JsonProperty("action")]
        public string Action { get; set; }

        //TODO: [JsonProperty("changes")] (Have not found documentation yet)

        [JsonProperty("issue")]
        public Issue Issue { get; set; }

        [JsonProperty("comment")]
        public Comment Comment { get; set; }

        [JsonProperty("repository")]
        public Repository Repository { get; set; }

        [JsonProperty("sender")]
        public User Sender { get; set; }
    }
}
