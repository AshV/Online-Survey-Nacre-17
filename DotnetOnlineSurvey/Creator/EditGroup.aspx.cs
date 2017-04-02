using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BALOnlineSurvey.BL;
using BALOnlineSurvey.BE;
using Google.Contacts;

public partial class Creator_EditGroup : System.Web.UI.Page
{
    private BLProfileManagement objBLProfileManagement = new BLProfileManagement();
    private DataTable objDataTable = new DataTable();
    private int groupId;
    int surveyTakerId;
    public void GetData()
    {
        try
        {

            //  groupId = Convert.ToInt32(Request.QueryString["groupId"]);

            objDataTable = objBLProfileManagement.GetContactsUnderGroup(groupId);
            gvShowAllContacts.DataSource = objDataTable;
            gvShowAllContacts.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            groupId = int.Parse(Request["groupId"]);
            if (!IsPostBack)
                GetData();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void gvShowAllContacts_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvShowAllContacts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            surveyTakerId = Convert.ToInt32(gvShowAllContacts.Rows[e.RowIndex].Cells[1].Text);

            groupId = int.Parse(Request["groupId"]);
            Response.Write(surveyTakerId);
            objDataTable = objBLProfileManagement.DeleteContactFromGroup(groupId, surveyTakerId);
            gvShowAllContacts.DataSource = objDataTable;
            gvShowAllContacts.DataBind();

            GetData();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void gvShowAllContacts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvShowAllContacts.PageIndex = e.NewPageIndex;
            gvShowAllContacts.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void gvShowAllContacts_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvShowAllContacts.EditIndex = e.NewEditIndex;
            GetData();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void gvShowAllContacts_DataBound(object sender, EventArgs e)
    {

    }



    // protected void gvShowAllContacts_RowUpdating(object sender, GridViewUpdateEventArgs e)
    // {

    //     ////string surveyTakerEmailId = gvShowAllContacts.Rows[e.RowIndex].Cells[2].Text.ToString();

    //     ////surveyTakerId = Convert.ToInt32(gvShowAllContacts.Rows[e.RowIndex].Cells[1].Text);

    //     ////BE_user objuser = new BE_user();
    //     ////     Response.Write(surveyTakerId);
    //     ////    Response.Write(surveyTakerEmailId);
    //     ////    objDataTable = objBLProfileManagement.EditContactFromGroup(surveyTakerEmailId, surveyTakerId);
    //     ////    gvShowAllContacts.DataSource = objDataTable;
    //     ////    gvShowAllContacts.DataBind();

    //     ////    GetData();




    //}
    //protected void gvShowAllContacts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //    gvShowAllContacts.EditIndex = -1;
    //    gvShowAllContacts.DataSource = (DataTable)ViewState["GridData"];
    //    gvShowAllContacts.DataBind();
    //}

    protected void gvShowAllContacts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[i].Text == "&nbsp;")
                    {
                        e.Row.Cells[i].Text = "NOT AVAILABLE";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void btnAddGroup_Click(object sender, EventArgs e)
    {
        try
        {
            groupId = Convert.ToInt32(Request.QueryString["groupId"]);
            Response.Redirect("AddToGroup.aspx?groupId=" + groupId);
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}