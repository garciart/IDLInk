<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Server" content="Microsoft-IIS/7.0" />
    <meta http-equiv="X-Powered-By" content="ASP.NET" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>SFD ID Card Portal | Login</title>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/blitzer/jquery-ui.css"/>
    <script src="//code.jquery.com/jquery-1.10.2.min.js"></script>
    <script src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <link rel="stylesheet" href='<%# Page.ResolveUrl("~/App_Themes/Default/StyleSheet.css") %>' />
    <link rel='icon' href='<%# Page.ResolveUrl("~/resources/images/favicon.ico") %>' type='image/x-icon' />
    <link rel='shortcut icon' href='<%# Page.ResolveUrl("~/resources/images/favicon.ico") %>' type='image/x-icon' />
</head>
<body>
    <form id="form1" runat="server">
    <header>
        <span style="width:15%">
            <img src="resources/images/sfd-facebook-profile.png" alt="Welcome to the Salisbury Fire Department Identification Card Portal" title="Welcome to the Salisbury Fire Department Identification Card Portal" style="float:left; width:180px; z-index:0"/>
        </span>
        <h1 style="position:relative; top:70px; z-index:-1">Salisbury Fire Department Identification Card Portal</h1>    
    </header>
    <br style="clear:both" />
    <main style="margin-left:auto; margin-right:auto">
        <div style="display:block; float:left; margin-left:15%">
            <img alt="SOTP!" src="resources/images/sotp.png" title="Log In!" />
        </div>
        <div style="display:block; float:left; margin-left:5%">
            <asp:Login ID="Login1" runat="server" TextLayout="TextOnTop" DestinationPageUrl="~/Default.aspx" CssClass="login" TitleTextStyle-CssClass="login_title" TextBoxStyle-CssClass="login_textbox" FailureTextStyle-CssClass="login_failure" HyperLinkStyle-CssClass="login_hyperlink" HyperLinkStyle-ForeColor="Maroon">
            </asp:Login>
        </div>
    </main>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" LoadScriptsBeforeUI="False">
    </ajaxToolkit:ToolkitScriptManager>
    </form>
</body>
</html>
