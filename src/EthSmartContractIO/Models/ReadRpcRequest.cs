using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EthSmartContractIO.Models;

/// <summary>
/// Class for creating a read RPC request.
/// </summary>
public class ReadRpcRequest
{
    [JsonProperty("jsonrpc")]
    public string JsonRpc { get; set; }

    [JsonProperty("method")]
    public string Method { get; set; }

    [JsonProperty("params")]
    public JArray Params { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadRpcRequest"/> class.
    /// </summary>
    /// <param name="to">The address to send the request to.</param>
    /// <param name="data">The data to send with the request.</param>
    public ReadRpcRequest(string to, string data)
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
}
