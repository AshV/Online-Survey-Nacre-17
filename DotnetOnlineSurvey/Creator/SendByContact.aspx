<%@ Page Title="" Language="C#" MasterPageFile="~/Creator/NestedSendSurvey.master" AutoEventWireup="true" CodeFile="SendByContact.aspx.cs" Inherits="Creator_SendByContact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="nestedcontent" runat="Server">
    <link href="../css/SendSurvey.css" rel="stylesheet" />
    <script src="../js/SendByExcel.js"></script>
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
                            <td style="text-align: left; vertical-align: top; padding: 0; width: 759px;">
                                <asp:Label ID="lblMessage" ForeColor="#33CC33" runat="server"></asp:Label><br />
                                <asp:GridView ID="gvContacts" runat="server" AutoGenerateColumns="true" AllowPaging="true" CssClass="EU_DataTable" OnRowCreated="gvContacts_RowCreated" OnPageIndexChanging="PageIndexChanging" OnRowDataBound="RowDataBound" Width="100%">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate >
                                                <asp:CheckBox ID="chkboxSelectAll" runat="server" Text="Select All" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" onclick="Check_Click(this)" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 759px">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnSend" runat="server" Text="Send Survey" CssClass="button_example"
                                    OnClick="Btn_submit_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="width: 759px">&nbsp;</td>
                        </tr>
                    </table>

                </fieldset>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

