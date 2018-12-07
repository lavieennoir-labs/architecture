using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirualLab.Models;

namespace VirualLab.Services
{
    public class TaskResultService
    {
        public TaskResultsModel GetTaskExecutionResult(TaskAnswerModel userAnswer)
        {
            userAnswer.Commands = userAnswer.Commands[0].Split(',').ToList();
            var taskAnswers = GetTaskAnswers(userAnswer.TaskName);
            var analyseResult = AnalyseAnswer(userAnswer.Commands, taskAnswers, userAnswer.TaskName);
            var isAnswerEqualEtalon = analyseResult.Percentage == 1;

            return new TaskResultsModel
            {
                TotalResult = isAnswerEqualEtalon ? "Структура репозиторію відповідає цілі завдання" : "Структура репозиторію не відповідає цілі завдання",
                PercentageOfCorrectCommands = Math.Round(analyseResult.Percentage, 4) * 100,
                AnswerCommandsCount = userAnswer.Commands.Count,
                CorrectComandsCount = taskAnswers.Count,
                Advice = isAnswerEqualEtalon ? string.Empty : GetAdvice(userAnswer.Commands.Count, taskAnswers.Count),
                Reference = userAnswer.Reference
            };
        }

        private List<string> GetTaskAnswers(string taskName)
        {
            if (taskName == "Вивчаємо розгалудження у GIT")
            {
                return new List<string>
                {
                    "git commit",
                    "git commit"
                };
            }
            else
            {
                return new List<string>
                {
                    "git branch bugfix",
                    "git checkout bugfix",
                };
            }
        }

        private AnalyseModel AnalyseAnswer(List<string> answers, List<string> correctAnswers, string taskName)
        {
            var tmpAnswers = new List<string>(answers);
            tmpAnswers.RemoveAll(elem => !correctAnswers.Contains(elem));
            var tmpCorrect = new List<string>(correctAnswers);
            tmpCorrect.RemoveAll(elem => !tmpAnswers.Contains(elem));
            var correctCommandsCount = tmpAnswers.Zip(correctAnswers, (a, b) => new { a, b }).Where(x => x.a == x.b).Count();
            var percentage = correctCommandsCount / (double)Math.Max(answers.Count, correctAnswers.Count);
            
            return new AnalyseModel
            {
                CorrectCommandsCount = correctCommandsCount,
                WrongCommandsCount = answers.Count - correctCommandsCount,
                Percentage = percentage
            };
        }

        private string GetAdvice(int correctAnswers, int etalonAnswers)
        {
            if (correctAnswers < etalonAnswers)
            {
                return "Виконана не достатня кількість команд. Для отримання допомоги ознайомся з документацією git.";
            }
            if (correctAnswers == etalonAnswers)
            {
                return "Виконані некоректні комманди або в некоректній послідовності. Для отримання допомоги ознайомся з документацією git.";
            }
            else
            {
                return "Виконано забагато команд. Для отримання допомоги ознайомся з документацією git.";
            }
        }
    }
}