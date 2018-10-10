<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="edit_member.aspx.cs" Inherits="edit_member" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Editing Member...</h2>
    <h3><asp:Label ID="status_label" runat="server" ForeColor="Red" Visible="false"></asp:Label></h3>
    <ul>
        <li>
            <asp:Label ID="lname_label" runat="server" Text="Last Name: "></asp:Label>
            <asp:TextBox ID="lname" runat="server" MaxLength="50" CssClass="textbox"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="lname" ErrorMessage="Missing or Invalid Last Name" ForeColor="Red">&nbsp;</asp:RequiredFieldValidator>
        </li>
        <li>
            <asp:Label ID="fname_label" runat="server" Text="First Name: "></asp:Label>
            <asp:TextBox ID="fname" runat="server" MaxLength="50" CssClass="textbox"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="fname" ErrorMessage="Missing or Invalid First Name" ForeColor="Red" >&nbsp;</asp:RequiredFieldValidator>
        </li>
        <li>
            <asp:Label ID="mi_label" runat="server" Text="MI: "></asp:Label>
            <asp:TextBox ID="mi" runat="server" MaxLength="1" Columns="1" CssClass="textbox"></asp:TextBox>
        </li>
        <li>
            <asp:Label ID="dob_label" runat="server" Text="DOB: "></asp:Label>
            <asp:TextBox ID="dob" runat="server" CssClass="textbox"></asp:TextBox>
            <asp:CompareValidator id="dateValidator" runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="dob" ErrorMessage="Missing or Invalid Date of Birth" ForeColor="Red">&nbsp;</asp:CompareValidator>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="dob" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="dob" ErrorMessage="Missing or Invalid Date of Birth" ForeColor="Red" >&nbsp;</asp:RequiredFieldValidator>                    
        </li>
        <li>
            <asp:Label ID="gender_label" runat="server" Text="Gender: "></asp:Label>
            <asp:DropDownList ID="gender" runat="server">
                <asp:ListItem Value="M">Male</asp:ListItem>
                <asp:ListItem Value="F">Female</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <asp:Label ID="height_label" runat="server" Text="Height: "></asp:Label>
            <asp:DropDownList ID="height" runat="server" >
            </asp:DropDownList>
        </li>
        <li>
            <asp:Label ID="eye_color_label" runat="server" Text="Eye Color: "></asp:Label>
            <asp:DropDownList ID="eye_color" runat="server">
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
            </asp:DropDownList>
        </li>
        <li>
            <asp:Label ID="blood_type_label" runat="server" Text="Blood Type: "></asp:Label>
            <asp:DropDownList ID="blood_type" runat="server">
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
            </asp:DropDownList>
        </li>
        <li>
            <asp:Label ID="member_since_label" runat="server" Text="Member Since: "></asp:Label>
            <asp:DropDownList ID="member_since" runat="server" >
            </asp:DropDownList>
        </li>
        <li>
            <asp:Label ID="photo_label" runat="server" Text="Photo: (Ensure face is centered; image will be cropped and resized)"></asp:Label>
            <p><asp:Image ID="Image1" runat="server" /></p>
            <p>Change picture:&nbsp;<asp:FileUpLoad ID="photo_upload" runat="server" /></p>
        </li>
    </ul>
    <asp:Button ID="Cancel" runat="server" Text="Cancel" BackColor="Red" 
        ForeColor="White" onclick="Cancel_Click" />
    &nbsp;&nbsp;&nbsp;
    <asp:Button ID="Update" runat="server" Text="Update" 
        OnClientClick="return confirm('Are you sure your information is correct?');" 
        BackColor="DarkGreen" ForeColor="White" onclick="Update_Click" />
    <br />
    <br />
</asp:Content>

