using Nethereum.Web3;
using Nethereum.Contracts;

namespace RPC.Core.Managers;

public class ContractManager
{
    private readonly Web3 web3;
    public Contract Contract { get; private set; }

    public ContractManager(Web3 web3, string contractABI, string contractAddress)
    {
        this.web3 = web3;
        Contract = GetContract(contractABI, contractAddress);
    }

    private Contract GetContract(string contractABI, string contractAddress) =>
        web3.Eth.GetContract(contractABI, contractAddress);

    public Function GetMethod(string methodName) =>
        Contract.GetFunction(methodName);
}
