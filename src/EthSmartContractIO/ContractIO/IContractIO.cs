namespace EthSmartContractIO.ContractIO;

/// <summary>
/// Interface for interacting with Ethereum smart contracts.
/// </summary>
public interface IContractIO
{
    /// <summary>
    /// Executes an action on the Ethereum network.
    /// </summary>
    /// <returns>The result of the action.</returns>
    public string RunContractAction();
}
