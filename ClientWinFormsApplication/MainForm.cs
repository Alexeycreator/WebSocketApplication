using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientWinFormsApplication
{
  public partial class MainForm : Form
  {
    Logger loggerMainForm = LogManager.GetCurrentClassLogger();
    private CreateData createData;
    private string timeNow = DateTime.Now.ToShortTimeString();
    private DateTime timeWorkingDayStart = DateTime.Today.AddHours(8);
    private DateTime timeWorkingDayEnd = DateTime.Today.AddHours(23);

    public MainForm()
    {
      InitializeComponent();
      InitializeCreateData();
    }

    private void InitializeCreateData()
    {
      createData = new CreateData();
      createData.DataResponse += OnDataResponse;
    }

    private void OnDataResponse(List<ResponseModel> _responseModels)
    {
      if (InvokeRequired)
      {
        Invoke(new Action<List<ResponseModel>>(OnDataResponse), _responseModels);
        return;
      }
      DisplayDataInRichTextBox(_responseModels);

      loggerMainForm.Info($"Получено {_responseModels.Count} записей с сервера");
    }

    private void DisplayDataInRichTextBox(List<ResponseModel> _responseModels)
    {
      //TimeSpan workHours = timeWorkingDayEnd - timeWorkingDayStart;
      //int totalHours = (int)workHours.TotalHours;
      //rTbx.Clear();
      if (Convert.ToDateTime(timeNow) >= timeWorkingDayStart && Convert.ToDateTime(timeNow) <= timeWorkingDayEnd)
      {
        rTbx.AppendText($"=== ДАННЫЕ С СЕРВЕРА ===\n\n");
        foreach (var item in _responseModels)
        {
          rTbx.AppendText($"Цифровой код: {item.DigitalCode}\n");
          rTbx.AppendText($"Буквенный код: {item.LetterCode}\n");
          rTbx.AppendText($"Единицы: {item.Units}\n");
          rTbx.AppendText($"Валюта: {item.Currency}\n");
          rTbx.AppendText($"Курс: {item.Rate}\n");
          rTbx.AppendText("------------------------\n");
        }
        rTbx.AppendText($"\nИтого: {_responseModels.Count} записей\n");
        rTbx.AppendText("------------------------\n");
      }
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
    }

    private void button1_Click(object sender, EventArgs e)
    {
      //CreateData createData = new CreateData();
      //createData.DataCreation();
      if (Convert.ToDateTime(timeNow) >= timeWorkingDayStart && Convert.ToDateTime(timeNow) <= timeWorkingDayEnd)
      {
        rTbx.Clear();
        rTbx.AppendText("Начало процесса...\n");
        Task.Run(() => createData.DataCreation());
      }
      else
      {
        MessageBox.Show($"Не получается получить данные. Рабочее время с {timeWorkingDayStart} до {timeWorkingDayEnd}. Текущее время: {timeNow}");
      }

      //CreateData createData = new CreateData();
      //List<BankModel> dataToSend = createData.DataCreation();

      //Task.Run(() => settingsClient.Start(dataToSend));
    }
  }
}
