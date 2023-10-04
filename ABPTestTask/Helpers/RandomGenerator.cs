using System;
using System.Collections.Generic;
using System.Text;
using ABPTestTask.Models;

namespace ABPTestTask.Helpers;

public class RandomGenerator
{
  public  static string GenerateRandomGeneticString(int length)
    {
        Random random = new Random();
        StringBuilder randString = new StringBuilder(length);
        string characters = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz1234567890";
        for (int i = 0; i < length; i++)
        {
            int randomIndex = random.Next(characters.Length);
            randString.Append(characters[randomIndex]);
        }
        return randString.ToString();
    }

  public static string GetRandomButtonColor(string[] colors)
  {
      Random random = new Random();
      int randomNumber = random.Next(colors.Length);
      return colors[randomNumber];
  }
  
  public static PriceOption  SelectPriceWithProbability(List<PriceOption> priceOptions)
  {
      
      Random random = new Random();
      double randomValue = random.NextDouble();
      double cumulativeProbability = 0.0;
      
      foreach (PriceOption priceOpt in priceOptions)
      {
          cumulativeProbability +=priceOpt.Probabilty;
          if (randomValue < cumulativeProbability)
          {
              return priceOpt;
          }
      }

      throw new("");
  }

}