using BALOnlineSurvey.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using BALOnlineSurvey.BE;
using BALOnlineSurvey.BL;

public partial class Creator_CreateSurvey : System.Web.UI.Page
{
   
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            BE_Survey objBE_Survey = new BLCreateAndTakeSurvey().GetSurveyDetailsById(Convert.ToInt32(Request.QueryString["surveyID"]));
            Survey_Title.Text = objBE_Survey.Title;
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }
}