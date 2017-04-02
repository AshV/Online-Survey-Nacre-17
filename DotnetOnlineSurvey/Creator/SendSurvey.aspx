<%@ Page Title="" Language="C#" MasterPageFile="~/Creator/NestedSendSurvey.master" AutoEventWireup="true" CodeFile="SendSurvey.aspx.cs" Inherits="Creator_SendSurvey" %>

<asp:Content ID="Content1" ContentPlaceHolderID="nestedcontent" Runat="Server">
    <link href="../css/SendSurvey.css" rel="stylesheet" />
    <script src="../js/SendByExcel.js"></script>
    <script type="text/javascript">
        function showSendSurveyAlert(msg) {
            if (msg == "") {
                v.innerHTML = "";
                //alert("call");
            }
            else {
                var v = document.getElementById("ErrorSendMsg");
                v.style.color = "red";
                v.innerHTML = msg;
                //alert("call");
            }
        }

        function RedirectPage() {
            self.location = "SendByGroups.aspx";
        }
        </script> 
    <div>
        <asp:Label ID="lblError" ForeColor="Red" runat="server" Text=""></asp:Label>
    </div>
    <div>
        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="false" CssClass="EU_DataTable" OnPageIndexChanging="GridView1_PageIndexChanging"
            AllowPaging="True" OnRowDataBound="RowDataBound" OnRowCreated="GridView1_RowCreated" OnRowCommand="GridView1_RowCommand">
            <Columns>               
               
                <asp:TemplateField HeaderText="Survey Name">
                    <ItemTemplate>
                        <asp:Label ID="lblSurveyId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "surveyID") %>'></asp:Label>
                    </ItemTemplate>                   
                </asp:TemplateField>                

                 <asp:TemplateField HeaderText="Title">
                    <ItemTemplate>
                        <asp:Label ID="lbltitle" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "title") %>'></asp:Label>
                    </ItemTemplate>                   
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="lblIntroduction" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "introduction") %>'></asp:Label>
                    </ItemTemplate>                   
                </asp:TemplateField>

                 <asp:TemplateField  HeaderText="Select Survey">                    
                    <ItemTemplate>                        
                        <%-- <asp:Button ID="btnSelectSurvey" runat="server" Text="Select" OnClientClick="RedirectPage()" />--%>
                        <%--<input type="button" CommandArgument='<%# Eval("")  %>' value="Select" />--%>
                        <asp:Button Text="Send" ID="btnSend" CommandArgument='<%#Eval("surveyID") %>' CommandName="surveyID" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
               </Columns>
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
         <p id="ErrorSendMsg" style="color:red">
    </div>
</asp:Content>

