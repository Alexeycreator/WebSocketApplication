using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerConsoleApplication
{
  internal sealed class ResponseData
  {
    public string DigitalCode { get; set; }

    public string LetterCode { get; set; }

    public string Units { get; set; }

    public string Currency { get; set; }

    public double Rate { get; set; }
  }
}
