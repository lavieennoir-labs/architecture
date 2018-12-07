using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirualLab.Models
{
    public class TaskResultsModel
    {
        public string TotalResult { get; set; }
        public double PercentageOfCorrectCommands { get; set; }
        public int AnswerCommandsCount { get; set; }
        public int CorrectComandsCount { get; set; }
        public string Advice { get; set; }
        public string Reference { get; set; }
    }
}