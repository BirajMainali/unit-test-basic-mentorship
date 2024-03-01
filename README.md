## Introduction
This project is aimed at providing a robust framework for unit testing in C# using xUnit as the testing framework, NSubstitute for mocking, and Shouldy for fluent assertions. 

## Types of Testing
### Unit Testing
Unit testing involves testing individual units or components of the software in isolation. In this project, we focus on writing unit tests for specific methods, functions, or classes to ensure that they behave as expected.

### Integration Testing
Integration testing involves testing the interaction between different components or modules of the software. While this project primarily focuses on unit testing, integration testing can also be facilitated using the same framework by extending test scenarios to include interactions between components.

## Libraries Used
### xUnit
xUnit is a free, open-source unit testing tool for the .NET Framework. It provides a simple and elegant way to write automated tests, making it easy to maintain and extend test suites.

### NSubstitute
NSubstitute is a friendly substitute for .NET mocking frameworks. It allows developers to create mock objects in their tests, making it easier to isolate the code under test and simulate various scenarios.

### Shouldy
Shouldy is a fluent assertion library for .NET that provides a more readable and expressive way to write assertions in tests. It enhances the readability of test code and makes it easier to understand the expected behavior.

## Basic Example
```csharp
using Xunit;
using NSubstitute;
using Shouldy.Core;

public class CalculatorTests
{
    [Fact]
    public void Add_WhenCalled_ReturnsSum()
    {
        // Arrange
        var calculator = new Calculator();
        int a = 5;
        int b = 10;

        // Act
        int result = calculator.Add(a, b);

        // Assert
        result.ShouldBe(15); // Shouldy assertion
    }

    [Fact]
    public void Divide_ByNonZero_ReturnsQuotient()
    {
        // Arrange
        var calculator = new Calculator();
        int dividend = 10;
        int divisor = 2;

        // Act
        int result = calculator.Divide(dividend, divisor);

        // Assert
        result.ShouldBe(5); // Shouldy assertion
    }
}
```

In the above example, we have a simple test class `CalculatorTests` that contains two test methods. The `Add_WhenCalled_ReturnsSum` method tests the `Add` method of the `Calculator` class, and the `Divide_ByNonZero_ReturnsQuotient` method tests the `Divide` method. Each test method follows the Arrange-Act-Assert pattern, where we set up the test scenario, execute the code under test, and then assert the expected outcome using Shouldy assertions.
