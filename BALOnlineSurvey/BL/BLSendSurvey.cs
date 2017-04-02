using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.IO;
using MySql.Data.MySqlClient;
using DALOnlineSurvey;
using BALOnlineSurvey.BE;
using System.Data;

namespace BALOnlineSurvey.BL
{
    public class BLSendSurvey
    {
        static MySqlDataReader dr;
        static DataTable dt;
        /// <summary>
        /// gives all surveys created by the surveyCreator
        /// </summary>
        /// <param name="surveyCreatorID">surveyCreatorID is taken as parameter</param>
        /// <returns>datatable containing all surveys and its details</returns>
        public DataTable selectAllSurvey(int surveyCreatorID)
        {
            try
            {
                MySqlParameter[] p = new MySqlParameter[1];
                p[0] = new MySqlParameter("_surveyCreatorID", surveyCreatorID);
                dr = ConnectionFactory.ExecuteCommand("sp_selectAllSurvey", CommandType.StoredProcedure, p);
                dt = new DataTable();
                dt.Load(dr);
                return dt;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// gets all the group names from tbl_group for a particular user
        /// </summary>
        /// <param name="surveyCreatorID">the surveyCreatorID is passed as argument</param>
        /// <returns>data table containing all groups and its Ids</returns>
        public DataTable getGroups(int surveyCreatorID)
        {
            try
            {
                MySqlParameter[] p = new MySqlParameter[1];
                p[0] = new MySqlParameter("_surveyCreatorId", surveyCreatorID);
                dr = ConnectionFactory.ExecuteCommand("sp_GetGroups", CommandType.StoredProcedure, p);
                dt = new DataTable();
                dt.Load(dr);

                return dt;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Calls sp_SelectAllContacts and gets all the contacts from tbl_user in MySqlDataReader format
        /// </summary>
        /// <returns>MySqlDataReader converted to DataTable and Returned</returns>
        public DataTable selectAllContacts(int surveyCreatorID)
        {
            try
            {
                MySqlParameter[] p = new MySqlParameter[1];
                p[0] = new MySqlParameter("_surveyCreatorId", surveyCreatorID);
                dr = ConnectionFactory.ExecuteCommand("sp_SelectAllContacts", CommandType.StoredProcedure, p);
                dt = new DataTable();
                dt.Load(dr);
                return dt;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// gives all contacts present in a particular group
        /// </summary>
        /// <param name="groupID">selected groupId is Passed as argument</param>
        /// <returns>returns all contacts in datatable format</returns>
        public DataTable SelectByGroup(int groupID)
        {
            try
            {
                MySqlParameter[] p = new MySqlParameter[1];
                p[0] = new MySqlParameter("_groupID", groupID);
                dr = ConnectionFactory.ExecuteCommand("sp_selectContactsByGroup", CommandType.StoredProcedure, p);
                dt = new DataTable();
                dt.Load(dr);
                return dt;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Inserts contact details to tbl_user through excel sheet
        /// </summary>
        /// <param name="objuser">object of User class and surveyCreatorID</param>
        /// <returns>integer on successful execution of insert command</returns>
        public int InsertContactByExcel(BE_user objuser, int surveyCreatorID)
        {
            try
            {
            MySqlParameter[] p = new MySqlParameter[5];
            p[0] = new MySqlParameter("_surveyTakerEmailID", objuser.SurveyTakerEmailID);
            p[1] = new MySqlParameter("_mobileNumber", objuser.MobileNumber);
            p[2] = new MySqlParameter("_alternateEmailID", objuser.AlternateEmailID);
            p[3] = new MySqlParameter("_userName", objuser.SurveyTakerName);
            p[4] = new MySqlParameter("_surveyCreatorID", surveyCreatorID);

            
                dr = ConnectionFactory.ExecuteCommand("sp_InsertContactByExcel", CommandType.StoredProcedure, p);
                dr.Read();
                return dr.GetInt32(0);
            }
            catch
            {
                throw;
            }
        }

        public int CheckSurveyTaker(int surveyId, int surveyTakerID)
        {
            try
            {
            MySqlParameter[] SpParam = new MySqlParameter[2];
            SpParam[0] = new MySqlParameter("_surveyId", surveyId);
            SpParam[1] = new MySqlParameter("_surveyTakerId", surveyTakerID);
            
                dr = ConnectionFactory.ExecuteStoredProcedure("sp_CheckSurveyTaker", SpParam);
                dr.Read();
                return dr.GetInt32(0);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Inserts emailIds to tbl_user entered by user manually
        /// </summary>
        /// <param name="emailID">emailIds entered in the textbox</param>
        /// <returns>integer on successful execution of insert command</returns>
        public int InsertContactManually(string emailID, int surveyCreatorID)
        {
            try
            {
            MySqlParameter[] SpParam = new MySqlParameter[2];
            SpParam[0] = new MySqlParameter("_surveyTakerEmailID", emailID);
            SpParam[1] = new MySqlParameter("_surveyCreatorID", surveyCreatorID);

            
                dr = ConnectionFactory.ExecuteCommand("sp_InsertContactManually", CommandType.StoredProcedure, SpParam);
                dr.Read();
                return dr.GetInt32(0);
            }
            catch
            {
                throw;
            }
        }


        public List<int> SelectSurveyTakers(int surveyId)
        {
            try
            {
            MySqlParameter[] SpParam = new MySqlParameter[1];
            SpParam[0] = new MySqlParameter("_surveyId", surveyId);
            
                dr = ConnectionFactory.ExecuteCommand("sp_SelectSurveyTaker", CommandType.StoredProcedure, SpParam);
                dt = new DataTable();
                dt.Load(dr);
                List<int> li = new List<int>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    li.Add(Convert.ToInt32(dt.Rows[i][0]));
                }
                return li;
            }
            catch
            {
                throw;
            }
        }

        public void SendSurvey(List<BE_SurveyTaker> liSurveyTaker, int surveyID, string form)
        {
            try
            {
                EmailForm objEmailForm = new EmailForm();
                string surveyTitle = "";
                string surveyDescription = "";
                MySqlDataReader dr1;
                dr1 = GetSurveyTitle(surveyID);
                if (dr1.Read())
                {
                    surveyTitle = dr1.GetString(0);
                    surveyDescription = dr1.GetString(1);
                }

                objEmailForm.Subject = "Survey: " + surveyTitle;
                objEmailForm.From = "nacresms@Gmail.com";
                objEmailForm.Pwd = "Batch17Dotnet";

                StreamReader reader = new StreamReader(form);
                string myString = reader.ReadToEnd();
                //string myString = form;

                myString = myString.Replace("$$Title$$", surveyTitle);
                myString = myString.Replace("$$Description$$", surveyDescription);
                //objEmailForm.Body = "Hi,\nWe are conducting a survey titled as: '" + surveyTitle + "' \nDescription: " + surveyDescription + "\n\nWe would like to have your response on this.\nFor accessing the survey click on below link:\n\nhttp://www.nacresms.com/TakeSurvey.aspx?id=" + obj.UniqueID + "\n\nNote: Do not forword this mail.";

                foreach (BE_SurveyTaker obj in liSurveyTaker)
                {
                    if (1 == CheckSurveyTaker(obj.SurveyId, obj.SurveyTakerID))
                    {
                        objEmailForm.Body = myString.Replace("$$UniqueId", obj.UniqueID);
                        objEmailForm.To = obj.SurveyTakerEmailID;
                        if (SendEmailUsingGmail(objEmailForm))
                        {
                            SaveSurveyTaker(obj);
                        }
                    }
                }

            }
            catch
            {
                throw;
            }
        }

        private int SaveSurveyTaker(BE_SurveyTaker obj)
        {
            try
            {
            MySqlParameter[] SpParam = new MySqlParameter[3];
            SpParam[0] = new MySqlParameter("_surveyID", obj.SurveyId);
            SpParam[1] = new MySqlParameter("_surveyTakerID", obj.SurveyTakerID);
            SpParam[2] = new MySqlParameter("_uniqueID", obj.UniqueID);

            
                int rs = ConnectionFactory.ExecuteUpdate("sp_SendSurvey", CommandType.StoredProcedure, SpParam);
                return rs;
            }
            catch
            {
                throw;
            }
        }

        private MySqlDataReader GetSurveyTitle(int surveyID)
        {
            try
            {
                MySqlParameter[] p = new MySqlParameter[1];
                p[0] = new MySqlParameter("_surveyID", surveyID);
                dr = ConnectionFactory.ExecuteCommand("sp_GetSurveyTitle", CommandType.StoredProcedure, p);
                return dr;
            }
            catch
            {
                throw;
            }
        }

        private bool SendEmailUsingGmail(EmailForm objEmailForm)
        {
            try
            {


                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new NetworkCredential(objEmailForm.From, objEmailForm.Pwd);
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                MailMessage message = new MailMessage();
                message.From = new MailAddress(objEmailForm.From);
                message.To.Add(objEmailForm.To);
                message.Subject = objEmailForm.Subject;
                message.Body = objEmailForm.Body;
                message.IsBodyHtml = true;
                //MailAddress ma = new MailAddress("sachu17@gmail.com");
                //message.Sender = new MailAddress("sachu17@gmail.com");
                //message.From = new MailAddress("sachu1708@gmail.com");
                smtp.Send(message);
                return true;
            }
            catch 
            {
                throw;
            }
        }
    }
}
