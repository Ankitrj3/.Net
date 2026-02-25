using BussinessLogic;

public class Program
{
    public static void Main(string[] args)
    {
        BussinessLogicClass businessLogic = new();
        var res = businessLogic.ReverseStringBussinessLogic();
        Console.WriteLine(res);
    }
}
