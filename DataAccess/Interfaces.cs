using System;
using System.Collections.Generic;

namespace DataAccess
{
    internal class Interfaces
    {
    }
    public interface ICoursesData
    {
        int InsertCourse(string courseName, string courseNo);
        bool GetCourse(int courseID, ref string courseName, ref string courseNo);
        bool UpdateCourse(int courseID, string courseName, string courseNo);
        bool DeleteCourse(int courseID);
    }
    public interface IQuestionTypesData
    {
        int InsertQuestionType(string questionTypeName);
        bool GetQuestionType(int questionTypeID, ref string questionTypeName);
        bool UpdateQuestionType(int questionTypeID, string questionTypeName);
        bool DeleteQuestionType(int questionTypeID);
    }
    public interface IQuestionsData
    {
        int InsertQuestion(string questionText, int questionTypeID, string correctAnswer, int createdByUserID);
        bool GetQuestion(int questionID, ref string questionText, ref int questionTypeID, ref string correctAnswer, ref int createdByUserID);
        bool UpdateQuestion(int questionID, string questionText, int questionTypeID, string correctAnswer);
        bool DeleteQuestion(int questionID);
    }
    public interface IAnsweringOptionsData
    {
        int InsertOption(int questionID, string optionText);
        bool GetOptionByQuestionID(int questionID, ref int  optionID, ref string optionText);
        bool UpdateOption(int optionID, string optionText);
        bool DeleteOption(int optionID);
    }
    public interface IExamsData
    {
        int InsertExam(int CourseID,string ExamType,int CreatedByUserID);
        bool GetExam(int examID, ref int CourseID, ref string ExamType,ref int CreatedByUserID);
        bool UpdateExam(int examID,  int CourseID,  string ExamType);
        bool DeleteExam(int examID);
        /*
         ExamID
         CourseID  
         CreatedByUserID
         ExamType
         */
    }
    public interface IExamQuestionsData
    {
        int InsertExamQuestion(int examID, int questionID);
        List<int> GetExamQuestionsIDs(int examID);
        bool DeleteExamQuestion(int examQuestionID);
    }
    public interface IResultsData
    {
        int InsertResult(int ExamID,int UserID,int Score, DateTime CompletionTime);
        bool GetResult(int resultID, ref int ExamID,ref int UserID, ref int Score, ref DateTime CompletionTime);
       // void UpdateResult(ref Result result);
        bool DeleteResult(int resultID);
        /*
         ResultID INT
         ExamID INT
         UserID INT
         Score INT
         CompletionTime DATETIME
         */
    }
    public interface IStudentAnswersData
    {
        int InsertStudentAnswer(int ResultID,int QuestionID,string SelectedOption,int UserID, int TimeSpent);
        bool GetStudentAnswer(int answerID, ref int ResultID, ref int QuestionID, ref string SelectedOption, ref int UserID, ref int TimeSpent);
      //  void UpdateStudentAnswer();
        bool DeleteStudentAnswer(int answerID);

        /*
         
         AnswerID INT P
         ResultID INT N
         QuestionID INT
         SelectedOption
         UserID INT NOT
         TimeSpent INT,
         
         
         
         */
    }


}
