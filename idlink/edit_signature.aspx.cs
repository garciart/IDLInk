using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ConsumedByCode.SignatureToImage;

public partial class edit_signature : System.Web.UI.Page
{
    member_data memberData = new member_data();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        // Clear cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoServerCaching();
        Response.Cache.SetNoStore();
        // Get data
        memberData = SQLiteDatabase.GetSingleMemberData(Request.QueryString["member_id"]);
        string user_filename = memberData.lname.ToLower() + "_" + memberData.fname.ToLower() + "_" + memberData.member_since;
        if (File.Exists(Server.MapPath("photos/" + user_filename + "_sign.png")))
        {
            signatureOnFile.ImageUrl = Page.ResolveUrl("photos/" + user_filename + "_sign.png?");
        }
        else
        {
            signatureOnFile.ImageUrl = Page.ResolveUrl("resources/images/no_picture.png");
        }
    }

    protected void submit_button_click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string user_filename = memberData.lname.ToLower() + "_" + memberData.fname.ToLower() + "_" + memberData.member_since;

            // Save signature image
            SignatureToImage sigToImg = new SignatureToImage();
            System.Drawing.Bitmap signatureImage = sigToImg.SigJsonToImage(output.Value);
            string signature_path = Server.MapPath("photos/" + user_filename + "_sign.png");
            signatureImage.Save(signature_path, System.Drawing.Imaging.ImageFormat.Png);
            signatureImage.Dispose();

            HttpContext.Current.ClearError();
            Response.Redirect("~/edit_card.aspx", false);
            return;
        }
    }
}