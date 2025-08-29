using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ClientWinFormsApplication
{
  public partial class MainForm : Form
  {
    Logger loggerMainForm = LogManager.GetCurrentClassLogger();
    private CreateData createData;
    private string timeNow = DateTime.Now.ToShortTimeString();
    private DateTime timeWorkingDayStart = DateTime.Today.AddHours(8);
    private DateTime timeWorkingDayEnd = DateTime.Today.AddHours(23);
    private const int sleepTime = 60000;
    TimeSpan timeSpan;
    private int minutesInterval;
    //private static int count = 0;

    public MainForm()
    {
      InitializeComponent();
      InitializeCreateData();
    }

    private void InitializeCreateData()
    {
      createData = new CreateData(sleepTime);
      createData.DataResponse += OnDataResponse;
    }

    private void OnDataResponse(List<ResponseModel> _responseModels)
    {
      if (InvokeRequired)
      {
        Invoke(new Action<List<ResponseModel>>(OnDataResponse), _responseModels);
        return;
      }
      DisplayDataInChart(_responseModels);
      //count++;

      loggerMainForm.Info($"Получено {_responseModels.Count} записей с сервера");
    }

    private void DisplayDataInChart(List<ResponseModel> _responseModels)
    {
      TimeSpan workHours = timeWorkingDayEnd - timeWorkingDayStart;
      int totalHours = (int)workHours.TotalHours;

      rTbx.Clear();
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
     
      var letterCode = _responseModels.Select(lc => lc.LetterCode).ToList();
      foreach (var name in letterCode)
      {
        if (chartGraph.Series.Any(n => n.Name == name))
        {
          continue;
        }
        var series = new Series(name)
        {
          ChartType = SeriesChartType.Line,
          Color = System.Drawing.Color.Green,
          MarkerStyle = MarkerStyle.Circle,
          MarkerSize = 8,
          Enabled = true,
          IsValueShownAsLabel = true,
          IsXValueIndexed = false,
        };
        chartGraph.Series.Add(series);
        seriesChListBx.Items.Add(name, true);
      }
      
      foreach (var data in _responseModels)
      {
        var series = chartGraph.Series[data.LetterCode];
        int xValue = series.Points.Count;
        double yValue = Convert.ToDouble(data.Rate);
        DataPoint point = new DataPoint(xValue, yValue)
        {
          ToolTip = $"Значение в точке = {yValue}",
          MarkerStyle = MarkerStyle.Circle,
          MarkerSize = 8,
          MarkerColor = series.Color
        };
        series.Points.Add(point);
        loggerMainForm.Info($"Отрисована точка {xValue} со значением {yValue}");
        //double maxX = chartGraph.Series[0].Points.Max(p => p.XValue);
        //if (maxX > chartGraph.ChartAreas[0].AxisX.ScaleView.Position + 8)
        //{
        //  chartGraph.ChartAreas[0].AxisX.ScaleView.Position = maxX - 7;
        //}
      }
      SettingsChart();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
      ClearElements();
    }

    private void btnPrintGraph_Click(object sender, EventArgs e)
    {
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
    }

    private void ClearElements()
    {
      chartGraph.Series.Clear();
      seriesChListBx.Items.Clear();
    }

    private void SettingsChart()
    {
      var chGraphAreas = chartGraph.ChartAreas[0];
      chGraphAreas.AxisX.IsMarginVisible = false;
      chGraphAreas.AxisX.Minimum = 0;
      chGraphAreas.AxisX.Maximum = 5;
      chGraphAreas.AxisX.IsStartedFromZero = true;
      //chGraphAreas.AxisX.Minimum = timeWorkingDayStart.ToOADate();
      //chGraphAreas.AxisX.LabelStyle.Format = "HH:mm";
      //chGraphAreas.AxisX.IntervalType = DateTimeIntervalType.Minutes;
      //chGraphAreas.AxisX.Interval = ConversionToMinutes();
      chGraphAreas.AxisX.Title = "Время корректировки в минутах";

      chGraphAreas.AxisY.Title = "Цена валюты (руб)";
      chGraphAreas.Name = "График курса валют ЦБ РФ";
      chGraphAreas.CursorX.IsUserEnabled = true;
      chGraphAreas.CursorY.IsUserEnabled = true;
    }

    private int ConversionToMinutes()
    {
      timeSpan = TimeSpan.FromMilliseconds(sleepTime);
      return (int)timeSpan.TotalMinutes;
    }

    private int Test()
    {
      int t = 0;
      if (t == 14)
      {
        return t;
      }
      else
      {
        return t++;
      }
    }
  }
}
