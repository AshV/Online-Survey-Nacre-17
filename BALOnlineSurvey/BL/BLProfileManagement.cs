using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.OleDb;
using Google.GData.Contacts;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.Contacts;
using System.Data;
using DALOnlineSurvey;
using BALOnlineSurvey.BE;

namespace BALOnlineSurvey.BL
{
    public class BLProfileManagement
    {
        public String[] GetExcelSheetNames(string excelFileName)
        {
            OleDbConnection objConnOleDbConnection = null;
            DataTable dt = null;
            string str = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelFileName +
                         ";Extended Properties='Excel 12.0 xml;HDR=YES;'";
            try
            {
                objConnOleDbConnection = new OleDbConnection(str);
                objConnOleDbConnection.Open();
                dt = objConnOleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dt == null)
                {
                    return null;
                }
                String[] excelSheets = new String[dt.Rows.Count];
                int i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();
                    i++;
                }
                return excelSheets;
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetExcelData(string excelFileName, string excelSheetName)
        {
            try
            {
                string str = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelFileName +
                             ";Extended Properties='Excel 12.0 xml;HDR=YES;'";
                OleDbConnection con = new OleDbConnection(str);
                OleDbCommand cmd = new OleDbCommand("Select * From [" + excelSheetName + "]", con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();

                da.Fill(ds, "" + excelSheetName + "");
                DataTable dt = ds.Tables[0];
                return dt;
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetGmailContacts(string Uname, string UPassword)
        {
            string App_Name = "MyNetwork Web Application!";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataColumn C2 = new DataColumn();
            try
            {
                C2.DataType = Type.GetType("System.String");
                C2.ColumnName = "EmailID";
                dt.Columns.Add(C2);
                RequestSettings rs = new RequestSettings(App_Name, Uname, UPassword);
                rs.AutoPaging = true;
                ContactsRequest cr = new ContactsRequest(rs);
                Feed<Contact> f = cr.GetContacts();
                foreach (Contact t in f.Entries)
                {
                    foreach (EMail email in t.Emails)
                    {
                        DataRow dr1 = dt.NewRow();
                        dr1["EmailID"] = email.Address.ToString();
                        dt.Rows.Add(dr1);
                    }
                }
                //ds.Tables.Add(dt);
                //return ds;
                return dt;
            }
            catch
            {
                throw;
            }
        }

        public int DeleteSelectedContact(BE_user objtblUserDelete)
        {
            try
            {
                MySqlParameter[] parm = new MySqlParameter[1];

                parm[0] = new MySqlParameter("_surveyTakerEmailId", objtblUserDelete.SurveyTakerEmailID);
                int number = ConnectionFactory.ExecuteUpdate("sp_DeleteSelectedContact",
                    System.Data.CommandType.StoredProcedure, parm);
                return number;
            }
            catch
            {
                throw;
            }
        }

        public MySqlDataReader GetContactsForManage()
        {
            try
            {
                MySqlDataReader objMySqlDataReader =
                    ConnectionFactory.ExecuteCommand("sp_selectContactsInManageContacts",
                        System.Data.CommandType.StoredProcedure);
                return objMySqlDataReader;
            }
            catch
            {
                throw;
            }
        }

        public int EditSelectedContact(BE_user objtblUserEdit)
        {
            try
            {
                MySqlParameter[] parm = new MySqlParameter[5];
                parm[0] = new MySqlParameter("_surveyTakerId", objtblUserEdit.SurveyTakerId);
                parm[1] = new MySqlParameter("_surveyTakerName", objtblUserEdit.SurveyTakerName);
                parm[2] = new MySqlParameter("_surveyTakerEmailId", objtblUserEdit.SurveyTakerEmailID);
                parm[3] = new MySqlParameter("_alternateEmailID", objtblUserEdit.AlternateEmailID);
                parm[4] = new MySqlParameter("_mobileNumber", objtblUserEdit.MobileNumber);
                int number = ConnectionFactory.ExecuteUpdate("sp_EditSelectedContact",
                    System.Data.CommandType.StoredProcedure, parm);
                return number;
            }
            catch
            {
                throw;
            }
        }

        public DataTable RetrieveGroups(BE.BE_Group objGroup)
        {
            try
            {

                DataTable objDataTable = new DataTable();
                MySqlDataReader dr = DALOnlineSurvey.ConnectionFactory.ExecuteCommand("sp_RetrieveGroup",
                    CommandType.StoredProcedure);
                objDataTable.Load(dr);
                return objDataTable;
            }
            catch
            {
                throw;
            }

        }

        public int DeleteGroups(string groupValues)
        {
            try
            {
                MySqlParameter[] param = new MySqlParameter[1]
                {
                    new MySqlParameter("_groupId", groupValues),

                };
                DataTable objDataTable = new DataTable();
                int result = DALOnlineSurvey.ConnectionFactory.ExecuteUpdate("sp_DeleteGroups",
                    CommandType.StoredProcedure, param);

                return result;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Groups not found", ex);
            }
        }
         public DataTable getGroups(int surveyCreatorID)
        {
            try
            {
                MySqlParameter[] p = new MySqlParameter[1];
                p[0] = new MySqlParameter("_surveyCreatorId", surveyCreatorID);
                MySqlDataReader dr = ConnectionFactory.ExecuteCommand("sp_GetGroups", CommandType.StoredProcedure, p);
                DataTable dt = new DataTable();
                dt.Load(dr);

                return dt;
            }
            catch
            {
                throw;
            }
        }

        public int CreateGroup(BE.BE_Group Groupobj)
        {
            try
            {
                MySqlParameter[] pram = new MySqlParameter[3]
            {

                new MySqlParameter("_GroupName", Groupobj.GroupName),
                new MySqlParameter("_GroupDesc", Groupobj.GroupDescription),

                new MySqlParameter("_SurveyCreatorID", Groupobj.SurveyCreatorID)


            };

                return ConnectionFactory.ExecuteUpdate("sp_CreateGroup", CommandType.StoredProcedure, pram);

            }
            catch { throw; }
        }

        public MySqlDataReader GetGroupName(BE.BE_Group Groupobj)
        {
            try
            {
                MySqlParameter[] pram = new MySqlParameter[1]
            {

                new MySqlParameter("_surveyCreatorID", Groupobj.SurveyCreatorID)
            };

                MySqlDataReader dr = ConnectionFactory.ExecuteCommand("sp_getGroupName", CommandType.StoredProcedure, pram);
                return dr;
            }
            catch { throw; }
        }

        public static MySqlDataReader GetEmailId(string surveyTakerEmailId)
        {
            try
            {
                MySqlParameter[] objParameter = new MySqlParameter[1];
                objParameter[0] = new MySqlParameter("_SurveyTakerEmailID", surveyTakerEmailId);


                MySqlDataReader objDataReader = ConnectionFactory.ExecuteCommand("sp_searchEmailId ",
                    CommandType.StoredProcedure, objParameter);
                return objDataReader;
            }
            catch
            {
                throw;
            }
        }
        public static MySqlDataReader GetCreatorCompanyId(int surveyCreatorId)
        {
            try
            {
                MySqlParameter[] objParameter = new MySqlParameter[1];
                objParameter[0] = new MySqlParameter("_surveyCreator", surveyCreatorId);


                MySqlDataReader objDataReader = ConnectionFactory.ExecuteCommand("sp_GetCreatorCompanyID ",
                    CommandType.StoredProcedure, objParameter);
                return objDataReader;
            }
            catch
            {
                throw;
            }
        }
        public static MySqlDataReader GetGroupName(string GroupName)
        {
            try
            {
                MySqlParameter[] objParameter = new MySqlParameter[1];
                objParameter[0] = new MySqlParameter("_groupName", GroupName);


                MySqlDataReader objDataReader = ConnectionFactory.ExecuteCommand("sp_GetSearchGroupName ",
                    CommandType.StoredProcedure, objParameter);
                return objDataReader;
            }
            catch 
            {
                throw;
            }
        }

        public static DataTable GetSearch(string prifixtext)
        {
            try
            {
                MySqlParameter[] objParameter = new MySqlParameter[1];
                objParameter[0] = new MySqlParameter("_prifixtext", prifixtext);
                MySqlDataReader objDataReader = ConnectionFactory.ExecuteCommand("sp_searchuserbyEmailId",
                    CommandType.StoredProcedure, objParameter);

                DataTable dt = new DataTable();
                dt.Load(objDataReader);
                return dt;


            }
            catch 
            {
                throw ;
            }
        }
        public static DataTable GetSearchGroup(string prifixtext)
        {
            try
            {
                MySqlParameter[] objParameter = new MySqlParameter[1];
                objParameter[0] = new MySqlParameter("_prifixtext", prifixtext);
                MySqlDataReader objDataReader = ConnectionFactory.ExecuteCommand("sp_GetSearchGroup",
                    CommandType.StoredProcedure, objParameter);

                DataTable dt = new DataTable();
                dt.Load(objDataReader);
                return dt;


            }
            catch 
            {
                throw ;
            }
        }
        public static DataTable AddtoGroup(int groupId, string surveyTakerEmailId)
        {
            try
            {
                MySqlParameter[] objParameter = new MySqlParameter[2];
                objParameter[0] = new MySqlParameter("_groupid", groupId);
                objParameter[1] = new MySqlParameter("_surveyTakerEmailId", surveyTakerEmailId);
                MySqlDataReader objDataReader = ConnectionFactory.ExecuteCommand("sp_InsertDataIntoGroupMember",
                    CommandType.StoredProcedure, objParameter);

                DataTable dt = new DataTable();
                dt.Load(objDataReader);
                return dt;


            }
            catch 
            {
                throw ;
            }
        }
        public int SaveSelectedContact(BE_user obj, int surveyCreatorId)
        {
            try
            {
                MySqlParameter[] parm = new MySqlParameter[2];
                parm[0] = new MySqlParameter("_surveyTakerEmailID", obj.SurveyTakerEmailID);
                parm[1] = new MySqlParameter("_surveyCreatorId", surveyCreatorId);

                DataTable objDataTable = new DataTable();
                MySqlDataReader objDataReader = DALOnlineSurvey.ConnectionFactory.ExecuteCommand("sp_SaveContacts", CommandType.StoredProcedure, parm);
                if (objDataReader.Read())
                    return objDataReader.GetInt32(0);
                else
                    return 0;
            }
            catch 
            {
                throw;
            }
        }
        public static DataTable GetSearchbyGroup(string prifixtext)
        {
            try
            {
                MySqlParameter[] objParameter = new MySqlParameter[1];
                objParameter[0] = new MySqlParameter("_prifixtext", prifixtext);
                MySqlDataReader objDataReader = ConnectionFactory.ExecuteCommand("sp_SearchbyGroupname",
                    CommandType.StoredProcedure, objParameter);

                DataTable dt = new DataTable();
                dt.Load(objDataReader);
                return dt;


            }
            catch {
                throw;
            }
        }
         public int AddContactToGroup(int surveyTakerId, int groupId)
        {
            try
            {
                MySqlParameter[] param = new MySqlParameter[2];
                param[0] = new MySqlParameter("_groupid", groupId);
                param[1] = new MySqlParameter("_surveyTakerId", surveyTakerId);

                int result = DALOnlineSurvey.ConnectionFactory.ExecuteUpdate("sp_AddContactToGroup", CommandType.StoredProcedure, param);

                return result;
            }
            catch 
            {
                throw;
            }
        }
        public int DeleteContacts(string contacts)
        {
            try
            {
                MySqlParameter[] param = new MySqlParameter[1]
                {
                    new MySqlParameter("_contacts", contacts),

                };
                DataTable objDataTable = new DataTable();
                int result = DALOnlineSurvey.ConnectionFactory.ExecuteUpdate("sp_DeleteContacts", CommandType.StoredProcedure, param);

                return result;
            }
            catch 
            {
                throw;
            }
        }
        public DataTable GetContactsUnderGroup(int groupId)
        {
            try
            {
                MySqlParameter[] param = new MySqlParameter[1]
                {
                    new MySqlParameter("_groupId", groupId),

                };
                DataTable objDataTable = new DataTable();
                MySqlDataReader dr = DALOnlineSurvey.ConnectionFactory.ExecuteCommand("sp_EditSelectedGroup",
                    CommandType.StoredProcedure, param);
                objDataTable.Load(dr);
                return objDataTable;
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetContactsUnselectedGroups(int groupId)
        {
            try
            {
                MySqlParameter[] param = new MySqlParameter[1]
                {
                    new MySqlParameter("_groupId", groupId),

                };
                DataTable objDataTable = new DataTable();
                MySqlDataReader dr = DALOnlineSurvey.ConnectionFactory.ExecuteCommand("sp_EditUnselectedGroup",
                    CommandType.StoredProcedure, param);
                objDataTable.Load(dr);
                return objDataTable;
            }
            catch
            {
                throw;
            }
        }
        public DataTable DeleteContactsUnderGroup(int groupId)
        {
            try
            {
                MySqlParameter[] param = new MySqlParameter[1]
                {
                    new MySqlParameter("_groupId", groupId),

                };
                DataTable objDataTable = new DataTable();
                MySqlDataReader dr = DALOnlineSurvey.ConnectionFactory.ExecuteCommand("sp_DeleteContactsUnderGroup",
                    CommandType.StoredProcedure, param);
                objDataTable.Load(dr);
                return objDataTable;
            }
            catch
            {
                throw;
            }

        }
        public DataTable DeleteContactFromGroup(int groupId, int surveyTakerId)
        {
            try
            {
                MySqlParameter[] param = new MySqlParameter[2]
                {
                    new MySqlParameter("_groupId", groupId),
                    new MySqlParameter("_surveyTakerId",surveyTakerId), 


                };
                DataTable objDataTable = new DataTable();
                MySqlDataReader dr = DALOnlineSurvey.ConnectionFactory.ExecuteCommand("sp_DeleteContactFromGroup",
                    CommandType.StoredProcedure, param);
                objDataTable.Load(dr);
                return objDataTable;
            }
            catch
            {
                throw;
            }

        }
        //public DataTable EditContactFromGroup(string surveyTakerEmailId, int SurveyTakerId)
        //{
        //    try
        //    {
        //        MySqlParameter[] param = new MySqlParameter[2]
        //        {
                    
        //            new MySqlParameter("_surveyTakerEmailID",surveyTakerEmailId), 
        //            new MySqlParameter("_surveyTakerId",SurveyTakerId)


        //        };
        //        DataTable objDataTable = new DataTable();
        //        MySqlDataReader dr = DALOnlineSurvey.ConnectionFactory.ExecuteCommand("sp_EditContactFromGroup",
        //            CommandType.StoredProcedure, param);
        //        objDataTable.Load(dr);
        //        return objDataTable;
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //}
    }
}


