using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerConsoleApplication
{
  internal class CalculationData
  {
    public List<BankModel> ProcessData(string _data, ref List<BankModel> _allDataBankModel)
    {
      List<BankModel> jsonBankModels = JsonConvert.DeserializeObject<List<BankModel>>(_data);
      _allDataBankModel.AddRange(jsonBankModels);
      double averageRate = AverageRate(_allDataBankModel);
      foreach (var item in jsonBankModels)
      {
        item.Rate = averageRate.ToString();
      }
      return jsonBankModels;
    }

    private double AverageRate(List<BankModel> _data)
    {
      double sum = 0;
      int count = 0;
      foreach (var item in _data)
      {
        if (double.TryParse(item.Rate, NumberStyles.Any, CultureInfo.InvariantCulture, out double rateValue))
        {
          sum += rateValue;
          count++;
        }
      }
      return count > 0 ? sum / count : 0;
    }
  }
}
