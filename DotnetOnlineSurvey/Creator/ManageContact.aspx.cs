using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BALOnlineSurvey.BE;
using BALOnlineSurvey.BL;
using System.Data;
using MySql.Data.MySqlClient;
using DALOnlineSurvey;
using System.Collections.Specialized;
public partial class Creator_ManageContact : System.Web.UI.Page
{
    private BLProfileManagement objBLProfileManagement = new BLProfileManagement();
    private DataTable objDataTable = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            if (!IsPostBack)
                GetData();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    public void GetData()
    {
        try
        {
            MySqlDataReader objMySqlDataReader = objBLProfileManagement.GetContactsForManage();
            DataTable dt = new DataTable();
            dt.Load(objMySqlDataReader);
            gvShowAllContacts.DataSource = dt;
            gvShowAllContacts.DataBind();
            ViewState["GridData"] = dt;
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void gvShowAllContacts_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void gvShowAllContacts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvShowAllContacts.PageIndex = e.NewPageIndex;
            gvShowAllContacts.DataSource = (DataTable)ViewState["GridData"];
            gvShowAllContacts.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void gvShowAllContacts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvShowAllContacts.EditIndex = -1;
            gvShowAllContacts.DataSource = (DataTable)ViewState["GridData"];
            gvShowAllContacts.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void gvShowAllContacts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string emailId = gvShowAllContacts.Rows[e.RowIndex].Cells[3].Text.ToString();
            BE_user objBEUser = new BE_user();
            objBEUser.SurveyTakerEmailID = emailId;
            Response.Write(emailId);
            int result = objBLProfileManagement.DeleteSelectedContact(objBEUser);
            if (result >= 1)
            {
                GetData();
            }
            else
                Response.Write("NotDone");
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

    protected void gvShowAllContacts_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            string Userid = gvShowAllContacts.Rows[e.RowIndex].Cells[1].Text;
            TextBox UserName = (TextBox)gvShowAllContacts.Rows[e.RowIndex].Cells[2].Controls[0];
            TextBox SurveyTakerID = (TextBox)gvShowAllContacts.Rows[e.RowIndex].Cells[3].Controls[0];
            TextBox AlternativeEmailID = (TextBox)gvShowAllContacts.Rows[e.RowIndex].Cells[4].Controls[0];
            TextBox MobileNumber = (TextBox)gvShowAllContacts.Rows[e.RowIndex].Cells[5].Controls[0];

            BE_user objBEUser = new BE_user();
            objBEUser.SurveyTakerId = int.Parse(Userid);
            objBEUser.SurveyTakerName = UserName.Text.ToString();
            objBEUser.SurveyTakerEmailID = SurveyTakerID.Text.ToString();
            objBEUser.AlternateEmailID = AlternativeEmailID.Text.ToString();
            objBEUser.MobileNumber = MobileNumber.Text.ToString();
            int result = objBLProfileManagement.EditSelectedContact(objBEUser);
            if (result >= 1)
            {
                gvShowAllContacts.EditIndex = -1;
                GetData();

            }
            else
                Response.Write("NotDone");
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void gvShowAllContacts_DataBound(object sender, EventArgs e)
    {

    }

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
    protected void btnSaveChanges_Click(object sender, EventArgs e)
    {

    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetSuggestions(string prefixText)
    {
            MySqlDataReader dr = BLProfileManagement.GetEmailId(prefixText);
            List<string> custList = new List<string>();
            string custItem = string.Empty;
            //int i=0;
            while (dr.Read())
            {
                // custItem = AutoCompleteExtender.CreateAutoCompleteItem(dr[2].ToString(),i.ToString());
                //
                custList.Add(dr["surveyTakerEmailID"].ToString());

            }
            return custList;
       
    }

    protected void txtAutoSearch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string prifixtext = txtAutoSearch.Text;
            DataTable dt = BLProfileManagement.GetSearch(prifixtext);
            gvShowAllContacts.DataSource = dt;
            gvShowAllContacts.DataBind();
            txtAutoSearch.Focus();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            StringCollection objCollection = new StringCollection();
            string s = string.Empty;
            string strId = string.Empty;
            for (int i = 0; i < gvShowAllContacts.Rows.Count; i++)
            {
                CheckBox cb = (CheckBox)gvShowAllContacts.Rows[i].Cells[0].FindControl("chkSelect");
                if (cb != null & cb.Checked)
                {

                    //strId += gvShowAllGroups.DataKeys[gvrow.RowIndex].Value.ToString();
                    strId += gvShowAllContacts.Rows[i].Cells[1].Text;
                    // strId = gvShowAllGroups.Rows[].Cells[1].Text;
                    s += strId + ',';
                    strId = string.Empty;
                    // objCollection.Add(s);
                }

            }
            if (s != null)
            {
                int n = s.LastIndexOf(',');
                s = s.Remove(n, 1);

                int result = objBLProfileManagement.DeleteContacts(s);
                gvShowAllContacts.DataSource = objDataTable;
                //gvShowAllGroups.DataBind();
                GetData();
                //gvShowAllGroups.DataBind();
            }
            else
            {
                lblMessage.Text = "please select any row to delete";
            }

        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}