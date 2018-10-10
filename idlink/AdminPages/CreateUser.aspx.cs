using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.Security;

public partial class CreateUser : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        List<string> roles = new List<string>(System.Web.Security.Roles.GetAllRoles());
        foreach (string role in roles)
        {
            RoleList.Items.Add(role);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    // Create new user and retrieve create status result.
    protected void CreateButton_OnClick(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            try
            {
                MembershipCreateStatus userStatus;
                MembershipUser newUser = Membership.CreateUser(UserNameText.Text, PassText.Text, EmailText.Text, null, null, ActiveUser.Checked, out userStatus);
                if (newUser == null)
                {
                    string error_message;
                    switch (userStatus)
                    {
                        case MembershipCreateStatus.DuplicateUserName:
                            {
                                error_message = "Username already exists. Please enter a different user name.";
                                break;
                            }
                        case MembershipCreateStatus.DuplicateEmail:
                            {
                                error_message = "A username for that e-mail address already exists. Please enter a different e-mail address.";
                                break;
                            }
                        case MembershipCreateStatus.InvalidPassword:
                            {
                                error_message = "The password provided is invalid. Please enter a valid password value.";
                                break;
                            }
                        case MembershipCreateStatus.InvalidEmail:
                            {
                                error_message = "The e-mail address provided is invalid. Please check the value and try again.";
                                break;
                            }
                        case MembershipCreateStatus.InvalidAnswer:
                            {
                            error_message = "The password retrieval answer provided is invalid. Please check the value and try again.";
                                break;
                            }
                        case MembershipCreateStatus.InvalidQuestion:
                            {
                                error_message = "The password retrieval question provided is invalid. Please check the value and try again.";
                                break;
                            }
                        case MembershipCreateStatus.InvalidUserName:
                            {
                                error_message = "The user name provided is invalid. Please check the value and try again.";
                                break;
                            }
                        case MembershipCreateStatus.ProviderError:
                            {
                                error_message = "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
                                break;
                            }
                        case MembershipCreateStatus.UserRejected:
                            {
                                error_message = "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
                                break;
                            }
                        default:
                            {
                                error_message = "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
                                break;
                            }
                    }
                    LiteralControl literalControl = new LiteralControl("<p>" + error_message + "</p>");
                    PopUpText.Controls.Add(literalControl);
                    CreateButton_ModalPopupExtender.Show();
                }
                else
                {
                    System.Web.Security.Roles.AddUserToRole(newUser.UserName, RoleList.SelectedValue);
                    NameText.Text = "";
                    UserNameText.Text = "";
                    PassText.Text = "";
                    PassConfirmText.Text = "";
                    EmailText.Text = "";
                    LiteralControl literalControl = new LiteralControl("<p>User Successfully Added.</p>");
                    PopUpText.Controls.Add(literalControl);
                    CreateButton_ModalPopupExtender.Show();
                }
            }
            catch
            {
                LiteralControl literalControl = new LiteralControl("<p>An exception occurred creating the user.</p>");
                PopUpText.Controls.Add(literalControl);
                CreateButton_ModalPopupExtender.Show();
            }
        }
    }

    protected void CancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
}