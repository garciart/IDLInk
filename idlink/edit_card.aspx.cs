using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Web.Security;
using System.Drawing;
using System.Drawing.Imaging;

public partial class edit_card : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        statusLabel.ForeColor = System.Drawing.Color.Red;
        BindGrid();
    }

    // Populate the GridView with data
    private void BindGrid()
    {
        grid.DataSource = SQLiteDatabase.GetAllMemberData();
        grid.Sort("lname", SortDirection.Ascending);
        grid.DataBind();
    }

    // Enter row into edit mode
    protected void grid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        // Set status message
        statusLabel.ForeColor = System.Drawing.Color.Orange;
        statusLabel.Text = String.Format("Editing {0} {1}...", ((Label)grid.Rows[e.NewEditIndex].FindControl("fname_label")).Text, ((Label)grid.Rows[e.NewEditIndex].FindControl("lname_label")).Text);
        // Set the row for which to enable edit mode
        grid.EditIndex = e.NewEditIndex;
        // Reload the gridview
        BindGrid();
    }

    // Cancel edit mode
    protected void grid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        // Set status message
        statusLabel.ForeColor = System.Drawing.Color.Black;
        statusLabel.Text = "Editing canceled";
        // Cancel edit mode
        grid.EditIndex = -1;
        // Reload the gridview
        BindGrid();
    }

    // Delete a record
    protected void grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // Get the ID of the record to be deleted
        string member_id = grid.DataKeys[e.RowIndex].Value.ToString();
        // Get the filenames of images to be deleted
        string lname = ((Label)grid.Rows[e.RowIndex].FindControl("lname_label")).Text;
        string fname = ((Label)grid.Rows[e.RowIndex].FindControl("fname_label")).Text;
        string member_since = ((Label)grid.Rows[e.RowIndex].FindControl("member_since_label")).Text;
        string signature_filename = lname.ToLower() + "_" + fname.ToLower() + "_" + member_since;
        string signatureImage_path = Server.MapPath("photos/" + signature_filename + "_sign.png");
        string barcodeBitmap_path = Server.MapPath("photos/" + signature_filename + "_code.png");
        string photoBitmap_path = Server.MapPath("photos/" + signature_filename + "_code.png");
        // Execute the delete command
        bool success = SQLiteDatabase.DeleteMember(member_id);
        if (success == true)
        {
            statusLabel.ForeColor = System.Drawing.Color.Green;
            statusLabel.Text = "Delete successful";
            // Delete associated image files
            if (File.Exists(signatureImage_path))
            {
                File.Delete(signatureImage_path);
            }
            if (File.Exists(barcodeBitmap_path))
            {
                File.Delete(barcodeBitmap_path);
            }
            if (File.Exists(photoBitmap_path))
            {
                File.Delete(photoBitmap_path);
            }
        }
        else
        {
            statusLabel.ForeColor = System.Drawing.Color.Red;
            statusLabel.Text = "Delete unsuccessful";
        }
        // Cancel edit mode
        grid.EditIndex = -1;
        // Reload the gridview
        BindGrid();
    }

    protected void grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            // Retrieve updated data
            int i = grid.Rows[e.RowIndex].DataItemIndex;
            string member_id = grid.DataKeys[e.RowIndex].Value.ToString();
            string lname = ((TextBox)grid.Rows[e.RowIndex].FindControl("lname")).Text.ToUpper();
            string fname = ((TextBox)grid.Rows[e.RowIndex].FindControl("fname")).Text.ToUpper();
            string mi = ((TextBox)grid.Rows[e.RowIndex].FindControl("mi")).Text.ToUpper();
            string dob = ((TextBox)grid.Rows[e.RowIndex].FindControl("dob")).Text;
            string gender = ((DropDownList)grid.Rows[e.RowIndex].FindControl("gender")).SelectedValue;
            string height = ((DropDownList)grid.Rows[e.RowIndex].FindControl("height")).SelectedValue;
            string eye_color = ((DropDownList)grid.Rows[e.RowIndex].FindControl("eye_color")).SelectedValue;
            string blood_type = ((DropDownList)grid.Rows[e.RowIndex].FindControl("blood_type")).SelectedValue;
            string member_since = ((DropDownList)grid.Rows[e.RowIndex].FindControl("member_since")).SelectedValue;
            // Execute the update command
            bool success = SQLiteDatabase.UpdateMember(member_id, lname, fname, mi, dob, gender, height, eye_color, blood_type, member_since);
            if (success == true)
            {
                statusLabel.ForeColor = System.Drawing.Color.Green;
                statusLabel.Text = "Update successful";
            }
            else
            {
                statusLabel.ForeColor = System.Drawing.Color.Red;
                statusLabel.Text = "Update unsuccessful";
            }
            // Cancel edit mode
            grid.EditIndex = -1;
            // Reload the gridview
            BindGrid();
        }
    }

    protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) > 0)
        {
            DropDownList height = (DropDownList)e.Row.FindControl("height");
            Literal old_height = (Literal)e.Row.FindControl("old_height");
            for (int x = 48; x <= 96; x++)
            {
                int feet = (int)x / 12;
                int inches = x % 12;

                string height_text = String.Format("{0}\' {1}\"", feet, inches);

                ListItem height_item = new ListItem(height_text, x.ToString());

                height.Items.Add(height_item);
            }
            height.SelectedValue = old_height.Text;

            DropDownList member_since = (DropDownList)e.Row.FindControl("member_since");
            Literal old_member_since = (Literal)e.Row.FindControl("old_member_since");
            int current_year = DateTime.Today.Year;
            for (int x = 0; x < 100; x++)
            {
                member_since.Items.Add((current_year - x).ToString());
            }
            member_since.SelectedValue = old_member_since.Text;
        }
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

    protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid.PageIndex = e.NewPageIndex;
        grid.DataBind();
    }

    protected void gridView_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dataTable = grid.DataSource as DataTable;

        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

            grid.DataSource = dataView;
            grid.DataBind();
        }
    }

    protected void grid_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Response.Write(@"<script language='javascript'>alert('This link is currently disabled');</script>");
        // Response.Redirect(Page.ResolveUrl("~/edit_member.aspx?member_id=" + grid.SelectedDataKey.Value.ToString()));
    }
}