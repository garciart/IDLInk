<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="edit_card.aspx.cs" Inherits="edit_card" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Edit Member Information...</h2>
    <h3><asp:Label ID="statusLabel" runat="server"></asp:Label></h3>
    <asp:ValidationSummary ID="ValidationSummary1" HeaderText="Update failed due to the following errors:" DisplayMode="BulletList" EnableClientScript="true" runat="server" ForeColor="Red"/>
    <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="member_id" style="padding:3px" 
        AllowPaging="True" AllowSorting="True" CellPadding="4" ForeColor="#333333" 
        GridLines="None" Width="960px" onrowcancelingedit="grid_RowCancelingEdit" 
        onrowdeleting="grid_RowDeleting" onrowediting="grid_RowEditing" 
        onrowupdating="grid_RowUpdating" onrowdatabound="grid_RowDataBound" 
        onpageindexchanging="grid_PageIndexChanging" onsorting="gridView_Sorting" 
        EditRowStyle-VerticalAlign="Top" EnableViewState="False" 
    onselectedindexchanged="grid_SelectedIndexChanged" PageSize="50">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Last Name" SortExpression="lname">
                <ItemTemplate>
                    <asp:Label ID="lname_label" runat="server" 
                        Text='<%# DataBinder.Eval(Container.DataItem, "lname") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="lname" runat="server" MaxLength="50" 
                        Text='<%# DataBinder.Eval(Container.DataItem, "lname") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="lname" ErrorMessage="Missing or Invalid Last Name" 
                        ForeColor="Red">&nbsp;</asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="First Name" SortExpression="fname">
                <EditItemTemplate>
                    <asp:TextBox ID="fname" runat="server" 
                        Text='<%# DataBinder.Eval(Container.DataItem, "fname") %>' MaxLength="50"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="fname" ErrorMessage="Missing or Invalid First Name" 
                        ForeColor="Red">&nbsp;</asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="fname_label" runat="server" 
                        Text='<%# DataBinder.Eval(Container.DataItem, "fname") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MI" SortExpression="mi">
                <EditItemTemplate>
                    <asp:TextBox ID="mi" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "mi") %>' MaxLength="1" Columns="1"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="mi_label" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "mi") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="DOB" SortExpression="dob">
                <EditItemTemplate>
                    <asp:TextBox ID="dob" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "dob") %>'></asp:TextBox>
                    <asp:CompareValidator id="dateValidator" runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="dob" ErrorMessage="Missing or Invalid Date of Birth" ForeColor="Red">&nbsp;</asp:CompareValidator>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="dob" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="dob" ErrorMessage="Missing or Invalid Date of Birth" ForeColor="Red" >&nbsp;</asp:RequiredFieldValidator>                    
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="dob_label" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "dob") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sex" SortExpression="gender">
                <EditItemTemplate>
                    <asp:DropDownList ID="gender" runat="server" 
                        SelectedValue='<%# DataBinder.Eval(Container.DataItem, "gender") %>'>
                        <asp:ListItem Value="M">Male</asp:ListItem>
                        <asp:ListItem Value="F">Female</asp:ListItem>
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="gender_label" runat="server" 
                        Text='<%# DataBinder.Eval(Container.DataItem, "gender") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="HT" SortExpression="height">
                <EditItemTemplate>
                    <asp:Literal ID="old_height" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "height") %>' Visible="false"></asp:Literal>
                    <asp:DropDownList ID="height" runat="server" >
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="height_label" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "height") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Eyes" SortExpression="eye_color">
                <EditItemTemplate>
                    <asp:DropDownList ID="eye_color" runat="server" 
                        SelectedValue='<%# DataBinder.Eval(Container.DataItem, "eye_color") %>'>
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
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="eye_color_label" runat="server" 
                        Text='<%# DataBinder.Eval(Container.DataItem, "eye_color") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="BT" SortExpression="blood_type">
                <EditItemTemplate>
                    <asp:DropDownList ID="blood_type" runat="server" 
                        SelectedValue='<%# DataBinder.Eval(Container.DataItem, "blood_type") %>'>
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
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="blood_type_label" runat="server" 
                        Text='<%# DataBinder.Eval(Container.DataItem, "blood_type") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Since" SortExpression="member_since">
                <EditItemTemplate>
                    <asp:Literal ID="old_member_since" runat="server" 
                        Text='<%# DataBinder.Eval(Container.DataItem, "member_since") %>' 
                        Visible="false"></asp:Literal>
                    <asp:DropDownList ID="member_since" runat="server">
                    </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="member_since_label" runat="server" 
                        Text='<%# DataBinder.Eval(Container.DataItem, "member_since") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" CausesValidation="False" 
                ButtonType="Button" EditText="Quick Edit" />
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <input type="button" onclick="location.href='edit_photo.aspx?member_id=<%#Eval("member_id")%>'" value="Photo Edit" />
                    <input type="button" onclick="location.href='edit_signature.aspx?member_id=<%#Eval("member_id")%>'" value="Sign Edit" />
                    <asp:Button ID="Button1" runat="server" CausesValidation="False" 
                        CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this member?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EditRowStyle VerticalAlign="Top"></EditRowStyle>
        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
    </asp:GridView>

</asp:Content>

