using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    public string img;
    protected void Page_Load(object sender, EventArgs e)
    {
        getProfileImage();
    }

    public void getProfileImage()
    {
        
            string username = "";
        if (Request.Cookies["user"] != null)
        {
            username = Request.Cookies["user"].Value;
        }
        string localPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory)) + @"App_Data\Database.mdf";
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + localPath + "; Integrated Security = True";
        conn.Open();

        string sql = "SELECT [ImageName] FROM dbo.CARPOOLUSER WHERE [Username] = @uname";
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@uname", username);
            img = cmd.ExecuteScalar()?.ToString();
        }
    }
}