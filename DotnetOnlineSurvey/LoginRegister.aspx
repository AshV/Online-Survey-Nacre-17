<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMasterPage.master" AutoEventWireup="true" CodeFile="LoginRegister.aspx.cs" Inherits="LoginRegister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="js/jquery.validate-1.9.min.js"></script>
    
  
    <script type="text/javascript"> 
       
        $(document).ready(function(){
            $("#<%=txtCreator.ClientID%>").blur(function(){
            
                var creatorName =$("#<%=txtCreator.ClientID %>").val();
                if (creatorName =="")
                {
                    document.getElementById("<%=lblcreator.ClientID %>").innerHTML = "<br/>creator name is required";
                    $("#<%=txtCreator.ClientID %>").focus();
                }

            })
            $("#<%=txtUserName.ClientID %>").blur(function(){
            
                var UserName =$("#<%=txtUserName.ClientID%>").val();
                 if (UserName =="")
                 {
                     document.getElementById("<%=lblUserName.ClientID %>").innerHTML ="<br/> user name is required";
                     $("#<%=txtUserName.ClientID %>").focus();
                 }
             })
            $("#<%=txtCreator.ClientID %>").keypress(function () {
                console.log(document.getElementById("<%=lblcreator.ClientID %>").innerHTML = "");

            })
            $("#<%=txtPassword.ClientID %>").blur(function(){
            
                var password =$("#<%=txtPassword.ClientID%>").val();
                if (password  =="")
                {
                    document.getElementById("<%=lblPassword.ClientID %>").innerHTML = "<br/>Password is required";
                    $("#<%=txtPassword.ClientID %>").focus();
                }
            })
            $("#<%=txtPassword.ClientID %>").keypress(function () {
                console.log(document.getElementById("<%=lblPassword.ClientID %>").innerHTML = "");
            })
            $("#<%=txtConfirmPassword.ClientID %>").blur(function(){
            
                var conformpassword =$("#<%=txtConfirmPassword.ClientID%>").val();
                if (conformpassword  =="")
                {
                    document.getElementById("<%=lblConfirmPassword.ClientID %>").innerHTML = "<br/>Confirm Password is required";

                    $("#<%=txtConfirmPassword.ClientID %>").focus();
                }
            })
            $("#<%=txtConfirmPassword.ClientID %>").keypress(function () {
                console.log(document.getElementById("<%=lblConfirmPassword.ClientID %>").innerHTML = "");
            })

            $("#<%=txtContactEmail.ClientID %>").blur(function(){
            
                var email =$("#<%=txtContactEmail.ClientID%>").val();
                if (email =="")
                {
                    document.getElementById("<%=lblContactEmail.ClientID %>").innerHTML = "<br/>Contact email is required";
                     $("#<%=txtContactEmail.ClientID %>").focus();
                 }
            })
            $("#<%=txtContactEmail.ClientID %>").keypress(function () {
                console.log(document.getElementById("<%=lblContactEmail.ClientID %>").innerHTML = "");
            })
            $("#<%=txtMobileNumber.ClientID %>").blur(function(){
            
                var mobileno =$("#<%=txtMobileNumber.ClientID%>").val();
                if ( mobileno =="")
                {
                    document.getElementById("<%=lblMobileNumber.ClientID %>").innerHTML ="<br/>Mobile number is required";
                    $("#<%=txtMobileNumber.ClientID %>").focus();
                    if (mobileno.charAt(0) != "9" || mobileno.charAt(0) != "8") {
                        document.getElementById("<%=lblMobileNumber.ClientID %>").innerHTML ="<br/>Number starts with 8 or 9";
                        return false;
                    }
                }
            })
            $("#<%=txtMobileNumber.ClientID %>").keypress(function () {

                console.log(document.getElementById("<%=lblMobileNumber.ClientID %>").innerHTML = "");
            })
           

        });
    </script>    
      <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=btnSubmit.ClientID %>").click(function(){
                $("#form1").validate({ // initialize the plugin
                    focusInvalid: false,
                    focusCleanup: true,
                    debug: false,
                    onkeyup: false,
                    onclick: true,
                    onsubmit: true,
                    onkeyup: false,
                    rules: {   
                        <%=txtCreator.UniqueID %>: {
                            required: true
                        },
                        <%=txtUserName.UniqueID %>: {
                            required: true
                        },
                        <%=txtPassword.UniqueID %>:{
                            required: true,
                            minlength: 6
                        },
                        <%=txtConfirmPassword.UniqueID %>:{
                            required: true,
                            minlength: 6,
                            equalTo: "#<%=txtPassword.ClientID %>"
                        },
                        <%=txtContactEmail.UniqueID %>:{
                            required: true,
                            email: true
                        },
                        <%= txtMobileNumber.UniqueID %>:{
                            required: true,
                            minlength: 10,
                            digits: true
                        }
                    },
                    messages: {
                        <%=txtCreator.UniqueID %>:{ 
                            required:"</br>"+ "* Name is Required"
                        
                        },
                        <%=txtUserName.UniqueID %>:{ 
                            required:"</br>"+ "* UserName is Required"
                        
                        },

                        <%=txtPassword.UniqueID %>:{ 
                            required:"</br>"+ "* Password is Required"
                        
                        },
                        <%=txtConfirmPassword.UniqueID %>:{ 
                            required:"</br>"+ "* Confirm Password is Required"
                        
                        },
                        <%=txtContactEmail.UniqueID %>:{ 
                            required:"</br>"+ "* Contact email is Required"
                        
                        },
                        <%=txtMobileNumber.UniqueID %>:{ 
                            required:"</br>"+ "* Mobile Number is Required"
                        
                        },
                        <%=ddlCompanyName.UniqueID %>:{ 
                            required:"</br>"+ "* Select Company Name"
                    
                        }
                    }
                
                });
            });
        });
    </script>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#flip").click(function () {

                $("#panel").slideToggle("slow");
            });
        });

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=btnLogin.ClientID %>").click(function(){
                $("#form1").validate({
                    focusInvalid: false,
                    focusCleanup: true,
                    debug: false,
                    onkeyup: false,
                    onclick: true,
                    onsubmit: true,
                    onkeyup: false,
                    rules: {
                        <%=txtUName.UniqueID %>: {
                            required: true,
                            //email: true
                        },
                        <%=txtLoginPassword.UniqueID %>: {
                            required: true
                        },
                        hiddenOptionValidator: {
                            requireOne: true
                        },
                        submitHandler: function(form) {
                            form.submit();
                        }

                    },
                    messages: {
                        <%=txtUName.UniqueID %>: {
                            required:"</br>"+"Please enter EmailID"
                        },

                        <%=txtLoginPassword.UniqueID %>: {
                            required:"</br>"+"Please enter password"
                        }
                    }
                });
            });
        });
    </script>
    <script type="text/javascript">
        function showimage() {
            var i = document.getElementById("imggoogle");
            i.src = "images/google+logout+button.png";
            i.style.border = "1px solid white";
        }

    </script>
    <script type="text/javascript">
        function ValidateForgotPassword(){
            var txtForgotPassword=document.getElementById("<%=txtEmail.ClientID %>").value;
        
            if(txtForgotPassword=="")
            {
                document.getElementById("<%=lblForgotPassword.ClientID %>").innerHTML="Enter Email Id";
                return false;
            }
            else
                if (CheckMailId() == false) {
                    document.getElementById("<%=lblForgotPassword.ClientID %>").innerHTML="Mail Id is not in proper format";
                    // alert("Mail Id is not in proper format");
                    return false;
                }
        }
       

        function CheckMailId() {
            var EmailExp = "^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            var EmailId = document.getElementById('<%=txtEmail.ClientID %>').value;
            if (EmailId.match(EmailExp)) {
                return true;
            }
            else {
                document.getElementById('<%=txtEmail.ClientID %>').value = "";
                document.getElementById('<%=txtEmail.ClientID %>').focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <link href="CSS/RegistrationStyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="css/RegistrationStyleSheet2.css" rel="stylesheet" />
    <div>
        <div>
            <asp:Label ID="lblError" ForeColor="Red" runat="server" Text=""></asp:Label>
        </div>
        <div align="center">
            <h1><b>Sign in to your account</b></h1>
            <table style="border: inset; width: 70%">
                <tr>
                    <td valign="top" width="59.9%" align="left">
                        <table style="width: 100%">

                            <tr>
                                <td colspan="2">
                                    <h3 id="regText">Register Here</h3>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblMandetory" runat="server" Text="* Indicates mandetory fields"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%; text-align: right">Creator Name* :</td>
                                <td style="width: 50%">
                                    <asp:TextBox ID="txtCreator" runat="server" Width="220" Height="30"></asp:TextBox>
                                      <asp:Label ID="lblcreator" runat="server"></asp:Label>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="width: 30%; text-align: right">User Name* :</td>
                                <td style="width: 50%">
                                    <asp:TextBox ID="txtUserName" runat="server" Width="220" Height="30"></asp:TextBox>
                                      <asp:Label ID="lblUserName" runat="server"></asp:Label>
                                </td>
                                <td>
                                    
                                            <asp:LinkButton ID="lnkCheckUsername" runat="server" ForeColor="Lime" OnClick="lnkCheckUsername_Click">Check Availability</asp:LinkButton>
                                        
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%; text-align: right">Password* :</td>
                                <td style="width: 50%">
                                    <asp:TextBox ID="txtPassword" runat="server" Width="220" Height="30" TextMode="Password" />
                                      <asp:Label ID="lblPassword" runat="server"></asp:Label>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="width: 30%; text-align: right">Reenter Password* :</td>
                                <td style="width: 50%">
                                    <asp:TextBox ID="txtConfirmPassword" TextMode="Password" runat="server" Width="220" Height="30" />
                                      <asp:Label ID="lblConfirmPassword" runat="server"></asp:Label>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="width: 30%; text-align: right">Contact E-mail* :</td>
                                <td style="width: 50%">
                                    <asp:TextBox ID="txtContactEmail" runat="server" Width="220" Height="30" CssClass="" />
                                      <asp:Label ID="lblContactEmail" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%; text-align: right">Mobile Number* :</td>
                                <td style="width: 50%">
                                    <asp:TextBox ID="txtMobileNumber" runat="server" Width="220" Height="30" MaxLength="10" />
                                      <asp:Label ID="lblMobileNumber" runat="server"></asp:Label>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="width: 30%; text-align: right">Company Name* :</td>
                                <td style="width: 50%">
                                    <asp:DropDownList ID="ddlCompanyName" runat="server" Height="30px" Width="226px">
                                        <asp:ListItem Text="--Select--" Value="select"></asp:ListItem>

                                    </asp:DropDownList>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td align="left" class="auto-style1">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Sign Up" CssClass="button1" OnClick="btnSubmit_Click" />
                                </td>
                            </tr>
                        </table>
                        <asp:Label ID="lblSubmit" runat="server"></asp:Label>
                    </td>
                    <td style="background-color: white"></td>
                    <td valign="top" align="left" width="50%">


                        <div align="right">


                            <div id="flip"><b>Sign In</b></div>
                            <div id="panel">
                                User Name :<asp:TextBox ID="txtUName" runat="server"></asp:TextBox><br>
                                Password  :<asp:TextBox ID="txtLoginPassword" runat="server" TextMode="Password"></asp:TextBox><br>
                                <asp:CheckBox ID="ChkRememberme" Text="Remember Me" runat="server" /><br />

                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>

                                        <asp:LinkButton ID="lnkForgotPassword" runat="server" Text="Forgot Password" OnClick="lnkForgotPassword_Click"></asp:LinkButton>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="button1" OnClick="btnLogin_Click" /><br />
                                <asp:Label ID="lblResult" runat="server"></asp:Label>
                            </div>


                        </div>


                        <h3><b>Or sign in with your
                                <br />
                            Google or Facebook account </b></h3>
                        <pre>
Now you can link your accounts and 
sign in to Nacre Survey Management System 
using your Facebook or Google account.
It's quick, easy, and secure - your 
SurveyManagementSystem data will be
completely private.</pre>

                        <div id="loginform">

                            <div id="NotLoggedIn" runat="server">
                                Log in with :<br />
                               <%-- <img src="http://www.google.com/favicon.ico" />--%>

            


                                <asp:ImageButton ID="btnLoginToGoogle" ImageUrl="~/images/Gmail5.png" BorderWidth="1" BorderColor="WhiteSmoke" Width="125" Height="25" runat="server" OnCommand="OpenLogin_Click"
                                    ToolTip="Google_Login" CssClass="btngoogle"
                                    CommandArgument="https://www.google.com/accounts/o8/id" />
                                <p />
                                <asp:Label runat="server" ID="lblAlertMsg" />

                                <%--   <asp:Label ID="namelbl" runat="server"></asp:Label>
   <asp:Label ID="emaillbl" runat="server"></asp:Label>
    <asp:Label ID="birthdatelbl" runat="server"></asp:Label>
    <asp:Label ID="phonelbl" runat="server"></asp:Label>
    <asp:Label ID="genderlbl" runat="server"></asp:Label>

                                <%-- <a id="btngmaillogout" runat="server" onserverclick="btngmaillogout_click">   
        <img src="http://accounts.google.com/logout" id="imggoogle" title="Google_LogOut" onerror="javascript:return showimage();" /> 
    </a>--%>
                                <p />
                                <asp:ImageButton ID="imgbtnFBLogin" runat="server" ImageUrl="~/images/facebook.jpg" BorderWidth="1" BorderColor="WhiteSmoke" Width="125" Height="25" OnClick="imgbtnFBLogin_Click" />
                                <asp:Panel ID="pnlFaceBookUser" runat="server" Visible="false">

                                    <%--  <table>
                                        <tr>
                                            <td rowspan="5" valign="top">
                                                <asp:Image ID="ProfileImage" runat="server" Width="50" Height="50" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>ID:<asp:Label ID="lblId" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>UserName:<asp:Label ID="lblUserName" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Name:<asp:Label ID="lblName" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Email:<asp:Label ID="Label1" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                    </table>--%>
                                </asp:Panel>
                                <p />
                                <asp:ImageButton ID="imgbtnTwitterLogin" runat="server" ImageUrl="~/images/twitter.png" BorderWidth="1" BorderColor="WhiteSmoke" Width="125" Height="25" />
                                <asp:Label runat="server" ID="Label2" />
                            </div>

                        </div>

                    </td>
                </tr>
            </table>
        </div>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>

                <%-- Invalid Password Popup--%>
                <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Style="display: none;">

                    <div align="center">
                        <br />
                        <table style="background-color: none; background: none">
                            <tr>
                                <td>
                                    <img src="images/index.jpg" width="40px" height="40px" /></td>
                                <td>
                                    <asp:Label ID="Label4" runat="server" ForeColor="Red" Font-Size="Larger">Invalid Password</asp:Label></td>
                            </tr>
                        </table>
                        <asp:Button ID="btnRes" runat="server" Text="OK" OnClick="btnRes_Click" CssClass="button1" />
                    </div>

                </asp:Panel>
                <asp:Label ID="Label3" runat="server" Text="" Font-Size="Larger"></asp:Label>
                <asp:ModalPopupExtender ID="Panel1_ModalPopupExtender" runat="server" PopupControlID="Panel1"
                    OkControlID="btnRes" CancelControlID="btnRes" Enabled="True"
                    TargetControlID="Label3" BackgroundCssClass="modalBackground">
                </asp:ModalPopupExtender>

            </ContentTemplate>
        </asp:UpdatePanel>

        <%--  Invalid user name Popup--%>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>


                <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Style="display: none;">
                    <div align="center">
                        <br />

                        <table style="background-color: none; background: none">
                            <tr>
                                <td>
                                    <img src="Images/index.jpg" width="40px" height="40px" /></td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" ForeColor="Red" Font-Size="Larger">Invalid UserName</asp:Label></td>

                            </tr>
                        </table>
                        <asp:Button ID="Button1" runat="server" Text="OK" OnClick="Button1_Click" CssClass="button1" />
                    </div>

                </asp:Panel>
                <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel2"
                    OkControlID="Button1" CancelControlID="Button1" Enabled="True" TargetControlID="Label6" BackgroundCssClass="modalBackground">
                </asp:ModalPopupExtender>
            </ContentTemplate>
        </asp:UpdatePanel>


        <%-- record insertion sucessful message--%>


        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>


                <asp:Panel ID="Panel3" runat="server" CssClass="modalPopup" Style="display: none;">
                    <div align="center">
                        <br />

                        <table style="background-color: none; background: none">
                            <tr>
                                <td>
                                    <img src="images/successfull.jpg" width="40px" height="40px" /></td>
                                <td>
                                    <asp:Label ID="Label7" runat="server" ForeColor="Green" Font-Size="Medium">You Registered Sucessfully</asp:Label></td>

                            </tr>
                        </table>
                        <asp:Button ID="btnInserSucess" runat="server" Text="OK" OnClick="btnInserSucess_Click" CssClass="button1" />
                    </div>

                </asp:Panel>
                <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
                <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel3"
                    OkControlID="btnInserSucess" CancelControlID="btnInserSucess" Enabled="True"
                    TargetControlID="Label8" BackgroundCssClass="modalBackground">
                </asp:ModalPopupExtender>
            </ContentTemplate>
        </asp:UpdatePanel>


        <%--        Forgot password Popup--%>

        <asp:UpdatePanel ID="UpdatePanel5" runat="server" ChildrenAsTriggers="true">
            <ContentTemplate>


                <asp:Panel ID="Panel4" runat="server" CssClass="modalPopup" Height="141px" Width="499px">
                    <div align="center">
                        <br />

                        <table style="background-color: none; background: none">
                            <tr>
                                <td colspan="2">Enter Your Email Id for getting yor Survey Management System Password</td>
                            </tr>
                            <tr>
                                <td align="center">Enter E-mail Id :<asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
                                <td></td>
                            </tr>
                        </table>
                        <%-- <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                        --%>
                        <asp:Button ID="btnForgotPassword" runat="server" Text="Submit" OnClick="btnForgotPassword_Click" CssClass="button1" CausesValidation="true" OnClientClick="return ValidateForgotPassword()" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="button1" /><br />

                        <asp:Label ID="lblForgotPassword" runat="server"></asp:Label><br />
                        <%--    </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>

                </asp:Panel>
                <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                <asp:ModalPopupExtender ID="ModalPopupExtenderForgotPassword" runat="server" PopupControlID="Panel4"
                    Enabled="True"
                    TargetControlID="Label9" BackgroundCssClass="modalBackground">
                </asp:ModalPopupExtender>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel5" runat="server" CssClass="modalPopup" Height="131px" Width="499px">
                    <div align="center">
                        <br />

                        <table style="background-color: none; background: none">
                            <tr>
                                <td>
                                    <img src="images/successfull.jpg" width="40px" height="40px" /></td>
                                <td>
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Green" Font-Size="Medium">Password is send to your mail Sucessfully</asp:Label></td>

                            </tr>
                        </table>
                        <asp:Button ID="Button2" runat="server" Text="OK" OnClick="btnInserSucess_Click" CssClass="button1" />
                    </div>
                </asp:Panel>
                <asp:Label ID="Label1" runat="server"></asp:Label>
                <asp:ModalPopupExtender ID="mdlMessage" runat="server" PopupControlID="Panel5" Enabled="true" TargetControlID="Label1" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel6" runat="server" CssClass="modalPopup" Height="131px" Width="499px">
                    <div align="center">
                        <br />
                        <table style="background-color: none; background: none">
                            <tr>
                                <td>
                                    <img src="images/download.jpg" width="40px" height="40px" /></td>
                                <td>
                                    <asp:Label ID="Label10" runat="server" ForeColor="Red" Font-Size="Medium">Please enter a valid mail id</asp:Label></td>

                            </tr>
                        </table>
                        <asp:Button ID="Button3" runat="server" Text="OK" OnClick="btnInserSucess_Click" CssClass="button1" />
                    </div>
                </asp:Panel>
                <asp:Label ID="Label11" runat="server"></asp:Label>
                <asp:ModalPopupExtender ID="mdlMessage1" runat="server" PopupControlID="Panel6" Enabled="true" TargetControlID="Label11" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>

