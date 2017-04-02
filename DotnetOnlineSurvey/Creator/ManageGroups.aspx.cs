using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BALOnlineSurvey.BL;

using BALOnlineSurvey.BE;
using System.Collections.Specialized;
using MySql.Data.MySqlClient;

public partial class Creator_ManageGroups : System.Web.UI.Page
{
    private BLProfileManagement objBLProfileManagement = new BLProfileManagement();
    private DataTable objDataTable = new DataTable();
    private CheckBox chkDelete;
    public void GetData()
    {
        try
        {
            BE_Group objGroup = new BE_Group();

            objDataTable = objBLProfileManagement.RetrieveGroups(objGroup);
            gvShowAllGroups.DataSource = objDataTable;
            gvShowAllGroups.DataBind();
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

    protected void gvShowAllGroups_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int GroupId = Convert.ToInt32(gvShowAllGroups.Rows[e.RowIndex].Cells[1].Text);
            Response.Write(GroupId);
            BE_Group objGroup = new BE_Group();
            objDataTable = objBLProfileManagement.DeleteContactsUnderGroup(GroupId);
            gvShowAllGroups.DataSource = objDataTable;
            gvShowAllGroups.DataBind();
            GetData();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }

    }

    protected void gvShowAllGroups_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvShowAllGroups.PageIndex = e.NewPageIndex;
            gvShowAllGroups.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void gvShowAllGroups_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            StringCollection objCollection = new StringCollection();
            string s = string.Empty;
            string strId = string.Empty;
            for (int i = 0; i < gvShowAllGroups.Rows.Count; i++)
            {
                CheckBox cb = (CheckBox)gvShowAllGroups.Rows[i].Cells[0].FindControl("chkSelect");
                if (cb != null & cb.Checked)
                {

                    //strId += gvShowAllGroups.DataKeys[gvrow.RowIndex].Value.ToString();
                    strId += gvShowAllGroups.Rows[i].Cells[1].Text;
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

                int result = objBLProfileManagement.DeleteGroups(s);
                gvShowAllGroups.DataSource = objDataTable;
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
    protected void gvShowAllGroups_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string GroupId = gvShowAllGroups.Rows[e.NewEditIndex].Cells[1].Text.ToString();
        Response.Redirect("http://localhost:34832/Creator/EditGroup.aspx?groupId=" + GroupId);
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetSuggestions(string prefixText)
    {
       

            MySqlDataReader dr = BLProfileManagement.GetGroupName(prefixText);
            List<string> custList = new List<string>();
            string custItem = string.Empty;
            //int i=0;
            while (dr.Read())
            {
                // custItem = AutoCompleteExtender.CreateAutoCompleteItem(dr[2].ToString(),i.ToString());
                //
                custList.Add(dr["groupName"].ToString());

            }
            return custList;

       
    }
    protected void txtAutoSearch2_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string prifixtext = txtAutoSearch2.Text;
            DataTable dt = BLProfileManagement.GetSearchbyGroup(prifixtext);
            gvShowAllGroups.DataSource = dt;
            gvShowAllGroups.DataBind();
            txtAutoSearch2.Focus();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}