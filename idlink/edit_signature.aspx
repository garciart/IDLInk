<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="edit_signature.aspx.cs" Inherits="edit_signature" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    $(document).ready(function () {
        var options = {
            penColour: '#000000',
            bgColour: 'transparent',
            drawOnly: true,
            lineTop: 90
        };
        $('.sigPad').signaturePad(options);
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Current Signature:</h2>
    <p>
        <asp:Image ID="signatureOnFile" runat="server" AlternateText="Signature On File" />
    </p>
    <p>
        Enter a new signature below or click the back button to cancel:
    </p>
    <div class="sig sigWrapper" style="padding:12px 0px">
        <div class="typed"></div>
        <canvas class="pad" width="320" height="120" style="border:1px black solid"></canvas>
        <input type="hidden" name="output" class="output" id="output" runat="server" />
    </div>
    <asp:Button ID="clear_button" runat="server" CssClass="clearButton" Text="Reset Signature" PostBackUrl="#clear" BackColor="Red" ForeColor="White"></asp:Button>&nbsp;
    <asp:Button ID="submit_button" runat="server" Text="Submit Information" OnClientClick="return confirm('Are you sure your information is correct?');"  onclick="submit_button_click" BackColor="DarkGreen" ForeColor="White" />&nbsp;
</asp:Content>

