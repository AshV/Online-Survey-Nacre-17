<%@ Page Title="" Language="C#" MasterPageFile="~/Creator/NestedSendSurvey.master" AutoEventWireup="true" CodeFile="SendByManually.aspx.cs" Inherits="Creator_SendByManually" %>

<asp:Content ID="Content1" ContentPlaceHolderID="nestedcontent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <style type="text/css">
        #overlay
        {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: #000000;
            filter: alpha(opacity=70);
            -moz-opacity: 0.7;
            -khtml-opacity: 0.7;
            opacity: 0.7;
            z-index: 100;
            display: none;
        }

        .red
        {
            color: #ff0022;
        }

        .content a
        {
            text-decoration: none;
        }

        .popup
        {
            width: 100%;
            margin: 0 auto;
            display: none;
            position: relative;
            z-index: 101;
        }

        .content
        {
            min-width: 600px;
            width: 600px;
            min-height: 150px;
            margin: 100px auto;
            background: grey;
            position: fixed;
            z-index: 103;
            padding: 10px;
            border-radius: 5px;
            box-shadow: 0 2px 5px #000;
        }


            .content p
            {
                clear: both;
                color: #555555;
                text-align: justify;
            }

                .content p a
                {
                    color: grey;
                    font-weight: bold;
                }

            .content .x
            {
                float: right;
                height: 35px;
                left: 22px;
                position: relative;
                top: -25px;
                width: 34px;
            }

                .content .x:hover
                {
                    cursor: pointer;
                }
    </style>
    <script type='text/javascript'>
        $(function () {
            var overlay = $('<div id="overlay"></div>');
            $('.close').click(function () {
                $('.popup').hide();
                overlay.appendTo(document.body).remove();
                return false;
            });


            $('.x').click(function () {
                $('.popup').hide();
                overlay.appendTo(document.body).remove();
                return false;
            });

            $('.click').click(function () {
                overlay.show();
                overlay.appendTo(document.body);
                $('.popup').show();
                return false;
            });
        });


        $(document).ready(function () {
            $('#btnSendManually').click(function () {
                var flag = true;
                var textEntered = document.getElementById('<%=txtEmails.ClientID%>').value;
                var textResult = [];
                if ($.trim(textEntered).length == 0) {
                    alert("Please enter email address");
                    return false;
                }

                $.each(textEntered.split(', '), function (index, item) {
                    if (item.match(/^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/)) {
                    }
                    else {
                        flag = false;
                        textResult.push(item);
                    }

                });

                if (!flag) {
                    alert("Invalid Email Id(s): " + textResult.join(', '));
                    $('#divEdit').html("Invalid Email Id(s): " + textResult.join(', '));
                    return flag;
                }

            });
        });

    </script>
    <script type="text/javascript">

        function validateFileExtension() {
            var uploadcontrol = document.getElementById('<%=FileUpload1.ClientID%>').value;

            var ext = uploadcontrol.substring(uploadcontrol.lastIndexOf('.') + 1);
            var v = document.getElementById("ErrorFileMsg");
            //alert(v);
            //alert(ext);/
            if (ext == "xlsx" || ext == "xls") {
                //alert("true");                
                v.innerHTML = "";
                //alert("t");
                return true;
            }
            else {
                //alert("f");
                v.innerHTML = "* Please Select Excel File.";
                return false;
            }

        }

        function showalert(msg) {
            if (msg == "") {
                var v = document.getElementById("ErrorSendMsg");
                v.innerHTML = "";
            }
            else {
                var v = document.getElementById("ErrorSendMsg");
                v.style.color = "red";
                v.innerHTML = msg;
            }
        }

        function SendSuccssfully(msg) {
            var v = document.getElementById("ErrorSendMsg");
            if (msg == "") {
                v.innerHTML = "";
            }
            else {
                var v = document.getElementById("ErrorSendMsg");
                v.style.color = "green";
                v.innerHTML = msg;
            }
        }
    </script>
    <script src="../js/SendByExcel.js"></script>
    <link href="../css/SendSurvey.css" rel="stylesheet" />

    <style type="text/css">
        .style1
        {
            width: 163%;
            text-align: left;
        }

        .style2
        {
            width: 236px;
            text-align: left;
        }

        .style3
        {
            width: 244px;
        }

        .style4
        {
            width: 336px;
        }

        .auto-style3
        {
            width: 69px;
        }

        .auto-style4
        {
            width: 66px;
        }
    </style>

    <div>
        <asp:Label ID="lblError" ForeColor="Red" runat="server" Text=""></asp:Label>
    </div>
    <div id="accordion">
        <h3 style="font-size: 200%; text-align: left; padding: 5px 0 0 0; color: #09BCE8;">Send Manually</h3>
        <div id="Entermanually">
            <div class='popup'>
                <div class='content'>
                    <div class='red'>
                    </div>
                    <img src="../images/close.png" alt='quit' class='x' id='x' />
                    <p>
                        <table style="background-color: #fff" width="450 !important">
                            <tr>
                                <td align="center" colspan="2">Enter Email Id's with Comma seperation
                                        <br />
                                    Example: abc@mymail.com, xyz@mymail.in</td>
                            </tr>
                            <tr>

                                <td align="center">
                                    <asp:TextBox ID="txtEmails" runat="server" Height="66px" TextMode="MultiLine" ViewStateMode="Enabled" Width="316px" Wrap="False"></asp:TextBox>
                                    <div id="divEdit" runat="server" text align="left"></div>
                                </td>

                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnSendManually" ClientIDMode="Static" Text="Send" OnClick="btnSendManually_Click" runat="server" />
                            </tr>
                        </table>
                        <br />
                        <br />
                        <a href='' class='close'>Close</a>
                    </p>
                </div>
            </div>
            <div id='container' style="text-align: left">
                <a href='' class='click'>
                    <h2 style="text-align: left"><b>Send Survey Manually</b></h2>
                </a>
                <br />
                <asp:Label ID="lblMsgManual" runat="server" ForeColor="#009933"></asp:Label>
                <div id="divEmail" text align="left"></div>
                <asp:Panel ID="Panel1" runat="server"></asp:Panel>
            </div>
        </div>
        <h3 style="font-size: 200%; text-align: left; padding: 5px 0 0 0; color: #09BCE8;">Upload contacts from Excel</h3>
        <div>
            <table>
                <tr>
                    <td>
                        <asp:LinkButton ID="hlink_download_file" CssClass="hyperlink_download" runat="server"
                            ForeColor="#666666" OnClick="hlink_download_file_Click">Download Demo File</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" class="Cntrl1" Height="33px" />

                        <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="button_example" OnClientClick="return validateFileExtension()" OnClick="btnUpload_Click" />
                    </td>
                </tr>
                <asp:Label ID="lblFileMsg" runat="server" Text="" ForeColor="Red"></asp:Label>

            </table>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <br />
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    <asp:GridView ID="GridView1" runat="server" Width="100%" ShowFooter="true" AutoGenerateColumns="false" CssClass="EU_DataTable" OnPageIndexChanging="PageIndexChanging"
                        AllowPaging="True" EditRowStyle-BackColor="Yellow" EmptyDataText="No Data Found." ShowHeaderWhenEmpty="true" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit">
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="45px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkboxSelectAll" runat="server" Text="Select All" onclick="checkAll(this);" ToolTip="Check" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" Height="30px" onclick="Check_Click(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Email ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmailId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Email ID") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditEmailId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Email ID") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Mobile No">
                                <ItemTemplate>
                                    <asp:Label ID="lblMobileNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Mobile No") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditMobileNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Mobile No") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Alternate Email ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblAlterEmailId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Alternate Email ID") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditAlterEid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Alternate Email ID") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="User Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "User Name") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditUserName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "User Name") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" ImageUrl="../images/icon-edit.png" Height="28px" Width="28px" ToolTip="Edit" />
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ImageUrl="../images/clearIcon.png" Height="22px" Width="22px" ToolTip="Delete" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" ImageUrl="../images/icon-update.png" Height="28px" Width="28px" ToolTip="Update" />
                                    <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="../images/icon-Cancel.png" Height="28px" Width="28px" ToolTip="Cancel" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <PagerStyle BackColor="#404040" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>

                    <table class="style1">
                        <tr>

                            <td class="style4">
                                <asp:Button ID="Btn_submit" runat="server" CssClass="button_example" OnClick="Btn_submit_Click" Text="Send Survey" />
                                <br />
                                <p id="ErrorSendMsg" style="color: red">
                                </p>
                            </td>

                        </tr>

                    </table>
                    <br />
                </ContentTemplate>
                <%-- <Triggers>
                    <asp:PostBackTrigger ControlID="Btn_submit" />
                </Triggers>--%>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
