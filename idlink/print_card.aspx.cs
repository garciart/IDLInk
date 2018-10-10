using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Printing;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

public partial class print_card : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        statusLabel.ForeColor = System.Drawing.Color.Red;
        BindGrid();
    }

    // Populate the GridView with data
    private void BindGrid()
    {
        printgrid.DataSource = SQLiteDatabase.GetAllMemberData();
        printgrid.Sort("lname", SortDirection.Ascending);
        printgrid.DataBind();
    }

    private string ConvertSortDirectionToSql(SortDirection sortDirection)
    {
        string newSortDirection = String.Empty;

        switch (sortDirection)
        {
            case SortDirection.Ascending:
                newSortDirection = "ASC";
                break;

            case SortDirection.Descending:
                newSortDirection = "DESC";
                break;
        }

        return newSortDirection;
    }

    protected void printgrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        printgrid.PageIndex = e.NewPageIndex;
        printgrid.DataBind();
    }

    protected void gridView_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dataTable = printgrid.DataSource as DataTable;

        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

            printgrid.DataSource = dataView;
            printgrid.DataBind();
        }
    }

    protected void printgrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Get member info
        string lname = ((Label)printgrid.SelectedRow.FindControl("lname_label")).Text;
        string fname = ((Label)printgrid.SelectedRow.FindControl("fname_label")).Text;
        string mi = ((Label)printgrid.SelectedRow.FindControl("mi_label")).Text;
        string dob = ((Label)printgrid.SelectedRow.FindControl("dob_label")).Text;
        string gender = ((Label)printgrid.SelectedRow.FindControl("gender_label")).Text;
        string height = ((Label)printgrid.SelectedRow.FindControl("height_label")).Text;
        string eye_color = ((Label)printgrid.SelectedRow.FindControl("eye_color_label")).Text;
        string blood_type = ((Label)printgrid.SelectedRow.FindControl("blood_type_label")).Text;
        string member_since = ((Label)printgrid.SelectedRow.FindControl("member_since_label")).Text;

        // Load images
        Bitmap id_front_bitmap = new Bitmap(Server.MapPath("~/resources/images/id_card_front.png"));
        Bitmap id_back_bitmap = new Bitmap(Server.MapPath("~/resources/images/id_card_back.png"));
        Graphics id_front_Graphics = Graphics.FromImage(id_front_bitmap);
        Graphics id_back_Graphics = Graphics.FromImage(id_back_bitmap);

        // Draw front of card, signature first
        string member_filename = lname.ToLower() + "_" + fname.ToLower() + "_" + member_since;
        System.Drawing.Image member_signature = System.Drawing.Image.FromFile(Server.MapPath("~/photos/" + member_filename + "_sign.png"));
        member_signature = resizeImage(member_signature, new Size(240, 90));
        Bitmap member_signature_clear = new Bitmap(member_signature);
        // Make signature background color transparent
        member_signature_clear.MakeTransparent(System.Drawing.Color.White);
        id_front_Graphics.DrawImage(member_signature_clear, 90, 200);
        System.Drawing.Image member_picture = null;
        try
        {
            member_picture = System.Drawing.Image.FromFile(Server.MapPath("~/photos/" + member_filename + "_photo.png"));
        }
        catch
        {
            member_picture = System.Drawing.Image.FromFile(Server.MapPath("~/resources/images/no_picture.png"));
        }
        member_picture = resizeImage(member_picture, new Size(160, 160));
        id_front_Graphics.DrawImage(member_picture, 305, 72);
        // Create font and brush, then add info
        Font drawFont = new Font("Arial", 13.5f, FontStyle.Bold);
        SolidBrush drawBrush = new SolidBrush(System.Drawing.Color.Black);
        id_front_Graphics.DrawString(String.Format("{0},\r\n{1} {2}\r\n325 CYPRESS ST\r\nSALISBURY, MD 21801\r\n\r\nDOB: {3}\r\nSEX: {4} / HT: {5}\"\r\nEYES: {6} / BT: {7}", lname, fname, mi, dob, gender, height, eye_color, blood_type), drawFont, drawBrush, 92, 45);
        drawFont = new Font("Arial", 13, FontStyle.Bold | FontStyle.Italic);
        id_front_Graphics.DrawString(String.Format("Serving Since {0}", member_since), drawFont, drawBrush, 310, 250);

        // Draw back of card
        System.Drawing.Image member_barcode = System.Drawing.Image.FromFile(Server.MapPath("~/photos/" + member_filename + "_code.png"));
        /*
        // double aspect_ratio = ((double)id_back_bitmap.Height) * .9 / ((double)id_back_bitmap.Width) * .9;
        double aspect_ratio = ((double)id_back_bitmap.Height) / ((double)id_back_bitmap.Width);
        int new_barcode_height = (int)((member_barcode.Height) * aspect_ratio);
        int new_barcode_width = (int)((member_barcode.Width) * aspect_ratio);
        member_barcode = resizeImage(member_barcode, new Size(new_barcode_width, new_barcode_height));
        */
        // member_barcode.RotateFlip(RotateFlipType.Rotate90FlipNone);
        id_back_Graphics.DrawImage(member_barcode, 25, 170);

        // Create a new PDF document
        PdfDocument document = new PdfDocument();
        document.Info.Title = String.Format("ID Card for {0}, {1} {2}.", lname, fname, mi);

        // Create empty pages
        PdfPage page1 = document.AddPage();
        page1.Height = XUnit.FromInch(3.16);
        page1.Width = XUnit.FromInch(5);
        PdfPage page2 = document.AddPage();
        page2.Height = XUnit.FromInch(3.16);
        page2.Width = XUnit.FromInch(5);

        // Get XGraphics objects for drawing
        XGraphics gfx1 = XGraphics.FromPdfPage(page1);
        XGraphics gfx2 = XGraphics.FromPdfPage(page2);
        gfx2.SmoothingMode = XSmoothingMode.HighQuality;
        XImage temp_image = null;
        temp_image = id_front_bitmap;
        gfx1.DrawImage(temp_image, 0, 0);
        id_back_bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
        temp_image = id_back_bitmap;
        gfx2.DrawImage(temp_image, 0, 0);
        // Save to PDF
        string pdf_filename = Server.MapPath("~/photos/temp.pdf");
        document.Save(pdf_filename);

        Response.Clear();
        string filePath = Server.MapPath("~/photos/temp.pdf");
        Response.ContentType = "application/pdf";
        Response.WriteFile(filePath);
        Response.End();

        // Draw to screen
        /*
        Response.ContentType = "image/png";
        id_front_bitmap.Save(Response.OutputStream, ImageFormat.Png);
        Response.End();

        Response.ContentType = "image/png";
        id_back_bitmap.Save(Response.OutputStream, ImageFormat.Png);
        Response.End();
        */

        /*
        Bitmap multiTiff = new Bitmap(323, 204);
        string tiffPath = Server.MapPath("~/photos/temp.tif");
        // Generating Multi-Page TIFF files by Bob Powell at http://bobpowell.net/generating_multipage_tiffs.aspx
        // Get an encoder for saving with
        Encoder enc = Encoder.SaveFlag;
        // Obtain the TIFF codec info.
        ImageCodecInfo info = null;
        foreach (ImageCodecInfo ice in ImageCodecInfo.GetImageEncoders())
        {
            if (ice.MimeType == "image/tiff")
            {
                info = ice;
            }
        }
        // Create a parameter list. This needs 1 parameter in it.
        EncoderParameters ep = new EncoderParameters(1);
        // Place the MultiFrame encoder value in the parameter list
        ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.MultiFrame);
        // Save the first frame using the encoder and parameters
        multiTiff = id_front_bitmap;
        multiTiff.Save(tiffPath, info, ep);
        // Change the encoder value in the list to FrameDimensionPage
        ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.FrameDimensionPage);
        // Use first of the master frame's overloaded SaveAdd methods to add subsequent images. Repeat this step for as many images as you want to add.
        // multiTiff.SaveAdd(id_front_bitmap, ep);
        multiTiff.SaveAdd(id_back_bitmap, ep);
        // Change the encoder value in the list to Flush
        ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.Flush);
        // Use the second of the master frames overloaded SaveAdd methods to flush, save and close the image. 
        multiTiff.SaveAdd(ep);
        multiTiff.Dispose();
        */

        id_front_bitmap.Dispose();
        id_back_bitmap.Dispose();
        id_front_Graphics.Dispose();
        id_back_Graphics.Dispose();
    }

    public static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
    {
        return (System.Drawing.Image)(new Bitmap(imgToResize, size));
    }
}