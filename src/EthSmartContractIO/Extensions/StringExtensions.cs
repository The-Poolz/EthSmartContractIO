using Nethereum.Util;

namespace EthSmartContractIO.Extensions;

public static class StringExtensions
{
    public static string ToMethodSignature(this string functionName) =>
        "0x" + new Sha3Keccack().CalculateHash(functionName)[..8];
}