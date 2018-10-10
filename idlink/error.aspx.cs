using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get the error
        String errorText = Request.QueryString["error"];
        if (!string.IsNullOrEmpty(errorText))
        {
            errorMessageLabel.Text = errorText;
        }
    }
}