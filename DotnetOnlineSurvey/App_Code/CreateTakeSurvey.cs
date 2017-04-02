using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;


using MySql.Data.MySqlClient;
using System.Web.Configuration;
using System.IO;
using System.Data.OleDb;
using System.Data;
using BALOnlineSurvey.BL;
using BALOnlineSurvey.BE;
using DALOnlineSurvey;

/// <summary>
/// Summary description for CreateTakeSurvey
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class CreateTakeSurvey : System.Web.Services.WebService
{

    public CreateTakeSurvey()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public bool TakeUserResponse(string surveyId, string userId, string questionId, string response)
    {

            MySqlConnection objMySqlConnection = new MySqlConnection(WebConfigurationManager.ConnectionStrings["Database"].ConnectionString);
            MySqlCommand objMySqlCommand = new MySqlCommand("Insert into tbl_surveyresponse(surveyID, userID, questionID, Response, Date) values(" + surveyId + "," + userId + "," + questionId + ",'" + response + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "')", objMySqlConnection);
            objMySqlConnection.Open();
            int i = objMySqlCommand.ExecuteNonQuery();
            objMySqlConnection.Close();
            MySqlCommand objMySqlCommand1 = new MySqlCommand("Update tbl_surveytakers set completed=1 where surveyID=" + surveyId + " AND surveyTakerID=" + userId, objMySqlConnection);
            objMySqlConnection.Open();
            objMySqlCommand1.ExecuteNonQuery();
            objMySqlConnection.Close();
            if (i > 0)
                return true;
            else
                return false;
                
    }

    [WebMethod]
    public bool UpdateSurveyName(int survey, string title)
    {
        MySqlParameter[] objMySqlParameter = new MySqlParameter[2];
        objMySqlParameter[0] = new MySqlParameter("_surveyId", survey);
        objMySqlParameter[1] = new MySqlParameter("_newName", title);

        MySqlDataReader objMySqlDataReader = ConnectionFactory.ExecuteStoredProcedure("sp_UpdateSurveyName", objMySqlParameter);
        return true;
    }

    [WebMethod]
    public string GetCategoryWiseQuestions(int categoryId)
    {

        string surveyQuestionHtml = "";
        BLGenerateQuestionList objBLGenerateQuestionList = new BLGenerateQuestionList();
        MySqlConnection objMySqlConnection = new MySqlConnection(WebConfigurationManager.ConnectionStrings["Database"].ConnectionString);
        MySqlCommand objMySqlCommand = new MySqlCommand("Select q.questionTypeID, q.question, sq.required, sq.minVal, sq.maxVal, sq.intrval, q.option1, q.option2, q.option3, q.option4, q.option5, q.option6, q.option7, q.option8 from tbl_question q inner join tbl_surveyquestion sq on q.questionID =sq.questionID where q.categoryID=" + categoryId, objMySqlConnection);
        objMySqlConnection.Open();

        //Parsing Datareader for  QuestionsText
        MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader();
        int QuestionSrNo = 0;
        string[] Options = new string[8];
        string Question = "";
        string Required = "";
        int minVal = 0;
        int maxVal = 0;
        int intrval = 0;
        while (objMySqlDataReader.Read())
        {
            QuestionSrNo++;
            Question = objMySqlDataReader[1].ToString();
            Required = objMySqlDataReader[2].ToString();
            minVal = Convert.ToInt32(objMySqlDataReader[3]);
            maxVal = Convert.ToInt32(objMySqlDataReader[4]);
            intrval = Convert.ToInt32(objMySqlDataReader[5]);
            Options[0] = objMySqlDataReader[6].ToString();
            Options[1] = objMySqlDataReader[7].ToString();
            Options[2] = objMySqlDataReader[8].ToString();
            Options[3] = objMySqlDataReader[9].ToString();
            Options[4] = objMySqlDataReader[10].ToString();
            Options[5] = objMySqlDataReader[11].ToString();
            Options[6] = objMySqlDataReader[12].ToString();
            Options[7] = objMySqlDataReader[13].ToString();

            switch (Convert.ToInt32(objMySqlDataReader[0]))
            {
                case 1:
                    surveyQuestionHtml += "<br/><br/><input class='selectQuestion' type='checkbox' />Select Below Question To Add" + objBLGenerateQuestionList.Single(QuestionSrNo, Question, Options, Required);
                    break;
                case 2:
                    surveyQuestionHtml += "<br/><br/><input class='selectQuestion' type='checkbox' />Select Below Question To Add" + objBLGenerateQuestionList.Multiple(QuestionSrNo, Question, Options, Required);
                    break;
                case 3:
                    surveyQuestionHtml += "<br/><br/><input class='selectQuestion' type='checkbox' />Select Below Question To Add" + objBLGenerateQuestionList.Text(QuestionSrNo, Question, Required);
                    break;
                case 4:
                    surveyQuestionHtml += "<br/><br/><input class='selectQuestion' type='checkbox' />Select Below Question To Add" + objBLGenerateQuestionList.Ranking(QuestionSrNo, Question, minVal, maxVal, intrval, Required);
                    break;
            }
        }
        return surveyQuestionHtml;
    }

    //[WebMethod]
    //public string GetCategoryQuestion(string questionId)
    //{
    //    string surveyQuestionHtml = "";
    //    BLGenerateQuestionList objBLGenerateQuestionList = new BLGenerateQuestionList();
    //    MySqlConnection objMySqlConnection = new MySqlConnection(WebConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
    //    MySqlCommand objMySqlCommand = new MySqlCommand("Select questionTypeID, questionID, question, option1, option2, option3, option4, option5, option6, option7, option8 from tbl_question where questionId=" + questionId, objMySqlConnection);
    //    objMySqlConnection.Open();

    //    //Parsing Datareader for  QuestionsText
    //    MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader();
    //    int QuestionSrNo = 0;
    //    string[] Options = new string[8];
    //    string Question = "";
    //    string Required = "";
    //    int minVal = 0;
    //    int maxVal = 0;
    //    int intrval = 0;
    //    while (objMySqlDataReader.Read())
    //    {
    //        QuestionSrNo++;
    //        Question = objMySqlDataReader[1].ToString();
    //        Required = objMySqlDataReader[2].ToString();
    //        minVal = Convert.ToInt32(objMySqlDataReader[3]);
    //        maxVal = Convert.ToInt32(objMySqlDataReader[4]);
    //        intrval = Convert.ToInt32(objMySqlDataReader[5]);
    //        Options[0] = objMySqlDataReader[6].ToString();
    //        Options[1] = objMySqlDataReader[7].ToString();
    //        Options[2] = objMySqlDataReader[8].ToString();
    //        Options[3] = objMySqlDataReader[9].ToString();
    //        Options[4] = objMySqlDataReader[10].ToString();
    //        Options[5] = objMySqlDataReader[11].ToString();
    //        Options[6] = objMySqlDataReader[12].ToString();
    //        Options[7] = objMySqlDataReader[13].ToString();

    //        switch (Convert.ToInt32(objMySqlDataReader[0]))
    //        {
    //            case 1:
    //                surveyQuestionHtml += objBLGenerateQuestionList.Single(QuestionSrNo, Question, Options, Required);
    //                break;
    //            case 2:
    //                surveyQuestionHtml += objBLGenerateQuestionList.Multiple(QuestionSrNo, Question, Options, Required);
    //                break;
    //            case 3:
    //                surveyQuestionHtml += objBLGenerateQuestionList.Text(QuestionSrNo, Question, Required);
    //                break;
    //            case 4:
    //                surveyQuestionHtml += objBLGenerateQuestionList.Ranking(QuestionSrNo, Question, minVal, maxVal, intrval, Required);
    //                break;
    //        }
    //    }
    //    return surveyQuestionHtml;
    //}

    [WebMethod]
    public bool InsertQuestion(string categoryID, string creatorID, string surveyID, string questionTypeId, string questionSrNo, string question, string required, string minVal, string maxVal, string intrval, string options)
    {
        string[] option = new string[8];
        string[] tempoption = options.Substring(1, options.Length - 1).Split(',');
        for (int j = 0; j < 8; j++)
            option[j] = null;
        for (int i = 0; i < tempoption.Length; i++)
            option[i] = tempoption[i];
        //MySqlConnection objMySqlConnection = new MySqlConnection(WebConfigurationManager.ConnectionStrings["Database"].ConnectionString);
        //MySqlCommand objMySqlCommand = new MySqlCommand("Select Max(questionID)+1 from tbl_question", objMySqlConnection);
        //objMySqlConnection.Open();
        //int qID = Convert.ToInt32(objMySqlCommand.ExecuteScalar());
        //objMySqlCommand = new MySqlCommand(string.Format("Insert into tbl_question(questionTypeID, questionId, question, categoryID, creatorID, option1, option2, option3, option4, option5, option6, option7, option8)values({0},{1},'{2}',{3},{4},'{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')", questionTypeId, qID, question, categoryID, creatorID, option[0], option[1], option[2], option[3], option[4], option[5], option[6], option[7]), objMySqlConnection);
        //objMySqlCommand.ExecuteNonQuery();

        //objMySqlCommand = new MySqlCommand(string.Format("Insert into tbl_surveyquestion(questionID,QuestionSrNo,surveyID,required,minVal,maxVal,intrval)values({0},{1},{2},{3},{4},{5},{6})", qID, questionSrNo, surveyID, required, minVal, maxVal, intrval), objMySqlConnection);
        //objMySqlCommand.ExecuteNonQuery();

        //Modified
        MySqlConnection objMySqlConnection = new MySqlConnection(WebConfigurationManager.ConnectionStrings["Database"].ConnectionString);
        MySqlCommand objMySqlCommand;
        objMySqlConnection.Open();

        objMySqlCommand = new MySqlCommand(string.Format("Insert into tbl_question(questionTypeID, question, categoryID, creatorID, option1, option2, option3, option4, option5, option6, option7, option8)values({0},'{1}',{2},{3},'{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')", questionTypeId, question, categoryID, creatorID, option[0], option[1], option[2], option[3], option[4], option[5], option[6], option[7]), objMySqlConnection);
        objMySqlCommand.ExecuteNonQuery();
        //objMySqlConnection.Close();

        //objMySqlConnection.Open();
        //objMySqlCommand = new MySqlCommand("Select Max(questionID) from tbl_question where creatorID=" + creatorID, objMySqlConnection);
        //int qID = Convert.ToInt32(objMySqlCommand.ExecuteScalar());
        //objMySqlConnection.Close();


        //objMySqlConnection.Open();
        objMySqlCommand = new MySqlCommand(string.Format("Insert into tbl_surveyquestion(questionid,QuestionSrNo,surveyID,required,minVal,maxVal,intrval)values(GetMaxQid(),{0},{1},{2},{3},{4},{5})", questionSrNo, surveyID, required, minVal, maxVal, intrval), objMySqlConnection);
        objMySqlCommand.ExecuteNonQuery();

        //End Modified


        objMySqlConnection.Close();
        return true;
    }

    [WebMethod]
    public string GetExcelQuestions(string base64FileString, string fileExtension)
    {
        string questionTxt = "";
        string dstFile = "";
        if (fileExtension == "xls")
            dstFile = Server.MapPath("~/file.xls");
        if (fileExtension == "xlsx")
            dstFile = Server.MapPath("~/file.xlsx");
        try
        {
            byte[] dataBuffer = Convert.FromBase64String(base64FileString);
            using (FileStream objFileStream = new FileStream(dstFile, FileMode.Create, FileAccess.Write))
            {
                if (dataBuffer.Length > 0)
                {
                    objFileStream.Write(dataBuffer, 0, dataBuffer.Length);
                }
            }
            OleDbConnection con = null;
            if (fileExtension == "xls")
            {
                con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dstFile + ";Extended Properties=Excel 8.0;");

            }
            else if (fileExtension == "xlsx")
            {
                con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dstFile + ";Extended Properties=Excel 12.0;");
            }
            con.Open();
            DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string getExcelSheetName = dt.Rows[0]["Table_Name"].ToString();
            OleDbCommand ExcelCommand = new OleDbCommand(@"SELECT * FROM [" + getExcelSheetName + @"]", con);

            OleDbDataReader dr = ExcelCommand.ExecuteReader();

            int QuestionNo = 0;
            string Question = "";
            int minVal = 0;
            int maxVal = 0;
            int intrval = 0;
            string[] Options = new string[8];
            String Required = "";
            bool valid;

            BLGenerateQuestionList objGenerateExcelQuestion = new BLGenerateQuestionList();
            while (dr.Read())
            {
                QuestionNo++;
                valid = true;
                if (dr[0].ToString() != "" && dr[1].ToString() != "" && (dr[13].ToString() == "True" || dr[13].ToString() == "False"))
                {
                    Question = dr[1].ToString();
                    string s = dr[13].ToString();
                    Required = Convert.ToBoolean(dr[13]) ? "checked='checked'" : "";
                    Options[0] = dr[5].ToString();
                    Options[1] = dr[6].ToString();
                    Options[2] = dr[7].ToString();
                    Options[3] = dr[8].ToString();
                    Options[4] = dr[9].ToString();
                    Options[5] = dr[10].ToString();
                    Options[6] = dr[11].ToString();
                    Options[7] = dr[12].ToString();

                    switch (Convert.ToInt32(dr[0]))
                    {
                        case 1:
                            valid = IsOptionValid(Options);
                            break;
                        case 2:
                            valid = IsOptionValid(Options);
                            break;
                        case 3:
                            QuestionNo = 3;
                            break;
                        case 4:
                            if (dr[2].ToString() != "" && dr[3].ToString() != "" && dr[4].ToString() != "")
                            {
                                minVal = Convert.ToInt32(dr[2]);
                                maxVal = Convert.ToInt32(dr[3]);
                                intrval = Convert.ToInt32(dr[4]);
                            }
                            else
                                valid = false;
                            break;
                    }
                    if (valid)
                    {
                        switch (Convert.ToInt32(dr[0]))
                        {
                            case 1:
                                questionTxt += objGenerateExcelQuestion.Single(QuestionNo, Question, Options, Required);
                                break;
                            case 2:
                                questionTxt += objGenerateExcelQuestion.Multiple(QuestionNo, Question, Options, Required);
                                break;
                            case 3:
                                questionTxt += objGenerateExcelQuestion.Text(QuestionNo, Question, Required);
                                break;
                            case 4:
                                questionTxt += objGenerateExcelQuestion.Ranking(QuestionNo, Question, minVal, maxVal, intrval, Required);
                                break;
                        }
                    }
                }
            }
            con.Close();
        }
        catch
        {
            throw;
        }
        return questionTxt;
    }

    [WebMethod]
    public List<BALOnlineSurvey.BE.BE_Category> GetCategoryList()
    {
        List<BALOnlineSurvey.BE.BE_Category> returnCategoryList = new List<BALOnlineSurvey.BE.BE_Category>();
        MySqlConnection objMySqlConnection = new MySqlConnection(WebConfigurationManager.ConnectionStrings["Database"].ConnectionString);
        MySqlCommand objMySqlCommand = new MySqlCommand("Select * from tbl_category", objMySqlConnection);
        objMySqlConnection.Open();
        MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader();
        while (objMySqlDataReader.Read())
        {
            returnCategoryList.Add(new BALOnlineSurvey.BE.BE_Category { CategoryId = Convert.ToInt32(objMySqlDataReader[0]), CategoryName = objMySqlDataReader[1].ToString() });
        }
        objMySqlConnection.Close();
        return returnCategoryList;
    }

    [WebMethod]
    public bool DeleteAllFromSurvey(int surveyId)
    {
        MySqlDataReader objMySqlDataReader = ConnectionFactory.ExecuteStoredProcedure("sp_DeleteAllFromSurvey", new MySqlParameter("_surveyId", surveyId));
        return true;
    }

    [WebMethod]
    public List<BE_Survey> GetExistingSurveyList(int userId)
    {
        List<BE_Survey> returnSurveyList = new List<BE_Survey>();
        MySqlDataReader objMySqlDataReader = ConnectionFactory.ExecuteStoredProcedure("sp_SurveyList", new MySqlParameter("_userId", userId));
        while (objMySqlDataReader.Read())
        {
            returnSurveyList.Add(new BE_Survey { SurveyId = Convert.ToInt32(objMySqlDataReader[0]), Introduction = objMySqlDataReader[1].ToString(), Title = objMySqlDataReader[2].ToString(), SurveyCreatedDate = Convert.ToDateTime(objMySqlDataReader[3]), SurveyExpiryDate = Convert.ToDateTime(objMySqlDataReader[4]), CategoryId = Convert.ToInt32(objMySqlDataReader[5]), SurveyCreatorId = Convert.ToInt32(objMySqlDataReader[6]), SurevyLogo = objMySqlDataReader[7].ToString() });
        }
        return returnSurveyList;
    }


    [WebMethod]
    public string CreateSurvey(string introduction, string title, int categoryID, int surveyCreatorID, string surveyLogo)
    {
        string sId = "";
        MySqlConnection objMySqlConnection = new MySqlConnection(WebConfigurationManager.ConnectionStrings["Database"].ConnectionString);
        MySqlCommand objMySqlCommand = new MySqlCommand("Insert into tbl_survey(surveyCreatedDate,surveyExpiryDate,introduction,title,categoryID,surveyCreatorID,surveyLogo)values(curdate(),AddDate(Curdate(),interval 31 day),'" + introduction + "','" + title + "'," + categoryID + "," + surveyCreatorID + ",'" + surveyLogo + "')", objMySqlConnection);
        objMySqlConnection.Open();
        int res = objMySqlCommand.ExecuteNonQuery();
        objMySqlConnection.Close();
        if (res > 0 ? true : false)
        {
            MySqlConnection objMySqlConnection1 = new MySqlConnection(WebConfigurationManager.ConnectionStrings["Database"].ConnectionString);
            MySqlCommand objMySqlCommand1 = new MySqlCommand("Select Max(surveyID) from tbl_survey where surveyCreatorID=" + surveyCreatorID, objMySqlConnection);
            objMySqlConnection.Open();
            sId = objMySqlCommand1.ExecuteScalar().ToString();
            objMySqlConnection.Close();
        }
        return sId;
    }

    [WebMethod]
    public int GetSurveyId(string uniqueId)
    {
        MySqlDataReader objMySqlDataReader = ConnectionFactory.ExecuteStoredProcedure("sp_GetSurveyId", new MySqlParameter("_uniqueId", uniqueId));
        objMySqlDataReader.Read();
        return Convert.ToInt32(objMySqlDataReader[0]);
    }

    [WebMethod]
    public BE_Survey GetSurveyDetailsById(int surveyId)
    {
        MySqlConnection objMySqlConnection1 = new MySqlConnection(WebConfigurationManager.ConnectionStrings["Database"].ConnectionString);
        MySqlCommand objMySqlCommand1 = new MySqlCommand("Select surveyID,categoryID,introduction,surveyCreatedDate,surveyCreatorID,surveyExpiryDate,surveyLogo,title from tbl_survey where surveyID=" + surveyId, objMySqlConnection1);
        objMySqlConnection1.Open();
        MySqlDataReader objMySqlDataReader = objMySqlCommand1.ExecuteReader();
        objMySqlDataReader.Read();
        BE_Survey objBE_Survey = new BE_Survey { SurveyId = Convert.ToInt32(objMySqlDataReader[0]), CategoryId = Convert.ToInt32(objMySqlDataReader[1]), Introduction = objMySqlDataReader[2].ToString(), SurveyCreatedDate = Convert.ToDateTime(objMySqlDataReader[3]), SurveyCreatorId = Convert.ToInt32(objMySqlDataReader[4]), SurveyExpiryDate = Convert.ToDateTime(objMySqlDataReader[5]), SurevyLogo = objMySqlDataReader[6].ToString(), Title = objMySqlDataReader[7].ToString() };
        //  MySqlDataReader objMySqlDataReader = ConnectionFactory.ExecuteStoredProcedure("sp_GetSurveyDetails", new MySqlParameter("_surveyId", surveyId));
        //   objMySqlDataReader.Read();
        objMySqlConnection1.Close();
        return objBE_Survey;
    }

    //To Check After Retrieving Question from Data Source If Choice Question has at least 2 Options
    public bool IsOptionValid(string[] options)
    {
        int count = 0;
        foreach (string s in options)
        {
            if (s != "")
                count++;
        }
        return count > 1 ? true : false;
    }
}