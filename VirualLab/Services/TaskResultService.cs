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
            var isAnswerEqualEtalon = analyseResult.Percentage == 100;

            return new TaskResultsModel
            {
                TotalResult = isAnswerEqualEtalon ? "Структура репозиторію відповідає цілі завдання" : "Структура репозиторію не відповідає цілі завдання",
                PercentageOfCorrectCommands = Math.Round(analyseResult.Percentage, 4) * 100,
                AnswerCommandsCount = userAnswer.Commands.Count,
                CorrectComandsCount = taskAnswers.Count,
                Advice = isAnswerEqualEtalon ? string.Empty : GetAdvice(analyseResult.CorrectCommandsCount, taskAnswers.Count) 
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
                    "git branch",
                    "git checkout",
                };
            }
        }

        private AnalyseModel AnalyseAnswer(List<string> answers, List<string> correctAnswers, string taskName)
        {
            if (taskName == "Вивчаємо розгалудження у GIT")
            {
                int correctCommandsCount = 0;
                double percentage = 0.0;
                if (answers.Count <= correctAnswers.Count)
                {
                    correctCommandsCount = answers.Count(elem => elem.StartsWith("git commit"));
                    percentage = correctCommandsCount / 2.0 * 100;
                }
                else
                {
                    var answerPercentage = correctAnswers.Count / (double)answers.Count;
                    correctCommandsCount += answers[0].StartsWith("git commit") ? 1 : 0;
                    correctCommandsCount += answers[1].StartsWith("git commit") ? 1 : 0;
                    percentage = correctCommandsCount * answerPercentage - (answers.Count - correctCommandsCount) * answerPercentage;
                }

                return new AnalyseModel
                {
                    CorrectCommandsCount = correctCommandsCount,
                    WrongCommandsCount = answers.Count - correctCommandsCount,
                    Percentage = percentage
                };
            }
            else
            {
                int correctCommandsCount = 0;
                double percentage = 0.0;
                if (answers.Count <= correctAnswers.Count)
                {
                    correctCommandsCount += answers[0].StartsWith("git branch") ? 1 : 0;
                    correctCommandsCount += answers[1].StartsWith("git checkout") ? 1 : 0;
                    percentage = correctCommandsCount / 2.0 * 100;
                }
                else
                {
                    var answerPercentage = correctAnswers.Count / (double)answers.Count;
                    correctCommandsCount += answers[0].StartsWith("git branch") ? 1 : 0;
                    correctCommandsCount += answers[1].StartsWith("git checkout") ? 1 : 0;
                    percentage = correctCommandsCount * answerPercentage - (answers.Count - correctCommandsCount) * answerPercentage;
                }

                return new AnalyseModel
                {
                    CorrectCommandsCount = correctCommandsCount,
                    WrongCommandsCount = answers.Count - correctCommandsCount,
                    Percentage = percentage
                };

            }
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