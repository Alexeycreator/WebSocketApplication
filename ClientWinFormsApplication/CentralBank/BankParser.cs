using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace ClientWinFormsApplication.CentralBank
{
  internal sealed class BankParser
  {
    private Logger loggerBank = LogManager.GetCurrentClassLogger();
    private static string dateGetRate = DateTime.Now.ToShortDateString();
    private string urlCentralbank = $"https://www.cbr.ru/currency_base/daily/?UniDbQuery.Posted=True&UniDbQuery.To={dateGetRate}";
    private readonly HttpClient httpClient = new HttpClient();
    //private CsvWriter csvWriter = new CsvWriter();
    //private readonly string csvFilePath = Path.Combine(Directory.GetCurrentDirectory(), "CentralBank", $"{dateGetRate}");
    //private string typeExchange = "cb";
    private SettingsClient settingsClient = new SettingsClient();
    private const int countColumns = 5;

    public BankParser()
    {
      //if (!Directory.Exists(csvFilePath))
      //{
      //  Directory.CreateDirectory(csvFilePath);
      //}
      //string dateTimeNow = DateTime.Now.ToString("HH-mm");
      //string fileName = $"Rate_{dateTimeNow}.csv";
      //csvFilePath = Path.Combine(csvFilePath, fileName);
      //if (!File.Exists(csvFilePath))
      //{
      //  File.Create(csvFilePath).Close();
      //}
    }

    public List<BankModel> CentralBankParser()
    {
      return GetRate();
    }

    private List<BankModel> GetRate()
    {
      loggerBank.Info("Процесс получения курса валют запущен...");
      List<BankModel> bankModels = new List<BankModel>();
      try
      {
        loggerBank.Info($"Подключение к данным по адресу: {urlCentralbank}");
        var httpResponseMessage = httpClient.GetAsync(urlCentralbank).Result;
        if (httpResponseMessage.IsSuccessStatusCode)
        {
          loggerBank.Info($"Подключение прошло успешно: {httpResponseMessage.StatusCode}");
          var htmlResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;
          if (!string.IsNullOrEmpty(htmlResponse))
          {
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(htmlResponse);
            var container = document.GetElementbyId("content");
            if (container != null)
            {
              int cells = document.GetElementbyId("content").ChildNodes.FindFirst("tbody").ChildNodes.Where(c => c.Name == "tr").First().ChildNodes.Where(c => c.Name == "th").Count();
              if (cells == countColumns)
              {
                var tableBody = document.GetElementbyId("content").ChildNodes.FindFirst("tbody").ChildNodes.Where(t => t.Name == "tr").Skip(1).ToArray();
                loggerBank.Info("Извлечение данных");
                foreach (var tableRow in tableBody)
                {
                  var cellDigitalCode = tableRow.SelectSingleNode(".//td[1]").InnerText;
                  var cellLetterCode = tableRow.SelectSingleNode(".//td[2]").InnerText;
                  var cellUnits = tableRow.SelectSingleNode(".//td[3]").InnerText;
                  var cellCurrency = tableRow.SelectSingleNode(".//td[4]").InnerText;
                  var cellRate = tableRow.SelectSingleNode(".//td[5]").InnerText;

                  bankModels.Add(new BankModel
                  {
                    DigitalCode = cellDigitalCode,
                    LetterCode = cellLetterCode,
                    Units = cellUnits,
                    Currency = cellCurrency,
                    Rate = cellRate
                  });
                  if (bankModels != null)
                  {
                    loggerBank.Info($"Данные успешно получены. Количество {bankModels.Count}");
                    //loggerBank.Info("Запись в файл полученных данных");
                    //csvWriter.Write(csvFilePath, bankModels);
                  }
                }
              }
              else
              {
                throw new ArgumentException($"Количество столбцов данных таблицы изменилось с {countColumns} на {cells}");
              }
            }
            else
            {
              throw new NullReferenceException($"Данных нет");
            }
          }
          else
          {
            throw new NullReferenceException($"Ответ от страницы с данными пришел пустой");
          }
        }
        else
        {
          throw new HttpRequestException($"Подключиться не удалось: {httpResponseMessage.StatusCode}");
        }
      }
      catch (HttpRequestException ex)
      {
        loggerBank.Error(ex.Message);
      }
      catch (Exception ex)
      {
        loggerBank.Error(ex.Message);
      }
      //finally
      //{
      //  settingsClient.Start(bankModels);
      //}

      return bankModels;
    }
  }
}
