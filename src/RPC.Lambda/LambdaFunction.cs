using Amazon.Lambda.Core;
using Newtonsoft.Json.Linq;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace RPC.Lambda;

public class LambdaFunction
{
    public JToken Run(object? message, ILambdaContext context)
    {
        return new JObject()
        {
            new JProperty("message", message)
        };
    }
}
