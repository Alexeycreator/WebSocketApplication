using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using NLog;
using System.Configuration;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ServerConsoleApplication
{
  internal class SettingsServer
  {
    private Logger loggerServer = LogManager.GetCurrentClassLogger();
    private string timeNow = DateTime.Now.ToShortTimeString();
    private DateTime timeWorkingDayStart = DateTime.Today.AddHours(8);
    private DateTime timeWorkingDayEnd = DateTime.Today.AddHours(23);
    private CalculationData calculationData;

    public void Start()
    {
      string ip =  ConfigurationManager.AppSettings["ip"];
      string port = ConfigurationManager.AppSettings["port"];
      IPAddress ipAddress = IPAddress.Parse(ip);
      TcpListener tcpListener = new TcpListener(ipAddress, Convert.ToInt32(port));
      try
      {
        if (Convert.ToDateTime(timeNow) <= timeWorkingDayStart)
        {
          loggerServer.Warn($"Сервер запускается в {timeWorkingDayStart.ToShortTimeString()} по МСК");
        }
        else if (Convert.ToDateTime(timeNow) >= timeWorkingDayEnd)
        {
          loggerServer.Warn($"Сервер работает до {timeWorkingDayEnd.ToShortTimeString()} по МСК");
        }
        else
        {
          tcpListener.Start();
          loggerServer.Info("Сервер запущен.");
          Console.WriteLine("Ожидание подключений...");
          IncomingConnection(tcpListener);
        }
      }
      catch (Exception ex)
      {
        loggerServer.Error(ex.Message);
      }
      finally
      {
        tcpListener.Stop();
      }
    }

    private void IncomingConnection(TcpListener _tcpListener)
    {
      while (Convert.ToDateTime(timeNow) >= timeWorkingDayStart && Convert.ToDateTime(timeNow) <= timeWorkingDayEnd)
      {
        using (TcpClient client = _tcpListener.AcceptTcpClient())
        using (NetworkStream stream = client.GetStream())
        {
          loggerServer.Info("Клиент подключен.");
          loggerServer.Info($"Считывание данных.");
          byte[] buffer = new byte[1024];
          int bytesRead;
          string receivedData;
          using (var memoryStream = new MemoryStream())
          {
            do
            {
              bytesRead = stream.Read(buffer, 0, buffer.Length);
              memoryStream.Write(buffer, 0, bytesRead);
            }
            while (bytesRead > 0 && stream.DataAvailable);
            receivedData = Encoding.UTF8.GetString(memoryStream.ToArray());
          }
          loggerServer.Info($"Получены данные: {receivedData}");
          calculationData = new CalculationData();
          List<BankModel> processedData = calculationData.ProcessData(receivedData);
          loggerServer.Info($"Данные обработаны.");
          var jsonBankModel = JsonConvert.SerializeObject(processedData);
          byte[] responseData = Encoding.UTF8.GetBytes(jsonBankModel);
          stream.Write(responseData, 0, responseData.Length);
          loggerServer.Info($"Отправлены обработанные данные: {processedData}");
        }
      }
    }
  }
}
