using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BALOnlineSurvey.BE;
using BALOnlineSurvey.BL;
using MySql.Data.MySqlClient;
using System.Net.Mail;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;

public partial class Creator_SendByManually : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    BLSendSurvey objSendSurveyBL = new BLSendSurvey();
    BE_user usersObj = new BE_user();

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
                Response.Redirect("SendSurvey.aspx",false);
            }
            if (!IsPostBack)
            {
                Btn_submit.Visible = false;
            }
            lblMessage.Text = "";
            lblMsgManual.Text = "";
            lblFileMsg.Text = "";
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    //save excel file
    protected void btnUpload_Click(object sender, EventArgs e)
    {
       
        if (FileUpload1.HasFile)
        {
            try
            {
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                if (Extension == ".xls" || Extension == ".xlsx")
                {
                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                    string FilePath = Server.MapPath(FolderPath + FileName);
                    FileUpload1.SaveAs(FilePath);
                    Import_To_Grid(FilePath, Extension);
                    //GetExcelSheets(FilePath, Extension, "Yes");
                }
                else
                {
                    lblFileMsg.Text = "Please select Excel File.";
                }

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        else
        {
            lblFileMsg.Text = "Please select File.";
        }
    }

    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        // e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");
        // e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");
        //}
    }

    //import to grid
    private void Import_To_Grid(string FilePath, string Extension)
    {
        try
        {
            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
            }

            conStr = String.Format(conStr, FilePath, 0);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();

            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();

            //Bind Data to GridView
            GridView1.Caption = Path.GetFileName(FilePath);

            if (dt.Rows.Count > 0)
            {
                //validate data
                DataTable _ExcelData = checkEmpty_DataTable(dt);

                GridView1.DataSource = _ExcelData;
                GridView1.DataBind();
                Btn_submit.Visible = true;
                ViewState["CurrentTable"] = _ExcelData;

                //give color to rows in gridview
                ValidateRowColor();

                //clear erro
                ScriptManager.RegisterStartupScript(this, GetType(), "EmptyExcelFile", "EmptyExcelFile('');", true);
            }
            else
            {
                DataTable dt1 = new DataTable();
                GridView1.DataSource = dt;
                GridView1.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "EmptyExcelFile", "EmptyExcelFile('* No Data Found. Please Insert Data.');", true);
                Btn_submit.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    //remove space, and validate data
    public DataTable checkEmpty_DataTable(DataTable dt)
    {

            List<string> li_EmailId = new List<string>();
            List<string> li_AEmail = new List<string>();
            List<string> li_Mobile = new List<string>();

            DataTable table = new DataTable();
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Email ID";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Mobile No";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Alternate Email ID";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "User Name";
            table.Columns.Add(column);

            for (int k = 0; k < dt.Rows.Count; k++)
            {
                DataRow dr = dt.Rows[k];
                int count = 0;
                int temp = 0;

                foreach (object item in dr.ItemArray)
                {
                    if (item.ToString() == "")
                    {
                        count++;
                        if (temp == count && temp == 4)
                        {
                            break;
                        }
                    }
                    temp++;
                    if (temp == 4) //all 4 columns are empty
                    {
                        break;
                    }
                }
                if (count >= 4)
                {
                    //empty data
                }
                else
                {
                    DataRow gridrow = table.NewRow();

                    //check mail, add list
                    bool checkEmail = check_email(dr.ItemArray[0].ToString());
                    if (checkEmail != true)
                    {
                        li_EmailId.Add(dr.ItemArray[0].ToString());
                    }

                    //check alter mail, add list
                    string email = dr.ItemArray[2].ToString();
                    if (email != "")  //if empty, then don't check
                    {
                        bool checkAlternateEmail = check_email(dr.ItemArray[2].ToString());
                        if (checkAlternateEmail != true)
                        {
                            li_AEmail.Add(dr.ItemArray[2].ToString());
                        }
                    }

                    //check mobile, add list
                    string mobileNo = dr.ItemArray[1].ToString();
                    if (mobileNo != "")//if empty, then don't check
                    {
                        bool checkMobile = check_mobile(dr.ItemArray[1].ToString());
                        if (checkMobile != true)
                        {
                            li_Mobile.Add(dr.ItemArray[1].ToString());
                        }
                    }

                    gridrow[0] = dr.ItemArray[0];
                    gridrow[1] = dr.ItemArray[1];
                    gridrow[2] = dr.ItemArray[2];
                    gridrow[3] = dr.ItemArray[3];
                    table.Rows.Add(gridrow);
                }
            }
            ViewState["currentList"] = li_EmailId;
            ViewState["AlternateEmailList"] = li_AEmail;
            ViewState["MobileList"] = li_Mobile;

            return table;
    }

    //give color to rows in gridview
    public void ValidateRowColor()
    {
        try
        {
            List<string> li_Email = (List<string>)ViewState["currentList"];
            List<string> li_AEmail = (List<string>)ViewState["AlternateEmailList"];
            List<string> li_Mobile = (List<string>)ViewState["MobileList"];

            if (li_Email.Count > 0 || li_AEmail.Count > 0 || li_Mobile.Count > 0)
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    //DataRow dr = dtU.NewRow();
                    Label gvEmail = (Label)row.FindControl("lblEmailId");
                    Label gvAlterEmail = (Label)row.FindControl("lblAlterEmailId");
                    Label gvMobile = (Label)row.FindControl("lblMobileNo");

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

                    //check Mobile Num, assign color
                    foreach (string number in li_Mobile)
                    {
                        if (number != "")
                        {
                            if (gvMobile.Text == number)
                            {
                                //    row.BackColor = System.Drawing.ColorTranslator.FromHtml("#E18383");
                                row.Cells[2].BackColor = System.Drawing.ColorTranslator.FromHtml("#E18383");
                                break;
                            }
                        }
                    }

                    //check alternate emailID, assign color
                    foreach (string AlterEmail in li_AEmail)
                    {
                        if (AlterEmail != "")
                        {
                            if (gvAlterEmail.Text == AlterEmail)
                            {
                                //row.BackColor = Color.Red;
                                //row.BackColor = System.Drawing.ColorTranslator.FromHtml("#E18383");
                                row.Cells[3].BackColor = System.Drawing.ColorTranslator.FromHtml("#E18383");
                                break;
                            }
                        }
                    }

                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void BindGridData()
    {
        try
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            ValidateRowColor();
            //RePopulateCheckBoxes();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];

                if (dt.Rows.Count > 0)
                {
                    GridViewRow row1 = GridView1.Rows[e.RowIndex];
                    //Label lblEmailId = (Label)GridView1.Rows[e.RowIndex].FindControl("lblEmailId");

                    string prvEmail = dt.Rows[row1.DataItemIndex]["Email ID"].ToString();
                    string prv_AlterEmail = dt.Rows[row1.DataItemIndex]["Alternate Email ID"].ToString();
                    string prv_Mobile = dt.Rows[row1.DataItemIndex]["Mobile No"].ToString();

                    dt.Rows[row1.DataItemIndex].Delete();
                    ViewState["CurrentTable"] = dt;

                    if (dt.Rows.Count >= 0)
                    {
                        GridView1.DataSource = dt;
                        GridView1.DataBind();

                        List<string> li = (List<string>)ViewState["currentList"];
                        List<string> li_AlterEmail = (List<string>)ViewState["AlternateEmailList"];
                        List<string> li_Mobile = (List<string>)ViewState["MobileList"];

                        //bool checkEmail = check_email(lblEmailId.Text);                    
                        foreach (string emails in li)
                        {
                            if (emails == prvEmail)
                            {
                                li.Remove(prvEmail);
                                break;
                            }
                        }

                        foreach (string AlterEmails in li_AlterEmail)
                        {
                            if (AlterEmails == prv_AlterEmail)
                            {
                                li_AlterEmail.Remove(prv_AlterEmail);
                                break;
                            }
                        }
                        foreach (string MNo in li_Mobile)
                        {
                            if (MNo == prv_Mobile)
                            {
                                li_Mobile.Remove(prv_Mobile);
                                break;
                            }
                        }
                        ViewState["currentList"] = li;
                        ViewState["AlternateEmailList"] = li_AlterEmail;
                        ViewState["MobileList"] = li_Mobile;

                        ValidateRowColor();

                        ScriptManager.RegisterStartupScript(this, GetType(), "EmptyExcelFile", "EmptyExcelFile('');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "EmptyExcelFile", "EmptyExcelFile('* No Data Found. Please Insert Data.');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView1.EditIndex = e.NewEditIndex;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView1.EditIndex = -1;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                GridView1.DataSource = dt;
                GridView1.DataBind();

                ValidateRowColor();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            //Retrieve the table from the session object.
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];

                GridViewRow row1 = GridView1.Rows[e.RowIndex];

                TextBox txtEditEmailId = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtEditEmailId");
                TextBox txtEditMobileNo = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtEditMobileNo");
                TextBox txtEditAlterEid = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtEditAlterEid");
                TextBox txtEditUserName = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtEditUserName");

                //previous row data
                string prvEmail = dt.Rows[row1.DataItemIndex]["Email ID"].ToString();
                string prv_ALterEmail = dt.Rows[row1.DataItemIndex]["Alternate Email ID"].ToString();
                string prv_Mobile = dt.Rows[row1.DataItemIndex]["Mobile No"].ToString();
                string prv_UserName = dt.Rows[row1.DataItemIndex]["User Name"].ToString();

                dt.Rows[row1.DataItemIndex]["Email ID"] = txtEditEmailId.Text;
                dt.Rows[row1.DataItemIndex]["Mobile No"] = txtEditMobileNo.Text;
                dt.Rows[row1.DataItemIndex]["Alternate Email ID"] = txtEditAlterEid.Text;
                dt.Rows[row1.DataItemIndex]["User Name"] = txtEditUserName.Text;

                GridView1.EditIndex = -1;

                ViewState["CurrentTable"] = dt;
                GridView1.DataSource = dt;
                GridView1.DataBind();

                List<string> li_Email = (List<string>)ViewState["currentList"];
                List<string> li_AlterEmail = (List<string>)ViewState["AlternateEmailList"];
                List<string> li_Mobile = (List<string>)ViewState["MobileList"];

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

                //check Alter Email     
                if (txtEditAlterEid.Text != prv_ALterEmail)
                {
                    bool check_AlterEmail = check_email(txtEditAlterEid.Text);
                    if (txtEditAlterEid.Text == "")
                    {
                        check_AlterEmail = true;
                    }
                    if (check_AlterEmail == true)
                    {
                        foreach (string AlterEmails in li_AlterEmail)
                        {
                            if (AlterEmails == prv_ALterEmail)
                            {
                                li_AlterEmail.Remove(prv_ALterEmail);
                                break;
                            }
                        }
                    }
                    else
                    {
                        li_AlterEmail.Remove(prv_ALterEmail);
                        li_AlterEmail.Add(txtEditAlterEid.Text);
                    }
                }

                //check mobile                 
                if (txtEditMobileNo.Text != prv_Mobile)
                {
                    bool checkMobile = check_mobile(txtEditMobileNo.Text);
                    if (txtEditMobileNo.Text == "")
                    {
                        checkMobile = true;
                    }
                    if (checkMobile == true)
                    {
                        foreach (string MNo in li_Mobile)
                        {
                            if (MNo == prv_Mobile)
                            {
                                li_Mobile.Remove(prv_Mobile);
                                break;
                            }
                        }
                    }
                    else
                    {
                        li_Mobile.Remove(prv_Mobile);
                        li_Mobile.Add(txtEditMobileNo.Text);
                    }
                }

                ViewState["currentList"] = li_Email;
                ViewState["AlternateEmailList"] = li_AlterEmail;
                ViewState["MobileList"] = li_Mobile;

                ValidateRowColor();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
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

    protected bool check_mobile(string mobileNo)
    {
        if (mobileNo != "")
            {
                Regex isnumber = new Regex("[^0-9]");
                if (isnumber.IsMatch(mobileNo))
                {
                    return false;
                }
                if (mobileNo.Length != 10)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return true;  //if empty then true
            }
    }

    ///-----------save checkbox data

    protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView1.EditIndex = -1;
            foreach (GridViewRow row in GridView1.Rows)
            {
                var chkBox = row.FindControl("chkSelect") as CheckBox;

                IDataItemContainer container = (IDataItemContainer)chkBox.NamingContainer;
            }
            GridView1.PageIndex = e.NewPageIndex;
            BindGridData();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    //send and save data    
    protected void Btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            List<string> li = (List<string>)ViewState["currentList"];
            List<string> li_AlterEmail = (List<string>)ViewState["AlternateEmailList"];
            List<string> li_Mobile = (List<string>)ViewState["MobileList"];

            if (li.Count == 0 && li_AlterEmail.Count == 0 && li_Mobile.Count == 0)
            {
                DataTable dtU = new DataTable();
                int _NotChecked = 0;
                int _rowCount = GridView1.Rows.Count;

                if (GridView1.HeaderRow != null)
                {
                    for (int i = 1; i < GridView1.HeaderRow.Cells.Count; i++)
                    {
                        dtU.Columns.Add(GridView1.HeaderRow.Cells[i].Text);
                    }
                }

                List<BE_SurveyTaker> lstSurveyTaker = new List<BE_SurveyTaker>();

                foreach (GridViewRow row in GridView1.Rows)
                {
                    DataRow dr = dtU.NewRow();
                    CheckBox chk = (CheckBox)row.FindControl("chkSelect");

                    if (chk.Checked)
                    {
                        Label lblEmail = (Label)row.FindControl("lblEmailId");
                        Label lblMobile = (Label)row.FindControl("lblMobileNo");
                        Label lblAlterEmail = (Label)row.FindControl("lblAlterEmailId");
                        Label lblUserName = (Label)row.FindControl("lblUserName");


                        usersObj.SurveyTakerEmailID = lblEmail.Text;
                        usersObj.MobileNumber = lblMobile.Text;
                        usersObj.AlternateEmailID = lblAlterEmail.Text;
                        usersObj.SurveyTakerName = lblUserName.Text; ;

                        try
                        {

                            //use session to take surveycreaterid here assumed as 100
                            int surveyTakerId = objSendSurveyBL.InsertContactByExcel(usersObj, creatorID);
                            if (surveyTakerId >= 0)
                            {
                                BE_SurveyTaker objSurveyTaker = new BE_SurveyTaker();
                                objSurveyTaker.SurveyId = Convert.ToInt32(Session["CurrentSurvey"]);
                                objSurveyTaker.SurveyTakerEmailID = lblEmail.Text;
                                objSurveyTaker.SurveyTakerID = surveyTakerId;
                                objSurveyTaker.UniqueID = Guid.NewGuid().ToString("N");
                                lstSurveyTaker.Add(objSurveyTaker);

                                if (ViewState["CurrentTable"] != null)
                                {
                                    DataTable dt = (DataTable)ViewState["CurrentTable"];

                                    // GridViewRow row1 = GridView1.Rows[e.RowIndex];
                                    //Label lblEmailId = (Label)GridView1.Rows[e.RowIndex].FindControl("lblEmailId");
                                    //string GridEmail = row.Cells[1].Text;                                
                                    for (int k = 0; k < dt.Rows.Count; k++)
                                    {
                                        string DataTableEmail = dt.Rows[k]["Email ID"].ToString();
                                        string DataTableMobile = dt.Rows[k]["Mobile No"].ToString();
                                        string DataTableAlterEmail = dt.Rows[k]["Alternate Email ID"].ToString();

                                        if (DataTableEmail == lblEmail.Text && DataTableMobile == lblMobile.Text && DataTableAlterEmail == lblAlterEmail.Text)
                                        {
                                            dt.Rows[k].Delete();
                                            ViewState["CurrentTable"] = dt;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        { }
                    }
                    else
                    {
                        _NotChecked++;
                    }

                    //sending survey
                    string mailForm = Server.MapPath("~/Files/DemoMail.html");
                    objSendSurveyBL.SendSurvey(lstSurveyTaker, Convert.ToInt32(Session["CurrentSurvey"]), mailForm);
                }
                BindGridData();
                if (_rowCount == _NotChecked)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "SendSuccssfully('* Please Select At Least 1 Contact To Send.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "SendSuccssfully('* Send Survey Successfully.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "showalert('* Some Contacts Are Invalid.');", true);
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    //download sample demo excel
    protected void hlink_download_file_Click(object sender, EventArgs e)
    {
        try
        {
            DirectoryInfo directory = new DirectoryInfo(Server.MapPath("~/Files"));
            Response.ContentType = "Application/x-msexcel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=Demo.xlsx");
            Response.TransmitFile(Server.MapPath("~/Files/Demo.xlsx"));
            Response.End();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void btnSendManually_Click(object sender, EventArgs e)
    {
        try
        {
            string all = txtEmails.Text;
            string[] arrEmail = all.Split(',');
            List<BE_SurveyTaker> lstSurveyTaker = new List<BE_SurveyTaker>();
            for (int i = 0; i < arrEmail.Length; i++)
            {
                try
                {
                    BE_SurveyTaker objSurveyTaker = new BE_SurveyTaker();

                    //take surveycreaterID from session here assumed as 100;
                    int id = objSendSurveyBL.InsertContactManually(arrEmail[i].Trim(), creatorID);
                    objSurveyTaker.SurveyTakerEmailID = arrEmail[i].Trim();
                    objSurveyTaker.SurveyTakerID = id;
                    objSurveyTaker.SurveyId = Convert.ToInt32(Session["CurrentSurvey"]);
                    objSurveyTaker.UniqueID = Guid.NewGuid().ToString("N");
                    lstSurveyTaker.Add(objSurveyTaker);
                }
                catch (Exception ex)
                { }
                string mailForm = Server.MapPath("~/Files/DemoMail.html");
                objSendSurveyBL.SendSurvey(lstSurveyTaker, Convert.ToInt32(Session["CurrentSurvey"]), mailForm);
                lblMsgManual.Text = "Survey sent to given recipient and saved as contact.";
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}