using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class AdminPages_ManageUsers : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        List<string> roles = new List<string>(Roles.GetAllRoles());
        foreach (string role in roles)
        {
            RoleList.Items.Add(role);
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void UserGrid_IndexChanged(object sender, EventArgs e)
    {
        UserNameText.Text = UserGrid.SelectedRow.Cells[1].Text;
        UserEmailTxt.Text = UserGrid.SelectedRow.Cells[2].Text;
        MembershipUser user = Membership.GetUser(UserNameText.Text);
        if (user != null)
        {
            ActiveBox.Checked = user.IsApproved;
            List<string> roles = new List<string>(Roles.GetAllRoles());
            List<string> rolesOfUser = new List<string>(Roles.GetRolesForUser(user.UserName));
            foreach (string role in roles)
            {
                if (rolesOfUser.Contains(role))
                {
                    RoleList.SelectedValue = role;
                }    
            }
            UserGrid_ModalPopupExtender.Show();
        }
        List<MembershipUser> list = new List<MembershipUser>();
        list.Add(Membership.GetUser(user.UserName));
        UserDetails.DataSource = list;
        UserDetails.DataBind();
    }

    protected void ManageUserSave(object sender, EventArgs e)
    {
        MembershipUser user = Membership.GetUser(UserGrid.SelectedRow.Cells[1].Text);
        if (user != null)
        {
            user.Email = UserEmailTxt.Text;
            user.IsApproved = ActiveBox.Checked;
            Membership.UpdateUser(user);
            if (Roles.GetRolesForUser(user.UserName).Length != 0)
            {
                Roles.RemoveUserFromRoles(user.UserName, Roles.GetRolesForUser(user.UserName));
            }
            Roles.AddUserToRole(user.UserName, RoleList.SelectedValue);
            UserGrid.DataBind();
        } 
    }

    protected void DeleteUser(object sender, EventArgs e)
    {  
        if (!String.IsNullOrEmpty(UserNameText.Text))
        {
            Membership.DeleteUser(UserNameText.Text);
            UserGrid.DataBind();
        }
    }

    protected void AskMessage(object sender, EventArgs e)
    {
        ModalPopupExtender2.Show();
    }
}