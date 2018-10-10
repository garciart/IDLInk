using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
}