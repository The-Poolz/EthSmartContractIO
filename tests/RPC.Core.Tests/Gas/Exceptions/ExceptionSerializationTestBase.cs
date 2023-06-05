using System.Runtime.Serialization;

namespace RPC.Core.Gas.Exceptions.Tests;

public class ExceptionSerializationTestBase
{
    protected static SerializationInfo GetSerializationInfo<TException>(string message)
    {
        var info = new SerializationInfo(typeof(TException), new FormatterConverter());
        info.AddValue("Message", message);
        info.AddValue("InnerException", null, typeof(Exception));
        info.AddValue("HelpURL", null, typeof(string));
        info.AddValue("StackTraceString", null, typeof(string));
        info.AddValue("RemoteStackTraceString", null, typeof(string));
        info.AddValue("HResult", int.MinValue, typeof(int));
        info.AddValue("Source", null, typeof(string));
        info.AddValue("ClassName", "Exception");
        info.AddValue("RemoteStackIndex", 0, typeof(int));
        info.AddValue("ExceptionMethod", null, typeof(string));

        return info;
    }
}
