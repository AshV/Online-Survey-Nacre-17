using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using MySql.Data.MySqlClient;
using System.Web.Configuration;
using BALOnlineSurvey.BL;

public partial class Creator_PreviewSurvey : System.Web.UI.Page
{
    MySqlConnection objMySqlConnection = new MySqlConnection(WebConfigurationManager.ConnectionStrings["Database"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string surveyId = Request.QueryString["surveyId"];
            BLHtmlControlCreator objHtmlControlCreator = new BLHtmlControlCreator();
            MySqlCommand objMySqlCommand = new MySqlCommand("Select q.questionTypeID, q.questionID, sq.required, q.question, sq.minVal, sq.maxVal, sq.intrval, q.option1, q.option2, q.option3, q.option4, q.option5, q.option6, q.option7, q.option8 from tbl_surveyquestion sq inner join tbl_question q on sq.questionID= q.questionID AND surveyId=" + surveyId, objMySqlConnection);
            objMySqlConnection.Open();
            MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader();
            while (objMySqlDataReader.Read())
            {
                ltAllQuestion.Text += objHtmlControlCreator.GetHtml(Convert.ToInt32(objMySqlDataReader[0]), Convert.ToInt32(objMySqlDataReader[1]), Convert.ToBoolean(objMySqlDataReader[2]), objMySqlDataReader[3].ToString(), Convert.ToInt32(objMySqlDataReader[4]), Convert.ToInt32(objMySqlDataReader[5]), Convert.ToInt32(objMySqlDataReader[6]), objMySqlDataReader[7].ToString(), objMySqlDataReader[8].ToString(), objMySqlDataReader[9].ToString(), objMySqlDataReader[10].ToString(), objMySqlDataReader[11].ToString(), objMySqlDataReader[12].ToString(), objMySqlDataReader[13].ToString(), objMySqlDataReader[14].ToString());
            }
            objMySqlConnection.Close();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}