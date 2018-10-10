using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPage : System.Web.UI.MasterPage
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
