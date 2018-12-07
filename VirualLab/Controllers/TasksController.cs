using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirualLab.Models;
using VirualLab.Services;

namespace VirualLab.Controllers
{
    public class TasksController : Controller
    {
        private TaskResultService _taskResultService;
        public TaskResultService TaskResultService
        {
            get
            {
                if(_taskResultService == null)
                {
                    _taskResultService = new TaskResultService();
                }
                return _taskResultService;
            }
        }

        // GET: Tasks
        public ActionResult Check(TaskAnswerModel userAnswer)
        {
            var model = TaskResultService.GetTaskExecutionResult(userAnswer);
            return View(model);
        }

        public ActionResult Input()
        {
            return View();
        }

        public ActionResult Input2()
        {
            return View();
        }

        public ActionResult Goal()
        {
            return View();
        }

        public ActionResult Goal2()
        {
            return View();
        }
    }
}