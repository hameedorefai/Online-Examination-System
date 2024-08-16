using DataAccess;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BusinessLayer
{
    public class clsExam
    {
        public int ExamID { get; set; }
        public int CourseID { get; set; }
        public int CreatedByUserID { get; set; }
        public string ExamType { get; set; }
        public QuestionDTO questionDTO { get; set; }

        // public ExamDTO ExamDTO { get { return new ExamDTO(examDTO, questionDTO, answeringOptionDTO); } }
        public clsExam(int courseID, int createdByUserID, string examType)
        {
            CourseID = courseID;
            CreatedByUserID = createdByUserID;
            ExamType = examType;
        }
        public clsExam(int examID, int courseID, int createdByUserID, string examType)
        {
            ExamID = examID;
            CourseID = courseID;
            CreatedByUserID = createdByUserID;
            ExamType = examType;
        }
        
        static private int _AddNewExamInfo(AddExamDTO examDTO)
        {
            return clsExamData.AddNewExam(examDTO);
        }
        static public ExamDTO GetExamInfoByExamID(int examID)
        {
            return clsExamData.GetExamByExamID(examID);
        }
        static public ExamDTO GetFullExamByExamID(int examID)
        {
            ExamDTO exam = clsExamData.GetExamByExamID(examID);
            exam.QuestionsList = clsExamQuestions.GetQuestionsByExamID(exam.ExamID);
            return exam;
        }
        static public bool UpdateExamInfoByExamID(UpdateExamDTO examDTO)
        {
            return clsExamData.UpdateExamInfoByExamID(examDTO);
        }
        static public bool DeleteExam(int examID)
        {
            var Exam = GetFullExamByExamID(examID);
            clsExamQuestions.DeleteExamQuestionByExamID(examID);

            foreach(QuestionDTO question in Exam.QuestionsList)
            {
                clsQuestion.DeleteQuestion(question.QuestionID);
            }
            return clsExamData.DeleteExam(examID);

        }
        static public List<ExamDTO> GetExamsInfoForCourseID(int CourseID)
        {
            return clsExamData.GetExamsInfoForCourseID(CourseID);
        }
        static public List<ExamDTO> GetFullExamsForCourseID(int CourseID)
        {
            List<ExamDTO> smallerList = GetExamsInfoForCourseID(CourseID);
            List<ExamDTO> MainList = new List<ExamDTO>();


            foreach (ExamDTO exam in smallerList)
            {
                List<QuestionDTO> questionsList = clsExamQuestions.GetQuestionsByExamID(exam.ExamID);

                MainList.Add(new ExamDTO(exam, questionsList));
            }
            return MainList;

        }
        static public int AddNewFullExam(AddExamDTO FullExam)
        {

            // here you need to add all exam info,
            // questions with its correct answers, and all questions options.

            if (clsGlobal.CurrentUser == null || clsGlobal.CurrentUser.RoleName.ToLower() == "standard")
                return -1;
            else
            {
                FullExam.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            }
            FullExam.ExamID = _AddNewExamInfo(FullExam);

            foreach (AddQuestionDTO question in FullExam.AddQuestionsList)
            {
                question.CreatedByUserID = FullExam.CreatedByUserID;
                int QuestinoID = clsQuestion.AddNewQuestion(question);
                clsExamQuestions.AddNewExamQuestion(FullExam.ExamID, QuestinoID);
                

                foreach (AnsweringOptionDTO option in question.optionsDTO)
                {
                    option.QuestionID = question.QuestionID;
                    /* int optionID = */
                    clsAnsweringOption.AddNewOption(option);
                }
            }
            return FullExam.ExamID;
        }



    }
}
