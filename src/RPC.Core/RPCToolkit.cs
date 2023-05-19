using Flurl.Http;
using System.Text;
using Nethereum.Web3;
using Newtonsoft.Json.Linq;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts;

namespace RPC.Core;

public class RPCToolkit
{
    private readonly Web3 web3;
    private readonly string rpcConnection;
    private readonly HexBigInteger chainId;

    public RPCToolkit(string rpcConnection, HexBigInteger chainId)
    {
        this.rpcConnection = rpcConnection;
        this.chainId = chainId;

        web3 = new Web3(rpcConnection);
    }

    public string WriteToNetwork(
        Account account,
        string contractAddress,
        string contractABI,
        string methodName,
        HexBigInteger gasLimit,
        HexBigInteger gasPriceGwei,
        params object[] functionInput
    )
    {
        var contract = web3.Eth.GetContract(contractABI, contractAddress);
        var method = contract.GetFunction(methodName);

        var transaction = method.CreateTransactionInput(account.Address, functionInput);
        transaction.GasPrice = gasPriceGwei;
        transaction.Gas = gasLimit;
        transaction.ChainId = chainId;
        transaction.To = contractAddress;

        var signedTransaction = account.TransactionManager.SignTransactionAsync(transaction)
            .GetAwaiter()
            .GetResult();

        var transactionHash = web3.Eth.Transactions.SendRawTransaction.SendRequestAsync(signedTransaction)
            .GetAwaiter()
            .GetResult();

        return transactionHash;
    }

    public JToken ReadFromNetwork(string json)
    {
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = rpcConnection.PostAsync(content)
            .GetAwaiter()
            .GetResult();

        return response.GetJsonAsync<JToken>()
            .GetAwaiter()
            .GetResult();
    }
}
