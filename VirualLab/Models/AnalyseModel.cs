using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirualLab.Models
{
    public class AnalyseModel
    {
        public double Percentage { get; set; }
        public int CorrectCommandsCount { get; set; }
        public int WrongCommandsCount { get; set; }
    }
}