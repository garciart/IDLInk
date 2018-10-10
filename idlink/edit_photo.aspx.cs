using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

public partial class edit_photo : System.Web.UI.Page
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
        if (File.Exists(Server.MapPath("photos/" + user_filename + "_photo.png")))
        {
            photoOnFile.ImageUrl = Page.ResolveUrl("photos/" + user_filename + "_photo.png?");
        }
        else
        {
            photoOnFile.ImageUrl = Page.ResolveUrl("resources/images/no_picture.png");
        }
    }

    protected void submit_button_click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            // Get data
            memberData = SQLiteDatabase.GetSingleMemberData(Request.QueryString["member_id"]);
            string user_filename = memberData.lname.ToLower() + "_" + memberData.fname.ToLower() + "_" + memberData.member_since;

            // Save photo
            Response.Write(photo_upload.HasFile);
            Response.Write(photo_upload.FileName);
            if (photo_upload.HasFile)
            {
                //Create an image object from the uploaded file
                Bitmap photoImage = new Bitmap(System.Drawing.Image.FromStream(photo_upload.PostedFile.InputStream));
                string photo_path = Server.MapPath("photos/" + user_filename + "_photo.png");

                if (photoImage.Width >= photoImage.Height)
                {
                    photoImage = photoImage.Clone(new Rectangle(((photoImage.Width / 2) - (photoImage.Height / 2)), 0, photoImage.Height, photoImage.Height), PixelFormat.DontCare);
                }
                else
                {
                    photoImage = photoImage.Clone(new Rectangle(0, ((photoImage.Height / 2) - (photoImage.Width / 2)), photoImage.Width, photoImage.Width), PixelFormat.DontCare);
                }
                photoImage = new Bitmap(photoImage, new Size(300, 300));
                photoImage.Save(photo_path, System.Drawing.Imaging.ImageFormat.Png);
                photoImage.Dispose();

            }
            else
            {
                status_label.Text = "Photo required";
                status_label.Visible = true;
                Response.Write(@"<script language='javascript'>alert('Photo Required');</script>");
            }
        }
        HttpContext.Current.ClearError();
        Response.Redirect("~/edit_card.aspx", false);
        return;
    }

    // Special thanks to Mudassar Ahmed Khan at www.aspsnippets.com/Articles/Display-image-after-upload-without-page-refresh-or-postback-using-ASP.Net-AsyncFileUpload-Control.asp //
    protected string UploadFolderPath = "~/photos/";
    protected void FileUploadComplete(object sender, EventArgs e)
    {
        // Create an image object from the uploaded file
        string photo_path = Server.MapPath("photos/a_photo.png");

        if (File.Exists(photo_path))
        {
            File.Delete(photo_path);
        }

        Bitmap photoImage = new Bitmap(System.Drawing.Image.FromStream(photo_upload.PostedFile.InputStream));

        try
        {
            if (photoImage.Width >= photoImage.Height)
            {
                photoImage = photoImage.Clone(new Rectangle(((photoImage.Width / 2) - (photoImage.Height / 2)), 0, photoImage.Height, photoImage.Height), PixelFormat.DontCare);
            }
            else
            {
                photoImage = photoImage.Clone(new Rectangle(0, ((photoImage.Height / 2) - (photoImage.Width / 2)), photoImage.Width, photoImage.Width), PixelFormat.DontCare);
            }
            photoImage = new Bitmap(photoImage, new Size(300, 300));
            photoImage.Save(photo_path, System.Drawing.Imaging.ImageFormat.Png);
            photoImage.Dispose();
        }
        catch
        {
            photoImage.Dispose();
        }
    }

    protected void cancel_button_Click(object sender, EventArgs e)
    {
        HttpContext.Current.ClearError();
        Response.Redirect("~/edit_card.aspx", false);
        return;
    }
}