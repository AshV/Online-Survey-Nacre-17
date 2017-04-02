<%@ Page Title="" Language="C#" MasterPageFile="~/Creator/ProfileManagementMaster.master" AutoEventWireup="true" CodeFile="ManageGroups.aspx.cs" Inherits="Creator_ManageGroups" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="nestedcontent" Runat="Server">
        <script src="../js/jquery-1.7.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=gvShowAllGroups.ClientID %>').find('input:checkbox[id$="chkParent"]').click(function () {
                var isChecked = $(this).prop("checked");
                $("#<%=gvShowAllGroups.ClientID %> [id*=chkSelect]:checkbox").prop('checked', isChecked);
            });

            $('#<%=gvShowAllGroups.ClientID %>').find('input:checkbox[id$="chkSelect"]').click(function () {
                var flag = true;
                $("#<%=gvShowAllGroups.ClientID %> [id*=chkSelect]:checkbox").each(function () {
                    if ($(this).prop("checked") == false)
                        flag = false;
                });
                $("#<%=gvShowAllGroups.ClientID %> [id*=chkParent]:checkbox").prop('checked', flag);
            });
        });
    </script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <table style="width: 100%">
        <tr>
            <td>
            </td>
            <td></td>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                     <td >Search Based on Group Name:<asp:TextBox ID="txtAutoSearch2" runat="server" AutoPostBack="true" style="font-size: large" OnTextChanged="txtAutoSearch2_TextChanged"></asp:TextBox></td>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" MinimumPrefixLength="1" EnableCaching="true" CompletionInterval="1000" ServiceMethod="GetSuggestions" TargetControlID="txtAutoSearch2"></asp:AutoCompleteExtender>  </ContentTemplate>
            </asp:UpdatePanel>
           
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblError" ForeColor="Red" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr align="center" >
            <td colspan="3" style=" height: 246px;" align="center"><asp:GridView ID="gvShowAllGroups" EmptyDataText="No records Found" runat="server" BackColor="#CCCCCC" BorderColor="#999999"
                BorderWidth="3px" CellPadding="4" Font-Names="Arial" Font-Size="Small"
                AutoGenerateColumns="False" ForeColor="Black" Width="100%" AllowPaging="True"  PageSize="8" BorderStyle="Solid" CellSpacing="2"   Height="5px" OnRowDataBound="gvShowAllGroups_RowDataBound"   OnRowDeleting="gvShowAllGroups_RowDeleting" OnRowEditing="gvShowAllGroups_RowEditing" >
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
               <asp:BoundField HeaderText="Group Id"  ItemStyle-Height="3px"  DataField="GroupId"  />
               <asp:BoundField HeaderText="Group Name" ItemStyle-Height="3px" DataField="GroupName" />
                <asp:BoundField HeaderText="Group Description" ItemStyle-Height="3px" DataField="groupDescription" />
                
                <asp:CommandField  ShowDeleteButton="true" ShowEditButton="True" ControlStyle-Height="35px" ControlStyle-width="85px" ButtonType="Image" EditImageUrl="~/images/Edit.jpeg" DeleteImageUrl="~/images/Delete.jpeg" />
            </Columns>
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView></td>
            <td style="height: 246px">&nbsp;</td>
        </tr>
        <tr align="right">
            <td colspan="3"><asp:Button ID="btnDelete" runat="server" Text="Delete" Width="193px" Height="37px" style="font-size: x-large; font-weight: 700; color: #FFFFFF; background-color: #0066FF" OnClick="btnDelete_Click" /></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

