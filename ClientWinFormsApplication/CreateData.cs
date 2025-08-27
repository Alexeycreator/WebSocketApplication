using ClientWinFormsApplication.CentralBank;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace ClientWinFormsApplication
{
  internal sealed class CreateData
  {
    private string timeNow = DateTime.Now.ToShortTimeString();
    private DateTime timeWorkingDayStart = DateTime.Today.AddHours(8);
    private DateTime timeWorkingDayEnd = DateTime.Today.AddHours(23);
    private Logger loggerCreateData = LogManager.GetCurrentClassLogger();
    private SettingsClient settingsClient;
    private const int sleepTime = 60000;
    public event Action<List<ResponseModel>> DataResponse;
    //private List<BankModel> bankList = new List<BankModel>();

    public CreateData()
    {
      settingsClient = new SettingsClient();
      settingsClient.DataResponse += OnDataResponse;
    }

    public void DataCreation()
    {
      try
      {
        TimeSpan workHours = timeWorkingDayEnd - timeWorkingDayStart;
        int totalHours = (int)workHours.TotalHours;
        if (Convert.ToDateTime(timeNow) >= timeWorkingDayStart && Convert.ToDateTime(timeNow) <= timeWorkingDayEnd)
        {
          for (int i = 1; i <= totalHours; i++)
          {
            loggerCreateData.Info($"Запрос № {i} из {totalHours}");
            BankParser bankParser = new BankParser();
            List<BankModel> bankList = bankParser.CentralBankParser();
            loggerCreateData.Info($"Данные {i} запроса получены");
            settingsClient.Start(bankList);
            if (i < totalHours)
            {
              var stopwatch = Stopwatch.StartNew();
              Thread.Sleep(sleepTime);
              stopwatch.Stop();
              loggerCreateData.Info($"Задержка между запросом в {stopwatch.Elapsed}");
            }
          }
        }
        else
        {
          throw new ArgumentException($"Не получается получить данные. Рабочее время с {timeWorkingDayStart} до {timeWorkingDayEnd}. Текущее время: {timeNow}");
        }
      }
      catch (ArgumentException ex)
      {
        loggerCreateData.Error(ex.Message);
      }
      catch (Exception ex)
      {
        loggerCreateData.Error(ex.Message);
      }
      //return bankList;
    }

    private void OnDataResponse(List<ResponseModel> responseModels)
    {
      DataResponse?.Invoke(responseModels);
    }
  }
}
