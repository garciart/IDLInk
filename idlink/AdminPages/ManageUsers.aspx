<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPage.master" AutoEventWireup="true" CodeFile="ManageUsers.aspx.cs" Inherits="AdminPages_ManageUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <p><b>Manage Users</b></p>
        <asp:ObjectDataSource ID="UserDataSource" runat="server" SelectMethod="CustomGetAllUsers" TypeName="WSATTest.GetAllUsers"/>
        &nbsp;&nbsp;&nbsp;
        <asp:GridView ID="UserGrid" DataSourceID="UserDataSource" DataKeyNames="UserName" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" AllowSorting="True" Width="915px" OnSelectedIndexChanged="UserGrid_IndexChanged"> 
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" ButtonType="Button"  />
        </Columns>
        <EditRowStyle BackColor="#7C6F57" />
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#E3EAEB" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F8FAFA" />
        <SortedAscendingHeaderStyle BackColor="#246B61" />
        <SortedDescendingCellStyle BackColor="#D4DFE1" />
        <SortedDescendingHeaderStyle BackColor="#15524A" />
    </asp:GridView>
    <ajaxToolKit:ModalPopupExtender ID="UserGrid_ModalPopupExtender" runat="server" DynamicServicePath="" TargetControlID="hideButton" Enabled="True" PopupControlID="divPopUp" PopupDragHandleControlID="panelDragHandle">
    </ajaxToolKit:ModalPopupExtender>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" DynamicServicePath="" TargetControlID="hideButton" Enabled="True" PopupControlID="divConfirmDelete" PopupDragHandleControlID="panelDragHandle">
    </ajaxToolKit:ModalPopupExtender>
    <div id="hiddenButton" style="display:none;">
        <asp:Button id="hideButton" runat="server" />
    </div>
    <div id="divConfirmDelete" style="display:none; background-color:White; padding:6px; border: 2px solid black;">
        <p>Are you sure you want to delete this user?</p>
        <div style="margin:auto auto auto auto; display:inline">
            <asp:Button runat="server" Text="Confirm" ID="ConfirmDelete" OnClick="DeleteUser" />
            <asp:Button runat="server" id="CancelDelete" Text="Cancel" />
        </div>
    </div>
    <div id="divPopUp" style="display:none; background-color:White; border:2px solid black">
        <asp:Panel runat="Server" ID="panelDragHandle" Height="100%" style="padding:6px">
            Manage Users
            <p>
                <asp:DetailsView ID="UserDetails" runat="server"></asp:DetailsView>
            </p>
            <p>
                <asp:Label ID="Label3" runat="server" Text="User Name:"></asp:Label>
                <asp:TextBox runat="server" Enabled="false" ID="UserNameText" />
            </p>
            <p>
                <asp:Label ID="Label4" runat="server" Text="Email Address:"></asp:Label>
                <asp:TextBox runat="server" ID="UserEmailTxt" />
            </p>
            <p>
                <asp:CheckBox ID="ActiveBox" runat="server" Text="Active User" />
            </p>
            <p>
                <asp:Label ID="Label1" runat="server" Text="Roles:"></asp:Label>
                <asp:DropDownList ID="RoleList" runat="server"></asp:DropDownList>
            </p>  
            <div id="btnPanel" style="margin:auto auto auto auto">
                <asp:Button runat="server" ID="ManageSave" Text="Save" OnClick="ManageUserSave" />
                <asp:Button runat="server" ID="ManageDelete" Text="Delete" OnClick="AskMessage" />
                <asp:Button ID="Button1" runat="server" Text="Cancel" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>

