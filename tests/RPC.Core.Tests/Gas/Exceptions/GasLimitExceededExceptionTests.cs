using Xunit;
using System.Text.Json;

namespace RPC.Core.Gas.Exceptions.Tests;

public class GasLimitExceededExceptionTests
{
    [Fact]
    public void GasLimitExceededException_SerializationTest()
    {
        var originalException = new GasLimitExceededException();
        GasLimitExceededException deserializedException;

        var jsonString = JsonSerializer.Serialize(originalException);
        deserializedException = JsonSerializer.Deserialize<GasLimitExceededException>(jsonString)!;

        Assert.Equal(originalException.Message, deserializedException.Message);
    }
}
