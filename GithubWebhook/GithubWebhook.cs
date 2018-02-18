using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using GithubWebhook.Events;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GithubWebhook
{
    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None
        };
    }

    public class GithubWebhook
    {
        [Obsolete("This can still be used, but it is preffered to pass in the HttpRequest instead.")]
        public GithubWebhook(string strEvent, string signature, string delivery, string payloadText)
        {
            Event = strEvent;
            Signature = signature;
            Delivery = delivery;
            PayloadText = payloadText;

            PayloadObject = ConvertPayload();
        }
        
        public GithubWebhook(HttpRequest hookIn)
        {
            hookIn.Headers.TryGetValue("X-GitHub-Event", out var strEvent);
            hookIn.Headers.TryGetValue("X-Hub-Signature", out var signature);
            hookIn.Headers.TryGetValue("X-GitHub-Delivery", out var delivery);
            hookIn.Headers.TryGetValue("Content-type", out var content);

            Event = strEvent;
            Signature = signature;
            Delivery = delivery;

            if (content != "application/json")
            {
                throw new Exception("Invalid content type. Expected application/json");
            }

            using (var reader = new StreamReader(hookIn.Body, Encoding.UTF8))
            {
                PayloadText = reader.ReadToEnd();
            }

            PayloadObject = ConvertPayload();



        }

        public string Delivery { get; }
        public string Event { get; }
        public object PayloadObject { get; }

        private string PayloadText { get; }
        public string Signature { get; }

        private static string ValidateSignature(string payload, string signatureWithPrefix, string secret)
        {
            if (!signatureWithPrefix.StartsWith("sha1=", StringComparison.OrdinalIgnoreCase))
                return "Invalid shaPrefix";

            var secretBytes = Encoding.UTF8.GetBytes(secret);
            var payloadBytes = Encoding.UTF8.GetBytes(payload);

            using (var hmSha1 = new HMACSHA1(secretBytes))
            {
                var hash = hmSha1.ComputeHash(payloadBytes);

                return $"sha1={ToHexString(hash)}";
            }
        }

        private static string ToHexString(IReadOnlyCollection<byte> bytes)
        {
            var builder = new StringBuilder(bytes.Count * 2);
            foreach (var b in bytes) builder.AppendFormat("{0:x2}", b);

            return builder.ToString();
        }

        private object ConvertPayload()
        {
            switch (Event)
            {
                case "ping":
                    return  PingEvent.FromJson(PayloadText);
                case "commit_comment":
                    return  CommitCommentEvent.FromJson(PayloadText);
                case "create":
                    return  CreateEvent.FromJson(PayloadText);
                case "delete":
                    return  DeleteEvent.FromJson(PayloadText);
                case "deployment":
                    return  DeploymentEvent.FromJson(PayloadText);
                case "deployment_status":
                    return  DeploymentStatusEvent.FromJson(PayloadText);
                case "fork":
                    return  ForkEvent.FromJson(PayloadText);
                case "gollum":
                    return  GollumEvent.FromJson(PayloadText);
                case "installation":
                    return  InstallationEvent.FromJson(PayloadText);
                case "installation_repositories":
                    return  InstallationRepositoriesEvent.FromJson(PayloadText);
                case "issue_comment":
                    return  IssueCommentEvent.FromJson(PayloadText);
                case "issues":
                    return  IssuesEvent.FromJson(PayloadText);
                case "label":
                    return  LabelEvent.FromJson(PayloadText);
                case "member":
                    return  MemberEvent.FromJson(PayloadText);
                case "membership":
                    return  MembershipEvent.FromJson(PayloadText);
                case "milestone":
                    return  MilestoneEvent.FromJson(PayloadText);
                case "organization":
                    return  OrganizationEvent.FromJson(PayloadText);
                case "org_block":
                    return  OrgBlockEvent.FromJson(PayloadText);
                case "page_build":
                    return  PageBuildEvent.FromJson(PayloadText);
                case "project_card":
                    return  ProjectCardEvent.FromJson(PayloadText);
                case "project_column":
                    return  ProjectColumnEvent.FromJson(PayloadText);
                case "project":
                    return  ProjectEvent.FromJson(PayloadText);
                case "public":
                    return  PublicEvent.FromJson(PayloadText);
                case "pull_request":
                    return  PullRequestEvent.FromJson(PayloadText);
                case "pull_request_review":
                    return  PullRequestReviewEvent.FromJson(PayloadText);
                case "pull_request_review_comment":
                    return  PullRequestReviewCommentEvent.FromJson(PayloadText);
                case "push":
                    return  PushEvent.FromJson(PayloadText);
                case "release":
                    return  ReleaseEvent.FromJson(PayloadText);
                case "repository":
                    return  RepositoryEvent.FromJson(PayloadText);
                case "status":
                    return  StatusEvent.FromJson(PayloadText);
                case "watch":
                    return  WatchEvent.FromJson(PayloadText);
                default:
                    throw new NotImplementedException(
                        $"Event Type: `{Event}` is not implemented. Want it added? Open an issue at https://github.com/promofaux/GithubWebhooks");
            }
        }


        public bool SignatureValid(string clientSecret)
        {
            return ValidateSignature(PayloadText, Signature, clientSecret) == Signature;
        }

        public string GetExpectedSignature(string clientSecret)
        {
            return ValidateSignature(PayloadText, Signature, clientSecret);
        }
    }
}