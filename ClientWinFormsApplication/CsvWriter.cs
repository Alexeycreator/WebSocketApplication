using ClientWinFormsApplication.CentralBank;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClientWinFormsApplication
{
  internal sealed class CsvWriter
  {
    public void Write(string CSVFilePath, List<BankModel> rates)
    {
      StringBuilder csvBuilder = new StringBuilder();
      csvBuilder.AppendLine("DigitalCode;LetterCode;Units;Currency;Rate");
      foreach (var rate in rates)
      {
        csvBuilder.AppendLine($"{rate.DigitalCode};{rate.LetterCode};{rate.Units};{rate.Currency};{rate.Rate}");
      }
      File.WriteAllText(CSVFilePath, csvBuilder.ToString());
    }
  }
}
