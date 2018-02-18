using System;
using System.Security.Cryptography;
using System.Text;
using GithubWebhook.Events;
using Newtonsoft.Json;

namespace GithubWebhook
{
    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }

    public class GithubWebhook
    {
        public string Event { get; }
        public string Signature { get; }
        public string Delivery { get; }
        public string CalculatedSignature { get; }
        public bool SignatureValid { get; }
        public object PayloadObject { get; }

        private static string ValidateSignature(string payload, string signatureWithPrefix, string secret)
        {
            if (!signatureWithPrefix.StartsWith("sha1=", StringComparison.OrdinalIgnoreCase))
            {
                return "Invalid shaPrefix";
            }

            var secretBytes = Encoding.UTF8.GetBytes(secret);
            var payloadBytes = Encoding.UTF8.GetBytes(payload);

            using (var hmSha1 = new HMACSHA1(secretBytes))
            {
                var hash = hmSha1.ComputeHash(payloadBytes);

                return $"sha1={ToHexString(hash)}";
            }

        }

        private static string ToHexString(byte[] bytes)
        {
            var builder = new StringBuilder(bytes.Length * 2);
            foreach (var b in bytes)
            {
                builder.AppendFormat("{0:x2}", b);
            }

            return builder.ToString();
        }

        public GithubWebhook(string strEvent, string signature, string delivery, string payloadText, string clientSecret)
        {
            Event = strEvent;
            Signature = signature;
            Delivery = delivery;

            CalculatedSignature = ValidateSignature(payloadText, signature, clientSecret);

            SignatureValid = CalculatedSignature == signature;

            switch (Event)
            {
                case "ping":
                    PayloadObject = PingEvent.FromJson(payloadText);
                    break;
                case "commit_comment":
                    PayloadObject = CommitCommentEvent.FromJson(payloadText);
                    break;
                case "create":
                    PayloadObject = CreateEvent.FromJson(payloadText);
                    break;
                case "delete":
                    PayloadObject = DeleteEvent.FromJson(payloadText);
                    break;
                case "deployment":
                    PayloadObject = DeploymentEvent.FromJson(payloadText);
                    break;
                case "deployment_status":
                    PayloadObject = DeploymentStatusEvent.FromJson(payloadText);
                    break;
                case "fork":
                    PayloadObject = ForkEvent.FromJson(payloadText);
                    break;
                case "gollum":
                    PayloadObject = GollumEvent.FromJson(payloadText);
                    break;
                case "installation":
                    PayloadObject = InstallationEvent.FromJson(payloadText);
                    break;
                case "installation_repositories":
                    PayloadObject = InstallationRepositoriesEvent.FromJson(payloadText);
                    break;
                case "issue_comment":
                    PayloadObject = IssueCommentEvent.FromJson(payloadText);
                    break;
                case "issues":
                    PayloadObject = IssuesEvent.FromJson(payloadText);
                    break;
                case "label":
                    PayloadObject = LabelEvent.FromJson(payloadText);
                    break;
                case "member":
                    PayloadObject = MemberEvent.FromJson(payloadText);
                    break;
                case "membership":
                    PayloadObject = MembershipEvent.FromJson(payloadText);
                    break;
                case "milestone":
                    PayloadObject = MilestoneEvent.FromJson(payloadText);
                    break;
                case "organization":
                    PayloadObject = OrganizationEvent.FromJson(payloadText);
                    break;
                case "org_block":
                    PayloadObject = OrgBlockEvent.FromJson(payloadText);
                    break;
                case "page_build":
                    PayloadObject = PageBuildEvent.FromJson(payloadText);
                    break;
                case "project_card":
                    PayloadObject = ProjectCardEvent.FromJson(payloadText);
                    break;
                case "project_column":
                    PayloadObject = ProjectColumnEvent.FromJson(payloadText);
                    break;
                case "project":
                    PayloadObject = ProjectEvent.FromJson(payloadText);
                    break;
                case "public":
                    PayloadObject = PublicEvent.FromJson(payloadText);
                    break;
                case "pull_request":
                    PayloadObject = PullRequestEvent.FromJson(payloadText);
                    break;
                case "pull_request_review":
                    PayloadObject = PullRequestReviewEvent.FromJson(payloadText);
                    break;
                case "pull_request_review_comment":
                    PayloadObject = PullRequestReviewCommentEvent.FromJson(payloadText);
                    break;
                case "push":
                    PayloadObject = PushEvent.FromJson(payloadText);
                    break;
                case "release":
                    PayloadObject = ReleaseEvent.FromJson(payloadText);
                    break;
                case "repository":
                    PayloadObject = RepositoryEvent.FromJson(payloadText);
                    break;
                case "status":
                    PayloadObject = StatusEvent.FromJson(payloadText);
                    break;
                case "watch":
                    PayloadObject = WatchEvent.FromJson(payloadText);
                    break;
                default:
                    throw new NotImplementedException($"Event Type: `{Event}` is not implemented. Want it added? Open an issue at https://github.com/promofaux/GithubWebhooks");
            }

        }
    }
}
