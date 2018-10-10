<%@ Page Title="Error!" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="error.aspx.cs" Inherits="error" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="padding:6px; text-align:center">
        <br />
        <asp:Image ID="errorImage" runat="server" ImageUrl="~/resources/images/error.gif" AlternateText="Is it dead?" ToolTip="Is it dead?" />
        <br />
        <h1>Houston, we've had a problem.</h1>
        <p>We apologize for the inconvenience, but your request has caused an error!</p>
        <p><asp:Label ID="errorMessageLabel" runat="server" /></p>
        <p>Please contact us at customerservice@shoreps.com as soon as possible.</p>
        <p>We'll retrive the generated error report and try to resolve this issue as soon as possible.</p>
    </div>
</asp:Content>