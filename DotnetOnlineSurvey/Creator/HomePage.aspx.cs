using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using MySql.Data.MySqlClient;
using ASPSnippets.FaceBookAPI;
using System.Data;
using BALOnlineSurvey.BE;
using BALOnlineSurvey.BL;



public partial class Creator_HomePage : System.Web.UI.Page
{
    string creatorName = "";
    string creatorUserName="";
    int creatorID;

    protected void Page_PreInit(object sender, EventArgs e)
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
    protected void Page_Load(object sender, EventArgs e)
    {
        BLRegistration objBLRegister = new BLRegistration();
        BE_Creator objBECreator = new BE_Creator();
        if (Session["HomeCreatorUserName"] != null)
        {
            string creatorUserName = Session["HomeCreatorUserName"].ToString();
            lblCreatorName.Text = "Welcome " + creatorUserName;
            DataTable dt = new DataTable();
            // MySqlDataReader dr = objBLRegister.GetCreatorID(objBECreator);
            MySqlDataReader dr = objBLRegister.GetCreatorIDByUserName(creatorUserName);
            dt.Load(dr);
            creatorID = Convert.ToInt32(dt.Rows[0][0].ToString());
            Session["HomeCreatorId"] = creatorID;
        }
    }
    protected void lnkFbSignOut_Click(object sender, EventArgs e)
    {
        Response.Redirect("Welcome.aspx");
    }
}