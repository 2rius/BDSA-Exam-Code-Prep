namespace Linq;

public static class ExtensionsMethods
{
    public static void PrintAll<T>(this IEnumerable<T> list)
    {
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
    }
}