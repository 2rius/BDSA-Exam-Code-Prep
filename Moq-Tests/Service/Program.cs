namespace Service;

public class CoffeeMachineUser
{
    private readonly ICoffeeMachine _coffeeMachine;

    public CoffeeMachineUser(ICoffeeMachine coffeeMachine)
    {
        _coffeeMachine = coffeeMachine;
    }

    public bool CanDrinkEspresso()
    {
        return _coffeeMachine.CanBrew("Espresso");
    }

    public void BrewCoffee(string type)
    {
        if (!_coffeeMachine.CanBrew(type))
        {
            throw new Exception("Could not brew coffee");
        }
    }


    // To allow dotnet build
    public static void Main() {}
}

public interface ICoffeeMachine
{
    bool CanBrew(string type);
}