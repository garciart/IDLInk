using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.Security;


public partial class MasterPage : System.Web.UI.MasterPage
{
    /// <summary
    /// Use DataBind() to allow <%#ResolveUrl("...")%> to work
    /// </summary>
    protected void Page_Init(object sender, EventArgs e)
    {
        Page.Header.DataBind();
        Page.DataBind();
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Roles.IsUserInRole("administrator"))
        {
            edit_card_link.Visible = true;
            print_card_link.Visible = true;
            manage_users_link.Visible = true;
        }
        else if(Roles.IsUserInRole("manager"))
        {
            edit_card_link.Visible = true;
            print_card_link.Visible = true;
            manage_users_link.Visible = false;
        }
        else if (Roles.IsUserInRole("officer"))
        {
            edit_card_link.Visible = true;
            print_card_link.Visible = false;
            manage_users_link.Visible = false;
        }
        else
        {
            edit_card_link.Visible = false;
            print_card_link.Visible = false;
            manage_users_link.Visible = false;
        }
    }

    /// <summary
    /// Used to move viewstate info to bottom of page
    /// Thanks, Scott Hanselman at http://www.hanselman.com/blog/movingviewstatetothebottomofthepage.aspx
    /// </summary>
    protected override void Render(System.Web.UI.HtmlTextWriter writer)
    {
        System.IO.StringWriter stringWriter = new System.IO.StringWriter();
        HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);
        base.Render(htmlWriter);
        string html = stringWriter.ToString();
        int StartPoint = html.IndexOf("<input type=\"hidden\" name=\"__VIEWSTATE\"");
        if (StartPoint >= 0)
        {
            int EndPoint = html.IndexOf("/>", StartPoint) + 2;
            string viewstateInput = html.Substring(StartPoint, EndPoint - StartPoint);
            html = html.Remove(StartPoint, EndPoint - StartPoint);
            int FormEndStart = html.IndexOf("</form>");
            if (FormEndStart >= 0)
            {
                html = html.Insert(FormEndStart, viewstateInput);
            }
        }
        writer.Write(html);
    }
}
