# EthSmartContractIO

[![SonarCloud](https://sonarcloud.io/images/project_badges/sonarcloud-white.svg)](https://sonarcloud.io/summary/new_code?id=The-Poolz_RPCToolkit)

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=The-Poolz_RPCToolkit&metric=alert_status&token=2eea303b94df298cfeda35ef2fb09e4d8583bdea)](https://sonarcloud.io/summary/new_code?id=The-Poolz_RPCToolkit)
[![SonarScanner for .NET 6](https://github.com/The-Poolz/RPCToolkit/actions/workflows/dotnet.yml/badge.svg)](https://github.com/The-Poolz/RPCToolkit/actions/workflows/dotnet.yml)
[![CodeFactor](https://www.codefactor.io/repository/github/the-poolz/ethsmartcontractio/badge)](https://www.codefactor.io/repository/github/the-poolz/ethsmartcontractio)

`EthSmartContractIO` is a .NET library aimed at simplifying the interaction with Ethereum smart contracts. It allows developers to execute actions on the Ethereum network by wrapping the complexity of Ethereum RPC calls into a more manageable, high-level API.

## Navigation of [EthSmartContractIO](#ethsmartcontractio)

- [Getting Started](#getting-started)
- [Core Components](#core-components)
    - [RpcRequest](#rpcrequest)
        - [Read Request](#read-request)
        - [Write Request](#write-request)
    - [GasSettings](#gassettings)
    - [IAccountProvider](#iaccountprovider)
        - [Example](#example)
    - [ContractIO](#contractio)
- [How to Use](#how-to-use)
- [Custom Implementations](#custom-implementations-and-dependency-injection)
    - [Interfaces](#interfaces)
        - [IGasPricer](#igaspricer)
        - [ITransactionSigner](#itransactionsigner)
        - [ITransactionSender](#itransactionsender)
    - [IServiceProvider](#iserviceprovider)
- [Testing SmartContractIO](#testing-smartcontractio)
    - [Overriding ExecuteAction Method](#overriding-executeaction-method)
    - [Creating an IWeb3 Moq Object](#creating-an-iweb3-moq-object)

## Navigation of [EthSmartContractIO.AccountProvider](#ethsmartcontractioaccountprovider)

- [Getting Started](#getting-started)
- [Account Providers](#account-providers)
    - [MnemonicAccountProvider](#mnemonicaccountprovider)
    - [PrivateKeyAccountProvider](#privatekeyaccountprovider)

## Getting Started

To use `EthSmartContractIO`, you will need to add it as a dependency to your project.
You can do this by adding it as a NuGet package:

.NET CLI
```
dotnet add package EthSmartContractIO
```

Package Manager
```
Install-Package EthSmartContractIO
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
A Write request can call contract write methods, and send money to other account
This type of request creates a transaction that needs to be mined, which requires gas settings and potentially a value.

**Call contract example:**

```csharp
var writeRequest = new RpcRequest(
    rpcUrl: "https://mainnet.infura.io/v3/YOUR_INFURA_PROJECT_ID",
    to: "0xYourContractAddress",
    writeRequest: new WriteRpcRequest(
        chainId: 1,
        value: new HexBigInteger(1000000000000000),
        gasSettings: new GasSettings(maxGasLimit: 31000, maxGweiGasPrice: 20),
        accountProvider: new YourIAccountProvider() // The class that implements the interface 'IAccountProvider'
    ),
    data: "0xYourData" // Optional parameter, set if need to call contract method
);
```

**Send money example:**

```csharp
var writeRequest = new RpcRequest(
    rpcUrl: "https://mainnet.infura.io/v3/YOUR_INFURA_PROJECT_ID",
    to: "0xOtherAccountAddress",
    writeRequest: new WriteRpcRequest(
        chainId: 1,
        value: new HexBigInteger("0xDE0B6B3A7640000"),
        gasSettings: new GasSettings(maxGasLimit: 31000, maxGweiGasPrice: 20),
        accountProvider: new YourIAccountProvider() // The class that implements the interface 'IAccountProvider'
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
using Nethereum.Web3.Accounts;
using EthSmartContractIO.Providers;

public class MyAccountProvider : IAccountProvider 
{
    public Account Account => new Account("0x4e3c79ee2f53da4e456cb13887f4a7d59488677e9e48b6fb6701832df828f7e9");
}
```

In this example, we're providing an Ethereum account with a specific private key.
Please note that hardcoding private keys is not a good practice, this is just an illustrative example.
Always store private keys in a secure manner.

### ContractIO

`ContractIO` is the main class that interfaces with your Ethereum node.
You can use it to execute actions (read or write) on the Ethereum network.

```csharp
// Initialize the ContractIO object
var contractIO = new ContractIO();

// Execute the action
var result = contractRpc.ExecuteAction(myRpcRequest);

// Handle the result
Console.WriteLine(result);
```

In the example above, `ContractIO` uses the strategy pattern to decide which class (`ContractReader` for read operations and `ContractWriter` for write operations) to use to execute the action.

## How to Use

Here's a simple example of how to **read** data from a smart contract using this library:

```csharp
// Create a read request
var readRequest = new RpcRequest(
    rpcUrl: "https://mainnet.infura.io/v3/YOUR_INFURA_PROJECT_ID",
    to: "0xYourContractAddress",
    data: "0xYourData"
);

// Initialize the ContractIO object
var contractIO = new ContractIO();

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

// Initialize the ContractIO object
var contractIO = new ContractIO();

// Execute the action
var result = contractRpc.ExecuteAction(writeRequest);

// Handle the result
Console.WriteLine(result);
```

## Custom Implementations and Dependency Injection

With `EthSmartContractIO`, you're not limited to the default implementation of certain interfaces like `IGasPricer`, `ITransactionSigner`, and `ITransactionSender`.
You can provide your own custom implementations and inject them into the `ContractIO` class using the `IServiceProvider` property.

### Interfaces

Here are the interfaces you can replace:

#### IGasPricer

This interface is responsible for providing the current gas price.
If you have a custom strategy for gas price, you can implement this interface and provide your own method for getting the current gas price.

```csharp
public interface IGasPricer
{
    HexBigInteger GetCurrentWeiGasPrice();
}
```

#### ITransactionSigner

This interface is responsible for signing transactions.
If you have a custom transaction signing method, you can implement this interface and provide your own transaction signing logic.

```csharp
public interface ITransactionSigner
{
    string SignTransaction(TransactionInput transaction);
}
```

#### ITransactionSender
This interface is responsible for sending transactions to the Ethereum network.
If you have a custom method for sending transactions, you can implement this interface and provide your own transaction sending logic.

```csharp
public interface ITransactionSender
{
    string SendTransaction(string signedTransaction);
}
```

### IServiceProvider

To use your custom implementations, you need to provide an `IServiceProvider` instance to the `ContractRpc` class that contains your implementations.
You can use the `ServiceProviderBuilder` class from the `EthSmartContractIO.Builders` namespace to create an `IServiceProvider` object.

Here is a sample usage of `ServiceProviderBuilder`:

```csharp
// Add your custom implementations
var serviceProvider = new ServiceProviderBuilder()
    .AddGasPricer(new MyCustomGasPricer())
    .AddTransactionSigner(new MyCustomTransactionSigner())
    .AddTransactionSender(new MyCustomTransactionSender())
    .Build();

// Create a ContractRpc instance and inject your custom implementations
var contractRpc = new ContractRpc()
{
    ServiceProvider = serviceProvider
};
```

In the example above, `MyCustomGasPricer`, `MyCustomTransactionSigner`, and `MyCustomTransactionSender` are your custom implementations of the `IGasPricer`, `ITransactionSigner`, and `ITransactionSender` interfaces, respectively.

By using this approach, you can easily customize the behavior of the library to suit your specific needs.

## Testing EthSmartContractIO

Testing is a critical part of software development and ensures the reliability and accuracy of your code. EthSmartContractIO's architecture allows it to be tested in a couple of ways:

### Overriding `ExecuteAction` Method

The `ExecuteAction` method in the `ContractIO` class is marked as `virtual`.
This allows the method to be overridden in a subclass, which is useful for testing scenarios.
You can use mocking libraries such as `Moq` to mock the method's behavior.

Here's an example using `Moq`:

```csharp
var contractIOMock = new Mock<ContractIO> { CallBase = true };
contractIOMock
    .Setup(x => x.ExecuteAction(It.IsAny<RpcRequest>()))
    .Returns("YourMockedResult");

// Now when you call ExecuteAction, it will return "YourMockedResult"
var result = contractIOMock.Object.ExecuteAction(someRpcRequest);

Assert.Equal("YourMockedResult", result);
```

In this example, regardless of what `RpcRequest` you pass to `ExecuteAction`, it will always return the string `"YourMockedResult"`.

### Creating an IWeb3 Moq Object

Alternatively, you can mock the `IWeb3` object and pass it to `ContractIO` using the `ServiceProvider`.
The `ServiceProviderBuilder` class provides an `AddWeb3` method for this purpose.

Here's an example of how to mock `IWeb3` using `Moq` and inject it using `ServiceProviderBuilder`:

```csharp
var web3Mock = new Mock<IWeb3>();
// Setup your web3Mock...

var serviceProvider = new ServiceProviderBuilder()
    .AddWeb3(web3Mock.Object)
    .Build();

var contractIO = new ContractIO(serviceProvider)
```

In this example, `ContractIO` will use your mocked `IWeb3` object, allowing you to control its behavior for testing.

By leveraging these strategies, you can create comprehensive unit tests for your code that interacts with the EthSmartContractIO library.
This ensures the correct behavior of your Ethereum interactions.

# EthSmartContractIO.AccountProvider

The `EthSmartContractIO.AccountProvider` is a NuGet package that provides various account providers for Ethereum based smart contract I/O operations.
The package includes two account providers: `MnemonicAccountProvider` and `PrivateKeyAccountProvider`.

## Getting Started

To use `EthSmartContractIO.AccountProvider`, you will need to add it as a dependency to your project.
You can do this by adding it as a NuGet package:

.NET CLI
```
dotnet add package EthSmartContractIO.AccountProvider
```

Package Manager
```
Install-Package EthSmartContractIO.AccountProvider
```

## Account Providers

The package provides different classes for account management.
These classes implement the `IAccountProvider` interface, providing flexibility and support for different Ethereum account types.

### MnemonicAccountProvider

`MnemonicAccountProvider` is a class that generates an Ethereum account using a mnemonic (a list of words) that represents a wallet's private key.
This class provides two constructors:

- The first constructor accepts the mnemonic words, account ID, chain ID and optionally, a password for the seed.
It generates an account corresponding to the account ID from the given mnemonic.

- The second constructor uses an instance of `ISecretsProvider` to obtain the mnemonic words.
This can be useful when you have a secure way to store and retrieve the mnemonic words and want to keep them separate from your main application code.

```csharp
using Nethereum.HdWallet;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts;
using EthSmartContractIO.Providers;

namespace EthSmartContractIO.AccountProvider;

public class MnemonicAccountProvider : IAccountProvider
{
    public Account Account { get; private set; }

    public MnemonicAccountProvider(string mnemonicWords, uint accountId, uint chainId, string seedPassword = "")
    {
        var wallet = new Wallet(words: mnemonicWords, seedPassword: seedPassword);
        Account = wallet.GetAccount((int)accountId, new HexBigInteger(chainId));
    }

    public MnemonicAccountProvider(ISecretsProvider secretsProvider, uint accountId, uint chainId, string seedPassword = "")
        : this(secretsProvider.Secret, accountId, chainId, seedPassword)
    { }
}
```

### PrivateKeyAccountProvider

The `PrivateKeyAccountProvider` is a class that generates an Ethereum account using a private key.
This class provides two constructors:

- The first constructor accepts a private key and a chain ID.
It generates an account from the given private key for the specified chain.

- The second constructor uses an instance of `ISecretsProvider` to obtain the private key.
This can be useful when you have a secure way to store and retrieve the private key and want to keep it separate from your main application code.

Here is the code for PrivateKeyAccountProvider:

```csharp
using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts;
using EthSmartContractIO.Providers;

namespace EthSmartContractIO.AccountProvider;

public class PrivateKeyAccountProvider : IAccountProvider
{
    public Account Account { get; private set; }

    public PrivateKeyAccountProvider(string privateKey, uint chainId)
    {
        Account = new Account(privateKey, new HexBigInteger(chainId));
    }

    public PrivateKeyAccountProvider(ISecretsProvider secretsProvider, uint chainId)
        : this(secretsProvider.Secret, chainId)
    { }
}
```

## Contribute

We welcome contributions from the community. Please submit pull requests for bug fixes, improvements and new features.

Happy coding!

## More Information
[RPC.Core Documentation](https://github.com/The-Poolz/RPCToolkit/blob/master/src/RPC.Core/README.md)
