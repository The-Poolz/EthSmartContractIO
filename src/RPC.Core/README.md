# RPC.Core

The RPC.Core project consists of a number of classes that facilitate the interaction with Ethereum based blockchains.
It allows developers to read from and write to the blockchain using the Ethereum RPC JSON API.
This library was designed to be easy to use and efficient, while also following the principles of clean code and best practices

## Navigation

- [ContractIO]()
  - [ContractRpcReader]()
  - [ContractRpcWriter]()
- [Gas]()
  - [GasEstimator]()
  - [GasPricer]()
  - [GasBase]()
- [Managers]()
  - [AccountManager]()
- [Models]()
  - [RpcRequest]()
- [Providers]()
  - [WalletProvider]()
- [Transaction]()
  - [TransactionSender]()
  - [TransactionSigner]()
  - [TransactionBase]()
- [Validation]()
  - [RpcRequestValidator]()

## ContractIO

### ContractRpcReader
The `ContractRpcReader` class is responsible for reading data from the Ethereum network.
It takes an `RpcRequest` as input and returns the response from the network as a `JToken`.

### ContractRpcWriter
The `ContractRpcWriter` class is responsible for writing data to the Ethereum network.
It uses the provided web3 connection and performs transaction signing and sending.

## Gas

### GasEstimator
The `GasEstimator` class is used to estimate the gas required for a transaction.
It also adds a buffer to the estimated gas to prevent transaction failure due to out-of-gas errors.

### GasPricer
The `GasPricer` class is used to retrieve the current gas price in Wei from the Ethereum network.

### GasBase
`GasPricer` is an abstract class that provides the common properties used by the `GasEstimator` and `GasPricer` classes.

## Managers

### AccountManager
The `AccountManager` class is used to manage Ethereum accounts.
It uses the `SecretManager` and `WalletProvider` to get the wallet and return an account based on the given id and chainId.

## Models

### RpcRequest
The `RpcRequest` class models a JSON-RPC request to the Ethereum network.
It includes properties for the JSON-RPC version, the method to be invoked, the parameters to the method, and an ID for the request.

## Providers

### WalletProvider
The `WalletProvider` class is used to manage and retrieve secrets from an external source.
It retrieves a `Wallet` based on the given `SecretManager`.

## Transaction

### TransactionSender
The `TransactionSender` class is responsible for sending a signed transaction to the Ethereum network.

### TransactionSigner
The `TransactionSigner` class is used to sign a transaction before it is sent to the Ethereum network.

### TransactionBase
`TransactionBase` is an abstract class that provides the common properties used by the `TransactionSender` and `TransactionSigner` classes.

## Validation

### RpcRequestValidator
The `RpcRequestValidator` class is used to validate the `RpcRequest` object before it is sent to the Ethereum network.
It checks for null values and correct formatting of the `to` and `data` parameters in the request.
