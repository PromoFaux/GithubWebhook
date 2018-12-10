using GithubWebhook.Events;
using Microsoft.AspNetCore.Http;
using System.IO;
using Xunit;

namespace GithubWebhook.Tests
{
    public class EventTests
    {
        private MemoryStream ConvertResourceFileToMemoryStream(string fileName)
        {
            var file = File.ReadAllBytes($"./Resource/{fileName}.json");
            var stream = new MemoryStream(file);
            return stream;
        }

        [Fact]
        public void Event_With_Signature()
        {
            var hook = new GhWebhook(CreateDummyRequestForPayloadWithSignature(PushEvent.EventString), "clientSecret");
            Assert.IsType<PushEvent>(hook.PayloadObject);
        }

        [Fact]
        public void Event_With_Signature_Failed_Invalid_Signature()
        {
            var exception = Assert.Throws<System.Exception>(() =>
                     new GhWebhook(CreateDummyRequestForPayloadWithSignature(PullRequestEvent.EventString, false), "clientSecret"));
            Assert.Equal("Invalid shaPrefix", exception.Message);
        }

        [Fact]
        public void Event_With_Signature_Failed_Invalid_Secret()
        {
            var exception = Assert.Throws<System.Exception>(() =>
                    new GhWebhook(CreateDummyRequestForPayloadWithSignature(PullRequestEvent.EventString), "invalidSecret"));
            Assert.Contains("Invalid Signature. Expected", exception.Message);
        }

        [Fact]
        public void Event_Invalid_ContentType()
        {
            var exception = Assert.Throws<System.Exception>(() => new GhWebhook(CreateDummyRequestForPayload(PullRequestEvent.EventString, false)));
            Assert.Contains("Invalid content type. Expected application/json", exception.Message);
        }

        [Fact]
        public void Push_Event()
        {
            var hook = new GhWebhook(CreateDummyRequestForPayload(PushEvent.EventString));
            Assert.IsType<PushEvent>(hook.PayloadObject);
        }

        [Fact]
        public void PullRequest_Event()
        {
            var hook = new GhWebhook(CreateDummyRequestForPayload(PullRequestEvent.EventString));
            Assert.IsType<PullRequestEvent>(hook.PayloadObject);

        }

        [Fact]
        public void Create_Event()
        {
            var hook = new GhWebhook(CreateDummyRequestForPayload(CreateEvent.EventString));
            Assert.IsType<CreateEvent>(hook.PayloadObject);
        }

        [Fact]
        public void CommmitComment_Event()
        {
            var hook = new GhWebhook(CreateDummyRequestForPayload(CommitCommentEvent.EventString));
            Assert.IsType<CommitCommentEvent>(hook.PayloadObject);
        }

        [Fact]
        public void Delete_Event()
        {
            var hook = new GhWebhook(CreateDummyRequestForPayload(DeleteEvent.EventString));
            Assert.IsType<DeleteEvent>(hook.PayloadObject);
        }

        [Fact]
        public void Deployment_Event()
        {
            var hook = new GhWebhook(CreateDummyRequestForPayload(DeploymentEvent.EventString));
            Assert.IsType<DeploymentEvent>(hook.PayloadObject);
        }


        [Fact]
        public void DeploymentStatus_Event()
        {
            var hook = new GhWebhook(CreateDummyRequestForPayload(DeploymentStatusEvent.EventString));
            Assert.IsType<DeploymentStatusEvent>(hook.PayloadObject);
        }

        [Fact]
        public void Fork_Event()
        {
            var hook = new GhWebhook(CreateDummyRequestForPayload(ForkEvent.EventString));
            Assert.IsType<ForkEvent>(hook.PayloadObject);
        }

        [Fact]
        public void Gollum_Event()
        {
            var hook = new GhWebhook(CreateDummyRequestForPayload(GollumEvent.EventString));
            Assert.IsType<GollumEvent>(hook.PayloadObject);
            var gollumEvent = (GollumEvent)hook.PayloadObject;
            Assert.NotNull(gollumEvent.Pages);            
            Assert.NotNull(gollumEvent.Repository);
            Assert.NotNull(gollumEvent.Sender);
        }

        private HttpRequest CreateDummyRequestForPayload(string type, bool validContent = true)
        {
            var context = new DefaultHttpContext();
            context.Request.ContentType = validContent ? "application/json" : "application/x-www-form-urlencoded";
            context.Request.Headers.Add("X-Github-Event", $"{type}");
            context.Request.Body = ConvertResourceFileToMemoryStream($"{type}");
            return context.Request;
        }

        private HttpRequest CreateDummyRequestForPayloadWithSignature(string type, bool validSha1 = true)
        {
            var context = new DefaultHttpContext();
            context.Request.ContentType = "application/json";
            var text = validSha1 ? "sha1=" : "";
            context.Request.Headers.Add("X-Hub-Signature", $"{text}08da62a7e389b818f9f8cb1eaca0caede83eb93a");
            context.Request.Headers.Add("X-Github-Event", $"{type}");
            context.Request.Body = ConvertResourceFileToMemoryStream($"{type}");
            return context.Request;
        }

    }
}
