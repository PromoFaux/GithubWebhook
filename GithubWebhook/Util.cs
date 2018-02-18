using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
     
   
}
