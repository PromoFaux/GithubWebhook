using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
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
        public void Push_Event()
        {
            var context = new DefaultHttpContext();
            context.Request.ContentType = "application/json";
            context.Request.Headers.Add("X-Github-Event", "push");
            context.Request.Body = ConvertResourceFileToMemoryStream("Push");

            var hook = new GhWebhook(context.Request);
            Assert.Equal("GithubWebhook.Events.PushEvent", hook.PayloadObject.GetType().ToString());
        }

        [Fact]
        public void PullRequest_Event()
        {
            var context = new DefaultHttpContext();
            context.Request.ContentType = "application/json";
            context.Request.Headers.Add("X-Github-Event", "pull_request");
            context.Request.Body = ConvertResourceFileToMemoryStream("PullRequest");

            var hook = new GhWebhook(context.Request);
            Assert.Equal("GithubWebhook.Events.PullRequestEvent", hook.PayloadObject.GetType().ToString());

        }

    }
}
