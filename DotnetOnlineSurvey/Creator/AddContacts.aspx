<%@ Page Title="" Language="C#" MasterPageFile="~/Creator/ProfileManagementMaster.master" AutoEventWireup="true" CodeFile="AddContacts.aspx.cs" Inherits="Creator_AddContacts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="nestedcontent" Runat="Server">
     <style type="text/css">
        #overlay {
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
            z-index: 101;
            display: none;
        }

        /*#divEdit {
            background-color: #fff;
            border: 1px solid #000;
            padding: 5px;
            width: 400px;
            line-height: 15px;
            height: 45px;
            overflow: auto;
        }*/

        .content a {
            text-decoration: none;
        }

        .content1 a {
            text-decoration: none;
        }

        .content2 a {
            text-decoration: none;
        }

        .popup {
            margin: 0 auto;
            display: none;
            position: fixed;
            z-index: 101;
        }

        .popup1 {
            margin: 0 auto;
            display: none;
            position: fixed;
            z-index: 101;
        }

        .popup2 {
            margin: 0 auto;
            display: none;
            position: fixed;
            z-index: 101;
        }
        .content4 a {
            text-decoration: none;
        }

        .popup4 {
            width: 100%;
            margin: 0 auto;
            display: none;
            position: fixed;
            z-index: 101;
        }

        .content4{
            min-width: 600px;
            width: 600px;
            min-height: 150px;
            margin: 100px auto;
            background: #6B8E23;
            position: relative;
            z-index: 103;
            padding: 10px;
            border-radius: 5px;
            box-shadow: 0 2px 5px #000;
        }

            .content4 p {
                clear: both;
                color: #555555;
                text-align: justify;
            }

                .content4 p a {
                    color: #d91900;
                    font-weight: bold;
                }

                .content4 p:hover {
                    cursor: pointer;
                }
                
            .content4 .x {
                float: right;
                height: 35px;
                left: 22px;
                position: relative;
                top: -25px;
                width: 34px;
            }

                .content4.x:hover {
                    cursor: pointer;
                }


        .content {
            min-height: 50px;
            background: #6B8E23;
            position: fixed;
            z-index: 50;
            padding: 10px;
            border-radius: 5px;
            box-shadow: 0 2px 5px #000;
        }

        .content1 {
            min-height: 50px;
            background: #6B8E23;
            position: fixed;
            z-index: 50;
            padding: 10px;
            border-radius: 5px;
            box-shadow: 0 2px 5px #000;
        }

        .content2 {
            min-height: 50px;
            background: #6B8E23;
            position: fixed;
            z-index: 50;
            padding: 10px;
            border-radius: 5px;
            box-shadow: 0 2px 5px #000;
        }

        .content p {
            clear: both;
            color: #555555;
            text-align: justify;
        }

        .content1 p {
            clear: both;
            color: #555555;
            text-align: justify;
        }

        .content2 p {
            clear: both;
            color: #555555;
            text-align: justify;
        }

        .content p a {
            color: #d91900;
            font-weight: bold;
        }

        .content1 p a {
            color: #d91900;
            font-weight: bold;
        }

        .content2 p a {
            color: #d91900;
            font-weight: bold;
        }

        .content .x {
            float: right;
            height: 35px;
            left: 22px;
            position: relative;
            top: -25px;
            width: 34px;
        }

        .content1 .x {
            float: right;
            height: 35px;
            left: 22px;
            position: relative;
            top: -25px;
            width: 34px;
        }

        .content2 .x {
            float: right;
            height: 35px;
            left: 22px;
            position: relative;
            top: -25px;
            width: 34px;
        }

        .content .x:hover {
            cursor: pointer;
        }

        .content1 .x:hover {
            cursor: pointer;
        }

        .content2 .x:hover {
            cursor: pointer;
        }

        /*.red {
            color: #ff0022;
        }*/

        /*.auto-style6 {
            width: 183px;
        }

        .auto-style8 {
            width: 263px;
        }

        .auto-style9 {
            width: 37px;
        }*/
        .auto-style3 {
            width: 100%;
        }
    </style>
    <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
    <meta http-equiv='X-UA-Compatible' content='IE=edge,chrome=1' />
    <link rel='stylesheet' type='text/css' href='styles2.css' />
    <script src='http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js'></script>
    <script type='text/javascript' src='menu_jquery.js'></script>
    <link href='http://fonts.googleapis.com/css?family=Yanone+Kaffeesatz:700' rel='stylesheet' type='text/css'>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.js"></script>
    <link rel="stylesheet" type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.1/themes/base/jquery-ui.css" />
    <script src="jquery/braviPopup.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>

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

            $('#<%=btngmail.ClientID %>').click(function () {
                overlay.show();
                //overlay.appendTo(document.body);
                $('.popup').show();
                $('.popup1').hide();
                $('.popup2').hide();
                $('.popup3').hide();
                $('.popup4').hide();
                return false;
            });

        });
        $(function () {
            var overlay = $('<div id="overlay"></div>');
            $('.close').click(function () {
                $('.popup1').hide();
                overlay.appendTo(document.body).remove();
                return false;
            });

            $('.x').click(function () {
                $('.popup1').hide();
                overlay.appendTo(document.body).remove();
                return false;
            });

            $('#<%=btnExcel.ClientID %>').click(function () {
                overlay.show();
                //  overlay.appendTo(document.body);
                $('.popup1').show();
                $('.popup').hide();

                $('.popup2').hide();
                $('.popup3').hide();
                $('.popup4').hide();

                return false;
            });
        });
        $(function () {
            var overlay = $('<div id="overlay"></div>');
            $('.close').click(function () {
                $('.popup2').hide();
                overlay.appendTo(document.body).remove();
                return false;
            });

            $('.x').click(function () {
                $('.popup2').hide();
                overlay.appendTo(document.body).remove();
                return false;
            });

            $('#<%=btnmanual.ClientID %>').click(function () {
                overlay.show();
                //overlay.appendTo(document.body);
                $('.popup2').show();
                $('.popup').hide();
                $('.popup1').hide();

                $('.popup3').hide();
                $('.popup4').hide();

                return false;
            });
        });

        $(function () {
            var overlay1 = $('<div id="overlay"></div>');
            $('.close').click(function () {
                $('.popup4').hide();
                overlay1.appendTo(document.body).remove();
                return false;
            });

            $('.x').click(function () {
                $('.popup4').hide();
                overlay1.appendTo(document.body).remove();
                return false;
            });

            $('#<%=btnAddToGroup.ClientID%>').click(function () {
                overlay1.show();
                overlay1.appendTo(document.body);
                $('.popup4').show();
                $('.popup').hide();
                $('.popup1').hide();
                $('.popup2').hide();
                $('.popup3').hide();


                return false;

            });
        });
        $(function () {
            $('#<%=ddlExcel.ClientID%>').attr("disabled", true);
            $('#<%=fuExcel.ClientID %>').change(
                function () {
                    var fileExtension = ['xlsx', 'xls'];
                    if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                        // alert("Only '.jpeg','.jpg' formats are allowed.");
                        $('#<%=ddlExcel.ClientID%>').attr("disabled", true);
                        $('#<%=btnSubmit.ClientID %>').attr("disabled", true);

                        $('#<%= myLabel.ClientID %>').html("Only excel formats are allowed.");

                    }
                    else {
                        $('#<%=ddlExcel.ClientID%>').attr("disabled", false);
                        $('#<%=btnSubmit.ClientID %>').attr("disabled", false);
                        $('#<%= myLabel.ClientID %>').html(" ");

                    }
                })
        })
            $(document).ready(function () {
                $('#<%=btnSubmit.ClientID%>').click(function () {
                if ($('#<%=ddlExcel.ClientID%>').val() > 0) {

                    return true;
                }
                else {
                    alert('Please select Excel File and then Excel Sheet');
                    return false;
                }
            })
        });

        <%-- $(function () {
            $('#<%=btnsubmit1.ClientID%>').click(function () {
                var textEntered = $('#<%=txtEmailId.ClientID%>').text()
                var textResult = [];

                //if ($.trim(textEntered).length == 0) {
                //    alert("Please enter email address");
                //    return false;
                //}

                $.each(textEntered.split(','), function (index, item) {
                    if (item.match(/^\S+@\S+\.\S+$/)) {
                        textResult.push(item);
                    }
                    else {
                        textResult.push('<span class="red">' + item + '</span>');
                    }
                });
                $('#<%=txtEmailId.ClientID%>').html(textResult.join(','));
            });
        });--%>
        <%-- $(document).ready(function () {
            $('#<%=btnsubmit1.ClientID%>').click(function () {
                var textEntered = $('#divEdit').text();
                var textResult = [];
                var count = 0;




                if ($.trim(textEntered).length == 0) {
                    alert("Please enter email address");
                    return false;
                }



                


                $.each(textEntered.split(','), function (index, item) {
                    if (item.match(/^\S+@\S+\.\S+$/)) {
                        textResult.push(item);

                    }

                    else {

                        textResult.push('<span class="red">' + item + '</span>');
                        count++;

                    }

                });



                $('#divEdit').html(textResult.join(','));


                if (count > 0) {
                    alert("Invalid Email Addresses");
                    textResult.push('<span class="red">' + item + '</span>');
                    return false;
                }
                if (count == 0) {
                    alert("Valid Email Addressess");
                    return true;
                }
            });


        });--%>
        //$('#tblNewGroup').fadeOut();
        //$('#tblExistingGroup').fadeOut();

        $('#btnSubmit').click(function () {
            if ($('#ddlGroupName').val() > 0) {
                return true;
            }
            else {
                alert("please select any one group");
                return false;
            }
        });

        $('#<%=btnAddToGroup.ClientID%>').click(function () {
            rbGroupName.visible;
            rbExistingGroup.visible;
        });


        $('#<%=rbGroupName.ClientID%>').click(function () {

            $('#tblNewGroup').fadeIn();
            $('#tblExistingGroup').fadeOut();


        });

        $('#<%=rbExistingGroup.ClientID%>').click(function () {

            $('#tblExistingGroup').fadeIn();
            $('#tblNewGroup').fadeOut();

        });
        $('#<%=btnAdd.ClientID%>').click(function () {
            var name = $('#<%=txtgroup.ClientID%>').val();
            var groupdesc = $('#<%=txtDescription.ClientID%>').val();
            if (name == '' || groupdesc == '') {
                $('#<%=lblresult.ClientID%>').text = "please enter group name or description";
                    return false;
                }
                else {
                    alert(" Successfully group name added");
                    return true;
                }
        });
            $('#<%=btnGroup.ClientID%>').click(function () {
            if ($('#ddlGroup').val() > 0) {
                alert("successfully added to existing group");
                return true;
            }
            else {
                alert("please select any group");
                return false;
            }
        });




        $(document).ready(function () {
            $('#<%=btnImport.ClientID%>').click(function () {
                var EmailText = $('#<%=txtUserName.ClientID%>').val();
                var PwdText = $('#<%=txtPassword.ClientID%>').val();
                var emailExp = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([com\co\.\in])+$/;
                if (EmailText == '' && PwdText == '') {
                    alert("Please enter email address and Password");
                    return false;
                }

                if (EmailText == '') {
                    alert("Please Enter Email ID");
                    return false;
                }

                if (PwdText == '') {
                    alert("Please Enter Password");
                    return false;
                }
                //if (($.trim(PwdText).length <= 8) || ($.trim(PwdText).length >= 16)) {
                //    alert("Password should be btween 8 and 16 characters");
                //    return false;
                //}

                if (EmailText != '') {
                    if (!EmailText.match(emailExp)) {
                        alert("Invalid Email Id");
                        return false;
                    }
                }

                return true;


            });
        });

        $(document).ready(function () {
            $('#<%=btnSubmit1.ClientID%>').click(function () {
                var EmailText = $('#<%=txtEmailId.ClientID%>').val();
                if ($.trim(EmailText).length == 0) {
                    alert("Please enter email address");
                    return false;
                }
                if (validateEmail(EmailText)) {
                    
                    return true;
                }
                else {
                    alert('Invalid Email Address');
                    return false;
                }
            });
        });
        function validateEmail(sEmail) {


            var filter = /^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[,]{0,1}\s*)+$/;
            if (filter.test(sEmail)) {


                return true;
            }
            else {


                return false;
            }
        }

    </script>
    <script src="../js/jquery-1.7.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=gvShowAllContacts.ClientID %>').find('input:checkbox[id$="chkParent"]').click(function () {
                var isChecked = $(this).prop("checked");
                $("#<%=gvShowAllContacts.ClientID %> [id*=chkSelect]:checkbox").prop('checked', isChecked);
            });

            $('#<%=gvShowAllContacts.ClientID %>').find('input:checkbox[id$="chkSelect"]').click(function () {
                var flag = true;
                $("#<%=gvShowAllContacts.ClientID %> [id*=chkSelect]:checkbox").each(function () {
                    if ($(this).prop("checked") == false)
                        flag = false;
                });
                $("#<%=gvShowAllContacts.ClientID %> [id*=chkParent]:checkbox").prop('checked', flag);
            });
        });
    </script>
    <div class='popup4'>
            <div class='content4'>
                <img src="../images/Close1.jpg" alt='quit' class='x' id='Img3' />
               
                 <asp:RadioButton ID="rbGroupName" runat="server" GroupName="rd" Text="New Group" />
                    <br />
                    <table id="tblNewGroup" style="width:600px">
                        <tr>
                            <td style="width:100px">
                                Enter new group name:
                            </td>
                            <td style="width:100px"> 
                                <asp:TextBox runat="server" ID="txtgroup" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width:100px">
                               Enter Description:
                            </td>
                            <td style="width:100px">
                              <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnAdd" OnClick="btnAdd_Click" runat="server" Text="Add"  />
                                <asp:Label runat="server" ID="lblresult"></asp:Label>
                            </td>
                        </tr>
                    </table>
                   
                 <asp:RadioButton ID="rbExistingGroup" runat="server" GroupName="rd" Text="Existing Group" />
                <br />
                <table id="tblExistingGroup" style="width:500px">
                    <tr>
                        <td style="width:250px" >
                            choose existing group name:
                        </td>
                        <td style="width:250px" >
                            <asp:DropDownList ID="ddlGroup" runat="server" >
                               
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           <asp:Button ID="btnGroup" OnClick="btnGroup_Click" runat="server" Text="AddtoGroup" /> </td>
                    </tr>
                </table>
                
            </div>
        </div>
    <div class='popup'>
        <div class='content'>
            <img src="../images/Close1.jpg" alt='quit' class='x' id='Img2' />

            <table align="center" border="0" cellpadding="0" cellspacing="0" style="background-color: #fff" width="100%">
                <tr>
                    <td align="center" colspan="2">User Login</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="Center">UserName:<asp:TextBox ID="txtUserName" TextMode="SingleLine" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="Center">Password:<asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="Center">
                        <asp:Button ID="btnImport" Text="Submit" runat="server" OnClick="btnImport_Click" /></td>
                </tr>
            </table>

            <br />
            <br />
            <a href='' class='close'>Close</a>
        </div>
    </div>
    <div class='popup1'>
        <div class='content1'>

            <img src="../images/Close1.jpg" alt='quit' class='x' id='x' />
            <p>
                <div align="center">
                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="background-color: #fff" width="100%">
                        <tr align="center">
                            <td align="center" colspan="2">Browse your Excel File</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr align="center">
                            <td align="center">
                                <asp:FileUpload ID="fuExcel" runat="server" /><asp:Label ID="myLabel" runat="server" ForeColor="Red"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr align="center">
                            <td align="center">
                                <asp:DropDownList ID="ddlExcel" runat="server">
                                    <asp:ListItem Selected="True">Select Excel Sheet</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                </asp:DropDownList></td>

                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr align="center">
                            <td align="center">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" /></td>
                        </tr>
                    </table>
                </div>
                <br />
                <br />
                <a href='' class='close'>Close</a>
            </p>
        </div>
    </div>

    <table>
        <tr>
            <td colspan="6">
                <asp:Label ID="lblEror" runat="server" ForeColor="Red" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btngmail" runat="server" Text="Gmail" CssClass="btn" /></td>
            <td></td>
            <td>
                <asp:Button runat="server" ID="btnExcel" Text="Excel" CssClass="btn" /></td>


            <td></td>
            <td>
                <asp:Button runat="server" ID="btnmanual" Text="Manual" CssClass="btn" OnClick="btnmanual_Click" /></td>
        </tr>

    </table>


    <div class='popup2'>
        <div class='content2'>

            <img src="../images/Close1.jpg" alt='quit' class='x' id='Img1' />
            <%-- <table align="center"  border="0" cellpadding="0" cellspacing="0" style="background-color:#fff" width="100%">
        <tr>
            <td align="center" colspan="2" >Enter Email Id with comma seperation</td>
        </tr>
         <tr>
             <td>&nbsp;</td>
         </tr>
       <tr>
         <td align="center"><asp:TextBox ID="txtEmailId" TextMode="MultiLine" runat="server" ></asp:TextBox></td>
       </tr>
        <tr>
         <td>&nbsp;</td>
        </tr>
        <tr>
          <td align="center"><asp:Button ID="btnsubmit1"  runat="server" Text="Submit" OnClientClick="EmailCheck()"/></td>
        </tr>
</table>--%>
             <table align="center"  border="0" cellpadding="0" cellspacing="0" style="background-color:#fff" width="100%">
 <tr>
<td align="center" colspan="2" >Enter Email Id's with Comma seperation</td>
 </tr>
     <tr><td>&nbsp;</td></tr>
     <tr>

<td align="center"><asp:TextBox ID="txtEmailId" TextMode="MultiLine" Height="102px" runat="server" Width="227px" ></asp:TextBox></td>
         
</tr>
     <tr><td>&nbsp;</td></tr>
     <tr><td align="center"><asp:Button ID="btnSubmit1" runat="server" Text="Submit" OnClick="btnSubmit1_Click"/></td></tr>
</table>

            <%--<asp:Label ID="lblmsg" runat="server" Text="Currently you are not having any Contacts...!Please select one of option From Above..!"></asp:Label>--%>
            <br />
            <br />
            <a href='#' class='close'>Close</a>
        </div>
    </div>
    <div align="center">
        <%--<asp:Label ID="lblmsg" runat="server" Text="Currently you are not having any Contacts...!Please select one of option From Above..!"></asp:Label>--%>
        <table class="auto-style3">
            <tr>
                <td>&nbsp;</td>
                <td>
                    <div align="center">
                        <%--<asp:BoundField HeaderText="Email ID" ItemStyle-Height="7px" DataField="EmailID" />--%>
                        <asp:GridView ID="gvShowAllContacts"  EmptyDataText="No records Found" runat="server" BackColor="#CCCCCC" BorderColor="#999999"
                            BorderWidth="3px" CellPadding="4" Font-Names="Arial" Font-Size="Small" OnRowCancelingEdit="gvShowAllContacts_RowCancelingEdit" OnRowUpdating="gvShowAllContacts_RowUpdating"
                            AutoGenerateColumns="False" ForeColor="Black" Width="100%" AllowPaging="True" PageSize="8" BorderStyle="Solid" CellSpacing="2" Height="0px" OnPageIndexChanging="gvShowAllContacts_PageIndexChanging" OnRowDeleting="gvShowAllContacts_RowDeleting" OnRowEditing="gvShowAllContacts_RowEditing">
                            <FooterStyle BackColor="#CCCCCC" />
                            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#000099" ForeColor="White" Font-Bold="True" />
                            <HeaderStyle BackColor="Black" Font-Bold="True" HorizontalAlign="Left" ForeColor="White" />
                            <RowStyle HorizontalAlign="Left" BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText=" ">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkParent" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField HeaderText="Email ID" ItemStyle-Height="7px" DataField="EmailID" />--%>

                                 <asp:TemplateField HeaderText="Email ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmailId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "EmailID") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditEmailId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "EmailID") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" ImageUrl="../images/Edit.jpeg" ToolTip="Edit" />
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ImageUrl="../images/Delete.jpeg"  ToolTip="Delete" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" ImageUrl="../images/update.jpg" ToolTip="Update" />
                                    <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="../images/Cancel.jpg" ToolTip="Cancel" />
                                </EditItemTemplate>                                   
                            </asp:TemplateField>

                               <%-- <asp:CommandField ShowDeleteButton="true" ShowEditButton="true" ShowCancelButton="True" ControlStyle-Height="35px" ControlStyle-Width="85px" 
                                    ButtonType="Image" DeleteImageUrl="../images/Delete.jpeg" EditImageUrl="../images/Edit.jpeg" UpdateImageUrl="../images/Update.jpeg" 
                                    CancelImageUrl="../images/Cancel.jpeg" />--%>

                            </Columns>

                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#808080" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#383838" />
                        </asp:GridView>
                        <br />
                        <asp:Label ID="lblMessages" runat="server" Font-Underline="True"></asp:Label>
                    </div>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
 <asp:ImageButton ID="btnSaveToDatabase" runat="server" ImageUrl="../images/SaveToDatabase.jpg" OnClick="btnSaveToDatabase_Click" />&nbsp;
        <asp:ImageButton ID="btnAddToGroup" runat="server" ImageUrl="../images/AddToGroup.jpeg"  />
    
    </div>
</asp:Content>

