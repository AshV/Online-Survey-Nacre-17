<%@ Page Title="" Language="C#" MasterPageFile="~/Creator/CreatorMaster.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Creator_ChangePassword" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../js/jquery-1.6.min.js"></script>
    <script src="../js/jquery.validate-1.9.min.js"></script>
    <link href="../css/RegistrationStyleSheet.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtOldpassword.ClientID %>").keypress(function () {
                console.log(document.getElementById("<%=lblOldpassword.ClientID %>").innerHTML = "");

            })
            $("#<%=txtnewpassword.ClientID %>").keypress(function () {
                console.log(document.getElementById("<%=lblNewpassword.ClientID %>").innerHTML = "");

              })
            $("#<%=txtConformpassword.ClientID %>").keypress(function () {
                console.log(document.getElementById("<%=lblConformpassword.ClientID %>").innerHTML = "");


              })

        });

    </script>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#<%=btnsubmit.ClientID %>").click(function () {
                  var Oldpassword = $("#<%=txtOldpassword.ClientID %>").val();
                  var Newpassword = $("#<%=txtnewpassword.ClientID %>").val();
                  var Conformpassword = $("#<%=txtConformpassword.ClientID %>").val();

                  if (Oldpassword == "") {
                      document.getElementById("<%=lblOldpassword.ClientID %>").innerHTML = "Old password is required";
                      return false;
                  }
                  if (Newpassword == "") {

                      document.getElementById("<%=lblNewpassword.ClientID %>").innerHTML = "New password is required";
                      return false;
                  }

                  if (Conformpassword == "") {

                      document.getElementById("<%=lblConformpassword.ClientID %>").innerHTML = "Conform password is required";
                      return false;
                  }
                  if (Newpassword != Conformpassword) {

                      document.getElementById("<%=lblConformpassword.ClientID %>").innerHTML = "New password and Conform password must be same";
                      return false;
                  }
                  if (Newpassword.length <= 6) {
                      document.getElementById("<%=lblNewpassword.ClientID %>").innerHTML = "Password length must be more than 6 letters";
                      //  $("#<%=txtnewpassword.ClientID %>").focus();
                      return false;
                  }
                  return true;
              });
          });

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtOldpassword.ClientID %>").blur(function () {
                var Oldpassword = $("#<%=txtOldpassword.ClientID %>").val();
                if (Oldpassword == "") {
                    document.getElementById("<%=lblOldpassword.ClientID %>").innerHTML = "Old password is required";
                    $("#<%=txtOldpassword.ClientID %>").focus();
                    return false;
                }
                document.getElementById("<%=lblOldpassword.ClientID %>").innerHTML = "";
            });

            $("#<%=txtnewpassword.ClientID %>").blur(function () {
                var Newpassword = $("#<%=txtnewpassword.ClientID %>").val();
                if (Newpassword == "") {
                    document.getElementById("<%=lblNewpassword.ClientID %>").innerHTML = "New password is required";
                    //alert("New password is required. ");
                    $("#<%=txtnewpassword.ClientID %>").focus();
                    document.getElementById("<%=lblNewpassword.ClientID %>").innerHTML = "";
                }
                if (Newpassword.length <= 6) {
                    document.getElementById("<%=lblNewpassword.ClientID %>").innerHTML = "Password length must be more than 6 letters";
                    $("#<%=txtnewpassword.ClientID %>").focus();
                    return false;
                }
                document.getElementById("<%=lblNewpassword.ClientID %>").innerHTML = " ";

            });
            $("#<%=txtConformpassword.ClientID %>").blur(function () {
                var firstNameVal = $("#<%=txtConformpassword.ClientID %>").val();
                if (firstNameVal == "") {
                    document.getElementById("<%=lblConformpassword.ClientID %>").innerHTML = "Conform password is required";
                    //alert("Conform password is required. ");
                    $("#<%=txtConformpassword.ClientID %>").focus();
                    // document.getElementById("<%=lblConformpassword.ClientID %>").innerHTML = "";
                    return false;
                }

            });
        });


    </script>




    <style type="text/css">
        .button1
        {
        }
    </style>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div align="center">
        <asp:UpdatePanel ID="upbtn" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label ID="lblError" ForeColor="Red" runat="server" Text=""></asp:Label>

                <table id="tbl1" style="border-style: inset; border-color: inherit; border-width: medium; background-color: azure; width: 601px;">
                    <tr>
                        <td colspan="3">
                            <h3>Change your password here...</h3>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%; text-align: right;">
                            <h4>Old Password:</h4>
                        </td>
                        <td style="width: 35%; text-align: left;">
                            <asp:TextBox TextMode="Password" ID="txtOldpassword" placeholder="Enter your old Password" runat="server" Height="30px" Width="200px" Style="text-align: left" />
                        </td>
                        <td>
                            <asp:Label ID="lblOldpassword" runat="server" ForeColor="Red"></asp:Label>
                        </td>

                    </tr>
                    <tr>
                        <td style="width: 30%; text-align: right;">
                            <h4>New Password :</h4>
                        </td>
                        <td style="width: 35%; text-align: left;">
                            <asp:TextBox TextMode="Password" ID="txtnewpassword" runat="server" placeholder="Enter New Password" Height="30px" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblNewpassword" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%; text-align: right;">
                            <h4>Conform Password :</h4>
                        </td>
                        <td style="width: 35%; text-align: left;">
                            <asp:TextBox ID="txtConformpassword" runat="server" TextMode="Password" placeholder="Enter conform Password" Height="30px" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblConformpassword" runat="server" ForeColor="Red"></asp:Label>
                        </td>

                    </tr>
                    <tr>

                        <td colspan="1" style="text-align: right;">
                            <%-- <asp:UpdatePanel ID="upbtn" runat="server">
                                <ContentTemplate>--%>
                            <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click1" CssClass="button1" Width="135px" />
                            <%-- </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </td>
                        <td style="text-align: left;">

                            <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="button1" Width="135px" />

                        </td>

                        <td colspan="3">
                            <asp:Label ID="lblChangePassword" runat="server"></asp:Label>
                        </td>
                    </tr>

                </table>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>

</asp:Content>

