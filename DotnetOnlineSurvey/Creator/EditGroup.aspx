<%@ Page Title="" Language="C#" MasterPageFile="~/Creator/ProfileManagementMaster.master" AutoEventWireup="true" CodeFile="EditGroup.aspx.cs" Inherits="Creator_EditGroup" %>

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
    <table >
        <tr>
            <td>
                <asp:Label ID="lblError" ForeColor="Red" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">Search Based on Email Id:<asp:TextBox ID="txtAutoSearch" runat="server" style="font-size: large"></asp:TextBox></td>
        </tr>
        <tr align="center">
            <td style="height: 246px; width: 146px;"></td>
            <td style="width: 241px; height: 246px;" align="center">   <asp:GridView ID="gvShowAllContacts"  EmptyDataText="No records Found" runat="server" BackColor="#CCCCCC" BorderColor="#999999"
                BorderWidth="3px" CellPadding="4" Font-Names="Arial" Font-Size="Small" AllowPaging="True" PageSize="8"
                AutoGenerateColumns="False" ForeColor="Black" Width="100%"  BorderStyle="Solid" CellSpacing="2" OnPageIndexChanging="gvShowAllContacts_PageIndexChanging" OnRowDeleting="gvShowAllContacts_RowDeleting" Height="5px" OnSelectedIndexChanged="gvShowAllContacts_SelectedIndexChanged" OnRowEditing="gvShowAllContacts_RowEditing"  OnDataBound="gvShowAllContacts_DataBound" OnRowDataBound="gvShowAllContacts_RowDataBound" >
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
                   <asp:BoundField HeaderText="SurveyTaker ID" DataField="surveyTakerID"  />                
               <asp:BoundField HeaderText="SurveyTaker EmailID" ItemStyle-Height="7px" DataField="surveyTakerEmailID" />
               
                 <%--<asp:CommandField ShowEditButton="true" ControlStyle-Height="35px" ControlStyle-width="85px" ButtonType="Image" EditImageUrl="~/images/Edit.jpeg"  UpdateImageUrl="~/images/update.jpg" CancelImageUrl="~/images/Cancel.jpg" />--%>
                <asp:CommandField  ShowDeleteButton="true" ControlStyle-Height="35px" ControlStyle-width="85px" ButtonType="Image"  DeleteImageUrl="~/images/Delete.jpeg" />
            </Columns>
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView></td>

            <td style="height: 246px">&nbsp;</td>
        </tr>
        <tr align="right">
            <td colspan="3"><asp:Button ID="btnAddGroup" runat="server" Text="Add to Group" Width="193px" Height="37px" style="font-size: x-large; font-weight: 700; color: #FFFFFF; background-color: #0066FF" OnClick="btnAddGroup_Click" /></td>
        </tr>
         </table> 
</asp:Content>

