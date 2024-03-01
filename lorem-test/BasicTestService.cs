using Shouldly;

namespace lorem_test;

public class BasicTestService
{
    // Unit test & Integrations Testing
    // Unit Testing -> It does not use database  -> Employee -> Create() -> Insert into database -> It does not use database
    // Integration Testing -> It use database -> Employee -> Create() -> Insert into database -> It use database

    // Arrange -> Preparing a data.
    // Act -> It is used to call the method
    // Assert -> It is used to check the result of the method

    // Fact -> it does not take any parameter
    // Theory  -> it takes parameter


    // Xunit -> It is a testing framework 
    // NSubstitute -> It is a mocking library -> Moq
    // Shouldly -> It is a assertion library

    // Mocking / Faking -> every thing is fake. It does not use database
    // Basically it creates a fake object.


    [Fact]
    public void test_sum_of_two_numbers()
    {
        var a = 10;
        var b = 20;

        var result = a + b;
        result.ShouldBe(30);
    }

    [Theory]
    [InlineData(10, 20, 30)]
    [InlineData(10, 20, 30)]
    [InlineData(10, 10, 20)]
    public void test_sum_of_two(decimal a, decimal b, decimal expected)
    {
        var result = a + b;
        result.ShouldBe(expected);
    }
}