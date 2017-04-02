<%@ Page Title="" Language="C#" MasterPageFile="~/Creator/CreatorMaster.master" AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="Creator_HomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>

        <div align="center">
        </div>
        <b>Profile page</b><br />

        <asp:Label ID="lblCreatorName" runat="server"></asp:Label><br />
        <asp:Label ID="lblUname" runat="server"></asp:Label><br />

        <asp:LinkButton runat="server" ID="lnkFbSignOut" OnClick="lnkFbSignOut_Click" Text="SignOut"></asp:LinkButton><br />
        <asp:LinkButton runat="server" ID="lnkCreateSurvey" Text="Create Survey"></asp:LinkButton><br />
        <asp:LinkButton runat="server" ID="lnkViewReport" Text="View Report"></asp:LinkButton><br />
    </div>
</asp:Content>

