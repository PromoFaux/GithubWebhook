
namespace GithubWebhook
{

    using Newtonsoft.Json;
    
    public partial class Comment
    {
        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("position")]
        public long Position { get; set; }

        [JsonProperty("line")]
        public long Line { get; set; }

        [JsonProperty("commit_id")]
        public string CommitId { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("created_at")]
        public System.DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public System.DateTime UpdatedAt { get; set; }

        [JsonProperty("issue_url")]
        public string IssueUrl { get; set; }
    }
}
