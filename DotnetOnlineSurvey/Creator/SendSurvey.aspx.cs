using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BALOnlineSurvey.BE;
using BALOnlineSurvey.BL;
using MySql.Data.MySqlClient;
using System.Data;

public partial class Creator_SendSurvey : System.Web.UI.Page
{
    BE_user objUser = new BE_user();
    BLSendSurvey objSendSurvey = new BLSendSurvey();
    int creatorID;
    BLRegistration objGetEmailForFBRegistration = new BLRegistration();
    MySqlDataReader objDataReader;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
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
        try
        {
            lblError.Text = "";
            creatorID = Convert.ToInt32(Session["HomeCreatorId"]);
            if (!IsPostBack)
            {
                BindAllSurvey();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    public void BindAllSurvey()
    {
        try
        {
            DataTable dtAllSurvey = objSendSurvey.selectAllSurvey(creatorID);
            if (dtAllSurvey.Rows.Count > 0)
            {
                GridView1.DataSource = dtAllSurvey;
                GridView1.DataBind();
            }
            else //no survey found
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showSendSurveyAlert", "showSendSurveyAlert('No Survey Created.');", true);
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }


    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");
                e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //   if (e.row.rowtype == datacontrolrowtype.datarow | e.row.rowtype == datacontrolrowtype.header)
            //  {
            //hide eno column using cells[0] column
            // e.row.cells[1].visible = false;
            // }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "surveyID")
            {
                Button btn = (Button)e.CommandSource;
                Session["CurrentSurvey"] = e.CommandArgument.ToString();
                string s = Session["CurrentSurvey"].ToString();
                Response.Redirect("SendByGroups.aspx");
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindAllSurvey();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    
}