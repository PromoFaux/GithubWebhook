
namespace GithubWebhook
{

    using Newtonsoft.Json;

    public partial class Forkee : Repository 
    {
        [JsonProperty("public")]
        public bool? Public { get; set; }
    }
}
