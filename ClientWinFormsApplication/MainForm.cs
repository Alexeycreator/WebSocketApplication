using ClientWinFormsApplication.CentralBank;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientWinFormsApplication
{
  public partial class MainForm : Form
  {
    private List<ResponseModel> responseModel;
    public RichTextBox richTextBox => rTbx;

    public MainForm()
    {
      InitializeComponent();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
    }

    private void button1_Click(object sender, EventArgs e)
    {
      CreateData createData = new CreateData();
      createData.DataCreation();
      //foreach (var item in responseModel)
      //{
      //  rTbx.AppendText($"{item.DigitalCode}\n" +
      //    $"{item.Rate}\n" +
      //    $"{item.LetterCode}\n");
      //}
    }
  }
}
