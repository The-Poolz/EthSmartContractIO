# SmartContractIO

[![SonarCloud](https://sonarcloud.io/images/project_badges/sonarcloud-white.svg)](https://sonarcloud.io/summary/new_code?id=The-Poolz_RPCToolkit)

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=The-Poolz_RPCToolkit&metric=alert_status&token=2eea303b94df298cfeda35ef2fb09e4d8583bdea)](https://sonarcloud.io/summary/new_code?id=The-Poolz_RPCToolkit)
[![SonarScanner for .NET 6](https://github.com/The-Poolz/RPCToolkit/actions/workflows/dotnet.yml/badge.svg)](https://github.com/The-Poolz/RPCToolkit/actions/workflows/dotnet.yml)
[![CodeFactor](https://www.codefactor.io/repository/github/the-poolz/rpctoolkit/badge?s=3c1a503d1a1a95c78bec0850d0814c40f3a2b20f)](https://www.codefactor.io/repository/github/the-poolz/rpctoolkit)

`SmartContractIO` is a .NET library aimed at simplifying the interaction with Ethereum smart contracts. It allows developers to execute actions on the Ethereum network by wrapping the complexity of Ethereum RPC calls into a more manageable, high-level API.

## Getting Started

To use `SmartContractIO`, you will need to add it as a dependency to your project. You can do this by adding it as a NuGet package:

.NET CLI
```
dotnet add package SmartContractIO
```

Package Manager
```
Install-Package SmartContractIO
```

## Core Components

### RpcRequest

`RpcRequest` is the main object that is used to communicate with the Ethereum network.
It represents a request to either read data from a smart contract or write data to a smart contract.

#### Read Request

A Read request is created by calling the constructor `RpcRequest(string rpcUrl, string to, string data)`.
This type of request doesn't require a transaction to be mined and therefore doesn't require gas settings or a value.

```csharp
var readRequest = new RpcRequest(
    rpcUrl: "https://mainnet.infura.io/v3/YOUR_INFURA_PROJECT_ID",
    to: "0xYourContractAddress",
    data: "0xYourData"
);
```

#### Write Request

A Write request is created by calling the constructor `RpcRequest(string rpcUrl, int accountId, uint chainId, string to, HexBigInteger value, GasSettings gasSettings, string? data = null)`.
This type of request creates a transaction that needs to be mined, which requires gas settings and potentially a value.

```csharp
var writeRequest = new RpcRequest(
    rpcUrl: "https://mainnet.infura.io/v3/YOUR_INFURA_PROJECT_ID",
    accountId: 0, // account index in the HD Wallet derived from the mnemonic
    chainId: 1, // Ethereum Mainnet
    to: "0xYourContractAddress",
    value: new HexBigInteger(0),
    gasSettings: new GasSettings(maxGasLimit: 31000, maxGweiGasPrice: 20),
    data: "0xYourData"
);
```

### GasSettings

`GasSettings` is a simple class that holds the `GasPrice` and `GasLimit` settings for a write request.
Gas is the mechanism that Ethereum uses to allocate resources among nodes.

```csharp
var gasSettings = new GasSettings(maxGasLimit: 31000, maxGweiGasPrice: 20);
```

### IMnemonicProvider

`IMnemonicProvider` is an interface that should be implemented by the class responsible for providing the mnemonic.
The mnemonic is a secret phrase that is used to derive the private key for the Ethereum account that will be used to sign the transaction.
The `GetMnemonic` method is expected to return the mnemonic as a string.

```csharp
public class MyMnemonicProvider : IMnemonicProvider 
{
    public string GetMnemonic() =>
        "believe awake season ahead air royal patrol annual found habit shed choice";
}
```

### ContractRpc

`ContractRpc` is the main class that interfaces with your Ethereum node.
You can use it to execute actions (read or write) on the Ethereum network.

```csharp
// Initialize your mnemonic provider
var myMnemonicProvider = new MyMnemonicProvider();

// Initialize the ContractRpc object
var contractRpc = new ContractRpc(myMnemonicProvider);

// Execute the action
var result = contractRpc.ExecuteAction(myRpcRequest);

// Handle the result
Console.WriteLine(result);
```

In the example above, `ContractRpc` uses the strategy pattern to decide which class (`ContractRpcReader` for read operations and `ContractRpcWriter` for write operations) to use to execute the action.
This decision is based on the `ActionType` property of the `RpcRequest` object.

## How to Use

Here's a simple example of how to read data from a smart contract using this library:

```csharp
// Initialize your mnemonic provider
var myMnemonicProvider = new MyMnemonicProvider();

// Create a read request
var readRequest = new RpcRequest(
    rpcUrl: "https://mainnet.infura.io/v3/YOUR_INFURA_PROJECT_ID",
    to: "0xYourContractAddress",
    data: "0xYourData"
);

// Initialize the ContractRpc object
var contractRpc = new ContractRpc(myMnemonicProvider);

// Execute the action
var result = contractRpc.ExecuteAction(readRequest);

// Handle the result
Console.WriteLine(result);
```

And here's how to write data to a smart contract:

```csharp
// Initialize your mnemonic provider
var myMnemonicProvider = new MyMnemonicProvider();

// Create a write request
var writeRequest = new RpcRequest(
    rpcUrl: "https://mainnet.infura.io/v3/YOUR_INFURA_PROJECT_ID",
    accountId: 0,
    chainId: 1,
    to: "0xYourContractAddress",
    value: new HexBigInteger(0),
    gasSettings: new GasSettings { GasPrice = new HexBigInteger(20), GasLimit = new HexBigInteger(21000) },
    data: "0xYourData"
);

// Initialize the ContractRpc object
var contractRpc = new ContractRpc(myMnemonicProvider);

// Execute the action
var result = contractRpc.ExecuteAction(writeRequest);

// Handle the result
Console.WriteLine(result);
```

## Contribute

We welcome contributions from the community. Please submit pull requests for bug fixes, improvements and new features.

Happy coding!

## More Information
[RPC.Core Documentation](https://github.com/The-Poolz/RPCToolkit/blob/master/src/RPC.Core/README.md)
