using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Data;

public partial class SignUp :  System.Web.UI.Page
{
    public User newUser;
    protected void Page_Load(object sender, EventArgs e)
    {
        string userName = Request.Form["uname"];
        string passWord = Request.Form["psw"];
        string foreName = Request.Form["forename"];
        string surName = Request.Form["surname"];
        string gender = Request.Form["gender"];

        
        newUser = new User(userName, passWord, foreName, surName, gender);
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\harve\Documents\GitHub\AgileGroup2\CarPoolSite\App_Data\Database.mdf; Integrated Security = True";
        conn.Open();
        string sql = "INSERT INTO dbo.CARPOOLUSER ([Username],[Password],[FirstName],[Surname],[Gender],[Image],[CourseName],[isDriver]) values (@uname,@pword,@fname,@sname,@gnder,@image,@crse,@drvr)";
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@uname", userName);
            cmd.Parameters.AddWithValue("@pword", passWord);
            cmd.Parameters.AddWithValue("@fname", foreName);
            cmd.Parameters.AddWithValue("@sname", surName);
            cmd.Parameters.AddWithValue("@gnder", gender);
            cmd.Parameters.Add("@image", SqlDbType.VarBinary).Value = DBNull.Value;
            cmd.Parameters.AddWithValue("@crse", DBNull.Value);
            cmd.Parameters.AddWithValue("@drvr", 0);
            cmd.ExecuteNonQuery();
        }
    }


    protected void Upload(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string fileName = newUser.USERNAME.ToString() + Path.GetExtension(FileUpload1.PostedFile.FileName);
            string path = Server.MapPath("~/images/") + fileName;
            FileUpload1.PostedFile.SaveAs(path);
            
            Image1.ImageUrl = path;
            //Response.Redirect(Request.Url.AbsoluteUri);
        }
    }

    

}