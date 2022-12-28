namespace Linq;

// Ch. 8-9 in CSN

public record Person(string Name, int Age, string City);

public class Program
{
    public static readonly IEnumerable<Person> people = new List<Person>
    {
        new Person("John", 42, "London"),
        new Person("Jane", 39, "Paris"),
        new Person("Mary", 7, "London"),
        new Person("Bill", 42, "New York"),
        new Person("Alex", 16, "Paris"),
    };

    public static void Main(string[] args)
    {
        var queryresult = 
            // QuerySyntax.GetPeopleWhoseNameStartsWithA(people);
            // Extensions.GetPeopleWhoseNameStartsWithA(people);
            // QuerySyntax.GetPeopleNamesOrderedByCity(people);
            // Extensions.GetPeopleNamesOrderedByCity(people);
            // QuerySyntax.GetPeopleNameAndAgeInCity(people, "Paris");
            // Extensions.GetPeopleNamesInCity(people, "Paris");
            // QuerySyntax.GetOldestAgeInCity(people, "Paris");
            // Extensions.GetOldestAgeInCity(people, "Paris");
            // QuerySyntax.GetCityGroupedPersonNames(people);
            Extensions.GetCityGroupedPersonNames(people);
        
        people.PrintAll();
    }
}

public static class QuerySyntax
{
    public static IEnumerable<Person> GetPeopleWhoseNameStartsWithA(IEnumerable<Person> people)
    {
        return from person in people
               where person.Name.StartsWith("A")
               select person;
    }


    public static IEnumerable<string> GetPeopleNamesOrderedByCity(IEnumerable<Person> people)
    {
        return from person in people
               orderby person.City
               select person.Name;
    }


    public static IEnumerable<System.Object> GetPeopleNameAndAgeInCity(IEnumerable<Person> people, string city)
    {
        return from person in people
               where person.City == city
               select new { Name = person.Name, Age = person.Age };
    }


    public static int GetOldestAgeInCity(IEnumerable<Person> people, string city)
    {
        return (from person in people
                where person.City == city
                select person.Age).Max();
    }


    // Grouped by city in reverse order by city name and then wizard name
    public static IEnumerable<string> GetCityGroupedPersonNames(IEnumerable<Person> people)
    {
        return from cperson in
               (from person in people
                orderby person.City descending, person.Name
                group person by person.City into g
                select g)
               from person in cperson
               select person.Name;
    }


    // Summary:
    // - always from clause first, select clause last
    // - from clause can be followed by where and orderby clause
    // - aggregations are not allowed in the query syntax (follow up with an extension method)
    // - when getting multiple values from a query, use an anonymous type (new { ... })
    // - when using the query syntax, the compiler will translate the query into a method call
}

public static class Extensions
{
    public static IEnumerable<Person> GetPeopleWhoseNameStartsWithA(this IEnumerable<Person> people)
    {
        return people.Where(person => person.Name.StartsWith("A"));
    }


    public static IEnumerable<string> GetPeopleNamesOrderedByCity(IEnumerable<Person> people)
    {
        return people.OrderBy(person => person.City)
            .Select(person => person.Name);
    }


    public static IEnumerable<System.Object> GetPeopleNamesInCity(this IEnumerable<Person> people, string city)
    {
        return people.Where(person => person.City == city)
            .Select(person => new { Name = person.Name, Age = person.Age });
    }


    public static int GetOldestAgeInCity(this IEnumerable<Person> people, string city)
    {
        return people.Where(person => person.City == city)
            .Select(person => person.Age)
            .Max();
    }


    // Grouped by city in reverse order by city name and then wizard name
    public static IEnumerable<string> GetCityGroupedPersonNames(IEnumerable<Person> people)
    {
        return people.OrderByDescending(person => person.City)
            .ThenBy(person => person.Name)
            .GroupBy(person => person.City)
            .SelectMany(g => g)
            .Select(person => person.Name);
    }

    // Summary:
    // - no from clause
    // - always start with a collection (collection.Method())
    // - always end with a select clause
    // - same method names as in the query syntax, except SelectMany instead of Group
}