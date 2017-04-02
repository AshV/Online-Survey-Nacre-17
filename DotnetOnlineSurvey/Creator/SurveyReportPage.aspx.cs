using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using MySql.Data.MySqlClient;
using System.Text;
using BALOnlineSurvey.BL;

public partial class Creator_SurveyReportPage : System.Web.UI.Page
{
    StringBuilder str = new StringBuilder();
    StringBuilder divHTML = new StringBuilder();
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();
    static int surveyCreatorID;//  = 100; 
    static int categoryID = 0, groupID = 0, statusID = 0;
    // static DateTime  toDate;
    //static DateTime  fromDate;
    static string fromDate, toDate;
    int days = 0;
    //int counter = 1, c = 1, q = 1;


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
            //Response.Write(@"<script language='javascript'>alert('The following errors have occurred: \n" + ex.Message + " .');</script>");
        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            surveyCreatorID =  Convert.ToInt32(Session["HomeCreatorId"].ToString());
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Display Table", "displayDataTable()", true);
            GetCategoryNames();
            GetGroupNames(surveyCreatorID);
            GetSurveyStatus();

        }

    }

    [WebMethod]
    public static SurveyDetails[] BindDatatable()
    {
        List<SurveyDetails> details = new List<SurveyDetails>();
        MySqlDataReader objdr = BLReport.GetSurveyData(surveyCreatorID, categoryID, groupID, fromDate, toDate);
        while (objdr.Read())
        {
            SurveyDetails objSurveyDetails = new SurveyDetails();
            objSurveyDetails.surveyID = objdr[0].ToString();
            objSurveyDetails.surveyName = objdr[1].ToString();
            objSurveyDetails.totalResponses = Convert.ToInt32(objdr[2]);
            objSurveyDetails.creationDate = objdr[3].ToString();
            objSurveyDetails.surveyStatus = Convert.ToInt32(objdr[4]);
            details.Add(objSurveyDetails);
        }
        return details.ToArray();
    }

    [WebMethod]
    public static List<SurveyDetails> DisplayChartData()
    {
        MySqlDataReader objdr = BLReport.GetSurveyData(surveyCreatorID, categoryID, groupID, fromDate, toDate);
        List<SurveyDetails> dataList = new List<SurveyDetails>();
        while (objdr.Read())
        {
            SurveyDetails objDisplayChartData = new SurveyDetails();
            if (statusID == 0)
            {
                objDisplayChartData.surveyName = objdr[1].ToString();
                objDisplayChartData.totalResponses = Convert.ToInt32(objdr[2]);
            }
            else if (statusID == 1)
            {
                if (Convert.ToInt32(objdr[4]) == 0)
                {
                    objDisplayChartData.surveyName = objdr[1].ToString();
                    objDisplayChartData.totalResponses = Convert.ToInt32(objdr[2]);
                }
            }
            else
            {
                if (Convert.ToInt32(objdr[4]) == 1)
                {
                    objDisplayChartData.surveyName = objdr[1].ToString();
                    objDisplayChartData.totalResponses = Convert.ToInt32(objdr[2]);
                }
            }
            dataList.Add(objDisplayChartData);
        }
        return dataList;
    }

    public class SurveyDetails
    {
        public string surveyID { get; set; }
        public string surveyName { get; set; }
        public int totalResponses { get; set; }
        public string creationDate { get; set; }
        public int surveyStatus { get; set; }
    }
    protected void GetCategoryNames()
    {
        MySqlDataReader objdr = BLReport.GetCategories();
        ddlCategory.DataSource = objdr;
        ddlCategory.DataTextField = "categoryName";
        ddlCategory.DataValueField = "categoryID";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, "Choose");
        //while (objdr.Read())
        //{
        //    ddlCategory.Items.Insert(Convert.ToInt32(objdr[0]), objdr[1].ToString());
        //}
    }
    protected void GetGroupNames(int surveyCreatorId)
    {
        MySqlDataReader objdr = BLReport.GetGroupNames(surveyCreatorId);
        ddlGroup.DataSource = objdr;
        ddlGroup.DataTextField = "groupName";
        ddlGroup.DataValueField = "groupID";
        ddlGroup.DataBind();
        ddlGroup.Items.Insert(0, "Choose");
       
        //for (int i = 0; i <= dt.Rows.Count - 1; i++)
        //{
          //  ddlGroup.Items.Insert(Convert.ToInt32(dt.Rows[i]["groupID"]), dt.Rows[i]["groupName"].ToString());
        //}
        //while (objdr.Read())
        //{
        //    ddlGroup.Items.Insert(Convert.ToInt32(objdr[0]), objdr[1].ToString());
        //}
    }
    protected void GetSurveyStatus()
    {
        ddlSurveyStatus.Items.Insert(0, "Choose");
        ddlSurveyStatus.Items.Insert(1, "ACTIVE");
        ddlSurveyStatus.Items.Insert(2, "EXPIRED");
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        getDropDownValues();
    }
    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        getDropDownValues();
    }
    protected void ddlSurveyStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        getDropDownValues();
    }
    protected void getDropDownValues()
    {
        categoryID = Convert.ToInt32(ddlCategory.SelectedIndex);
        groupID = Convert.ToInt32(ddlGroup.SelectedIndex);
        statusID = Convert.ToInt32(ddlSurveyStatus.SelectedIndex);
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "displayDataTable()", true);
    }

    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        fromDate = txtFromDate.Text;
        toDate = txtToDate.Text;
        if (fromDate != "")
        {
            days = (Convert.ToDateTime(fromDate) - Convert.ToDateTime(toDate)).Days;
            if (days < 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "displayDataTable()", true);
            }

            else
                Response.Write("<script>alert('ToDate value should be greater than FromDate value');</scipt>");
        }
        else
        {
            fromDate = null; toDate = null;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "displayDataTable()", true);
        }
    }
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {

        //fromDate = Convert.ToDateTime(txtFromDate.Text);
        // toDate = Convert.ToDateTime(txtToDate.Text);
        // days = (fromDate - toDate).Days;
        fromDate = txtFromDate.Text;
        toDate = txtToDate.Text;
        if (toDate != "")
        {
            days = (Convert.ToDateTime(fromDate) - Convert.ToDateTime(toDate)).Days;
            if (days < 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "displayDataTable()", true);
            }
            else
                Response.Write("<script>alert('ToDate value should be greater than FromDate value');</scipt>");
        }
        else
        {
            fromDate = null; toDate = null;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "displayDataTable()", true);
        }
    }
}