using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using BALOnlineSurvey.BE;
using BALOnlineSurvey.BL;
using System.Data;

public partial class Creator_ChangePassword : System.Web.UI.Page
{
    BLRegistration objBLRegistration = new BLRegistration();
    int creatorID;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void btnsubmit_Click1(object sender, EventArgs e)
    {
        try
        {
            //Session["HomeCreatorName"] = "smith";
            int creatorID = Convert.ToInt32(Session["HomeCreatorId"].ToString());

            BLRegistration objBusinessLogic = new BLRegistration();
            if (objBusinessLogic.ChangePassword(creatorID, txtOldpassword.Text, txtnewpassword.Text))
            {
                lblChangePassword.Text = "Password change failed";
                //Response.Write(@"<script language='javascript'>alert('Password change failed');</script>");
            }
            else
            {
                //Response.Write(@"<script language='javascript'>alert('Password changed sucessfully');</script>");
                ResetFormFields();
            }
            

        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    private void ResetFormFields()
    {
        try
        {
            this.txtOldpassword.Text = string.Empty;
            this.txtnewpassword.Text = string.Empty;
            this.txtConformpassword.Text = string.Empty;
            lblChangePassword.Text = "Password change successfully";
            this.upbtn.Update();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}