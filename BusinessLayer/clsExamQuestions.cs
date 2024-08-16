using DataAccess;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class clsExamQuestions
    {
        public int ExamQuestionID { get; set; }
        public int ExamID { get; set; }
        public int QuestionID { get; set; }

        public clsExamQuestions()
        {
            ExamQuestionID = -1;
            ExamID = -1;
            QuestionID = -1;
        }
        public clsExamQuestions(int examQuestionID, int examID, int questionID)
        {
            ExamQuestionID = examQuestionID;
            ExamID = examID;
            QuestionID = questionID;
        }

        static public int AddNewExamQuestion(int examID, int questionID)
        {
            return clsExamQuestionsData.AddNewExamQuestion(examID, questionID);
        }

        static public List<QuestionDTO> GetQuestionsByExamID(int examID)
        {
            List<QuestionDTO> questionsList = clsExamQuestionsData.GetExamQuestionsByExamID(examID);
            List<QuestionDTO> FinalquestionsList = new List<QuestionDTO>();
            foreach (QuestionDTO question in questionsList)
            {
                List<AnsweringOptionDTO> optionDTO = new List<AnsweringOptionDTO>();
                optionDTO = clsAnsweringOption.GetOptionsByQuestionID(question.QuestionID);
                FinalquestionsList.Add( new QuestionDTO(question.QuestionID, question.QuestionText, optionDTO));
            }
            return FinalquestionsList;
        }

        static public bool DeleteExamQuestionByExamID(int ExamID)
        {
            return clsExamQuestionsData.DeleteExamQuestionByExamID(ExamID);
        }
    }
}
