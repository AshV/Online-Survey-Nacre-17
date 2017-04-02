using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BALOnlineSurvey.BE;
using BALOnlineSurvey.BL;
using System.Data;


public partial class Creator_SendByGroups : System.Web.UI.Page
{
    BLSendSurvey objSendSurveyBL = new BLSendSurvey();

    int creatorID;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //Session["SendSurveyCreatorID"] = 100;
        creatorID = Convert.ToInt32(Session["HomeCreatorId"]);
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            lblError.Text = "";
            if (Session["CurrentSurvey"] == null)
            {
                Response.Redirect("SendSurvey.aspx");
            }
            if (!IsPostBack)
            {
                BindGroups();
                foreach (ListItem li in clbGroups.Items)
                {
                    li.Selected = true;
                }
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
            List<int> groups = new List<int>();
            DataTable dt = null;
            foreach (ListItem li in clbGroups.Items)
            {
                if (li.Selected == true)
                {
                    if (dt == null)
                        dt = objSendSurveyBL.SelectByGroup(Convert.ToInt32(li.Value));
                    else
                    {
                        DataTable tempDt = objSendSurveyBL.SelectByGroup(Convert.ToInt32(li.Value));
                        dt.Merge(tempDt);
                    }
                }
            }
            //  dt = RemoveDuplicatesRecords(dt);

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

    private DataTable RemoveDuplicatesRecords(DataTable dt)
    {
        
            var UniqueRows = dt.AsEnumerable().Distinct(DataRowComparer.Default);
            DataTable dt2 = UniqueRows.CopyToDataTable();
            return dt2;
        

    }

    private void BindGroups()
    {
        try
        {
            DataTable dt = objSendSurveyBL.getGroups(creatorID);
            clbGroups.DataSource = dt;
            clbGroups.DataTextField = "groupName";
            clbGroups.DataValueField = "groupID";
            clbGroups.DataBind();
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

    protected void clbGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            cbSelectAll.Checked = true;
            foreach (ListItem li in clbGroups.Items)
            {
                if (li.Selected == false)
                    cbSelectAll.Checked = false;
            }
            BindContacts();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void cbSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (cbSelectAll.Checked == true)
            {
                foreach (ListItem li in clbGroups.Items)
                {
                    li.Selected = true;
                }
            }
            else
            {
                foreach (ListItem li in clbGroups.Items)
                {
                    li.Selected = false;
                }
            }
            BindContacts();
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
            //   if (e.row.rowtype == datacontrolrowtype.datarow | e.row.rowtype == datacontrolrowtype.header)
            //  {
            //hide eno column using cells[0] column
            // e.row.cells[1].visible = false;
            // }

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