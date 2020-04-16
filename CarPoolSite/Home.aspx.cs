using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    public string img;
    public int notifNum = 0;
    public string username = "";
    protected void Page_Load(object sender, EventArgs e) //this is the openNav 
    {
        //string username = "";
        if (Request.Cookies["user"] != null)
        {
            username = Request.Cookies["user"].Value;
        }
        img = Actions.getProfileImage(username);
        if (Actions.isDriver(username) == "True")
        {
            riderView.Visible = false;
        }
        List<string> notifs = Actions.Notifications(username);
        if(notifs.Count == 0)
        {
            notifDiv.Visible = false;
        }
        else
        {
            notifNum = notifs.Count;
        }
    }

    [WebMethod]
    public static string requestRide(string origin)
    {
        string sLocation = origin;
        string localPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory)) + @"App_Data\Database.mdf";
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + localPath + "; Integrated Security = True";
        conn.Open();
        string sql = "UPDATE [dbo].[RIDERS] SET  ";
        if (sLocation != "")
        {
            sql += " [Location] = @sLocation,";
        }
        sql = sql.Remove(sql.Length - 1);
        sql += " WHERE [Id] = @username";
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@sLocation", sLocation);
            cmd.ExecuteNonQuery();
        }
            return "This string is from Code behind";
    }
    
}