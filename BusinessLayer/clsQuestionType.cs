using DataAccess;

namespace BusinessLayer
{
    public class clsQuestionType
    {
        public int QuestionTypeID { get; set; }
        public string QuestionTypeName { get; set; }

        public clsQuestionType(int questionTypeID, string questionTypeName)
        {
            QuestionTypeID = questionTypeID;
            QuestionTypeName = questionTypeName;
        }

        public int AddNewQuestionType(string questionTypeName)
        {
            return clsQuestionTypeData.AddNewQuestionType(questionTypeName);
        }

        public clsQuestionType GetQuestionTypeDetails(int questionTypeID)
        {
            string questionTypeName = "";

         //   bool found = clsQuestionTypeData.GetQuestionType(questionTypeID, ref questionTypeName);

            if (/*found*/ false)
            {
                return new clsQuestionType(questionTypeID, questionTypeName);
            }

            return null;
        }

        public bool UpdateQuestionType(int questionTypeID, string questionTypeName)
        {
            return false;
           // return clsQuestionTypeData.UpdateQuestionType(questionTypeID, questionTypeName);
        }

        public bool DeleteQuestionType(int questionTypeID)
        {
            return clsQuestionTypeData.DeleteQuestionType(questionTypeID);
        }
    }
}
