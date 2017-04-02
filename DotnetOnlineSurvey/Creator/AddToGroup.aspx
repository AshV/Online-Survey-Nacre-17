<%@ Page Title="" Language="C#" MasterPageFile="~/Creator/ProfileManagementMaster.master" AutoEventWireup="true" CodeFile="AddToGroup.aspx.cs" Inherits="Creator_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
                <asp:Label ID="lblError" runat="server" ForeColor="Red" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                     <td colspan="3">Search Based on Email Id:<asp:TextBox ID="txtAutoSearch1" runat="server" AutoPostBack="true" style="font-size: large" OnTextChanged="txtAutoSearch1_TextChanged"></asp:TextBox></td>
            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" MinimumPrefixLength="1" EnableCaching="true" CompletionInterval="1000" ServiceMethod="GetSuggestions" TargetControlID="txtAutoSearch1" ></asp:AutoCompleteExtender>
                </ContentTemplate>
            </asp:UpdatePanel>
             </tr>
        <tr align="center">
            <td style="height: 246px; width: 146px;"></td>
            <td style="width: 241px; height: 246px;" align="center">   <asp:GridView ID="gvShowAllContacts"  EmptyDataText="No records Found" runat="server" BackColor="#CCCCCC" BorderColor="#999999"
                BorderWidth="3px" CellPadding="4" Font-Names="Arial" Font-Size="Small"
                AutoGenerateColumns="False" ForeColor="Black" Width="100%" AllowPaging="True" PageSize="8" BorderStyle="Solid" CellSpacing="2" OnPageIndexChanging="gvShowAllContacts_PageIndexChanging" OnRowEditing="addtogroup_click" OnRowDeleting="gvShowAllContacts_RowDeleting" Height="5px" OnSelectedIndexChanged="gvShowAllContacts_SelectedIndexChanged" OnRowDataBound="gvShowAllContacts_RowDataBound" >
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
                                   
               <asp:BoundField HeaderText="SurveyTaker EmailID" ItemStyle-Height="7px" DataField="surveyTakerEmailID" />
               <asp:TemplateField >
                   <ItemTemplate>
                       <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../images/addtogroup.jpeg" OnClick="addtogroup_click"/>
                   </ItemTemplate>
                    </asp:TemplateField>
        
                
            </Columns>
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
              
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView></td>

            <td style="height: 246px">&nbsp;</td>
        </tr>
       
         </table> 
  

</asp:Content>

