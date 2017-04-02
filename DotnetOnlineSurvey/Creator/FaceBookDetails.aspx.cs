using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using BALOnlineSurvey.BE;
using BALOnlineSurvey.BL;
using MySql.Data.MySqlClient;
using System.Data;


public partial class Creator_FaceBookDetails : System.Web.UI.Page
{
    BLRegistration objBL = new BLRegistration();

    BE_Creator objSurveyReg = new BE_Creator();
   

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            MySqlDataReader dr = objBL.GetCompanyId();


            ddlCompanyName.DataSource = dr;
            ddlCompanyName.DataTextField = "companyName";
            ddlCompanyName.DataValueField = "companyID";
            ddlCompanyName.DataBind();

            while (dr.Read())
            {
                ddlCompanyName.Items.Add(new ListItem(dr["companyName"].ToString(), dr["companyID"].ToString()));
            }

            //if (Session["GmailName"] != null)
            //{

            //    txtCreator.Text = Session["GmailName"].ToString();
            //    // txtUserName.Text = Session["username"].ToString();
            //    txtContactEmail.Text = Session["GmilemailId"].ToString();
            //}

            if (Session["HomeUserEmailID"] != null)
            {
                if (Session["HomeCreatorName"] != null)
                {
                    txtCreator.Text = Session["HomeCreatorName"].ToString();
                }
                txtUserName.Text = Session["HomeCreatorUserName"].ToString();
                // txtUserName.Text = Session["username"].ToString();
                txtContactEmail.Text = Session["HomeUserEmailID"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objSurveyReg.CreatorName = txtCreator.Text;
            objSurveyReg.Username = txtUserName.Text;
            objSurveyReg.Password = txtPassword.Text;
            objSurveyReg.EmailID = txtContactEmail.Text;
            objSurveyReg.MobileNumber = txtMobileNumber.Text;
            //objSurveyReg.companyID = ddlCompanyName.SelectedValue.ToString();

            objSurveyReg.CompanyId = int.Parse(ddlCompanyName.SelectedValue);
            int res1 = objBL.InsertEmp(objSurveyReg);
            if (res1 > 0)
            {
                DataTable dt = new DataTable();
                dt = GetCreatorID();
                Session["RegisterCreatorId"] = dt.Rows[0][0];
                Session["RegisterCreatorName"] = txtCreator.Text;
                Session["RegisterCreatorUserName"] = txtUserName.Text;

                //Response.Write("<Script>alert('Record Inserted successfully')</Script>");
                //ModalPopupExtender2.Show();

                // Response.Write("Data Inserted sucessfully");


                //lblFbResult.Text = "You registered sucessfully";
                Response.Redirect("~/Creator/CreatorHomePage.aspx");

            }
        }
        catch (Exception ex)
        {
           // Response.Write(@"<script language='javascript'>alert('The following errors have occurred: \n" + ex.Message + " .');</script>");
            lblError.Text = ex.Message;
        }
    }
    protected DataTable GetCreatorID()
    {
        DataTable dt = new DataTable();
        try
        {
            MySqlDataReader dr = objBL.GetCreatorID(objSurveyReg);
            dt.Load(dr);
            return dt;
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
        return dt;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //txtCreator.Text = string.Empty;
            //txtCreator.Text = string.Empty;
            //txtUserName.Text = string.Empty;
            FormsAuthentication.SignOut();
            Response.Redirect("Welcome.aspx");
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}