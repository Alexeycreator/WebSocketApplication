using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
    private TimeSpan timeSpan;
    private int colorIndex = 0;
    private int countElements = 0;
    private Color[] distinctColors = new Color[]
    {
      Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Purple, Color.Teal, Color.Magenta, Color.Lime, Color.Brown, Color.Pink,
      Color.Cyan, Color.Gold, Color.Navy, Color.Maroon, Color.Olive, Color.IndianRed, Color.LightCoral, Color.Salmon, Color.DarkSalmon,
      Color.LightSalmon, Color.Crimson, Color.Firebrick, Color.DarkRed, Color.DeepPink, Color.HotPink, Color.LightPink, Color.PaleVioletRed,
      Color.MediumVioletRed, Color.Coral, Color.Tomato, Color.OrangeRed, Color.DarkOrange, Color.Goldenrod, Color.DarkGoldenrod, Color.RosyBrown,
      Color.Sienna, Color.SaddleBrown, Color.Chocolate, Color.Peru, Color.SandyBrown, Color.BurlyWood, Color.Tan, Color.Wheat,
      Color.NavajoWhite, Color.Bisque, Color.BlanchedAlmond, Color.Cornsilk, Color.LemonChiffon, Color.LightGoldenrodYellow, Color.PapayaWhip,
      Color.Moccasin, Color.PeachPuff, Color.PaleGoldenrod, Color.Khaki, Color.DarkKhaki, Color.Lavender, Color.Thistle, Color.Plum,
      Color.Violet, Color.Orchid, Color.MediumOrchid, Color.MediumPurple, Color.BlueViolet, Color.DarkViolet, Color.DarkOrchid, Color.DarkMagenta
    };

    public MainForm()
    {
      InitializeComponent();
      InitializeCreateData();
    }

    private void InitializeCreateData()
    {
      createData = new CreateData(sleepTime);
      createData.DataResponse += OnDataResponse;
      //TimeSpan workHours = timeWorkingDayEnd - timeWorkingDayStart;
      //int _totalHours = (int)workHours.TotalHours;
    }

    #region Settings

    private void MainForm_Load(object sender, EventArgs e)
    {    
      SettingsButtons();
      ClearElements();
    }

    private void SettingsButtons()
    {
      btnPrintGraph.Enabled = false;
      btnCheckedAll.Enabled = false;
      btnUnCheckedAll.Enabled = false;
    }

    private void ClearElements()
    {
      chartGraph.Series.Clear();
      seriesChListBx.Items.Clear();
    }

    private void SettingsChart()
    {
      var chGraphAreas = chartGraph.ChartAreas[0];

      //отключаем автоматическое положение начала отсчета и задаем интервалы
      chGraphAreas.AxisX.IsMarginVisible = false;
      chGraphAreas.AxisX.Minimum = timeWorkingDayStart.ToOADate();
      chGraphAreas.AxisX.Maximum = timeWorkingDayEnd.ToOADate();
      chGraphAreas.AxisX.LabelStyle.Format = "HH:mm";

      //настройка интервала
      chGraphAreas.AxisX.IntervalType = DateTimeIntervalType.Minutes;
      chGraphAreas.AxisX.Interval = 30; //задать метод интервала в минутах

      //настройка сетки отображения данных
      chGraphAreas.BackColor = Color.White;
      chGraphAreas.BackGradientStyle = GradientStyle.None;
      chGraphAreas.BackSecondaryColor = Color.White;
      chGraphAreas.AxisX.MajorGrid.LineColor = Color.LightGray;
      chGraphAreas.AxisY.MajorGrid.LineColor = Color.LightGray;
      chGraphAreas.AxisX.MajorGrid.LineWidth = 1;
      chGraphAreas.AxisY.MajorGrid.LineWidth = 1;
      chGraphAreas.AxisX.LineColor = Color.Gray;
      chGraphAreas.AxisY.LineColor = Color.Gray;
      chGraphAreas.AxisX.MajorTickMark.LineColor = Color.Gray;
      chGraphAreas.AxisY.MajorTickMark.LineColor = Color.Gray;
      chGraphAreas.AxisX.LabelStyle.ForeColor = Color.Black;
      chGraphAreas.AxisY.LabelStyle.ForeColor = Color.Black;
      chGraphAreas.AxisX.TitleForeColor = Color.Black;
      chGraphAreas.AxisY.TitleForeColor = Color.Black;

      //настройка курсоров и подписи к осям
      chGraphAreas.AxisX.Title = "Время корректировки в минутах";
      chGraphAreas.AxisY.Title = "Цена валюты (руб)";
      chGraphAreas.Name = "График курса валют ЦБ РФ";
      chGraphAreas.CursorX.IsUserEnabled = true;
      chGraphAreas.CursorY.IsUserEnabled = true;
    }

    #endregion

    #region Buttons

    private void btnPrintGraph_Click(object sender, EventArgs e)
    {
      if (Convert.ToDateTime(timeNow) >= timeWorkingDayStart && Convert.ToDateTime(timeNow) <= timeWorkingDayEnd)
      {
        Task.Run(() => createData.DataCreation());
      }
      else
      {
        MessageBox.Show($"Не получается получить данные. Рабочее время с {timeWorkingDayStart} до {timeWorkingDayEnd}. Текущее время: {timeNow}");
      }
    }

    private void btnCheckedAll_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < countElements; i++)
      {
        seriesChListBx.SetItemCheckState(i, CheckState.Checked);
      }
      loggerMainForm.Info($"Добавлено выделение для всех элементов на графике");
    }

    private void btnUnCheckedAll_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < countElements; i++)
      {
        seriesChListBx.SetItemCheckState(i, CheckState.Unchecked);
      }
      loggerMainForm.Info($"Снято выделение со всех элементов на графике");
    }

    #endregion

    #region Methods

    private void OnDataResponse(List<ResponseModel> _responseModels)
    {
      if (InvokeRequired)
      {
        Invoke(new Action<List<ResponseModel>>(OnDataResponse), _responseModels);
        return;
      }
      DisplayDataInChart(_responseModels);
      loggerMainForm.Info($"Получено {_responseModels.Count} записей с сервера");
    }

    private void DisplayDataInChart(List<ResponseModel> _responseModels)
    {
      try
      {
        if (Convert.ToDateTime(timeNow) >= timeWorkingDayStart && Convert.ToDateTime(timeNow) <= timeWorkingDayEnd)
        {
          countElements = _responseModels.Count;
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
              Color = GetColorSeries(),
              MarkerStyle = MarkerStyle.Circle,
              MarkerSize = 8,
              Enabled = true,
              IsXValueIndexed = false,
            };
            chartGraph.Series.Add(series);
            seriesChListBx.Items.Add(name, true);
            loggerMainForm.Info($"Создано {chartGraph.Series.Count} серий");
            btnCheckedAll.Enabled = true;
            btnUnCheckedAll.Enabled = true;
            seriesChListBx.ItemCheck += (s, e) =>
            {
              if (e.Index >= 0 && e.Index < chartGraph.Series.Count)
              {
                chartGraph.Series[e.Index].Enabled = (e.NewValue == CheckState.Checked);
              }
            };
          }
          foreach (var data in _responseModels)
          {
            var series = chartGraph.Series[data.LetterCode];
            TimeSpan interval = TimeSpan.FromMinutes(30); //задать метод определения минут
            DateTime xValue = timeWorkingDayStart.AddMinutes(series.Points.Count * interval.TotalMinutes);
            double yValue = Convert.ToDouble(data.Rate);
            DataPoint point = new DataPoint(xValue.ToOADate(), yValue)
            {
              ToolTip = $"Валюта: {data.LetterCode}\nКурс: {yValue} руб",
              MarkerStyle = MarkerStyle.Circle,
              MarkerSize = 8,
              MarkerColor = series.Color
            };
            series.Points.Add(point);
            loggerMainForm.Info($"Отрисована точка {xValue} со значением {yValue}");
          }
          SettingsChart();
          loggerMainForm.Info($"Отрисованы точки на графике и настроено отображение");
        }
      }
      catch (Exception ex)
      {
        loggerMainForm.Error(ex.Message);
      }
    }

    private Color GetColorSeries()
    {
      return distinctColors[colorIndex++];
    }

    private int ConversionToMinutes()
    {
      timeSpan = TimeSpan.FromMilliseconds(sleepTime);
      return (int)timeSpan.TotalMinutes;
    }

    #endregion

    #region Handlers



    #endregion

    private void cmbxSelectedPrint_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (cmbxSelectedPrint.SelectedIndex == 0)
      {
        btnPrintGraph.Enabled = true;
      }
    }
  }
}
