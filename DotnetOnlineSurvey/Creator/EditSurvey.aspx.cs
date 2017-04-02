using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BALOnlineSurvey.BE;
using BALOnlineSurvey.BL;


public partial class Creator_EditSurvey : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int surveyID = Convert.ToInt32(Request.QueryString["surveyID"]);
            BLCreateAndTakeSurvey objBLCreateAndTakeSurvey = new BLCreateAndTakeSurvey();
            ltlAllQuestions.Text = objBLCreateAndTakeSurvey.GetQuestionListAccordingToSurvey(surveyID);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}