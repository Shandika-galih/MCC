public class Program
{
    public static void Main(String[] args )
    {
        Dictionary<int, string> num = new Dictionary<int, string>();
        num.Add(1, "one");
        num.Add(2, "two");
        num.Add(3, "tre");
        num.Add(4, "four");
        num.Add(5, "five");

        foreach (var item in num)
        {
            Console.WriteLine( item.ToString() );
        }
    }
}