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

A Write request is created by calling the constructor `RpcRequest(string rpcUrl, string to, WriteRpcRequest writeRequest, string? data = null)`.
This type of request creates a transaction that needs to be mined, which requires gas settings and potentially a value.

```csharp
var writeRequest = new RpcRequest(
    rpcUrl: "https://mainnet.infura.io/v3/YOUR_INFURA_PROJECT_ID",
    to: "0xYourContractAddress",
    writeRequest: new WriteRpcRequest(
        chainId: 1,
        value: new HexBigInteger(0),
        gasSettings: new GasSettings(maxGasLimit: 31000, maxGweiGasPrice: 20),
        accountProvider: new YourIAccountProvider()
    )
);
```

### GasSettings

`GasSettings` is a simple class that holds the `GasPrice` and `GasLimit` settings for a write request.
Gas is the mechanism that Ethereum uses to allocate resources among nodes.

```csharp
var gasSettings = new GasSettings(maxGasLimit: 31000, maxGweiGasPrice: 20);
```

### IAccountProvider

`IAccountProvider` is an interface that should be implemented by the class responsible for providing an Ethereum account.
The Ethereum account holds a private key that will be used to sign the Ethereum transaction.
The `Account` property is expected to return an instance of the `Account` object from the `Nethereum.Web3.Accounts` namespace.

#### Example

Here is an example of how to implement this interface:

```csharp
using RPC.Core.Providers;
using Nethereum.Web3.Accounts;

public class MyAccountProvider : IAccountProvider 
{
    public Account Account => new Account("0x4e3c79ee2f53da4e456cb13887f4a7d59488677e9e48b6fb6701832df828f7e9");
}
```

In this example, we're providing an Ethereum account with a specific private key.
Please note that hardcoding private keys is not a good practice, this is just an illustrative example.
Always store private keys in a secure manner.

#### MnemonicProvider

`MnemonicProvider` is a concrete class that implements the `IAccountProvider` interface.
This class uses a mnemonic to generate an Ethereum account.

Here is a description of the `MnemonicProvider`:

- `Account`: This property provides access to the Ethereum account derived from the provided mnemonic.
- `MnemonicProvider Constructor`: This constructor takes four parameters:
    - `mnemonicWords`: A string containing the mnemonic phrase. This phrase is used to generate the wallet from which the Ethereum account is derived.
    - `accountId`: A uint representing the index of the account to derive from the wallet. Each wallet can generate multiple accounts, and this index identifies which account to use.
    - `chainId`: A uint that represents the ID of the Ethereum chain to use. Different chains have different IDs, and this ID helps ensure that the correct chain is used.
    - `seedPassword (optional)`: A string representing the password to use when generating the wallet from the mnemonic. This password adds an additional layer of security to the wallet.

```cshapr
using Nethereum.HdWallet;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts;

namespace RPC.Core.Providers;

public class MnemonicProvider : IAccountProvider
{
    public Account Account { get; private set; }

    public MnemonicProvider(string mnemonicWords, uint accountId, uint chainId, string seedPassword = "")
    {
        var wallet = new Wallet(words: mnemonicWords, seedPassword: seedPassword);
        Account = wallet.GetAccount((int)accountId, new HexBigInteger(chainId));
    }
}
```

In the above example, an Ethereum wallet is created using the provided mnemonic words and an optional seed password.
Then, an Ethereum account is derived from this wallet using the specified account ID and chain ID.
The derived Ethereum account is assigned to the `Account` property of the `MnemonicProvider` class.

Remember, the mnemonic phrase (and optionally a password) is used to derive the private key.
The private key should be kept secure as it allows full control over the Ethereum account it is associated with.

### ContractRpc

`ContractRpc` is the main class that interfaces with your Ethereum node.
You can use it to execute actions (read or write) on the Ethereum network.

```csharp
// Initialize your account provider
var myAccountProvider = new MyAccountProvider();

// Initialize the ContractRpc object
var contractRpc = new ContractRpc(myAccountProvider);

// Execute the action
var result = contractRpc.ExecuteAction(myRpcRequest);

// Handle the result
Console.WriteLine(result);
```

In the example above, `ContractRpc` uses the strategy pattern to decide which class (`ContractRpcReader` for read operations and `ContractRpcWriter` for write operations) to use to execute the action.
This decision is based on the `ActionType` property of the `RpcRequest` object.

## How to Use

Here's a simple example of how to **read** data from a smart contract using this library:

```csharp
// Create a read request
var readRequest = new RpcRequest(
    rpcUrl: "https://mainnet.infura.io/v3/YOUR_INFURA_PROJECT_ID",
    to: "0xYourContractAddress",
    data: "0xYourData"
);

// Initialize the ContractRpc object
var contractRpc = new ContractRpc();

// Execute the action
var result = contractRpc.ExecuteAction(readRequest);

// Handle the result
Console.WriteLine(result);
```

And here's how to **write** data to a smart contract:

```csharp
// Initialize your account provider
var myAccountProvider = new MyAccountProvider();

// Create a write request
var writeRequest = new RpcRequest(
    rpcUrl: "https://mainnet.infura.io/v3/YOUR_INFURA_PROJECT_ID",
    to: "0xYourContractAddress",
    writeRequest: new WriteRpcRequest(
        chainId: 1,
        value: new HexBigInteger(10000000000000000),
        gasSettings: new GasSettings(30000, 6),
        accountProvider: myAccountProvider
    ),
    data: "0xYourData"
);

// Initialize the ContractRpc object
var contractRpc = new ContractRpc();

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
