using System.Text;
using Microsoft.VisualStudio.TestPlatform.CoreUtilities.Extensions;
using Robotc.Lib;

namespace Robotc.Test;

public class IntegrationTests
{
    [Fact]
    void EndToEndTest()
    {
        var commands = new StringBuilder()
            .AppendLine("PLACE 3,3,SOUTH")
            .AppendLine("MOVE")
            .AppendLine("REPORT")
            .AppendLine("LEFT")
            .AppendLine("MOVE")
            .AppendLine("REPORT")
            .AppendLine("RIGHT")
            .AppendLine("MOVE")
            .AppendLine("REPORT")
            .AppendLine("EXIT");

        var expected = new StringBuilder()
            .AppendLine("3,2,SOUTH")
            .AppendLine("4,2,EAST")
            .AppendLine("4,1,SOUTH");  


        var reader = new StringReader(commands.ToString());
        var writer = new StringWriter();

        using var app = new App([], reader, writer);
        
        app.Run();

        Assert.Equal(expected.ToString(), writer.ToString());

    }

}