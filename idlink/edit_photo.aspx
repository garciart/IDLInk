<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="edit_photo.aspx.cs" Inherits="edit_photo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script type="text/javascript">
    function uploadStarted() {
        $get("imgDisplay").style.display = "none";
    }
    function uploadComplete(sender, args) {
        var imgDisplay = $get("imgDisplay");
        imgDisplay.src = "resources/images/no_picture.png";
        imgDisplay.style.cssText = "";
        var img = new Image();
        img.onload = function () {
            var imgHeight = this.height;
            var imgWidth = this.width;
            var offset = 0;
            imgBox.style.cssText = "width:200px; height:200px; overflow:hidden; position:relative;";
            if (imgWidth >= imgHeight) {
                offset = Math.round(((imgWidth - imgHeight) * 200) / imgHeight) / 2;
                imgDisplay.style.cssText = "position:absolute; left:-" + offset + "px; height:200px;";
            }
            else {
                offset = Math.round(((imgHeight - imgWidth) * 200) / imgWidth) / 2;
                imgDisplay.style.cssText = "position:absolute; top:-" + offset + "px; width:200px;";
            }
            imgDisplay.src = img.src;
        };
        img.src = "<%=ResolveUrl(UploadFolderPath) %>" + "a_photo.png?" + new Date().getTime();
    }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2><asp:Label ID="status_label" runat="server" ForeColor="Red" Visible="false"></asp:Label></h2>
    <h2>Current Photo:</h2>
    <p>
        <asp:Image ID="photoOnFile" runat="server" AlternateText="Photo On File" />
    </p>
    <p>
        Upload a new photo below or click cancel to go back:<br />
        Note - Ensure face is centered; image will be cropped and resized!
    </p>
    <p><ajaxToolkit:AsyncFileUpload ID="photo_upload" runat="server" OnClientUploadStarted = "uploadStarted" OnClientUploadComplete="uploadComplete" OnUploadedComplete="FileUploadComplete" ThrobberID="imgLoader" UploaderStyle="Modern" />
    <p><asp:Image ID="imgLoader" runat="server" ImageUrl="~/resources/images/no_picture.png" /></p>
    <p id="imgBox"><img id="imgDisplay" alt="" src="" style="display:none" /></p>
    <asp:Button ID="submit_button" runat="server" Text="Submit Information" OnClientClick="return confirm('Are you sure your information is correct?');"  onclick="submit_button_click" BackColor="DarkGreen" ForeColor="White" />&nbsp;
    <asp:Button ID="cancel_button" runat="server" Text="Cancel" BackColor="Yellow" ForeColor="Black" onclick="cancel_button_Click" />
    <br />
    <br />
</asp:Content>

