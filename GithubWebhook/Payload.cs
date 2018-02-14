namespace GithubWebhook
{

    using Newtonsoft.Json;

    public partial class Payload
    {
        [JsonProperty("task")]
        public string Task { get; set; }
    }
}
