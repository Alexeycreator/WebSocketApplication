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
      this.btnPrintGraph = new System.Windows.Forms.Button();
      this.chartGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
      this.seriesChListBx = new System.Windows.Forms.CheckedListBox();
      this.btnCheckedAll = new System.Windows.Forms.Button();
      this.btnUnCheckedAll = new System.Windows.Forms.Button();
      this.cmbxSelectedPrint = new System.Windows.Forms.ComboBox();
      ((System.ComponentModel.ISupportInitialize)(this.chartGraph)).BeginInit();
      this.SuspendLayout();
      // 
      // btnPrintGraph
      // 
      this.btnPrintGraph.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.btnPrintGraph.Location = new System.Drawing.Point(415, 787);
      this.btnPrintGraph.Name = "btnPrintGraph";
      this.btnPrintGraph.Size = new System.Drawing.Size(244, 46);
      this.btnPrintGraph.TabIndex = 1;
      this.btnPrintGraph.Text = "Показать зависимость";
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
      this.chartGraph.Location = new System.Drawing.Point(11, 12);
      this.chartGraph.Name = "chartGraph";
      series1.ChartArea = "ChartArea1";
      series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
      series1.Legend = "Legend1";
      series1.Name = "Series1";
      this.chartGraph.Series.Add(series1);
      this.chartGraph.Size = new System.Drawing.Size(1289, 769);
      this.chartGraph.TabIndex = 2;
      this.chartGraph.Text = "chart1";
      // 
      // seriesChListBx
      // 
      this.seriesChListBx.FormattingEnabled = true;
      this.seriesChListBx.Location = new System.Drawing.Point(1306, 12);
      this.seriesChListBx.Name = "seriesChListBx";
      this.seriesChListBx.Size = new System.Drawing.Size(158, 769);
      this.seriesChListBx.TabIndex = 3;
      // 
      // btnCheckedAll
      // 
      this.btnCheckedAll.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.btnCheckedAll.Location = new System.Drawing.Point(1142, 787);
      this.btnCheckedAll.Name = "btnCheckedAll";
      this.btnCheckedAll.Size = new System.Drawing.Size(158, 46);
      this.btnCheckedAll.TabIndex = 4;
      this.btnCheckedAll.Text = "Выделить все";
      this.btnCheckedAll.UseVisualStyleBackColor = true;
      this.btnCheckedAll.Click += new System.EventHandler(this.btnCheckedAll_Click);
      // 
      // btnUnCheckedAll
      // 
      this.btnUnCheckedAll.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.btnUnCheckedAll.Location = new System.Drawing.Point(1306, 787);
      this.btnUnCheckedAll.Name = "btnUnCheckedAll";
      this.btnUnCheckedAll.Size = new System.Drawing.Size(158, 46);
      this.btnUnCheckedAll.TabIndex = 5;
      this.btnUnCheckedAll.Text = "Убрать все";
      this.btnUnCheckedAll.UseVisualStyleBackColor = true;
      this.btnUnCheckedAll.Click += new System.EventHandler(this.btnUnCheckedAll_Click);
      // 
      // cmbxSelectedPrint
      // 
      this.cmbxSelectedPrint.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.cmbxSelectedPrint.FormattingEnabled = true;
      this.cmbxSelectedPrint.Items.AddRange(new object[] {
            "Курсы валют"});
      this.cmbxSelectedPrint.Location = new System.Drawing.Point(11, 796);
      this.cmbxSelectedPrint.Name = "cmbxSelectedPrint";
      this.cmbxSelectedPrint.Size = new System.Drawing.Size(398, 30);
      this.cmbxSelectedPrint.TabIndex = 6;
      this.cmbxSelectedPrint.SelectedIndexChanged += new System.EventHandler(this.cmbxSelectedPrint_SelectedIndexChanged);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1474, 842);
      this.Controls.Add(this.cmbxSelectedPrint);
      this.Controls.Add(this.btnUnCheckedAll);
      this.Controls.Add(this.btnCheckedAll);
      this.Controls.Add(this.seriesChListBx);
      this.Controls.Add(this.chartGraph);
      this.Controls.Add(this.btnPrintGraph);
      this.Name = "MainForm";
      this.Text = "Приложение";
      this.Load += new System.EventHandler(this.MainForm_Load);
      ((System.ComponentModel.ISupportInitialize)(this.chartGraph)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Button btnPrintGraph;
    private System.Windows.Forms.DataVisualization.Charting.Chart chartGraph;
    private System.Windows.Forms.CheckedListBox seriesChListBx;
    private System.Windows.Forms.Button btnCheckedAll;
    private System.Windows.Forms.Button btnUnCheckedAll;
    private System.Windows.Forms.ComboBox cmbxSelectedPrint;
  }
}

