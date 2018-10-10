<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="new_card.aspx.cs" Inherits="new_card" %>

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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2><asp:Label ID="status_label" runat="server" ForeColor="Red" Visible="false"></asp:Label></h2>
    <h2>Enter your information below (No apostrophes or special characters allowed):</h2>
    <ul>
        <li>First Name:&nbsp;<asp:TextBox ID="fname" runat="server" MaxLength="50" CssClass="textbox"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="fname" ErrorMessage="Required" ForeColor="Red" ></asp:RequiredFieldValidator>
        </li>
        <li>Middle Initial:&nbsp;<asp:TextBox ID="mi" runat="server" MaxLength="1" Columns="1" CssClass="textbox"></asp:TextBox></li>
        <li>Last Name:&nbsp;<asp:TextBox ID="lname" runat="server" MaxLength="50" CssClass="textbox"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="lname" ErrorMessage="Required" ForeColor="Red" ></asp:RequiredFieldValidator>
        </li>
        <li>Date of Birth:&nbsp;<input type="text" id="dob" runat="server" style="background-color:#DDDDDD; margin-top:6px; margin-bottom:6px" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="dob" ErrorMessage="Required" ForeColor="Red" ></asp:RequiredFieldValidator>
            <asp:CompareValidator id="dateValidator" runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="dob" ErrorMessage="Missing or Invalid Date of Birth" ForeColor="Red"></asp:CompareValidator>
        </li>
        <li>Gender:&nbsp;<asp:DropDownList ID="gender" runat="server" CssClass="textbox">
            <asp:ListItem Value="M">Male</asp:ListItem>
            <asp:ListItem Value="F">Female</asp:ListItem>
        </asp:DropDownList></li>
        <li>Height:&nbsp;<asp:DropDownList ID="height" runat="server" CssClass="textbox">
        </asp:DropDownList></li>
        <li>Eye Color:&nbsp;<asp:DropDownList ID="eye_color" runat="server" CssClass="textbox">
            <asp:ListItem Value="BRO">Brown</asp:ListItem>
            <asp:ListItem Value="BLK">Black</asp:ListItem>
            <asp:ListItem Value="BLU">Blue</asp:ListItem>
            <asp:ListItem Value="GRN">Green</asp:ListItem>
            <asp:ListItem Value="GRY">Gray</asp:ListItem>
            <asp:ListItem Value="HAZ">Hazel</asp:ListItem>
            <asp:ListItem Value="MAR">Maroon</asp:ListItem>
            <asp:ListItem Value="PNK">Pink</asp:ListItem>
            <asp:ListItem Value="MUL">Multicolored</asp:ListItem>
            <asp:ListItem Value="XXX">Unknown</asp:ListItem>
        </asp:DropDownList></li>
        <li>Blood Type:&nbsp;<asp:DropDownList ID="blood_type" runat="server" CssClass="textbox">
            <asp:ListItem Value="APOS">A+</asp:ListItem>
            <asp:ListItem Value="ANEG">A-</asp:ListItem>
            <asp:ListItem Value="BPOS">B+</asp:ListItem>
            <asp:ListItem Value="BNEG">B-</asp:ListItem>
            <asp:ListItem Value="ABPS">AB+</asp:ListItem>
            <asp:ListItem Value="ABNG">AB-</asp:ListItem>
            <asp:ListItem Value="OPOS">O+</asp:ListItem>
            <asp:ListItem Value="ONEG">O-</asp:ListItem>
            <asp:ListItem Value="RARE">Other/Rare</asp:ListItem>
            <asp:ListItem Value="UNKN">Unknown</asp:ListItem>
        </asp:DropDownList></li>
            <li>Member Since:&nbsp;<asp:DropDownList ID="member_since" runat="server" CssClass="textbox">
        </asp:DropDownList></li>
        <li><asp:Label id="photo_upload_text" runat="server" Text="Photo: (Ensure face is centered; image will be cropped and resized)" />

            <p><ajaxToolkit:AsyncFileUpload ID="photo_upload" runat="server" OnClientUploadStarted = "uploadStarted" OnClientUploadComplete="uploadComplete" OnUploadedComplete="FileUploadComplete" ThrobberID="imgLoader" UploaderStyle="Modern" /></p>
            <p><asp:Image ID="imgLoader" runat="server" ImageUrl="~/resources/images/no_picture.png" /></p>
            <p id="imgBox"><img id="imgDisplay" alt="" src="" style="display:none" /></p>

        </li>
        <li>Signature:
            <div class="sig sigWrapper" style="padding:12px 0px">
                <div class="typed"></div>
                <canvas class="pad" width="320" height="120" style="border:1px black solid"></canvas>
                <input type="hidden" name="output" class="output" id="output" runat="server" />
            </div>
            <asp:Button ID="clear_button" runat="server" CssClass="clearButton" Text="Reset Signature" PostBackUrl="#clear" BackColor="Red" ForeColor="White"></asp:Button>&nbsp;
            <asp:Button ID="submit" runat="server" Text="Submit Information" OnClientClick="return confirm('Are you sure your information is correct?');"  onclick="submit_Click" BackColor="DarkGreen" ForeColor="White" />
        </li>
    </ul>
</asp:Content>

