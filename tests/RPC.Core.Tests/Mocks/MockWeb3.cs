using Moq;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts;
using Nethereum.JsonRpc.Client;
using Nethereum.RPC.Eth.Services;
using Nethereum.RPC.TransactionManagers;

namespace RPC.Core.Tests.Mocks;

internal static class MockWeb3
{
    internal static IWeb3 GetMock
    {
        get
        {
            var clientMock = new Mock<IClient>();

            var transactionManagerMock = new Mock<TransactionManager>(clientMock.Object);
            var accountMock = new Mock<Account>();

            transactionManagerMock.Setup(tm => tm.SignTransactionAsync(It.IsAny<TransactionInput>()))
                .ReturnsAsync("signedTransaction");

            var ethApiTransactionsServiceMock = new Mock<IEthApiTransactionsService>();
            ethApiTransactionsServiceMock.Setup(tr => tr.SendRawTransaction.SendRequestAsync(It.IsAny<string>(), null))
                .ReturnsAsync("transactionHash");

            var web3Mock = new Mock<IWeb3>(MockBehavior.Loose);
            web3Mock.SetupGet(w => w.TransactionManager.Account.TransactionManager).Returns(transactionManagerMock.Object);
            web3Mock.SetupGet(w => w.Eth.Transactions).Returns(ethApiTransactionsServiceMock.Object);
            web3Mock.Setup(x => x.Eth.GetContract(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(MockContractManager.ContractManager.Contract);
            web3Mock.Setup(x => x.Eth.GasPrice.SendRequestAsync(null))
                .ReturnsAsync(new HexBigInteger(5000000000));
            web3Mock.Setup(x => x.Eth.TransactionManager.EstimateGasAsync(It.IsAny<TransactionInput>()))
                .Returns((TransactionInput transactionInput) =>
                {
                    if (transactionInput?.Gas?.Value < 27109)
                    {
                        throw new RpcResponseException(
                            new RpcError(-32000, $"gas required exceeds allowance ({transactionInput.Gas.Value}): eth_estimateGas"));
                    }

                    if (transactionInput?.Value?.Value < 10000000000000000)
                    {
                        throw new RpcResponseException(new RpcError(-32000, "execution reverted: Not Enough Fee Provided: eth_estimateGas"));
                    }

                    return Task.FromResult(new HexBigInteger(27109));
                });

            return web3Mock.Object;
        }
    }
}
