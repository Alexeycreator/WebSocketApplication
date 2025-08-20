using ClientWinFormsApplication.CentralBank;
using NLog;
using System;
using System.Threading;

namespace ClientWinFormsApplication
{
  internal sealed class CreateData
  {
    private string timeNow = DateTime.Now.ToShortTimeString();
    private DateTime timeWorkingDayStart = DateTime.Today.AddHours(17);
    private DateTime timeWorkingDayEnd = DateTime.Today.AddHours(19);
    private Logger loggerCreateData = LogManager.GetCurrentClassLogger();

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
            loggerCreateData.Info($"Тест № {i}");
            BankParser bankParser = new BankParser();
            bankParser.CentralBankParser();
            Thread.Sleep(60000); //1 минута
            //Thread.Sleep(300000); //30 минут
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
    }
  }
}
