using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ServerConsoleApplication
{
  internal class CalculationData
  {
    private CsvWriter csvWriter = new CsvWriter();
    private Logger loggerCalculation = LogManager.GetCurrentClassLogger();
    private List<ResponseData> responseDatas = new List<ResponseData>();
    private static string dateGetRate = DateTime.Now.ToShortDateString();
    private readonly string csvFilePathRate = Path.Combine(Directory.GetCurrentDirectory(), "CentralBank", $"{dateGetRate}");

    public CalculationData()
    {
      if (!Directory.Exists(csvFilePathRate))
      {
        Directory.CreateDirectory(csvFilePathRate);
      }
      string dateTimeNowRate = DateTime.Now.ToString("HH-mm");
      string fileNameRate = $"Rate_{dateTimeNowRate}.csv";
      csvFilePathRate = Path.Combine(csvFilePathRate, fileNameRate);
      if (!File.Exists(csvFilePathRate))
      {
        File.Create(csvFilePathRate).Close();
      }
    }

    public List<BankModel> ProcessData(string _data)
    {
      List<BankModel> coming = JsonConvert.DeserializeObject<List<BankModel>>(_data);
      loggerCalculation.Info("Запись данных приходящего файла.");
      csvWriter.Write(csvFilePathRate, coming);
      loggerCalculation.Info("Запись данных успешно выполнена.");
      loggerCalculation.Info($"Преобразование полученных данных для отправки.");
      foreach (var bankItem in coming)
      {
        var response = new ResponseData
        {
          DigitalCode = bankItem.DigitalCode,
          LetterCode = bankItem.LetterCode,
          Units = bankItem.Units,
          Currency = bankItem.Currency,
          Rate = Convert.ToDouble(bankItem.Rate),
        };

        responseDatas.Add(response);
      }
      var digitalCodeGroups = responseDatas.GroupBy(item => item.DigitalCode);
      List<ResponseData> resultForFile = new List<ResponseData>();
      foreach (var group in digitalCodeGroups)
      {
        if (group.Count() > 1)
        {
          double averageRate = group.Average(item => item.Rate);
          var firstItem = group.First();
          firstItem.Rate = averageRate;
          resultForFile.Add(firstItem);
        }
        else
        {
          resultForFile.Add(group.First());
        }
      }

      List<BankModel> dataResponse = new List<BankModel>();
      foreach (var item in resultForFile)
      {
        dataResponse.Add(new BankModel
        {
          DigitalCode = item.DigitalCode,
          LetterCode = item.LetterCode,
          Units = item.Units,
          Currency = item.Currency,
          Rate = item.Rate.ToString()
        });
      }

      loggerCalculation.Info($"Данные успешно преобразованы. Обработано {dataResponse.Count} записей");

      return dataResponse;
    }
  }
}
