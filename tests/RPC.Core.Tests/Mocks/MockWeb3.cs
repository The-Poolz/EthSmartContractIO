using Moq;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3.Accounts;
using Nethereum.JsonRpc.Client;
using Nethereum.RPC.Eth.Services;

namespace RPC.Core.Tests.Mocks;

internal static class MockWeb3
{
    internal static IWeb3 GetMock
    {
        get
        {
            var clientMock = new Mock<IClient>();

            var transactionManagerMock = new Mock<Nethereum.RPC.TransactionManagers.TransactionManager>(clientMock.Object);
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

            return web3Mock.Object;
        }
    }
}
