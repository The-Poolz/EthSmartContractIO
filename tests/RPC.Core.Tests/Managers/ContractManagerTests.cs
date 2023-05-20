using Xunit;
using RPC.Core.Tests.Mocks;

namespace RPC.Core.Managers.Tests;

public class ContractManagerTests
{
    [Fact]
    internal void GetContract_ExpectedContract()
    {
        var contract = MockContractManager.ContractManager.Contract;

        Assert.NotNull(contract);
        Assert.Equal(MockContractManager.ContractAddress, contract.Address);
    }

    [Fact]
    internal void GetMethod_ExpectedMethod()
    {
        var method = MockContractManager.ContractManager.GetMethod("ActivatePool");

        Assert.NotNull(method);
        Assert.Equal(MockContractManager.ContractAddress, method.ContractAddress);
    }

    [Fact]
    internal void GetMethod_MethodNotFound_ThrowException()
    {
        Action testcode = () => MockContractManager.ContractManager.GetMethod("InvalidMethodName");

        var exception = Assert.Throws<Exception>(testcode);
        Assert.Equal("Function not found:InvalidMethodName", exception.Message);
    }
}
