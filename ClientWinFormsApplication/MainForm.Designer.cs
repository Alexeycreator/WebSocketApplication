namespace ClientWinFormsApplication
{
  partial class MainForm
  {
    /// <summary>
    /// Обязательная переменная конструктора.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Освободить все используемые ресурсы.
    /// </summary>
    /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Код, автоматически созданный конструктором форм Windows

    /// <summary>
    /// Требуемый метод для поддержки конструктора — не изменяйте 
    /// содержимое этого метода с помощью редактора кода.
    /// </summary>
    private void InitializeComponent()
    {
      System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
      System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
      System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
      this.rTbx = new System.Windows.Forms.RichTextBox();
      this.btnPrintGraph = new System.Windows.Forms.Button();
      this.chartGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
      this.seriesChListBx = new System.Windows.Forms.CheckedListBox();
      ((System.ComponentModel.ISupportInitialize)(this.chartGraph)).BeginInit();
      this.SuspendLayout();
      // 
      // rTbx
      // 
      this.rTbx.Location = new System.Drawing.Point(12, 12);
      this.rTbx.Name = "rTbx";
      this.rTbx.Size = new System.Drawing.Size(115, 364);
      this.rTbx.TabIndex = 0;
      this.rTbx.Text = "";
      // 
      // btnPrintGraph
      // 
      this.btnPrintGraph.Location = new System.Drawing.Point(12, 392);
      this.btnPrintGraph.Name = "btnPrintGraph";
      this.btnPrintGraph.Size = new System.Drawing.Size(115, 46);
      this.btnPrintGraph.TabIndex = 1;
      this.btnPrintGraph.Text = "Показать\r\nзависимость";
      this.btnPrintGraph.UseVisualStyleBackColor = true;
      this.btnPrintGraph.Click += new System.EventHandler(this.btnPrintGraph_Click);
      // 
      // chartGraph
      // 
      chartArea1.AxisX.IsMarginVisible = false;
      chartArea1.AxisX.Maximum = 3D;
      chartArea1.AxisX.Minimum = 0D;
      chartArea1.AxisX.ScaleBreakStyle.StartFromZero = System.Windows.Forms.DataVisualization.Charting.StartFromZero.Yes;
      chartArea1.AxisX2.IsMarginVisible = false;
      chartArea1.Name = "ChartArea1";
      this.chartGraph.ChartAreas.Add(chartArea1);
      legend1.Name = "Legend1";
      this.chartGraph.Legends.Add(legend1);
      this.chartGraph.Location = new System.Drawing.Point(133, 12);
      this.chartGraph.Name = "chartGraph";
      series1.ChartArea = "ChartArea1";
      series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
      series1.Legend = "Legend1";
      series1.Name = "Series1";
      this.chartGraph.Series.Add(series1);
      this.chartGraph.Size = new System.Drawing.Size(1091, 757);
      this.chartGraph.TabIndex = 2;
      this.chartGraph.Text = "chart1";
      // 
      // seriesChListBx
      // 
      this.seriesChListBx.FormattingEnabled = true;
      this.seriesChListBx.Location = new System.Drawing.Point(1230, 12);
      this.seriesChListBx.Name = "seriesChListBx";
      this.seriesChListBx.Size = new System.Drawing.Size(158, 752);
      this.seriesChListBx.TabIndex = 3;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1400, 781);
      this.Controls.Add(this.seriesChListBx);
      this.Controls.Add(this.chartGraph);
      this.Controls.Add(this.btnPrintGraph);
      this.Controls.Add(this.rTbx);
      this.Name = "MainForm";
      this.Text = "Form1";
      this.Load += new System.EventHandler(this.MainForm_Load);
      ((System.ComponentModel.ISupportInitialize)(this.chartGraph)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.RichTextBox rTbx;
    private System.Windows.Forms.Button btnPrintGraph;
    private System.Windows.Forms.DataVisualization.Charting.Chart chartGraph;
    private System.Windows.Forms.CheckedListBox seriesChListBx;
  }
}

