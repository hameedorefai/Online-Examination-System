using DataAccess;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
namespace BusinessLayer
{
    public class clsResult
    {
        public int ResultID { get; set; }
        public int ExamID { get; set; }
        public int UserID { get; set; }
        public int Score { get; set; }
        public DateTime CompletionTime { get; set; }
        public ResultDTO ResultDTO { get; set; }
        public clsResult(int resultID, int examID, int userID, int score, DateTime completionTime)
        {
            ResultID = resultID;
            ExamID = examID;
            UserID = userID;
            Score = score;
            CompletionTime = completionTime;
        }

        static private int _AddNewResult(ResultDTO resultDTO)
        {
            return clsResultData.AddNewResult(resultDTO);
        }
        static public ResultDTO GetResultByResultID(int resultID)
        {
            return clsResultData.GetResultByResultID(resultID);
        }
        static public List<ResultDTO> GetResultsForUserID(int userID)
        {
            return clsResultData.GetResultsForUserID(userID);
        }
        static public List<ResultDTO> GetResultsByUserIDAndExamID(int userID,int examID)
        {
            return clsResultData.GetResultsByUserIDAndExamID(userID, examID);
        }
        static public List<ResultDTO> GetResultsByExamID(int examID)
        {
            return clsResultData.GetResultsByExamID(examID);
        }
        static public bool DeleteResultByResultID(int resultID)
        {
           return clsResultData.DeactivateResultByResultID(resultID);
        }

        private static void _CalculateScore(ResultDTO resultDTO)
        {
            try
            {
                int questionsCount = clsExamQuestions.GetQuestionsByExamID(resultDTO.ExamID).Count;
                if (questionsCount == 0) return;
                float percentagePerQuestion = 100 / questionsCount;
                foreach (StudentAnswerDTO answer in resultDTO.studentAnswersDTO)
                {
                    if (answer.IsCorrect)
                    {
                        resultDTO.ScorePersantage += percentagePerQuestion;
                    }
                }

            }
            catch(Exception ex)
            {
                throw;
            }
        }
        private static void _SaveResult(ResultDTO resultDTO)
        {
            if(clsGlobal.CurrentUser == null)
            resultDTO.UserID = 18; // temporary.
            int resultID = _AddNewResult(resultDTO);
            resultDTO.ResultID = resultID;
        }
        static public GetResultDTO CalculateAndSaveResult(SubmitExamination SubmittedExamination)
        {
            // here, you need to compare the StudentAnswer and the correct answer for each question.
            // storing StudentAnswers and the Result into the database.
            // return the ResultDTO. (ResultDTO contains Answers and correct answers as well).
            // edit: return the whole exam info.

            try
            {
                // exam.resultDTO.ExamID = exam.ExamID;
                // result.UserID = 18;   // by default, wich is "Unkown" user in the database.
                ResultDTO result = new ResultDTO();
                result.ExamID = SubmittedExamination.ExamID;
                result.studentAnswersDTO = clsStudentAnswer.EvaluateAnswers(SubmittedExamination);
                result.StartTime = SubmittedExamination.StartTime;
                result.CompletionTime = SubmittedExamination.CompletionTime;
                _CalculateScore(result);
                _SaveResult(result);
                result.studentAnswersDTO.ForEach(answer => answer.resultID = result.ResultID);

                clsStudentAnswer.SaveStudentAnswers(result.studentAnswersDTO);
                TimeSpan TimeSpentByMinutes = result.CompletionTime - result.StartTime;

                //   return result;
                /*GetResultDTO GetResult =*/
                return new GetResultDTO(result.ScorePersantage.ToString() + "%", FormatDuration(TimeSpentByMinutes), result.studentAnswersDTO);
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static string FormatDuration(TimeSpan duration)
        {
            // Get total minutes and seconds
            int minutes = (int)duration.TotalMinutes;
            int seconds = duration.Seconds;

            // Format the result as "minutes:seconds"
            return $"{minutes}:{seconds:D2}";
        }


    }
}
