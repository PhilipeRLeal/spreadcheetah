using Xunit;

namespace SpreadCheetah.Test.Tests;

public static class XmlUtilityTests
{
    [Theory]
    [InlineData("", "")]
    [InlineData("Just a regular string without any special characters", "Just a regular string without any special characters")]
    [InlineData("&", "&amp;")]
    [InlineData("&<>'\"", "&amp;&lt;&gt;&apos;&quot;")]
    [InlineData("&OnlyFirstCharacter", "&amp;OnlyFirstCharacter")]
    [InlineData("&This<string>has'special\"characters in between", "&amp;This&lt;string&gt;has&apos;special&quot;characters in between")]
    [InlineData("This<string>has'special\"characters in between&", "This&lt;string&gt;has&apos;special&quot;characters in between&amp;")]
    [InlineData("\tHandling\r\nAllowed\nControl\tCharacters\n", "\tHandling\r\nAllowed\nControl\tCharacters\n")]
    [InlineData("\u0001Handling\u0002Invalid\u0003Control\u0004Characters\u0005", "HandlingInvalidControlCharacters")]
    [InlineData("\u0006", "")]
    [InlineData("\u0007\u0008", "")]
    public static void XmlUtility_XmlEncode_Success(string value, string expected)
    {
        // Act
        var result = XmlUtility.XmlEncode(value);

        // Assert
        Assert.Equal(expected, result);
    }
}
