namespace Demo;

public class Utils
{
    public static void WriteHeader(string message)
    {
        var padding = (Console.WindowWidth - message.Length) / 2;

        Console.WriteLine();
        Console.WriteLine(new string('*', Console.WindowWidth));
        Console.WriteLine(new string(' ', padding) + message);
        Console.WriteLine(new string('*', Console.WindowWidth));
        Console.WriteLine();
    }

    public static void Separator()
    {
        Console.WriteLine();
        Console.WriteLine(string.Empty.PadLeft(Console.WindowWidth / 2, '-'));
        Console.WriteLine();
    }
}
