namespace GithubWebhook
{

    using Newtonsoft.Json;

    public partial class PullRequest
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }

        [JsonProperty("diff_url")]
        public string DiffUrl { get; set; }

        [JsonProperty("patch_url")]
        public string PatchUrl { get; set; }
    }
}
