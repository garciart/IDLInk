using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ConsumedByCode.SignatureToImage;
using ZXing;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Security;
using System.IO;
using System.Text;

public partial class new_card : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int x, feet, inches;
        string height_text;
        int current_year = DateTime.Today.Year;

        for (x = 48; x <= 96; x++)
        {
            feet = (int)x / 12;
            inches = x % 12;

            height_text = String.Format("{0}\' {1}\"", feet, inches);

            ListItem height_item = new ListItem(height_text, x.ToString());

            height.Items.Add(height_item);
        }

        for (x = 0; x < 100; x++)
        {
            member_since.Items.Add((current_year - x).ToString());
        }

        if (Roles.IsUserInRole("administrator") || Roles.IsUserInRole("manager") || Roles.IsUserInRole("officer"))
        {
            photo_upload.Enabled = true;
        }
        else
        {
            photo_upload.Enabled = false;
            photo_upload_text.ForeColor = System.Drawing.Color.Gray;
        }
    }

    protected void submit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string fixed_lname = RemoveSpecialCharacters(lname.Text);
            string fixed_fname = RemoveSpecialCharacters(fname.Text);
            
            string user_filename = fixed_lname.ToLower() + "_" + fixed_fname.ToLower() + "_" + member_since.Text;
            
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

                // Prepare barcode info
                string member_info = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", fixed_lname.ToUpper(), fixed_fname.ToUpper(), mi.Text.ToUpper(), "325 CYPRESS ST SALISBURY MD 21801-4060", String.Format("{0:MM/dd/yyyy}", dob.Value), gender.SelectedValue, height.SelectedValue, eye_color.SelectedValue, blood_type.SelectedValue, member_since.SelectedValue);

                // Create and save barcode
                // http://stackoverflow.com/questions/13289742/zxing-net-encode-string-to-qr-code-in-cf //
                BarcodeWriter writer = new BarcodeWriter { Format = BarcodeFormat.PDF_417 };
                Bitmap barcodeBitmap;
                writer.Options.Margin = 0;
                barcodeBitmap = writer.Write(member_info);
                string pdf417_path = Server.MapPath("photos/" + user_filename + "_code.png");
                barcodeBitmap.Save(pdf417_path, System.Drawing.Imaging.ImageFormat.Png);
                barcodeBitmap.Dispose();

                // Save signature image
                SignatureToImage sigToImg = new SignatureToImage();
                System.Drawing.Bitmap signatureImage = sigToImg.SigJsonToImage(output.Value);
                string signature_path = Server.MapPath("photos/" + user_filename + "_sign.png");
                signatureImage.Save(signature_path, System.Drawing.Imaging.ImageFormat.Png);
                signatureImage.Dispose();

                // Execute the insert command
                bool success = SQLiteDatabase.AddMember(fixed_lname.ToUpper(), fixed_fname.ToUpper(), mi.Text.ToUpper(), dob.Value, gender.SelectedValue, height.SelectedValue, eye_color.SelectedValue, blood_type.SelectedValue, member_since.SelectedValue, signature_path, pdf417_path, photo_path);
                if (success == true)
                {
                    status_label.ForeColor = System.Drawing.Color.Green;
                    status_label.Text = "Member Added!";
                    status_label.Visible = true;
                    lname.Text = "";
                    fname.Text = "";
                    mi.Text = "";
                    dob.Value = "";
                    gender.SelectedValue = "M";
                    height.SelectedValue = "48";
                    eye_color.SelectedValue = "BRO";
                    blood_type.SelectedValue = "APOS";
                    member_since.SelectedValue = DateTime.Today.Year.ToString();
                }
                else
                {
                    status_label.ForeColor = System.Drawing.Color.Red;
                    status_label.Text = "Invalid Entry. Please review your information.";
                    status_label.Visible = true;
                    Response.Write(@"<script language='javascript'>alert('Invalid Entry. Please review your information.');</script>");
                }
            }
            else
            {
                status_label.Text = "Photo required";
                status_label.Visible = true;
                Response.Write(@"<script language='javascript'>alert('Photo Required');</script>");
            }
        }
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

    // Special thanks to Guffa http://stackoverflow.com/questions/1120198/most-efficient-way-to-remove-special-characters-from-string
    public static string RemoveSpecialCharacters(string str)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in str)
        {
            if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }
}