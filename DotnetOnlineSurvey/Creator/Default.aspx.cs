using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using BALOnlineSurvey.BE;
using BALOnlineSurvey.BL;

public partial class Creator_Default : System.Web.UI.Page
{
    BLRegistration objGetEmailForFBRegistration = new BLRegistration();
    MySqlDataReader objDataReader;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            Session["HomeCreatorId"] = 100;
            if (Session["HomeCreatorId"] == null)
            {

                if (Session["HomeUserEmailID"] != null)
                {
                    string getGmailMail = Session["HomeUserEmailID"].ToString();
                    objDataReader = objGetEmailForFBRegistration.GetEmailForFBRegistration(getGmailMail);

                }

                if (objDataReader.Read())
                {

                }
                else
                {
                    Response.Redirect("~/Creator/FaceBookDetails.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(@"<script language='javascript'>alert('The following errors have occurred: \n" + ex.Message + " .');</script>");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}