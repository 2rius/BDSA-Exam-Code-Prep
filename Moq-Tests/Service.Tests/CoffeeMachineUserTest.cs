namespace Service.Tests;

// https://docs.educationsmediagroup.com/unit-testing-csharp/moq/quick-glance-at-moq

public class CoffeeMachineUserTest
{
    [Fact]
    public void User_CanDrinkEspresso_Should_Return_True()
    {
        // Arrange

        // This will create a 'mock' object that inherits from the interface ICoffeeMachine
        var mockCoffeeMachine = new Mock<ICoffeeMachine>();

        // The mock object is then configured with .Setup, to return true to
        // the CanBrew() method with "Espresso" as parameter
        mockCoffeeMachine.Setup(c => c.CanBrew("Espresso")).Returns(true);

        // .Object will return the instantiated object
        var user = new CoffeeMachineUser(mockCoffeeMachine.Object);

        // Act
        var result = user.CanDrinkEspresso();

        // Assert
        result.Should().BeTrue();

        // .Verify will check that the method is called

        // Times.Once() checks if it is called exactly once,
        // otherwise a Times.Exactly(int calls) can be used, aswell as
        // .AtLeast(int calls), .AtMost(int calls), .Never(), .Between() etc.
        mockCoffeeMachine.Verify(c => c.CanBrew("Espresso"), Times.Once());
    }

    [Fact]
    public void User_BrewCoffee_Should_Call_CanBrew()
    {
        // Arrange
        var mockCoffeeMachine = new Mock<ICoffeeMachine>();

        // By using It.IsAny<string>() we can configure the mock object to return false for any string

        mockCoffeeMachine.Setup(c => c.CanBrew(It.IsAny<string>())).Returns(false);
        mockCoffeeMachine.Setup(c => c.CanBrew("Espresso")).Returns(true);
        mockCoffeeMachine.Setup(c => c.CanBrew("Americano")).Returns(true);

        var user = new CoffeeMachineUser(mockCoffeeMachine.Object);

        // Act
        user.BrewCoffee("Espresso");
        user.BrewCoffee("Americano");
        user.BrewCoffee("Americano");

        var action = new Action(() => user.BrewCoffee("Latte"));

        // Assert
        mockCoffeeMachine.Verify(c => c.CanBrew("Espresso"), Times.Once());
        mockCoffeeMachine.Verify(c => c.CanBrew(It.IsAny<string>()), Times.Exactly(3));

        action.Should().Throw<Exception>();
    }

    // Other (probably whatever) mock features:
    // mock.SetupSequence(methodcall).Returns(val1).Returns(val2)... - to return a sequence of values
    // mock.Setup(act).ReturnsAsync(val) - to return a value asynchronously
    // mock.Setup(act).Throws<Exception>() - to throw an exception
    // It.isRegex("regex") - to match a regex
    // It.isInRange(from, to) - to match a range
    // It.isIn(val1, val2, ...) - to match a list of values
    // It.isNot(val) - to match anything but val
    // etc. See link at top of file for more
}