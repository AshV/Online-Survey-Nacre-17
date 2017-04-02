<%@ Page Title="" Language="C#" MasterPageFile="~/Creator/ProfileManagementMaster.master" AutoEventWireup="true" CodeFile="ManageContact.aspx.cs" Inherits="Creator_ManageContact" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.5.40412.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="nestedcontent" Runat="Server">
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
    <table>
        <tr>
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="txtAutoSearch" />
                </Triggers>
                <ContentTemplate>
                    <td colspan="3">Search Based on Email Id:<asp:TextBox ID="txtAutoSearch" runat="server" AutoPostBack="true" Style="font-size: large" OnTextChanged="txtAutoSearch_TextChanged"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txtAutoSearch_AutoCompleteExtender" runat="server" MinimumPrefixLength="1" EnableCaching="true" CompletionInterval="1000" ServiceMethod="GetSuggestions" TargetControlID="txtAutoSearch">
                        </asp:AutoCompleteExtender>
                    </td>
                </ContentTemplate>   

            </asp:UpdatePanel>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblError" ForeColor="Red" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr align="center">
            <td style="width: 146px;"></td>
            <td style="width: 241px;" align="center">
                <asp:GridView ID="gvShowAllContacts" EmptyDataText="No records Found" runat="server" BackColor="#CCCCCC" BorderColor="#999999"
                    BorderWidth="3px" CellPadding="4" Font-Names="Arial" Font-Size="Small"
                    AutoGenerateColumns="False" ForeColor="Black" Width="100%" AllowPaging="True"  PageSize="8" BorderStyle="Solid" CellSpacing="2" OnPageIndexChanging="gvShowAllContacts_PageIndexChanging" OnRowDeleting="gvShowAllContacts_RowDeleting" Height="5px" OnSelectedIndexChanged="gvShowAllContacts_SelectedIndexChanged" OnRowEditing="gvShowAllContacts_RowEditing" OnRowUpdating="gvShowAllContacts_RowUpdating" OnRowCancelingEdit="gvShowAllContacts_RowCancelingEdit" OnDataBound="gvShowAllContacts_DataBound" OnRowDataBound="gvShowAllContacts_RowDataBound">
                    <FooterStyle Width="50px" BackColor="#CCCCCC" />
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
                        <asp:BoundField HeaderText="User ID" ItemStyle-Height="7px" ReadOnly="True" DataField="surveyTakerID" >
<ItemStyle Height="7px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="User Name" ItemStyle-Height="7px" DataField="userName" >
<ItemStyle Height="7px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="SurveyTaker EmailID" ItemStyle-Height="7px" DataField="surveyTakerEmailID" >
<ItemStyle Height="7px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Alternative EmailId" ItemStyle-Height="7px" DataField="alternateEmailID" >
<ItemStyle Height="7px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Mobile Number" ItemStyle-Height="7px" DataField="mobileNumber" >
<ItemStyle Height="7px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Company Name" ItemStyle-Height="7px" DataField="companyName" ReadOnly="True" >
<ItemStyle Height="7px"></ItemStyle>
                        </asp:BoundField>
                        <asp:CommandField ShowEditButton="true" ControlStyle-Height="35px" ControlStyle-Width="85px" ButtonType="Image" EditImageUrl="../images/Edit.jpeg" UpdateImageUrl="~/images/Update.jpeg" CancelImageUrl="~/images/Cancel.jpeg" >
<ControlStyle Height="35px" Width="85px"></ControlStyle>
                        </asp:CommandField>
                        <asp:CommandField ShowDeleteButton="true" ControlStyle-Height="35px" ControlStyle-Width="85px" ButtonType="Image" DeleteImageUrl="../images/delete.jpeg" >
<ControlStyle Height="35px" Width="85px"></ControlStyle>
                        </asp:CommandField>
                    </Columns>
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                </asp:GridView>
            </td>

            <td style="height: 246px">&nbsp;</td>
        </tr>
        <tr align="right">
            
                 <td colspan="3"><asp:Button ID="btnDelete" runat="server" Text="Delete" Width="193px" Height="37px" style="font-size: x-large; font-weight: 700; color: #FFFFFF; background-color: #0066FF" OnClick="btnDelete_Click" />
                <asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes" Width="193px" Height="37px" Style="font-size: x-large; font-weight: 700; color: #FFFFFF; background-color: #0066FF" OnClick="btnSaveChanges_Click" />
            
        </tr>
         <tr>
            <td><asp:Label ID="lblMessage" runat="server"></asp:Label></td>
        </tr>
    </table>
</asp:Content>

