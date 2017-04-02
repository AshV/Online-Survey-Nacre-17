using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BALOnlineSurvey.BE;
using BALOnlineSurvey.BL;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.IO;

public partial class Creator_AddContacts : System.Web.UI.Page
{
    BLProfileManagement objBLProfileManagement = new BLProfileManagement();
    DataTable dt1 = new DataTable();
    DataTable dt2 = new DataTable();
    int surveyCreatorId ;

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
        { lblEror.Text = ex.Message; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int surveyCreatorId = Convert.ToInt32(Session["HomeCreatorId"].ToString());
            //divEdit.Visible = true;
            if (!IsPostBack)
            {
                btnAddToGroup.Visible = false;
                btnSaveToDatabase.Visible = false;
            }
            BindGroups();
            lblMessages.Text = "";
        }
        catch (Exception ex)
        { lblEror.Text = ex.Message; }
    }
    private void BindGroups()
    {
        try
        {
            //Get creator id from session
            DataTable dt = objBLProfileManagement.getGroups(surveyCreatorId);
            ddlGroup.DataSource = dt;
            ddlGroup.DataTextField = "groupName";
            ddlGroup.DataValueField = "groupID";
            ddlGroup.DataBind();
        }
        catch (Exception ex)
        {
            lblEror.Text = ex.Message;
        }
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        string userName = txtUserName.Text;
        string password = txtPassword.Text;
        dt1 = (DataTable)ViewState["DataTable"];
        try
        {
            if (dt1 == null)
            {
                dt1 = objBLProfileManagement.GetGmailContacts(userName, password);

                //validate data
                DataTable _ValidateData = checkEmpty_DataTable(dt1);

                gvShowAllContacts.DataSource = dt1;
                gvShowAllContacts.DataBind();
                ViewState["DataTable"] = dt1;

                //give color to rows in gridview
                ValidateRowColor();
            }
            else
            {
                dt1 = (DataTable)ViewState["DataTable"];
                dt2 = objBLProfileManagement.GetGmailContacts(userName, password);
                dt1.Merge(dt2, true);

                //validate data
                DataTable _ValidateData = checkEmpty_DataTable(dt1);

                gvShowAllContacts.DataSource = dt1;
                gvShowAllContacts.DataBind();
                ViewState["DataTable"] = dt1;
                dt2 = null;

                //give color to rows in gridview
                ValidateRowColor();
            }
        }
        catch(Exception ex)
        {
            lblEror.Text = ex.Message;
        }
        btnSaveToDatabase.Visible = true;
        btnAddToGroup.Visible = true;
    }

    //give color to rows in gridview
    public void ValidateRowColor()
    {
        try
        {
            List<string> li_Email = (List<string>)ViewState["currentList"];

            if (li_Email.Count > 0)
            {
                foreach (GridViewRow row in gvShowAllContacts.Rows)
                {
                    //DataRow dr = dtU.NewRow();
                    Label gvEmail = (Label)row.FindControl("lblEmailId");

                    //check emailID, assign color
                    foreach (string emails in li_Email)
                    {
                        if (gvEmail.Text == emails)
                        {
                            //row.BackColor = Color.Red;
                            row.Cells[1].BackColor = System.Drawing.ColorTranslator.FromHtml("#E18383");
                            break;
                        }
                    }
                }
            }
        }
        catch { }
    }

    //remove space, and validate data
    public DataTable checkEmpty_DataTable(DataTable dt)
    {
        
            List<string> li_EmailId = new List<string>();

            DataTable table = new DataTable();
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "EmailID";
            table.Columns.Add(column);

            for (int k = 0; k < dt.Rows.Count; k++)
            {
                DataRow dr = dt.Rows[k];
                //int count = 0;
                //int temp = 0;

                //foreach (object item in dr.ItemArray)
                //{
                //    if (item.ToString() == "")
                //    {
                //       // count++;
                //        if (temp == count && temp == 4)
                //        {
                //            break;
                //        }
                //    }
                //    //temp++;
                //    //if (temp == 4) //all 4 columns are empty
                //    //{
                //    //    break;
                //    //}
                //}
                //if (count >= 4)
                //{
                //    //empty data
                //}
                //else
                //{
                DataRow gridrow = table.NewRow();

                //check mail, add list
                bool checkEmail = check_email(dr.ItemArray[0].ToString());
                if (checkEmail != true)
                {
                    li_EmailId.Add(dr.ItemArray[0].ToString());
                }

                gridrow[0] = dr.ItemArray[0];
                // gridrow[1] = dr.ItemArray[1];
                //gridrow[2] = dr.ItemArray[2];
                //  gridrow[3] = dr.ItemArray[3];
                table.Rows.Add(gridrow);
                // }
            }
            ViewState["currentList"] = li_EmailId;

            return table;
       
    }

    protected bool check_email(string email)
    {
        
            if (email == "")
            {
                return false;
            }

            bool isEmail = Regex.IsMatch(email.Trim(), @"\A(?:[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?)\Z");
            if (isEmail)
            {
                return true;
            }
            return false;
        
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string fileName = (Server.MapPath(fuExcel.FileName.ToString()));
        //string fleUpload = Path.GetExtension(fuExcel.FileName.ToString());
        //string path = fuExcel.PostedFile.FileName;
        //string fileName = "C:\\Users\\Rajashekhar\\Desktop\\All Common\\ImportingContacts1\\" + fuExcel.FileName;
        string sheetName = "Sheet1$";
        dt1 = (DataTable)ViewState["DataTable"];
        try
        {
            if (dt1 == null)
            {
                dt1 = objBLProfileManagement.GetExcelData(fileName, sheetName);//BusinessLogicLayerClass.GetExcelData(fileName, sheetName);
                gvShowAllContacts.DataSource = dt1;
                gvShowAllContacts.DataBind();
                ViewState["DataTable"] = dt1;
            }
            else
            {
                dt1 = (DataTable)ViewState["DataTable"];
                dt2 = objBLProfileManagement.GetExcelData(fileName, sheetName);
                dt1.Merge(dt2, true);
                gvShowAllContacts.DataSource = dt1;
                gvShowAllContacts.DataBind();
                ViewState["DataTable"] = dt1;
                dt2 = null;
            }
        }
        catch(Exception ex)
        {
            lblEror.Text = ex.Message;
        }
        btnSaveToDatabase.Visible = true;
        btnAddToGroup.Visible = true;
    }

    protected void gvShowAllContacts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (ViewState["DataTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["DataTable"];
                if (dt.Rows.Count > 0)
                {
                    GridViewRow row1 = gvShowAllContacts.Rows[e.RowIndex];

                    string prvEmail = dt.Rows[row1.DataItemIndex]["EmailID"].ToString();

                    dt.Rows[row1.DataItemIndex].Delete();
                    ViewState["CurrentTable"] = dt;

                    if (dt.Rows.Count >= 0)
                    {
                        gvShowAllContacts.DataSource = dt;
                        gvShowAllContacts.DataBind();

                        List<string> li = (List<string>)ViewState["currentList"];

                        //bool checkEmail = check_email(lblEmailId.Text);                    
                        foreach (string emails in li)
                        {
                            if (emails == prvEmail)
                            {
                                li.Remove(prvEmail);
                                break;
                            }
                        }

                        ViewState["currentList"] = li;

                        ValidateRowColor();
                    }
                    //dt.Rows[e.RowIndex].Delete();
                    //gvShowAllContacts.DataSource = dt;
                    //gvShowAllContacts.DataBind();                 
                }
            }
        }
        catch (Exception ex) { lblEror.Text = ex.Message; }
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
            lblEror.Text = ex.Message;
        }
    }

    protected void gvShowAllContacts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //gvShowAllContacts.PageIndex = e.NewPageIndex;
        //DataTable dt = (DataTable)ViewState["DataTable"];
        //gvShowAllContacts.DataSource = dt;
        //gvShowAllContacts.DataBind();

        try
        {
            gvShowAllContacts.EditIndex = -1;
            foreach (GridViewRow row in gvShowAllContacts.Rows)
            {
                var chkBox = row.FindControl("chkSelect") as CheckBox;

                IDataItemContainer container = (IDataItemContainer)chkBox.NamingContainer;
            }
            gvShowAllContacts.PageIndex = e.NewPageIndex;

            if (ViewState["DataTable"] != null)
            {
                DataTable dt1 = (DataTable)ViewState["DataTable"];
                gvShowAllContacts.DataSource = dt1;
                gvShowAllContacts.DataBind();
            }
            ValidateRowColor();
        }
        catch (Exception ex)
        {
            lblEror.Text = ex.Message;
        }
    }

    protected void gvShowAllContacts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvShowAllContacts.EditIndex = -1;

            if (ViewState["DataTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["DataTable"];
                gvShowAllContacts.DataSource = dt;
                gvShowAllContacts.DataBind();

                ValidateRowColor();
            }
        }
        catch (Exception ex)
        {
            lblEror.Text = ex.Message;
        }
    }

    protected void gvShowAllContacts_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvShowAllContacts.EditIndex = e.NewEditIndex;

            if (ViewState["DataTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["DataTable"];
                gvShowAllContacts.DataSource = dt;
                gvShowAllContacts.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblEror.Text = ex.Message;
        }
    }

    protected void btnmanual_Click(object sender, EventArgs e)
    {



    }

    protected void gvShowAllContacts_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            //Retrieve the table from the session object.
            if (ViewState["DataTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["DataTable"];

                GridViewRow row1 = gvShowAllContacts.Rows[e.RowIndex];

                TextBox txtEditEmailId = (TextBox)gvShowAllContacts.Rows[e.RowIndex].FindControl("txtEditEmailId");

                //previous row data
                string prvEmail = dt.Rows[row1.DataItemIndex]["EmailID"].ToString();

                dt.Rows[row1.DataItemIndex]["EmailID"] = txtEditEmailId.Text;

                gvShowAllContacts.EditIndex = -1;

                ViewState["DataTable"] = dt;
                gvShowAllContacts.DataSource = dt;
                gvShowAllContacts.DataBind();

                List<string> li_Email = (List<string>)ViewState["currentList"];

                //check Email
                if (txtEditEmailId.Text != prvEmail)
                {
                    bool checkEmail = check_email(txtEditEmailId.Text);
                    if (checkEmail == true) //right email
                    {
                        foreach (string emails in li_Email)
                        {
                            if (emails == prvEmail)
                            {
                                li_Email.Remove(prvEmail);
                                break;
                            }
                        }
                    }
                    else
                    {
                        li_Email.Remove(prvEmail);
                        li_Email.Add(txtEditEmailId.Text);
                    }
                }


                ViewState["currentList"] = li_Email;

                ValidateRowColor();
            }
        }
        catch (Exception ex)
        {
            lblEror.Text = ex.Message;
        }
    }
    protected void btnSaveCreate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int select = 0;
            int count = 100;
            for (int i = 0; i <= gvShowAllContacts.Rows.Count - 1; i++)
            {

                GridViewRow row = gvShowAllContacts.Rows[i];
                CheckBox Chbox = (CheckBox)row.FindControl("chkSelect");
                if (Chbox.Checked == true)
                {
                    select++;
                }
            }

            if (select == 0)
            {
                Response.Write("Select Checkbox...!");
                return;
            }

            for (int i = 0; i <= gvShowAllContacts.Rows.Count - 1; i++)
            {

                string email = gvShowAllContacts.Rows[i].Cells[1].Text;
                BE_user obj = new BE_user();
                obj.SurveyTakerId = count;
                obj.SurveyTakerEmailID = email;
                GridViewRow row = gvShowAllContacts.Rows[i];
                CheckBox Chbox = (CheckBox)row.FindControl("chkSelect");
                if (Chbox.Checked == true)
                {
                    //int result = objBLProfileManagement.SaveSelectedContact(email, Convert.ToInt32(Session["LoginCreatorId"]));
                    int result = objBLProfileManagement.SaveSelectedContact(obj, surveyCreatorId);
                }
            }
           lblMessages.Text="Record inserted successfully";
        }
        catch (Exception ex)
        {
            lblEror.Text = ex.Message;
        }
    }

    protected void btnSubmit1_Click(object sender, EventArgs e)
    {
        try
        {
            string str1 = txtEmailId.Text.Trim();// = divEdit.InnerHtml.ToString();
            dt1 = (DataTable)ViewState["DataTable"];
            if (dt1 == null)
            {
                DataColumn workCol = dt2.Columns.Add("EmailID", typeof(string));
                if (str1.Contains(","))
                {
                    string[] words = str1.Split(',');
                    foreach (string word in words)
                    {
                        DataRow row = dt2.NewRow();
                        row["EmailID"] = word;
                        dt2.Rows.Add(row);
                    }
                }
                else
                {
                    DataRow row = dt2.NewRow();
                    row["EmailID"] = str1;
                    dt2.Rows.Add(row);
                }
                gvShowAllContacts.DataSource = dt2;
                gvShowAllContacts.DataBind();
                ViewState["DataTable"] = dt2;
            }
            else
            {
                DataColumn workCol = dt2.Columns.Add("EmailID", typeof(string));
                if (str1.Contains(","))
                {
                    string[] words = str1.Split(',');
                    foreach (string word in words)
                    {
                        DataRow row = dt2.NewRow();
                        row["EmailID"] = word;
                        dt2.Rows.Add(row);
                    }
                }
                else
                {
                    DataRow row = dt2.NewRow();
                    row["EmailID"] = str1;
                    dt2.Rows.Add(row);
                }
                dt1.Merge(dt2, true);
                dt2 = null;
                gvShowAllContacts.DataSource = dt1;
                gvShowAllContacts.DataBind();
                ViewState["DataTable"] = dt1;
            }
            btnSaveToDatabase.Visible = true;
            btnAddToGroup.Visible = true;
        }
        catch (Exception ex)
        {
            lblEror.Text = ex.Message;
        }
    }
    protected void btnSaveToDatabase_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            foreach (GridViewRow row in gvShowAllContacts.Rows)
            {
                string email = ((Label)row.Cells[1].FindControl("lblEmailId")).Text;
                BE_user obj = new BE_user();
                obj.SurveyTakerEmailID = email;
                CheckBox check = (CheckBox)row.FindControl("chkSelect");
                if (check.Checked == true)
                {
                    //int result = objBLProfileManagement.SaveSelectedContact(obj, Convert.ToInt32(Session["LoginCreatorId"]));
                    int result = objBLProfileManagement.SaveSelectedContact(obj, surveyCreatorId);
                    lblMessages.Text = "Contact Saved Successfully";
                    //Response.Write("Record inserted successfully");
                }
            }
        }
        catch (Exception ex)
        {
            lblEror.Text = ex.Message;
        }
    }
    protected void btnGroup_Click(object sender, EventArgs e)
    {
        try
        {

            int grpID = Convert.ToInt32(ddlGroup.SelectedValue);
            if (grpID > 0)
            {
                foreach (GridViewRow row in gvShowAllContacts.Rows)
                {
                    string email = ((Label)row.Cells[1].FindControl("lblEmailId")).Text;
                    BE_user obj = new BE_user();
                    obj.SurveyTakerEmailID = email;
                    CheckBox check = (CheckBox)row.FindControl("chkSelect");
                    //int result = objBLProfileManagement.SaveSelectedContact(obj, Convert.ToInt32(Session["LoginCreatorId"]));
                    int result = objBLProfileManagement.SaveSelectedContact(obj, surveyCreatorId);
                    if (check.Checked == true)
                    {
                        objBLProfileManagement.AddContactToGroup(result, grpID);
                        Response.Write("Record inserted successfully");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblEror.Text = ex.Message;
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            BE_Group objGroup = new BE_Group();
            objGroup.GroupDescription = txtDescription.Text;
            objGroup.GroupName = txtgroup.Text;
            objGroup.SurveyCreatorID = surveyCreatorId; // Convert.ToInt32(Session["LoginCreatorId"])
            int grpID = objBLProfileManagement.CreateGroup(objGroup);
            if (grpID > 0)
            {
                objGroup.GroupId = grpID;
                foreach (GridViewRow row in gvShowAllContacts.Rows)
                {
                    string email = ((Label)row.Cells[1].FindControl("lblEmailId")).Text;
                    BE_user obj = new BE_user();
                    obj.SurveyTakerEmailID = email;
                    CheckBox check = (CheckBox)row.FindControl("chkSelect");
                    //int result = objBLProfileManagement.SaveSelectedContact(obj, Convert.ToInt32(Session["LoginCreatorId"]));
                    int result = objBLProfileManagement.SaveSelectedContact(obj, surveyCreatorId);
                    if (check.Checked == true)
                    {
                        objBLProfileManagement.AddContactToGroup(result, grpID);
                        lblMessages.Text = "Contact Saved Successfully";
                        lblMessages.Text="Record inserted successfully";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblEror.Text = ex.Message;
        }
    }
}