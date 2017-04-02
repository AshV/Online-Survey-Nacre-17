using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using DALOnlineSurvey;
using BALOnlineSurvey.BE;

namespace BALOnlineSurvey.BL
{
    public class BLRegistration
    {
        public int InsertEmp(BE_Creator objSurveyReg)
        {
            try
            {
                MySqlParameter[] objParam = new MySqlParameter[6];
                objParam[0] = new MySqlParameter("_PcreatorName", objSurveyReg.CreatorName);
                objParam[1] = new MySqlParameter("_Pusername", objSurveyReg.Username);
                objParam[2] = new MySqlParameter("_Ppassword", objSurveyReg.Password);
                objParam[3] = new MySqlParameter("_PemailID", objSurveyReg.EmailID);
                objParam[4] = new MySqlParameter("_PmobileNumber", objSurveyReg.MobileNumber);
                objParam[5] = new MySqlParameter("_PcompanyID", objSurveyReg.CompanyId);
                int i = ConnectionFactory.ExecuteUpdate("sp_InsertCreator", System.Data.CommandType.StoredProcedure, objParam);
                return i;

            }
            catch
            {
                throw;
            }
        }
        public MySqlDataReader GetEmailForFBRegistration(string email)
        {
            string strEmail = email;

            MySqlDataReader objDataReader;

            MySqlParameter[] objMySqlParameter1 = new MySqlParameter[1];
            objMySqlParameter1[0] = new MySqlParameter("_email", strEmail);



            try
            {
                objDataReader = ConnectionFactory.ExecuteStoredProcedure("sp_GetEmailDetails", objMySqlParameter1);

                return objDataReader;
            }
            catch
            {
                throw;
            }


        }
        public MySqlDataReader GetLoginDetails(string UserName, string Password)
        {
            string strUserName = UserName;
            string strPassword = Password;

            //BE_Employee objEmployee1=new BE_Employee();

            MySqlDataReader objDataReader;

            MySqlParameter[] objMySqlParameter1 = new MySqlParameter[2];


            objMySqlParameter1[0] = new MySqlParameter("_UserName", strUserName);
            objMySqlParameter1[1] = new MySqlParameter("_Password", strPassword);


            try
            {
                objDataReader = ConnectionFactory.ExecuteStoredProcedure("sp_GetLoginDetails", objMySqlParameter1);

                return objDataReader;
            }
            catch
            {
                throw;
            }


        }
        public bool ChangePassword(int creatorID, string currentPassword, string newPassword)
        {
            try
            {
                MySqlParameter[] objMySqlParameter ={
                                                  new MySqlParameter("_surveyCreatorID",creatorID),
                                                  new MySqlParameter("_Poldpassword",currentPassword),
                                                  new MySqlParameter("_Pnewpassword",newPassword)
                                              };
                MySqlDataReader objMySqlDataReader = ConnectionFactory.ExecuteStoredProcedure("sp_Changepassword", objMySqlParameter);
                return objMySqlDataReader.Read();
            }
            catch
            {
                throw;
            }
        }
        public MySqlDataReader Forgotpassword(BE_Creator objSurveyReg)
        {
            try
            {
                MySqlParameter[] pram = new MySqlParameter[]
                {                
                new MySqlParameter("_PemailID",objSurveyReg.EmailID)};
                MySqlDataReader objdr = ConnectionFactory.ExecuteStoredProcedure("sp_Forgotpassword", pram);
                return objdr;

            }
            catch
            {
                throw;
            }
        }
        public MySqlDataReader GetCompanyId()
        {
            try
            {
                MySqlDataReader objDataReader = ConnectionFactory.ExecuteCommand("sp_GetCompanyId", System.Data.CommandType.StoredProcedure);
                return objDataReader;
            }
            catch
            {
                throw;
            }
        }
        public MySqlDataReader CheckUsername(BE_Creator objBE)
        {
            try
            {
                MySqlParameter[] pram = new MySqlParameter[]
                {                
                new MySqlParameter("_userName", objBE.Username)};
                MySqlDataReader objdr = ConnectionFactory.ExecuteStoredProcedure("sp_checkCreatorUserName", pram);
                return objdr;
            }
            catch
            {
                throw;
            }
        }
        public MySqlDataReader GetCreatorID(BE_Creator objBE)
        {
            try
            {
                MySqlParameter[] pram = new MySqlParameter[]
                {                
                new MySqlParameter("_emailID", objBE.EmailID)};
                MySqlDataReader objdr = ConnectionFactory.ExecuteStoredProcedure("sp_getCreatorID", pram);
                return objdr;
            }
            catch
            {
                throw;
            }
        }
        public MySqlDataReader GetCreatorIDByUserName(string creatorUserName)
        {
            try
            {
                MySqlParameter[] pram = new MySqlParameter[]
                {                
               new MySqlParameter("_creatorUserName", creatorUserName)};
                //new MySqlParameter("_userName", objBE.Username)};
                MySqlDataReader objdr = ConnectionFactory.ExecuteStoredProcedure("sp_GetCreatorIDFromUName", pram);
                return objdr;
            }
            catch
            {
                throw;
            }
        }
        public MySqlDataReader GetCreatorUserName(int creatorID)
        {
            try
            {
                MySqlParameter[] pram = new MySqlParameter[]
                {                
               new MySqlParameter("_surveyCreatorID", creatorID)};
                //new MySqlParameter("_userName", objBE.Username)};
                MySqlDataReader objdr = ConnectionFactory.ExecuteStoredProcedure("sp_GetCreatorIDFromUName", pram);
                return objdr;
            }
            catch
            {
                throw;
            }
        }
    }
}
