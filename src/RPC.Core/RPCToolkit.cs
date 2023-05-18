using Flurl.Http;
using Nethereum.Web3;
using Newtonsoft.Json.Linq;
using System.Text;
using Nethereum.Web3.Accounts;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;

namespace RPC.Core;

public class RPCToolkit
{
    private readonly string rpcConnection;
    private readonly HexBigInteger chainId;

    public RPCToolkit(string rpcConnection, HexBigInteger chainId)
    {
        this.rpcConnection = rpcConnection;
        this.chainId = chainId;
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
        var web3 = new Web3(rpcConnection);

        var contract = web3.Eth.GetContract(contractABI, contractAddress);
        var method = contract.GetFunction(methodName);
        var txInput = method.CreateTransactionInput(account.Address, functionInput);

        var transactionInput = new TransactionInput()
        {
            To = contractAddress,
            Data = txInput.Data,
            GasPrice = gasPriceGwei,
            Gas = gasLimit,
            ChainId = chainId
        };


        var signedTransaction = account.TransactionManager.SignTransactionAsync(transactionInput)
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
