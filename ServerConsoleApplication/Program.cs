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
    }
  }
}
