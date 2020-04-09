using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Login(object sender, EventArgs e)
    {
        string username = Request.Form["uname"];
        string password = Request.Form["uname"];
        if (Logon(username, password))
        {
            Response.Cookies["user"].Value = username;
            Response.Cookies["user"].Expires = DateTime.Now.AddMinutes(10);
            Response.Redirect("Home.aspx");
        }
        else
        {
            return;
        }
    }

    public bool Logon(string username, string password)
    {
        //encrypts the password to compare with the database
        password = Encryption.Encrypt(password);

        //gets the localpath of the database so it can work on other hosts
        string localPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory)) + @"App_Data\Database.mdf";
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + localPath + "; Integrated Security = True";
        conn.Open();

        string sql = "SELECT * FROM dbo.CARPOOLUSER WHERE [Username] = @uname AND [Password] = @pword";
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@uname", username);
            cmd.Parameters.AddWithValue("@pword", password);
            using (var reader = cmd.ExecuteReader())
            {
                var count = 0;
                while (reader.Read())
                {
                    count = count + 1;
                }
                if (count == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}