using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;

namespace RPC.Core.Tests.Mocks;

internal static class MockTransactionInput
{
    internal static readonly TransactionInput MockTx = new()
    {
        ChainId = new HexBigInteger(97),
        Data = "0x2417a19b0000000000000000000000000000000000000000000000000000000000000001",
        From = "0xE433FfB237950BddCd4a2CD86515B43cea1fDCC8",
        Gas = new HexBigInteger(90000000000),
        GasPrice = new HexBigInteger(10000000000),
        MaxFeePerGas = null,
        MaxPriorityFeePerGas = null,
        Nonce = null,
        To = MockContractManager.ContractAddress,
        Type = null,
        Value = new HexBigInteger(10000000000000000)
    };
}
