<%@ Page Title="" Language="C#" MasterPageFile="~/Creator/CreatorMaster.master" AutoEventWireup="true" CodeFile="FaceBookDetails.aspx.cs" Inherits="Creator_FaceBookDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <link href="../css/RegistrationStyleSheet.css" rel="stylesheet" />
   <script src="js/jquery.validate-1.9.min.js"></script>
   <script src="../js/jquery-1.9.1.js"></script>
    <%-- <script type="text/javascript">
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
    </script>--%> 

  
            <script type="text/javascript">

                $(document).ready(function () {
                    $("#<%=btnSubmit.ClientID %>").click(function () {
                        var creator = $("#<%=txtCreator.ClientID %>").val();
                        var user = $("#<%=txtUserName.ClientID %>").val();
                        var password = $("#<%=txtPassword.ClientID %>").val();
                        var Confirmpassword = $("#<%=txtConfirmPassword.ClientID %>").val();
                        var email = $("#<%=txtContactEmail.ClientID %>").val();
                        var mobile = $("#<%=txtMobileNumber.ClientID %>").val();

                        if (creator == "") {
                            document.getElementById("<%=lblcreator.ClientID %>").innerHTML = "* required";
                    return false;
                }
                        if (user == "") {
                            document.getElementById("<%=lblUserName.ClientID %>").innerHTML = "* required";
                            return false;
                        }
                        if (password == "") {

                            document.getElementById("<%=lblPassword.ClientID %>").innerHTML = " password is required";
                            return false;
                        }
                        if (Confirmpassword == "") {

                            document.getElementById("<%=lblConfirmPassword.ClientID %>").innerHTML = "Conform password is required";
                            return false;
                        }
                        if (password != Confirmpassword) {

                            document.getElementById("<%=lblConfirmPassword.ClientID %>").innerHTML = "New password and Confirm password must be same";
                            return false;
                        }

                        if (email == "") {


                            //if (email.match(EmailExp)) {
                            //    return true;
                            //}
                            //else {

                            document.getElementById("<%=lblContactEmail.ClientID %>").innerHTML = "Email is required";
                            return false;
                        }
                        if (mobile == "") {

                            document.getElementById("<%=lblMobileNumber.ClientID %>").innerHTML = "Mobile No. is required";
                            return false;

                        }
                        if (re.test(mobile)) {
                            return true;
                        }

                        if (password != Confirmpassword) {

                            document.getElementById("<%=lblConfirmPassword.ClientID %>").innerHTML = "New password and Conform password must be same";
                            return false;
                        }
                        if (Newpassword.length <= 6) {
                            document.getElementById("<%=lblPassword.ClientID %>").innerHTML = "Password length must be more than 6 letters";

                    return false;
                }
                        return true;
                    });
                });

    </script>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#<%=txtCreator.ClientID%>").blur(function () {

                var creatorName = $("#<%=txtCreator.ClientID %>").val();
                if (creatorName == "") {
                    document.getElementById("<%=lblcreator.ClientID %>").innerHTML = "<br/>creator name is required";
                    $("#<%=txtCreator.ClientID %>").focus();
                    return false;
                }

            })
            $("#<%=txtUserName.ClientID %>").blur(function () {

                var UserName = $("#<%=txtUserName.ClientID%>").val();
                if (UserName == "") {
                    document.getElementById("<%=lblUserName.ClientID %>").innerHTML = "<br/> user name is required";
                    $("#<%=txtUserName.ClientID %>").focus();
                    return false;
                }
            })
            $("#<%=txtCreator.ClientID %>").keypress(function () {
                console.log(document.getElementById("<%=lblcreator.ClientID %>").innerHTML = "");

            })
            $("#<%=txtPassword.ClientID %>").blur(function () {

                var password = $("#<%=txtPassword.ClientID%>").val();
                if (password == "") {
                    document.getElementById("<%=lblPassword.ClientID %>").innerHTML = "<br/>Password is required";
                    $("#<%=txtPassword.ClientID %>").focus();
                    return false;
                }
            });

            $("#<%=txtPassword.ClientID %>").keypress(function () {
                console.log(document.getElementById("<%=lblPassword.ClientID %>").innerHTML = "");
            })

            $("#<%=txtPassword.ClientID %>").blur(function () {

                var password = $("#<%=txtPassword.ClientID%>").val();
                if (password.length <= 6) {
                    document.getElementById("<%=lblPassword.ClientID %>").innerHTML = "<br/Password length must be more than 6 letters";
                    $("#<%=txtPassword.ClientID %>").focus();
                    return false;
                }
            }); $("#<%=txtPassword.ClientID %>").keypress(function () {
                console.log(document.getElementById("<%=lblPassword.ClientID %>").innerHTML = "");
            })

            $("#<%=txtPassword.ClientID %>").keypress(function () {
                console.log(document.getElementById("<%=lblPassword.ClientID %>").innerHTML = "");
            })

            $("#<%=txtConfirmPassword.ClientID %>").blur(function () {

                var conformpassword = $("#<%=txtConfirmPassword.ClientID%>").val();
                if (conformpassword == "") {
                    document.getElementById("<%=lblConfirmPassword.ClientID %>").innerHTML = "<br/>Confirm Password is required";

                    $("#<%=txtConfirmPassword.ClientID %>").focus();
                    return false;
                }
            })
            $("#<%=txtConfirmPassword.ClientID %>").keypress(function () {
                console.log(document.getElementById("<%=lblConfirmPassword.ClientID %>").innerHTML = "");
            })

            $("#<%=txtContactEmail.ClientID %>").blur(function () {

                var email = $("#<%=txtContactEmail.ClientID%>").val();
                if (email == "") {
                    document.getElementById("<%=lblContactEmail.ClientID %>").innerHTML = "<br/>Contact email is required";
                    $("#<%=txtContactEmail.ClientID %>").focus();
                    return false;
                }
            })
            $("#<%=txtContactEmail.ClientID %>").keypress(function () {
                console.log(document.getElementById("<%=lblContactEmail.ClientID %>").innerHTML = "");
            })
            $("#<%=txtMobileNumber.ClientID %>").blur(function () {

                var mobileno = $("#<%=txtMobileNumber.ClientID%>").val();
                if (mobileno == "") {
                    document.getElementById("<%=lblMobileNumber.ClientID %>").innerHTML = "<br/>Mobile number is required";
                    $("#<%=txtMobileNumber.ClientID %>").focus();
                    if (mobileno.charAt(0) != "9" || mobileno.charAt(0) != "8") {
                        document.getElementById("<%=lblMobileNumber.ClientID %>").innerHTML = "<br/>Number starts with 8 or 9";
                        return false;
                    }
                    console.log(document.getElementById("<%=lblMobileNumber.ClientID %>").innerHTML = "");
                }
            })
            $("#<%=txtMobileNumber.ClientID %>").keypress(function () {

                console.log(document.getElementById("<%=lblMobileNumber.ClientID %>").innerHTML = "");
            })

            return true;
        });
    </script>
    <style type ="text/css">
        table {
        background-color:ThreeDFace;
        border:inset;
        }
        Content {
        background-color:ThreeDHighlight;
        
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div align="center">
        <div>
            <asp:Label ID="lblError" ForeColor="Red" runat="server" Text=""></asp:Label>
        </div>
    <table style="width:40%">

                            <tr>
                                <td colspan="3">
                                    <h3 id="regText" style="text-align:center">Please Enter Remaining Fields...</h3>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%; text-align: right">Creator Name * :</td>
                                <td>
                                    <asp:TextBox ID="txtCreator" runat="server" Width="220" Height="30"></asp:TextBox>
                                    <asp:Label ID="lblcreator" runat ="server" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%; text-align: right">User Name * :</td>
                                <td>
                                    <asp:TextBox ID="txtUserName" runat="server" Width="220" Height="30"></asp:TextBox><asp:Label ID="lblUserName" runat ="server" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%; text-align: right">Password * :</td>
                                <td>
                                    <asp:TextBox ID="txtPassword" runat="server" Width="220" Height="30" TextMode="Password" /><asp:Label ID="lblPassword" runat ="server" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%; text-align: right">Reenter Password * :</td>
                                <td>
                                    <asp:TextBox ID="txtConfirmPassword" TextMode="Password" runat="server" Width="220" Height="30" /><asp:Label ID="lblConfirmPassword" runat ="server" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%; text-align: right">Contact E-mail * :</td>
                                <td>
                                    <asp:TextBox ID="txtContactEmail" runat="server" Width="220" Height="30" CssClass="" /><asp:Label ID="lblContactEmail" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%; text-align: right">Mobile Number * :</td>
                                <td>
                                    <asp:TextBox ID="txtMobileNumber" runat="server" Width="220" Height="30" /><asp:Label ID="lblMobileNumber" runat ="server" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%; text-align: right">Company Name * :</td>
                                <td>
                                    <asp:DropDownList ID="ddlCompanyName" runat="server" Height="30px" Width="226px">
                                    </asp:DropDownList>
                                </td>

                            </tr>
                            <tr>
                                <td align="center" class="auto-style1" colspan="2">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Goto My Profile" CssClass="button1" OnClick="btnSubmit_Click"/>
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button1" OnClick ="btnCancel_Click"/>
                                </td>
                            </tr>
                        </table>
    </div>

</asp:Content>

