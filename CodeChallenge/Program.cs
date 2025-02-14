using CodeChallenge.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var inputString = Console.ReadLine() ?? string.Empty;
        var calculatorService = new CalculatorService();

        var result = calculatorService.CalculcateAddForString(inputString);
        Console.WriteLine($"Result: {result}");          
    }
}