using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BALOnlineSurvey.BL;
using BALOnlineSurvey.BE;
using Google.GData.Extensions;
using MySql.Data.MySqlClient;

public partial class Creator_Default : System.Web.UI.Page
{
    private BLProfileManagement objBLProfileManagement = new BLProfileManagement();
    private DataTable objDataTable = new DataTable();
    private int groupId;

    public void GetData()
    {
        try
        {

            groupId = Convert.ToInt32(Request.QueryString["groupId"]);
            objDataTable = objBLProfileManagement.GetContactsUnselectedGroups(groupId);
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
            string emailId = gvShowAllContacts.Rows[e.RowIndex].Cells[2].Text.ToString();
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

    protected void addtogroup_click(object sender, EventArgs e)
    {
        try
        {
            string emailId = "Arujun";
            groupId = 2;

            DataTable dt = BLProfileManagement.AddtoGroup(groupId, emailId);
            Console.WriteLine("groupmember is inserted successfully");
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetSuggestions(string prefixText)
    {
        try
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
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void txtAutoSearch1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string prifixtext = txtAutoSearch1.Text;
            DataTable dt = BLProfileManagement.GetSearchGroup(prifixtext);
            gvShowAllContacts.DataSource = dt;
            gvShowAllContacts.DataBind();
            txtAutoSearch1.Focus();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}