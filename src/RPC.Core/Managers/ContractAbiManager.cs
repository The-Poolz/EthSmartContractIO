using Flurl.Http;
using Newtonsoft.Json.Linq;

namespace RPC.Core.Managers;

public static class ContractAbiManager
{
    private const string baseUrl = "https://poolzfinancedata.com/contracts?NameVersion=";

    public static string GetContractAbi(string nameWithVersion)
    {
        var response = $"{baseUrl}{nameWithVersion}".GetJsonAsync<JArray>()
            .GetAwaiter()
            .GetResult();

        if (response?.Count == 0 || string.IsNullOrEmpty(response?[0]?["ABI"]?.ToString()))
        {
            throw new Exception("Contract ABI not found for the specified name and version.");
        }

        return response![0]!["ABI"]!.ToString();
    }
}
