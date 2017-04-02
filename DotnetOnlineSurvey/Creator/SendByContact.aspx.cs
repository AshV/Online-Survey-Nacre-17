using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BALOnlineSurvey.BE;
using BALOnlineSurvey.BL;
using System.IO;

public partial class Creator_SendByContact : System.Web.UI.Page
{
    BLSendSurvey objSendSurveyBL = new BLSendSurvey();
    int creatorID;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            creatorID = Convert.ToInt32(Session["HomeCreatorId"]);

            if (Session["CurrentSurvey"] == null)
            {
                Response.Redirect("SendSurvey.aspx");
            }
            if (!IsPostBack)
            {
                BindContacts();
            }
            lblMessage.Text = "";
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    private void BindContacts()
    {
        try
        {
            DataTable dt = null;
            //take survey creator id from session here assumed as 100;
            dt = objSendSurveyBL.selectAllContacts(100);

            //take survey id from session here assumed as 1;
            if (dt != null)
            {
                List<int> lst = objSendSurveyBL.SelectSurveyTakers(Convert.ToInt32(Session["CurrentSurvey"]));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (lst.Contains(Convert.ToInt32(dt.Rows[i][0])))
                    {
                        dt.Rows[i].Delete();
                    }
                }
            }
            gvContacts.DataSource = dt;
            gvContacts.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvContacts.PageIndex = e.NewPageIndex;
            BindContacts();
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

    protected void Btn_submit_Click(object sender, EventArgs e)
    {
        BE_SurveyTaker objSurveyTaker = new BE_SurveyTaker();
        try
        {
            bool hasrow = false;
            List<BE_SurveyTaker> liSurveyTaker = new List<BE_SurveyTaker>();
            foreach (GridViewRow row in gvContacts.Rows)
            {
                CheckBox check = (CheckBox)row.FindControl("chkSelect");
                if (check.Checked)
                {
                    hasrow = true;
                    BE_SurveyTaker obj = new BE_SurveyTaker();
                    obj.SurveyTakerID = Convert.ToInt32(row.Cells[1].Text);
                    obj.SurveyTakerEmailID = row.Cells[3].Text;
                    //Take from session
                    obj.SurveyId = Convert.ToInt32(Session["CurrentSurvey"]);
                    obj.UniqueID = Guid.NewGuid().ToString("N");
                    liSurveyTaker.Add(obj);
                }

            }
            if (hasrow == true)
            {
                //Take surveyId from Sission here assumed as 1;

                string mailForm = Server.MapPath("~/Files/DemoMail.html");
                objSendSurveyBL.SendSurvey(liSurveyTaker, Convert.ToInt32(Session["CurrentSurvey"]), mailForm);
                BindContacts();
                lblMessage.Text = "Survey sent to selected recipient.";
            }
            else
                lblMessage.Text = "Select atleast one recipient.";
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void gvContacts_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}