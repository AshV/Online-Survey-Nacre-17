using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Globalization;
using MySql.Data.MySqlClient;
using DALOnlineSurvey;

namespace BALOnlineSurvey.BL
{
    public class BLReport
    {
        public static MySqlDataReader GetSurveyData(int surveyCreatorID, int categoryID, int groupID, string fromDate, string toDate)
        {
            try
            {
                DateTime day1, day2;
                if (fromDate != null || toDate != null)
                {
                    day1 = Convert.ToDateTime(fromDate);
                    day2 = Convert.ToDateTime(toDate);
                    fromDate = day1.ToString("yyyy-MM-dd");
                    toDate = day2.ToString("yyyy-MM-dd");

                }
                MySqlParameter[] objGetSurveyData = new MySqlParameter[5];
                objGetSurveyData[0] = new MySqlParameter("_surveyCreatorID", surveyCreatorID);
                objGetSurveyData[1] = new MySqlParameter("_categoryID", categoryID);
                objGetSurveyData[2] = new MySqlParameter("_groupID", groupID);
                objGetSurveyData[3] = new MySqlParameter("_fromDate", fromDate);
                objGetSurveyData[4] = new MySqlParameter("_toDate", toDate);
                MySqlDataReader objdr = ConnectionFactory.ExecuteCommand("sp_DisplaySurveyDetails", CommandType.StoredProcedure, objGetSurveyData);
                return objdr;
            }
            catch { throw; }
        }
        public static MySqlDataReader GetSurveyQuestions(int surveyID)
        {
            try
            {
                MySqlParameter[] objGetSurveyQuestions = new MySqlParameter[1];
                objGetSurveyQuestions[0] = new MySqlParameter("_surveyID", surveyID);
                MySqlDataReader objdr = ConnectionFactory.ExecuteCommand("sp_GetSurveyQuestions", CommandType.StoredProcedure, objGetSurveyQuestions);
                return objdr;
            }
            catch { throw; }
        }
        public static MySqlDataReader GetSurveyResponsesToQuestions(int surveyID, int questionID)
        {
            try
            {
                MySqlParameter[] objGetSurveyResponsesToQuestions = new MySqlParameter[2];
                objGetSurveyResponsesToQuestions[0] = new MySqlParameter("_surveyID", surveyID);
                objGetSurveyResponsesToQuestions[1] = new MySqlParameter("_questionID", questionID);
                MySqlDataReader objdr = ConnectionFactory.ExecuteCommand("sp_GetResponsesToQuestions", CommandType.StoredProcedure, objGetSurveyResponsesToQuestions);
                return objdr;
            }
            catch { throw; }
        }
        public static MySqlDataReader GetCategories()
        {
            try
            {
                MySqlDataReader objdr = ConnectionFactory.ExecuteCommand("sp_GetCategories", CommandType.StoredProcedure);
                return objdr;
            }
            catch { throw; }
        }
        public static MySqlDataReader GetGroupNames(int surveyCreatorID)
        {
            try
            {
                MySqlParameter[] objGetGroupNames = new MySqlParameter[1];
                objGetGroupNames[0] = new MySqlParameter("_surveyCreatorID", surveyCreatorID);
                MySqlDataReader objdr = ConnectionFactory.ExecuteCommand("sp_GetGroupNames", CommandType.StoredProcedure, objGetGroupNames);
                return objdr;
            }
            catch { throw; }
        }
    }
}
