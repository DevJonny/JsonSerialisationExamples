using System.Text.RegularExpressions;
using Newtonsoft.Json.Serialization;
using Xunit;

namespace NewtonsoftJsonExamples.NamingConventions;

internal class UnderscoreNamingStrategy : NamingStrategy
{
    private static readonly Regex CapitalLetterGroup = new Regex("([A-Z])");
    
    protected override string ResolvePropertyName(string name)
        => CapitalLetterGroup.Replace(name, "_$1", -1, 1).ToLower();
}

public class UnderscoreNamingStrategyTests
{
    [Theory]
    [InlineData("FirstName", "first_name")]
    [InlineData("UsersFullName", "users_full_name")]
    [InlineData("Name1", "name1")]
    [InlineData("Name1AndName2", "name1_and_name2")]
    public void ConvertName_ValidString_ConvertsCorrectly(string input, string expectedOutput)
    {
        // arrange
        var policy = new UnderscoreNamingStrategy();
        
        // act
        var actualOutput = policy.GetPropertyName(input, false);
        
        // assert
        Assert.Equal(expectedOutput, actualOutput);
    }
}