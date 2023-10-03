using ABPTestTask.Models;

namespace ABPTestTask.Helpers;

public static class Constants
{
    public static List<PriceOption> PriceOptions = new List<PriceOption>
    {
        new() { Price = 10, Probabilty = 0.75 },
        new() { Price = 20, Probabilty = 0.10 },
        new() { Price = 50, Probabilty = 0.05 },
        new() { Price = 5, Probabilty = 0.10 }
    };

    public static string[] Colors = new[] { "#FF0000", "#00FF00", "#00FF00" };
}