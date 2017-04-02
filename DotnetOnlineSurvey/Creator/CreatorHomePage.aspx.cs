using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using ASPSnippets.FaceBookAPI;
using System.Data;
using System.Web.Services;
using BALOnlineSurvey.BE;
using BALOnlineSurvey.BL;

public partial class Creator_CreatorHomePage : System.Web.UI.Page
{
    string creatorName = "";
    string creatorUserName = "";
    int creatorID;
    static int surveyCreatorID; // =100; 
    static int categoryID = 0, groupID = 0, statusID = 0;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["HomeCreatorId"] != null)
        {

        }
        else
        {
            if (Session["LoginCreatorID"] != null)
            {
                creatorID = Convert.ToInt32(Session["LoginCreatorID"].ToString());
                Session["HomeCreatorId"] = creatorID;
                creatorUserName = Session["LoginCreatorUserName"].ToString();
                Session["HomeCreatorUserName"] = creatorUserName;

            }
            else if (Session["RegisterCreatorId"] != null)
            {
                creatorID = Convert.ToInt32(Session["RegisterCreatorId"].ToString());
                Session["HomeCreatorId"] = creatorID;
                creatorUserName = Session["RegisterCreatorUserName"].ToString();
                Session["HomeCreatorUserName"] = creatorUserName;
            }
            else if (Session["FBUserId"] != null)
            {
                Session["HomeUserEmailID"] = Session["FBemailId"];
                Session["HomeCreatorName"] = Session["FBname"];
                Session["HomeCreatorUserName"] = Session["FBUserName"];
            }
            else if (Session["GmilemailId"] != null)
            {
                Session["HomeUserEmailID"] = Session["GmilemailId"];
                creatorName = Session["GmailName"].ToString();
                Session["HomeCreatorUserName"] = creatorName;
            }
            else
                Response.Redirect("~/LoginRegister.aspx");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            BLRegistration objBLRegister = new BLRegistration();
            BE_Creator objBECreator = new BE_Creator();

            //1st time from fb/gmail
            if (Session["FBUserId"] != null || Session["GmilemailId"] != null)
            {
                string creatorUserName = Session["HomeCreatorUserName"].ToString();
                lblCreatorName.Text = "Welcome " + creatorUserName;
            }
                // directly login
                //register need to maintain
            else if (Session["HomeCreatorUserName"] != null)
            {
                string creatorUserName = Session["HomeCreatorUserName"].ToString();
                lblCreatorName.Text = "Welcome " + creatorUserName;
                DataTable dt = new DataTable();
                // MySqlDataReader dr = objBLRegister.GetCreatorID(objBECreator);
                MySqlDataReader dr = objBLRegister.GetCreatorIDByUserName(creatorUserName);
                dt.Load(dr);
                creatorID = Convert.ToInt32(dt.Rows[0][0].ToString());
                surveyCreatorID = creatorID;
                Session["HomeCreatorId"] = creatorID;
                
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    public class SurveyDetails
    {
        public string surveyID { get; set; }
        public string surveyName { get; set; }
        public int totalResponses { get; set; }
        public string creationDate { get; set; }
        public int surveyStatus { get; set; }
    }

    [WebMethod]
    public static List<SurveyDetails> DisplayChartData()
    {
        MySqlDataReader objdr = BLReport.GetSurveyData(surveyCreatorID, categoryID, groupID,null, null);
        List<SurveyDetails> dataList = new List<SurveyDetails>();
        while (objdr.Read())
        {
            SurveyDetails objDisplayChartData = new SurveyDetails();
            objDisplayChartData.surveyName = objdr[1].ToString();
            objDisplayChartData.totalResponses = Convert.ToInt32(objdr[2]);
            dataList.Add(objDisplayChartData);
        }
        return dataList;
    }
}