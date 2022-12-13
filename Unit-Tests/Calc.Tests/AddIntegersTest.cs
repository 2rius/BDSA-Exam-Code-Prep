namespace Calc.Tests;

// https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test
// https://improveandrepeat.com/2018/03/xunit-net-cheat-sheet-for-nunit-users/

// To run tests sequentially, instead of asynchronously, use [Collection("testCollectionName")]
[Collection("Calc collection")]
public class AddIntegersTest : IDisposable
{
    private readonly Program _program;

    // Constructor will run before all unit tests
    public AddIntegersTest() {
        _program = new Program();
    }


    // For simple tests, use [Fact]
    [Fact]
    public void AddIntegers_1_2_Returns_3()
    {
        // Arrange
        var x = 1;
        var y = 2;

        // Act
        var result = _program.AddIntegers(x, y);

        // Assert
        result.Should().Be(3);
    }


    // To ignore test, use [Fact(Skip = "comment")]
    [Fact(Skip = "Skipped test")]
    public void AddIntegers_1_2_Returns_4()
    {
        // Arrange
        var x = 1;
        var y = 2;

        // Act
        var result = _program.AddIntegers(x, y);

        // Assert
        result.Should().Be(4);
    }


    // To test multiple testcases, use [Theory], followed by [InlineData(args)]
    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(2, 3, 5)]
    [InlineData(3, 4, 7)]
    public void Add_Integers_TestCases(int x, int y, int expectedResult)
    {
        var result = _program.AddIntegers(x, y);

        result.Should().Be(expectedResult);
    }


    // Different fluent assertions
    public void Diff_Fluent_Assertions()
    {
        var test1 = _program.AddIntegers(1, 2);
        var test2 = _program.AddIntegers(2, 3);

        var testresultInList = new List<int>() {
            test1,
            test2
        };
        var emptyList = new List<int>();

        string? Null = null;

        // To test single value
        test1.Should().Be(3);
        test2.Should().NotBe(3);

        // To test for null
        "Not null".Should().NotBeNull();
        Null.Should().BeNull();

        // To test collections
        emptyList.Should().BeEmpty();
        testresultInList.Should().NotBeEmpty();

        testresultInList.Should().HaveCount(2);

        testresultInList.Should().Contain(3);
        testresultInList.Should().BeEquivalentTo<int>(new List<int>() {
            3,
            5
        });

        // Check boolean
        false.Should().BeFalse();
        true.Should().BeTrue();
    }


    // Dispose is for cleaning up code from constructor (e.g. close db connection)
    public void Dispose()
    {
        // _program.Dispose();
    }
}