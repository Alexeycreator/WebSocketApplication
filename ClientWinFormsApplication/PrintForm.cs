using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWinFormsApplication
{
  internal sealed class PrintForm : MainForm
  {
    //private List<ResponseModel> responseModels;

    public void UpdateUI(List<ResponseModel> _dataResponseModels)
    {
      // Проверка 1: RichTextBox существует?
      if (richTextBox == null)
      {
        Debug.WriteLine("RichTextBox is null");
        return;
      }

      // Проверка 2: Данные пришли?
      if (_dataResponseModels == null || _dataResponseModels.Count == 0)
      {
        Debug.WriteLine("No data received");
        return;
      }

      // Проверка 3: Форма видима?
      if (!this.Visible)
      {
        Debug.WriteLine("Form is not visible");
      }

      // Тестовый текст
      richTextBox.AppendText("Test text\n");
      //int i = 1;
      //foreach(var item in _dataResponseModels)
      //{
      //  richTextBox.AppendText($"№{i}-{item.LetterCode}\n" +
      //    $"{item.Rate}\n");
      //}
      //richTextBox.Invalidate();
      //richTextBox.Update();
    }
  }
}
