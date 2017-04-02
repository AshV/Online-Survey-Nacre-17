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
using System.Web.Security;
using System.Net;
using Newtonsoft.Json.Linq;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using System.IO;
using System.Net.Mail;
using System.Web.Script.Serialization;
using ASPSnippets.FaceBookAPI;

public partial class LoginRegister : System.Web.UI.Page
{
    BLRegistration objBLRegister = new BLRegistration();
    BE_Creator objBECreator = new BE_Creator();

    OpenIdRelyingParty openid = new OpenIdRelyingParty();
    //BusinessLogic objGetEmailForFBRegistration = new BusinessLogic();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                lblError.Text = "";
                FBLogin();
                HandleOpenIDProviderResponse();
                BindCompany();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void BindCompany()
    {
        MySqlDataReader dr = objBLRegister.GetCompanyId();

        ddlCompanyName.DataSource = dr;
        ddlCompanyName.DataTextField = "companyName";
        ddlCompanyName.DataValueField = "companyID";
        ddlCompanyName.DataBind();
        ddlCompanyName.Items.Insert(0, new ListItem("--Select--", "0"));

        while (dr.Read())
        {
            ddlCompanyName.Items.Add(new ListItem(dr["companyName"].ToString(), dr["companyID"].ToString()));
        }
    }
    protected void btnLoginToGoogle_Command(object sender, CommandEventArgs e)
    {
        //HandleOpenIDProviderResponse();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string apiUrl = "http://api.verify-email.org/api.php?";
            string apiUsername = "TejaswiYeluri";
            string apiPassword = "tejaswi";
            string email = txtContactEmail.Text.Trim();


            WebClient webClient = new WebClient();
            string result = webClient.DownloadString(string.Format("{0}usr={1}&pwd={2}&check={3}", apiUrl, apiUsername, apiPassword, email));

            JObject objJSON = default(JObject);
            objJSON = JObject.Parse(result);

            if (objJSON["verify_status"] != null)
                // Response.Write(string.Format("The email address {0} is {1}", email, Convert.ToBoolean(Convert.ToInt32(objJSON["verify_status"].ToString())) ? "GOOD" : "BAD or cannot be verified"));
                //  lblEmail.Text = string.Format("The email address {0} is {1}", email, Convert.ToBoolean(Convert.ToInt32(objJSON["verify_status"].ToString())) ? "Valid" : "invalid or cannot be verified");


                if (Convert.ToBoolean(Convert.ToInt32(objJSON["verify_status"].ToString())) == true)
                {
                    try
                    {
                        objBECreator.CreatorName = txtCreator.Text;
                        objBECreator.Username = txtUserName.Text;
                        objBECreator.Password = txtPassword.Text;
                        objBECreator.EmailID = txtContactEmail.Text;
                        objBECreator.MobileNumber = txtMobileNumber.Text;
                        objBECreator.CompanyId = Convert.ToInt32(ddlCompanyName.SelectedValue);

                        int res1 = objBLRegister.InsertEmp(objBECreator);
                        if (res1 > 0)
                        {
                            //Response.Write("<Script>alert('Record Inserted successfully')</Script>");
                            DataTable dt = new DataTable();
                            dt=GetCreatorID();                           
                            Session["RegisterCreatorId"] = dt.Rows[0][0];
                            Session["RegisterCreatorName"] = txtCreator.Text;
                            Session["RegisterCreatorUserName"] = txtUserName.Text;
                            Response.Redirect("~/Creator/HomePage.aspx");
                           // ModalPopupExtender2.Show();

                        }
                    }

                    catch (MySqlException ex)
                    {
                        //Response.Write(@"<script language='javascript'>alert('The following errors have occurred: \n" + ex.Message + " .');</script>");


                        lblError.Text = ex.Message;
                    }
                    catch (Exception ex)
                    {
                        lblError.Text = ex.Message;

                        //Response.Write(@"<script language='javascript'>alert('The following errors have occurred: \n" + ex.Message + " .');</script>");
                    }
                }
                else
                {
                    lblEmail.Text = "Please enter a valid email address";
                }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected DataTable GetCreatorID()
    {
        DataTable dt = new DataTable();
        try
        {
            MySqlDataReader dr = objBLRegister.GetCreatorID(objBECreator);
            dt.Load(dr);
            return dt;
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
        return dt;
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            MySqlDataReader objDataReader = objBLRegister.GetLoginDetails(txtUName.Text, txtLoginPassword.Text);
            dt.Load(objDataReader);

            if (dt != null)
            {
                string strUserName = dt.Rows[0][0].ToString();
                string strPassword = dt.Rows[0][1].ToString(); ;
                string strFirstName = dt.Rows[0][2].ToString();
                int CreatorID = Convert.ToInt32(dt.Rows[0][3].ToString());


                Session["LoginCreatorUserName"] = strUserName;
               // Session["LoginCreatorUName"] = strUserName;
                Session["LoginCreatorID"] = CreatorID;
                if (strPassword == txtLoginPassword.Text)
                {
                    
                    FormsAuthentication.RedirectFromLoginPage(strFirstName, true);
                    Response.Redirect("~/Creator/CreatorHomePage.aspx", false);
                    
                }
                else
                {
                    lblResult.Text = "Invalid Password";
                    //Response.Write("<Script>alert('Invalid password')</Script>");
                    Panel1_ModalPopupExtender.Show();
                }
            }
            else
            {
                lblResult.Text = "Invalid UserName";
                ModalPopupExtender1.Show();
                // Response.Write("<Script>alert('Invalid Username or EmailId')</Script>");
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void HandleOpenIDProviderResponse()
    {
        try
        {
            var response = openid.GetResponse();

            if (response != null)
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        NotLoggedIn.Visible = false;
                        // btngmaillogout.Visible = true;

                        var fetchResponse = response.GetExtension<FetchResponse>();
                        Session["FetchResponse"] = fetchResponse;
                        var response2 = Session["FetchResponse"] as FetchResponse;

                        Session["GmilemailId"] = response2.GetAttributeValue(WellKnownAttributes.Contact.Email);
                        Session["GmailName"] = GetFullname(response2.GetAttributeValue(WellKnownAttributes.Name.First), response2.GetAttributeValue(WellKnownAttributes.Name.Last));
                        //Session["birthdatelbl"] = response2.GetAttributeValue(WellKnownAttributes.BirthDate.WholeBirthDate);
                        //Session["phonelbl"] = response2.GetAttributeValue(WellKnownAttributes.Contact.Phone.Mobile);
                        //Session["genderlbl"] = response2.GetAttributeValue(WellKnownAttributes.Person.Gender);
                        Response.Redirect("~/Creator/CreatorHomePage.aspx");
                        break;
                    case AuthenticationStatus.Canceled:
                        lblAlertMsg.Text = "Cancelled.";
                        break;
                    case AuthenticationStatus.Failed:
                        lblAlertMsg.Text = "Login Failed.";
                        break;
                }
            }
            else
            {
                return;

            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void OpenLogin_Click(object src, CommandEventArgs e)
    {
        try
        {
           
            string discoveryUri = e.CommandArgument.ToString();
            var b = new UriBuilder(Request.Url) { Query = "" };
            var req = openid.CreateRequest(discoveryUri, b.Uri, b.Uri);

            var fetchRequest = new FetchRequest();
            fetchRequest.Attributes.AddRequired(WellKnownAttributes.Contact.Email);
            fetchRequest.Attributes.AddRequired(WellKnownAttributes.Name.First);
            fetchRequest.Attributes.AddRequired(WellKnownAttributes.Name.Last);
            fetchRequest.Attributes.AddRequired(WellKnownAttributes.Person.Gender);
            fetchRequest.Attributes.AddRequired(WellKnownAttributes.Contact.Phone.Mobile);
            fetchRequest.Attributes.AddRequired(WellKnownAttributes.BirthDate.WholeBirthDate);
            req.AddExtension(fetchRequest);
            req.RedirectToProvider();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    private static string GetFullname(string first, string last)
    {
            var _first = first ?? "";
            var _last = last ?? "";

            if (string.IsNullOrEmpty(_first) || string.IsNullOrEmpty(_last))
                return "";

            return _first + " " + _last;
    }
    protected void btngmaillogout_click(object sender, EventArgs e)
    {
        try
        {
            //emaillbl.Text = "";
            //namelbl.Text = "";
            //btngmaillogout.Visible = false;            
            Response.Redirect("~/Welcome.aspx");
        }
        catch(Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void imgbtnFBLogin_Click(object sender, ImageClickEventArgs e)
    {
       
            FaceBookConnect.Authorize("user_photos,email", Request.Url.AbsoluteUri.Split('?')[0]);
    }
    protected void FBLogin()
    {
        FaceBookConnect.API_Key = "1422724551316498";
        FaceBookConnect.API_Secret = "9ad3c8cec1a940c58de9d4a2484f2740";
        //if (!IsPostBack)
        //{
        if (Request.QueryString["error"] == "access_denied")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('User has denied access.')", true);
            return;
        }
        string code = Request.QueryString["code"];
        if (!string.IsNullOrEmpty(code))
        {
            string data = FaceBookConnect.Fetch(code, "me");
            FaceBookUser faceBookUser = new JavaScriptSerializer().Deserialize<FaceBookUser>(data);
            faceBookUser.PictureUrl = string.Format("https://graph.facebook.com/{0}/picture", faceBookUser.Id);
            pnlFaceBookUser.Visible = true;
            Session["FBUserId"] = faceBookUser.Id;
            Session["FBUserName"] = faceBookUser.UserName;
            Session["FBname"] = faceBookUser.Name;
            Session["FBemailId"] = faceBookUser.Email;

            Response.Redirect("~/Creator/CreatorHomePage.aspx");
            btnLogin.Enabled = false;
        }
    }
    public class FaceBookUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string PictureUrl { get; set; }
        public string Email { get; set; }
    }
    protected void btnRes_Click(object sender, EventArgs e)
    {
        try
        {
            Panel1_ModalPopupExtender.Hide();
        }
        catch (Exception ex)
        {
        lblError.Text = ex.Message;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            ModalPopupExtender1.Hide();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void btnInserSucess_Click(object sender, EventArgs e)
    {
        try
        {
            ModalPopupExtender2.Hide();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void lnkForgotPassword_Click(object sender, EventArgs e)
    {
        try
        {
            ModalPopupExtenderForgotPassword.Show();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void btnForgotPassword_Click(object sender, EventArgs e)
    {
        lblForgotPassword.Text = "";
        try
        {
            BE_Creator objBECreator = new BE_Creator();
            objBECreator.EmailID = txtEmail.Text;
            MailMessage mail = new MailMessage();
            mail.To.Add(txtEmail.Text);
            mail.From = new MailAddress("dotnetsurveymanagementsystem@gmail.com");
            mail.Subject = "Password Form SurveyManagementSystem";
            MySqlDataReader objdr = objBLRegister.Forgotpassword(objBECreator);

            if (objdr.Read())
            {
                string password = objdr.GetValue(0).ToString();

                //while (objdr.Read())
                //{
                //    s = objdr[0].ToString(); }
                if (password != null)
                {
                    // lblForgotPassword.Text = "Password Retrevied";
                    // Response.Write("<script>alert(password is retrevied by the database check)</script>");
                    string Body = "Hi, please check your password" + password;
                    mail.Body = Body;

                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential
                     ("dotnetsurveymanagementsystem@gmail.com", "nacre@naresh");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    mdlMessage.Show();
                    //lblForgotPassword.Text = "Send successfully";
                    //Response.Write("<script>alert(your password is sent to you are email id)</script>");
                    ModalPopupExtenderForgotPassword.Hide();

                }
                else
                {

                }
            }
            else
            {
                mdlMessage1.Show();
                //lblForgotPassword.Text = "Wrong email id";
                //ModalPopupExtenderForgotPassword.Hide();
            }
        }

        catch (Exception ex)
        {

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void lnkCheckUsername_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtUserName.Text == "")
            {
                lnkCheckUsername.Text = "Fill Mandetory fields";
            }
            else
            {
                BE_Creator objBECreator = new BE_Creator();
                objBECreator.Username = txtUserName.Text;
                MySqlDataReader objdr = objBLRegister.CheckUsername(objBECreator);
                if (objdr.Read())
                {
                    lnkCheckUsername.ForeColor = System.Drawing.Color.Red;
                    lnkCheckUsername.Text = "Not Available";
                }
                else
                {
                    lnkCheckUsername.Text = "Available";
                }
            }

        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}