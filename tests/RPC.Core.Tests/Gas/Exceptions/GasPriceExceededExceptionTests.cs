using System.Text.Json;
using Xunit;

namespace RPC.Core.Gas.Exceptions.Tests;

public class GasPriceExceededExceptionTests
{
    [Fact]
    public void GasPriceExceededException_SerializationTest()
    {
        var originalException = new GasPriceExceededException();
        GasPriceExceededException deserializedException;

        var jsonString = JsonSerializer.Serialize(originalException);
        deserializedException = JsonSerializer.Deserialize<GasPriceExceededException>(jsonString)!;

        Assert.Equal(originalException.Message, deserializedException.Message);
    }
}
