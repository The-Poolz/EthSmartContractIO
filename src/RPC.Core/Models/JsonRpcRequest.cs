using Newtonsoft.Json.Linq;

namespace RPC.Core.Models;

public class JsonRpcRequest
{
    public JsonRpcRequest(string to, string data)
    {
        JsonRpc = "2.0";
        Method = "eth_call";
        Params = new JArray()
        {
            new JObject()
            {
                { "to", to },
                { "data", data }
            },
            "latest"
        };
        Id = 0;
    }
    public string JsonRpc { get; set; }
    public string Method { get; set; }
    public JArray Params { get; set; }
    public int Id { get; set; }
}
