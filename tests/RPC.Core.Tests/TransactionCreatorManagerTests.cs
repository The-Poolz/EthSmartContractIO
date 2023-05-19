using Xunit;
using RPC.Core.Managers;
using RPC.Core.Tests.Data;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;

namespace RPC.Core.Tests;

public class TransactionCreatorManagerTests
{
    private static readonly Function contractMethod = MockContractManager.ContractManager.GetMethod("SignUp");
    private static readonly HexBigInteger chainId = new(97);
    private static readonly string from = "0xE433FfB237950BddCd4a2CD86515B43cea1fDCC8";
    private static readonly HexBigInteger gasLimit = new(90000000000);
    private static readonly HexBigInteger gasPriceGwei = new(10000000000);
    private static readonly string encodedMethodData = "0x2417a19b0000000000000000000000000000000000000000000000000000000000000001";

    [Fact]
    internal void CreateTransactionInput_ExpectedTransactionInput()
    {
        var transactionInput = TransactionCreatorManager.CreateTransactionInput(
            chainId,
            from,
            MockContractManager.ContractAddress,
            gasLimit,
            gasPriceGwei,
            contractMethod,
            new object[] { 1 }
        );

        Assert.NotNull(transactionInput);
        Assert.Equal(chainId, transactionInput.ChainId);
        Assert.Equal(encodedMethodData, transactionInput.Data);
        Assert.Equal(from, transactionInput.From);
        Assert.Equal(gasLimit, transactionInput.Gas);
        Assert.Equal(gasPriceGwei, transactionInput.GasPrice);
        Assert.Null(transactionInput.MaxFeePerGas);
        Assert.Null(transactionInput.MaxPriorityFeePerGas);
        Assert.Null(transactionInput.Nonce);
        Assert.Equal(MockContractManager.ContractAddress, transactionInput.To);
        Assert.Null(transactionInput.Type);
        Assert.Null(transactionInput.Value);
    }
}
