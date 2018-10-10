using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Web;

/// <summary>
/// Wraps member data details
/// </summary>
public struct member_data
{
    public int member_id;
    public string lname;
    public string fname;
    public string mi;
    // Converted to DateTime.ToString() during input (New, Edit, etc.) for accuracy
    public string dob;
    public string gender;
    public int height;
    public string eye_color;
    public string blood_type;
    public string member_since;
    public string signature_path;
    public string pdf417_path;
    public string photo_path;
}

/// <summary>
/// Summary description for SQLiteDatabase
/// </summary>
public class SQLiteDatabase
{
    String dbConnection;

    /// <summary>
    /// Default Constructor for SQLiteDatabase Class.
    /// </summary>
    public SQLiteDatabase()
    {
        String dataSource = HttpContext.Current.Server.MapPath(@"~\App_Data\idlink_member_data.sq3");
        dbConnection = "Data Source=" + dataSource + "; Version=3;Password=\"XXXXXXXX\";";
    }

    /// <summary>
    /// Single Param Constructor for specifying the DB file.
    /// </summary>
    /// <param name="inputFile">The File containing the DB</param>
    public SQLiteDatabase(String inputFile)
    {
        dbConnection = String.Format("Data Source={0}", inputFile);
    }

    /// <summary>
    /// Single Param Constructor for specifying advanced connection options.
    /// </summary>
    /// <param name="connectionOpts">A dictionary containing all desired options and their values</param>
    public SQLiteDatabase(Dictionary<String, String> connectionOpts)
    {
        String str = "";
        foreach (KeyValuePair<String, String> row in connectionOpts)
        {
            str += String.Format("{0}={1}; ", row.Key, row.Value);
        }
        str = str.Trim().Substring(0, str.Length - 1);
        dbConnection = str;
    }

    /// <summary>
    /// Allows the programmer to run a query against the Database.
    /// </summary>
    /// <param name="sql">The SQL to run</param>
    /// <returns>A DataTable containing the result set.</returns>
    public DataTable GetDataTable(string sql)
    {
        DataTable dt = new DataTable();
        try
        {
            SQLiteConnection cnn = new SQLiteConnection(dbConnection);
            cnn.Open();
            SQLiteCommand mycommand = new SQLiteCommand(cnn);
            mycommand.CommandText = sql;
            SQLiteDataReader reader = mycommand.ExecuteReader();
            dt.Load(reader);
            reader.Close();
            cnn.Close();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        return dt;
    }

    /// <summary>
    /// Allows the programmer to interact with the database for purposes other than a query.
    /// </summary>
    /// <param name="sql">The SQL to be run.</param>
    /// <returns>An Integer containing the number of rows updated.</returns>
    public int ExecuteNonQuery(string sql)
    {
        SQLiteConnection cnn = new SQLiteConnection(dbConnection);
        cnn.Open();
        SQLiteCommand mycommand = new SQLiteCommand(cnn);
        mycommand.CommandText = sql;
        int rowsUpdated = mycommand.ExecuteNonQuery();
        cnn.Close();
        return rowsUpdated;
    }

    /// <summary>
    /// Allows the programmer to retrieve single items from the DB.
    /// </summary>
    /// <param name="sql">The query to run.</param>
    /// <returns>A string.</returns>
    public string ExecuteScalar(string sql)
    {
        SQLiteConnection cnn = new SQLiteConnection(dbConnection);
        cnn.Open();
        SQLiteCommand mycommand = new SQLiteCommand(cnn);
        mycommand.CommandText = sql;
        object value = mycommand.ExecuteScalar();
        cnn.Close();
        if (value != null)
        {
            return value.ToString();
        }
        return "";
    }

    /// <summary>
    /// Allows the programmer to easily update rows in the DB.
    /// </summary>
    /// <param name="tableName">The table to update.</param>
    /// <param name="data">A dictionary containing Column names and their new values.</param>
    /// <param name="where">The where clause for the update statement.</param>
    /// <returns>A boolean true or false to signify success or failure.</returns>
    public bool Update(String tableName, Dictionary<String, String> data, String where)
    {
        String vals = "";
        Boolean returnCode = true;
        if (data.Count >= 1)
        {
            foreach (KeyValuePair<String, String> val in data)
            {
                vals += String.Format(" {0} = '{1}',", val.Key.ToString(), val.Value.ToString());
            }
            vals = vals.Substring(0, vals.Length - 1);
        }
        try
        {
            this.ExecuteNonQuery(String.Format("UPDATE {0} SET {1} WHERE {2};", tableName, vals, where));
        }
        catch
        {
            returnCode = false;
        }
        return returnCode;
    }

    /// <summary>
    /// Allows the programmer to easily delete rows from the DB.
    /// </summary>
    /// <param name="tableName">The table from which to delete.</param>
    /// <param name="where">The where clause for the delete.</param>
    /// <returns>A boolean true or false to signify success or failure.</returns>
    public bool Delete(String tableName, String where)
    {
        Boolean returnCode = true;
        try
        {
            this.ExecuteNonQuery(String.Format("DELETE FROM {0} WHERE {1};", tableName, where));
        }
        catch (Exception fail)
        {
            System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"" + fail.Message + "\")</SCRIPT>");
            returnCode = false;
        }
        return returnCode;
    }

    /// <summary>
    /// Allows the programmer to easily insert into the DB
    /// </summary>
    /// <param name="tableName">The table into which we insert the data.</param>
    /// <param name="data">A dictionary containing the column names and data for the insert.</param>
    /// <returns>A boolean true or false to signify success or failure.</returns>
    public bool Insert(String tableName, Dictionary<String, String> data)
    {
        String columns = "";
        String values = "";
        Boolean returnCode = true;
        foreach (KeyValuePair<String, String> val in data)
        {
            columns += String.Format(" {0},", val.Key.ToString());
            values += String.Format(" '{0}',", val.Value);
        }
        columns = columns.Substring(0, columns.Length - 1);
        values = values.Substring(0, values.Length - 1);
        try
        {
            this.ExecuteNonQuery(String.Format("INSERT INTO {0}({1}) VALUES({2});", tableName, columns, values));
        }
        catch (Exception fail)
        {
            System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"" + fail.Message + "\")</SCRIPT>");
            returnCode = false;
        }
        return returnCode;
    }

    /// <summary>
    /// Allows the programmer to easily delete all data from the DB.
    /// </summary>
    /// <returns>A boolean true or false to signify success or failure.</returns>
    public bool ClearDB()
    {
        DataTable tables;
        try
        {
            tables = this.GetDataTable("SELECT NAME FROM SQLITE_MASTER WHERE TYPE='table' ORDER BY NAME;");
            foreach (DataRow table in tables.Rows)
            {
                this.ClearTable(table["NAME"].ToString());
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Allows the user to easily clear all data from a specific table.
    /// </summary>
    /// <param name="table">The name of the table to clear.</param>
    /// <returns>A boolean true or false to signify success or failure.</returns>
    public bool ClearTable(String table)
    {
        try
        {

            this.ExecuteNonQuery(String.Format("DELETE FROM {0};", table));
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Get all member data
    /// </summary>
    public static DataTable GetAllMemberData()
    {
        SQLiteDatabase db = new SQLiteDatabase();
        DataTable table;
        String query = "SELECT * FROM member_data";
        table = db.GetDataTable(query);
        return table;
    }

    /// <summary>
    /// Delete member
    /// </summary>
    public static bool DeleteMember(string member_id)
    {
        bool success;
        SQLiteDatabase db = new SQLiteDatabase();
        try
        {
            success = db.Delete("member_data", String.Format("member_id = {0}", member_id));
        }
        catch
        {
            success = false;
        }
        return success;
    }

    /// <summary>
    /// Update page details
    /// </summary>
    public static bool UpdateMember(string member_id, string lname, string fname, string mi, string dob, string gender, string height, string eye_color, string blood_type, string member_since)
    {
        bool success;
        SQLiteDatabase db = new SQLiteDatabase();
        Dictionary<String, String> data = new Dictionary<String, String>();
        data.Add("member_id", member_id);
        data.Add("lname", lname);
        data.Add("fname", fname);
        data.Add("mi", mi);
        data.Add("dob", dob);
        data.Add("gender", gender);
        data.Add("height", height);
        data.Add("eye_color", eye_color);
        data.Add("blood_type", blood_type);
        data.Add("member_since", member_since);
        try
        {
            success = db.Update("member_data", data, String.Format("member_id = '{0}'", member_id));
        }
        catch
        {
            success = false;
        }
        return success;
    }

    /// <summary>
    /// Add a new member
    /// </summary>
    public static bool AddMember(string lname, string fname, string mi, string dob, string gender, string height, string eye_color, string blood_type, string member_since, string signature_path, string pdf417_path, string photo_path)
    {
        bool success;
        SQLiteDatabase db = new SQLiteDatabase();
        Dictionary<String, String> data = new Dictionary<String, String>();
        data.Add("lname", lname);
        data.Add("fname", fname);
        data.Add("mi", mi);
        data.Add("dob", dob);
        data.Add("gender", gender);
        data.Add("height", height);
        data.Add("eye_color", eye_color);
        data.Add("blood_type", blood_type);
        data.Add("member_since", member_since);
        data.Add("signature_path", signature_path);
        data.Add("pdf417_path", pdf417_path);
        data.Add("photo_path", photo_path);
        try
        {
            success = db.Insert("member_data", data);
        }
        catch
        {
            success = false;
        }
        return success;
    }

    /// <summary>
    /// Get member data
    /// </summary>
    public static member_data GetSingleMemberData(string member_id)
    {
        member_data member_data = new member_data();
        SQLiteDatabase db = new SQLiteDatabase();
        DataTable table;
        String query = "SELECT * FROM member_data WHERE member_id = \"" + member_id + "\"";
        table = db.GetDataTable(query);
        if (table.Rows.Count > 0)
        {
            member_data.member_id = Int32.Parse(table.Rows[0]["member_id"].ToString());
            member_data.lname = table.Rows[0]["lname"].ToString();
            member_data.fname = table.Rows[0]["fname"].ToString();
            member_data.mi = table.Rows[0]["mi"].ToString();
            member_data.dob = table.Rows[0]["dob"].ToString();
            member_data.gender = table.Rows[0]["gender"].ToString();
            member_data.height = Int32.Parse(table.Rows[0]["height"].ToString());
            member_data.eye_color = table.Rows[0]["eye_color"].ToString();
            member_data.blood_type = table.Rows[0]["blood_type"].ToString();
            member_data.member_since = table.Rows[0]["member_since"].ToString();
            member_data.signature_path = table.Rows[0]["signature_path"].ToString();
            member_data.pdf417_path = table.Rows[0]["pdf417_path"].ToString();
            member_data.photo_path = table.Rows[0]["photo_path"].ToString();
        }
        // Return member details
        return member_data;
    }
}
