using System.Text.Json;
using System.Text.RegularExpressions;
using Xunit;

namespace CustomJsonPropertyNamePolicy.NamingConventions;

internal class UnderscoreNamingPolicy : JsonNamingPolicy
{
    private static readonly Regex CapitalLetterGroup = new Regex("([A-Z])");

    public override string ConvertName(string name)
        => CapitalLetterGroup.Replace(name, "_$1", -1, 1).ToLower();
}

public class UnderscoreNamingPolicyTests
{
    [Theory]
    [InlineData("FirstName", "first_name")]
    [InlineData("UsersFullName", "users_full_name")]
    [InlineData("Name1", "name1")]
    [InlineData("Name1AndName2", "name1_and_name2")]
    public void ConvertName_ValidString_ConvertsCorrectly(string input, string expectedOutput)
    {
        // arrange
        var policy = new UnderscoreNamingPolicy();
        
        // act
        var actualOutput = policy.ConvertName(input);
        
        // assert
        Assert.Equal(expectedOutput, actualOutput);
    }
}