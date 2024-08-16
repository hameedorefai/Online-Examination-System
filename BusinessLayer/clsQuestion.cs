using DataAccess;
using System.Data;
using System.Collections.Generic;
namespace BusinessLayer
{
    public class clsQuestion
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public string QuestionTypeName { get; set; }
        public string CorrectAnswer { get; set; }
        public int CreatedByUserID { get; set; }
        public QuestionDTO questionDTO { get { return new QuestionDTO(this.QuestionID,this.QuestionText); } }

        public clsQuestion()
        {
            QuestionID = -1;
            QuestionText = "";
            QuestionTypeName = "";
            CorrectAnswer = "";
            CreatedByUserID = -1;

        }
        public clsQuestion(int questionID, string questionText, string QuestionTypeName)
        {
            QuestionID = questionID;
            QuestionText = questionText;
            this.QuestionTypeName = QuestionTypeName;
        }

        static public int AddNewQuestion(AddQuestionDTO QuestionDTO)
        {
            return clsQuestionData.AddNewQuestion(QuestionDTO);
        }

       static public bool UpdateQuestion(UpdateQuestionDTO questionDTO)
        {
            return clsQuestionData.UpdateQuestionInfo(questionDTO);
        }

       static public bool DeleteQuestion(int questionID)
        {
            List<AnsweringOptionDTO> options  = clsAnsweringOption.GetOptionsByQuestionID(questionID);
            foreach(AnsweringOptionDTO option in options)
            {
                clsAnsweringOption.DeleteOptionByOptionID(option.OptionID);
            }
            return clsQuestionData.DeleteQuestion(questionID);
        }
    
        static public int GetCorrectAnswerByQuestionID(int questionID)
        {
            return clsQuestionData.GetCorrectAnswerByQuestionID(questionID);
        }

    }
}
