using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using ConsumedByCode.SignatureToImage;
using ZXing;
using System.Drawing;

public partial class edit_member : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        status_label.ForeColor = System.Drawing.Color.Red;
        PopulateControls();
    }

    // Populate the controls
    private void PopulateControls()
    {
        string member_id = Request.QueryString["member_id"];
        member_data md = SQLiteDatabase.GetSingleMemberData(member_id);
        lname.Text = md.lname;
        fname.Text = md.fname;
        mi.Text = md.mi;
        dob.Text = md.dob;
        gender.Text = md.gender;

        for (int x = 48; x <= 96; x++)
        {
            int feet = (int)x / 12;
            int inches = x % 12;

            string height_text = String.Format("{0}\' {1}\"", feet, inches);

            ListItem height_item = new ListItem(height_text, x.ToString());

            height.Items.Add(height_item);
        }
        height.SelectedValue = md.height.ToString();

        eye_color.Text = md.eye_color;
        blood_type.Text = md.blood_type;

        int current_year = DateTime.Today.Year;
        for (int x = 0; x < 100; x++)
        {
            member_since.Items.Add((current_year - x).ToString());
        }
        member_since.SelectedValue = md.member_since;

        string member_filename = lname.Text.ToLower() + "_" + fname.Text.ToLower() + "_" + member_since.SelectedValue;
        // Image1.ImageUrl = "photos/" + member_filename + "_photo.png";
        string photo_info = Server.MapPath("~/photos/" + member_filename + "_photo.png");
        if(File.Exists(photo_info))
        {
            Image1.ImageUrl = "~/photos/" + member_filename + "_photo.png";
        }
        else
        {
            Image1.ImageUrl = "~/resources/images/no_picture.png";
        }
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/edit_card.aspx");
    }

    protected void Update_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string member_id = Request.QueryString["member_id"];
            member_data md = SQLiteDatabase.GetSingleMemberData(member_id);
            string old_member_filename = md.lname.ToLower() + "_" + md.fname.ToLower() + "_" + md.member_since;
            
            string member_info = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", lname.Text.ToUpper(), fname.Text.ToUpper(), mi.Text.ToUpper(), "325 CYPRESS ST SALISBURY MD 21801-4060", dob.Text, gender.SelectedValue, height.SelectedValue, eye_color.SelectedValue, blood_type.SelectedValue, member_since.SelectedValue);

            string member_filename = lname.Text.ToLower() + "_" + fname.Text.ToLower() + "_" + member_since.Text;

            // SignatureToImage sigToImg = new SignatureToImage();
            // System.Drawing.Bitmap signatureImage = sigToImg.SigJsonToImage(output.Value);
            string signature_path = "~/photos/" + member_filename + "_sign.png";
            // signatureImage.Save(signature_path, System.Drawing.Imaging.ImageFormat.Png);
            File.Move(Server.MapPath("~/photos/" + old_member_filename + "_sign.png"), Server.MapPath(signature_path));

            File.Delete(Server.MapPath("~/photos/" + old_member_filename + "_code.png"));
            // http://stackoverflow.com/questions/13289742/zxing-net-encode-string-to-qr-code-in-cf //
            BarcodeWriter writer = new BarcodeWriter { Format = BarcodeFormat.PDF_417 };
            Bitmap barcodeBitmap;
            writer.Options.Margin = 0;
            // writer.Options.PureBarcode = true;
            Bitmap result = writer.Write(member_info);
            barcodeBitmap = new Bitmap(result);
            string pdf417_path = Server.MapPath("photos/" + member_filename + "_code.png");
            barcodeBitmap.Save(pdf417_path, System.Drawing.Imaging.ImageFormat.Png);
            
            /*
            // DELETE AFTER BATCH UPLOAD //
            bool success = SQLiteDatabase.AddMember(lname.Text.ToUpper(), fname.Text.ToUpper(), mi.Text.ToUpper(), dob.Text, gender.SelectedValue, height.SelectedValue, eye_color.SelectedValue, blood_type.SelectedValue, member_since.SelectedValue, signature_path, pdf417_path, photo_path);
            if (success == true)
            {
                status_label.ForeColor = System.Drawing.Color.Green;
                status_label.Text = "Member Added!";
                status_label.Visible = true;
                lname.Text = "";
                fname.Text = "";
                mi.Text = "";
                dob.Text = "";
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
            }
            // DELETE AFTER BATCH UPLOAD //
            */

            // ADD AFTER BATCH UPLOAD TO ALLOW PHOTOS TO BE UPLOADED //
            string photo_path = "~/photos/" + member_filename + "_photo.png";
            try
            {
                File.Move(Server.MapPath("~/photos/" + old_member_filename + "_photo.png"), Server.MapPath(photo_path));
            }
            catch
            {
            }
            Response.Write(photo_upload.HasFile);
            Response.Write(photo_upload.FileName);
            Response.Write(photo_upload.PostedFile.ContentType);
            if (photo_upload.HasFile)
            {
                if (photo_upload.PostedFile.ContentType == "image/png")
                {
                    photo_upload.PostedFile.SaveAs(Server.MapPath(photo_path));
                }
                else
                {
                    status_label.Text = "Photo must be in PNG format";
                    status_label.Visible = true;
                }
            }
            // Execute the insert command
            bool success = SQLiteDatabase.UpdateMember(member_id, lname.Text.ToUpper(), fname.Text.ToUpper(), mi.Text.ToUpper(), dob.Text, gender.SelectedValue, height.SelectedValue, eye_color.SelectedValue, blood_type.SelectedValue, member_since.SelectedValue);
            if (success == true)
            {
                status_label.ForeColor = System.Drawing.Color.Green;
                status_label.Text = "Member Updated!";
                status_label.Visible = true;
                lname.Text = "";
                fname.Text = "";
                mi.Text = "";
                dob.Text = "";
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
            }
        }
    }
}