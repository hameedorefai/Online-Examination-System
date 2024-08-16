using DataAccess;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class clsStudentAnswer
    {
        public StudentAnswerDTO studentAnswerDTO { get;set;}
       

        static private int _AddNewStudentAnswer(StudentAnswerDTO answer)
        {
            return clsStudentAnswerData.AddStudentAnswer(answer);
        }
        static public void SaveStudentAnswers(List<StudentAnswerDTO> AnswersList)
        {
            foreach (StudentAnswerDTO answer in AnswersList)
            {
                try
                {
                _AddNewStudentAnswer(answer);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        static public StudentAnswerDTO GetStudentAnswerDetailsByQuestionID(int questionID)
        {
            return clsStudentAnswerData.GetStudentAnswerByQuestionID(questionID);
        }
        public static List<StudentAnswerDTO> EvaluateAnswers(SubmitExamination SubmittedExamination)
        {

            List<StudentAnswerDTO> studentAnswersList = new List<StudentAnswerDTO>();

            try
            {
                foreach (GetQuestionDTO question in SubmittedExamination.QuestionsList)
                {

                    StudentAnswerDTO StudentAnswer = new StudentAnswerDTO();

                    StudentAnswer.questionID = question.QuestionID;

                    StudentAnswer.selectedOptionID = question.StudentAnswerID;

                    StudentAnswer.userID = 18; // as default "Unkown".
                    StudentAnswer.timeSpent = 0;

                    StudentAnswer.CorrectAnswerID = (int)clsQuestion.GetCorrectAnswerByQuestionID(question.QuestionID);

                    StudentAnswer.IsCorrect = (StudentAnswer.selectedOptionID == StudentAnswer.CorrectAnswerID);

                    studentAnswersList.Add(StudentAnswer);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return studentAnswersList;
        }


    }
}






