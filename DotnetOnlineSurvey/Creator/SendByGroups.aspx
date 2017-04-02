<%@ Page Title="" Language="C#" MasterPageFile="~/Creator/NestedSendSurvey.master" AutoEventWireup="true" CodeFile="SendByGroups.aspx.cs" Inherits="Creator_SendByGroups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="nestedcontent" Runat="Server">
    <script src="../js/SendByExcel.js"></script>
    <link href="../css/SendSurvey.css" rel="stylesheet" />
    
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <asp:Label ID="lblError" ForeColor="Red" runat="server" Text=""></asp:Label>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>

        <fieldset style="width: auto;">
            <table>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td style="border:thin">
                                    Select Group(s)<br />
                                    <asp:CheckBox Text="Select All" ID="cbSelectAll" runat="server" Checked="true" AutoPostBack="True" OnCheckedChanged="cbSelectAll_CheckedChanged" /><br />
                                    <asp:CheckBoxList ID="clbGroups" runat="server" AutoPostBack="True" OnSelectedIndexChanged="clbGroups_SelectedIndexChanged" Width="177px"></asp:CheckBoxList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td  style="text-align:left;vertical-align:top;padding:0; width: 461px;">                      
                        <asp:Label ID="lblMessage" runat="server" ForeColor="#00CC00"></asp:Label><br />
                        <asp:GridView ID="gvContacts" runat="server" AutoGenerateColumn="true" OnRowCreated="gvContacts_RowCreated" CssClass="EU_DataTable" OnPageIndexChanging="PageIndexChanging" AllowPaging="true" OnRowDataBound="RowDataBound">
                            <Columns>
                              <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkboxSelectAll" runat="server" Text="Select All" onclick="checkAll(this);" />
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" onclick="Check_Click(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <%--  <asp:BoundField HeaderText="surveyTakerID" DataField="surveyTakerID" />--%>
                               <%-- <asp:BoundField HeaderText="User Name" DataField="userName" />--%>
                                <%--<asp:BoundField HeaderText="surveyTakerEmailID" DataField="surveyTakerEmailID" />                                        --%>
                            </Columns>

                         <PagerStyle BackColor="#284775" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="width: 33%"></td>
                    <td style="width: 461px">
                        <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="button_example"
                            OnClick="Btn_submit_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">

                    </td>
                </tr>
            </table>

        </fieldset>
                
            </ContentTemplate>
           <%-- <Triggers>
                    <asp:PostBackTrigger ControlID="btnSend" />
                </Triggers>--%>
        </asp:UpdatePanel>
    </div>
</asp:Content>

