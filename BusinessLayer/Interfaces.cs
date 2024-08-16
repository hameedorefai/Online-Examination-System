using System;

namespace BusinessLayer
{
    public class Interfaces
    {

        public interface ICoursesManager
        {
            int AddCourse(string courseName, string courseNo);
            clsCourse GetCourseDetails(int courseID, ref string courseName, ref string courseNo);
            bool UpdateCourseDetails(int courseID, string courseName, string courseNo);
            bool RemoveCourse(int courseID);
        }

        public interface IQuestionTypesManager
        {
            int AddQuestionType(string questionTypeName);
            bool GetQuestionTypeDetails(int questionTypeID, ref string questionTypeName);
            bool UpdateQuestionTypeDetails(int questionTypeID, string questionTypeName);
            bool RemoveQuestionType(int questionTypeID);
        }

        public interface IQuestionsManager
        {
            int AddQuestion(string questionText, int questionTypeID, string correctAnswer, int createdByUserID);
            bool GetQuestionDetails(int questionID, ref string questionText, ref int questionTypeID, ref string correctAnswer, ref int createdByUserID);
            bool UpdateQuestionDetails(int questionID, string questionText, int questionTypeID, string correctAnswer);
            bool RemoveQuestion(int questionID);
        }

        public interface IAnsweringOptionsManager
        {
            int AddOption(int questionID, string optionText);
            bool GetOptionDetails(int questionID, ref int optionID, ref string optionText);
            bool UpdateOptionDetails(int optionID, string optionText, int questionID);
            bool RemoveOption(int optionID);
        }

        public interface IExamsManager
        {
            int AddExam(int courseID, string examType, int createdByUserID);
            bool GetExamDetails(int examID, ref int courseID, ref string examType, ref int createdByUserID);
            bool UpdateExamDetails(int examID, int courseID, string examType);
            bool RemoveExam(int examID);
        }

        public interface IExamQuestionsManager
        {
            int AddExamQuestion(int examID, int questionID);
            /*List<int>*/ void GetExamQuestionIDs(int examID);
            bool RemoveExamQuestion(int examQuestionID);
        }

        public interface IResultsManager
        {
            int AddResult(int examID, int userID, int score, DateTime completionTime);
            bool GetResultDetails(int resultID, ref int examID, ref int userID, ref int score, ref DateTime completionTime);
            void RemoveResult(int resultID);
        }

        public interface IStudentAnswersManager
        {
            int AddStudentAnswer(int resultID, int questionID, string selectedOption, int userID, int timeSpent);
            bool GetStudentAnswerDetails(int answerID, ref int resultID, ref int questionID, ref string selectedOption, ref int userID, ref int timeSpent);
            bool UpdateStudentAnswerDetails(int answerID, int resultID, int questionID, string selectedOption, int userID, int timeSpent);
            bool RemoveStudentAnswer(int answerID);
        }
    }
}