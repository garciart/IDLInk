<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" %>

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
<style>
    .ModalPopupBG
    {
        background-color: #333;
        filter: alpha(opacity=50);
        opacity: 0.5;
    }

    .HellowWorldPopup
    {
        background:white;
        border:3px solid Black;
        padding:6px;
        width:320px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:button id="Button1" runat="server" text="Change Picture" />
    <br />
    <br />
    <asp:button id="Button2" runat="server" text="Change Signature" />

    <ajaxToolkit:ModalPopupExtender id="ModalPopupExtender1" runat="server" 
	    cancelcontrolid="pic_cancel_btn" okcontrolid="pic_ok_btn" 
	    targetcontrolid="Button1" popupcontrolid="Panel1" 
	    popupdraghandlecontrolid="PopupHeader" drag="true" 
	    backgroundcssclass="ModalPopupBG">
    </ajaxToolkit:ModalPopupExtender>

    <ajaxToolkit:ModalPopupExtender id="ModalPopupExtender2" runat="server" 
	    cancelcontrolid="sig_cancel_btn" okcontrolid="sig_ok_btn" 
	    targetcontrolid="Button2" popupcontrolid="Panel2" 
	    popupdraghandlecontrolid="PopupHeader" drag="true" 
	    backgroundcssclass="ModalPopupBG">
    </ajaxToolkit:ModalPopupExtender>

    <asp:panel id="Panel1" style="display: none" runat="server">
	    <div class="HellowWorldPopup">
            <p>
                Upload a new picture:
            </p>
            <p><ajaxToolkit:AsyncFileUpload ID="photo_upload" runat="server" OnClientUploadStarted = "uploadStarted" OnClientUploadComplete="uploadComplete" OnUploadedComplete="FileUploadComplete" ThrobberID="imgLoader" UploaderStyle="Modern" /></p>
            <p><asp:Image ID="imgLoader" runat="server" ImageUrl="~/resources/images/no_picture.png" /></p>
            <p id="imgBox"><img id="imgDisplay" alt="" src="" style="display:none" /></p>
            <div class="Controls">
                <input id="pic_ok_btn" type="button" value="Done" />
                <input id="pic_cancel_btn" type="button" value="Cancel" />
		    </div>
        </div>
    </asp:panel>

    <asp:panel id="Panel2" style="display: none" runat="server">
	    <div class="HellowWorldPopup">
            <p>
                Enter a new signature below:
            </p>
            <div class="sig sigWrapper" style="padding:12px 0px">
                <div class="typed"></div>
                <canvas class="pad" width="320" height="120" style="border:1px black solid"></canvas>
                <input type="hidden" name="output" class="output" id="output" runat="server" />
            </div>
            <div class="Controls">
                <asp:Button ID="clear_button" runat="server" CssClass="clearButton" Text="Reset Signature" PostBackUrl="#clear"></asp:Button>
                <input id="sig_ok_btn" type="button" value="Done" />
                <input id="sig_cancel_btn" type="button" value="Cancel" />
		    </div>
        </div>
    </asp:panel>
</asp:Content>

