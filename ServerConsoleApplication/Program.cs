using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ServerConsoleApplication
{
  internal class Program
  {
    static void Main(string[] args)
    {
      SettingsServer settingsServer = new SettingsServer();
      settingsServer.Start();
    //  IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
    //  int port = 10000;
    //  TcpListener listener = new TcpListener(ipAddress, port);
    //  try
    //  {
    //    listener.Start();
    //    Console.WriteLine("Сервер запущен. Ожидание подключений...");
    //    while (true)
    //    {
    //      // Принимаем входящее подключение
    //      using (TcpClient client = listener.AcceptTcpClient())
    //      using (NetworkStream stream = client.GetStream())
    //      {
    //        Console.WriteLine("Клиент подключен.");

    //        // Читаем данные от клиента
    //        byte[] buffer = new byte[1024];
    //        int bytesRead = stream.Read(buffer, 0, buffer.Length);
    //        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
    //        Console.WriteLine($"Получены данные: {receivedData}");

    //        // Обрабатываем данные (пример преобразования)
    //        string processedData = ProcessData(receivedData);

    //        // Отправляем обработанные данные обратно
    //        byte[] responseData = Encoding.UTF8.GetBytes(processedData);
    //        stream.Write(responseData, 0, responseData.Length);
    //        Console.WriteLine($"Отправлены обработанные данные: {processedData}");
    //      }
    //    }
    //  }
    //  catch (Exception ex)
    //  {
    //    Console.WriteLine(ex.ToString());
    //  }
    //  finally
    //  {
    //    listener.Stop();
    //  }
    //}
    //private static string ProcessData(string data)
    //{
    //  // Пример обработки данных - преобразуем в верхний регистр
    //  return data.ToUpper();

    //  // Здесь можно реализовать любую логику преобразования
    //  // Например:
    //  // - выполнить вычисления
    //  // - изменить формат данных
    //  // - добавить/удалить информацию
    }
  }
}
