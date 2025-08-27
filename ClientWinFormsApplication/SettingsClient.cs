using ClientWinFormsApplication.CentralBank;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Sockets;
using System.Text;

namespace ClientWinFormsApplication
{
  internal sealed class SettingsClient
  {
    private Logger loggerClient = LogManager.GetCurrentClassLogger();
    public event Action<List<ResponseModel>> DataResponse;

    public void Start(List<BankModel> dataToSend)
    {
      string serverIp = ConfigurationManager.AppSettings["ip"];
      string port = ConfigurationManager.AppSettings["port"];
      try
      {
        using (TcpClient client = new TcpClient(serverIp, Convert.ToInt32(port)))
        using (NetworkStream stream = client.GetStream())
        {
          loggerClient.Info("Подключение к серверу");
          string jsonData = JsonConvert.SerializeObject(dataToSend);
          var buffer = Encoding.UTF8.GetBytes(jsonData);
          stream.Write(buffer, 0, buffer.Length);
          loggerClient.Info($"Отправлены данные: {dataToSend}");

          var responseBuffer = new byte[buffer.Length];
          int bytesRead = stream.Read(responseBuffer, 0, responseBuffer.Length);
          string responseData = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);
          loggerClient.Info($"Получены обработанные данные: {responseData}");

          List<ResponseModel> responseModels = JsonConvert.DeserializeObject<List<ResponseModel>>(responseData);
          DataResponse?.Invoke(responseModels);
        }
      }
      catch (Exception ex)
      {
        loggerClient.Error(ex.Message);
      }
    }
  }
}
