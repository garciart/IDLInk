<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="print_card.aspx.cs" Inherits="print_card" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Print Member ID Card...</h2>
    <h3><asp:Label ID="statusLabel" runat="server"></asp:Label></h3>
    <asp:GridView ID="printgrid" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="member_id" style="padding:3px" 
        AllowPaging="True" AllowSorting="True" CellPadding="4" ForeColor="#333333" 
        GridLines="None" Width="960px" 
        onselectedindexchanged="printgrid_SelectedIndexChanged" 
    onpageindexchanging="printgrid_PageIndexChanging" onsorting="gridView_Sorting" PageSize="50">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Print" />
            <asp:TemplateField HeaderText="Last Name" SortExpression="lname">
                <ItemTemplate>
                    <asp:Label ID="lname_label" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lname") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="First Name" SortExpression="fname">
                <ItemTemplate>
                    <asp:Label ID="fname_label" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fname") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MI" SortExpression="mi">
                <ItemTemplate>
                    <asp:Label ID="mi_label" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "mi") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="DOB" SortExpression="dob">
                <ItemTemplate>
                    <asp:Label ID="dob_label" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "dob") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sex" SortExpression="gender">
                <ItemTemplate>
                    <asp:Label ID="gender_label" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "gender") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="HT" SortExpression="height">
                <ItemTemplate>
                    <asp:Label ID="height_label" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "height") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Eyes" SortExpression="eye_color">
                <ItemTemplate>
                    <asp:Label ID="eye_color_label" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "eye_color") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="BT" SortExpression="blood_type">
                <ItemTemplate>
                    <asp:Label ID="blood_type_label" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "blood_type") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Since" SortExpression="member_since">
                <ItemTemplate>
                    <asp:Label ID="member_since_label" runat="server" 
                        Text='<%# DataBinder.Eval(Container.DataItem, "member_since") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
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
    <asp:Panel ID="FrontPanel" runat="server">
    </asp:Panel>
    <asp:Panel ID="BackPanel" runat="server">
    </asp:Panel>
</asp:Content>

