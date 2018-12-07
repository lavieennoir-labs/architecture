using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirualLab.Models
{
    public class TaskAnswerModel
    {
        public string TaskName { get; set; }
        public List<string> Commands { get; set; }
    }
}