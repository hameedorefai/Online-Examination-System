using DataAccess;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class clsAnsweringOption
    {
        public int OptionID { get; set; }
        public int QuestionID { get; set; }
        public string OptionText { get; set; }
        public AnsweringOptionDTO answeringOptiofnDTO { get { return new AnsweringOptionDTO(QuestionID, OptionID, OptionText); } }

        private clsAnsweringOption obj = null;
        private clsAnsweringOption CreateObject()
        {
            if (obj != null)
            {
                return null;
            }
            else
          //      obj = new clsAnsweringOption();
            return obj;
        }
        private void TempclsAnsweringOption()
        {
            OptionID = -1;
            QuestionID = -1;
            OptionText = "";


            #region clsTheBestOfTheBest object = Hameedo;
                        #endregion
        
        
        }
        public clsAnsweringOption()
        {
            OptionID = -1;
            QuestionID = -1;
            OptionText = "";
        }
        public clsAnsweringOption(AnsweringOptionDTO answeringOptionDTO)
        {
            OptionID = answeringOptionDTO.OptionID;
            QuestionID = answeringOptionDTO.QuestionID;
            OptionText = answeringOptionDTO.OptionText;
        }

        static public int AddNewOption(AnsweringOptionDTO answeringOptionDTO)
        {
            return clsAnsweringOptionData.AddNewOption(answeringOptionDTO);
        }

        static public List<AnsweringOptionDTO> GetOptionsByQuestionID(int questionID)
        {
            return clsAnsweringOptionData.GetOptionsForQuestion(questionID);
        }

        static public bool UpdateOption(UpdateOptionDTO answeringOptionDTO)
        {
            return clsAnsweringOptionData.UpdateOption(answeringOptionDTO);
        }

       static public bool DeleteOptionByOptionID(int optionID)
        {
            return clsAnsweringOptionData.DeleteOption(optionID);
        }
    }
}
