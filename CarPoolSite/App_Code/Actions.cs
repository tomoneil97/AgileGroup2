using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data.SqlClient;
/// <summary>
/// Class for actions to perform on webpages
/// </summary>
public class Actions
{
    public static bool Logon(string username, string password)
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

    public static string getProfileImage(string username)
    {

        
        string localPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory)) + @"App_Data\Database.mdf";
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + localPath + "; Integrated Security = True";
        conn.Open();

        string sql = "SELECT [ImageName] FROM dbo.CARPOOLUSER WHERE [Username] = @uname";
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@uname", username);
            string image= cmd.ExecuteScalar()?.ToString();
            if(image == "")
            {
                return "images/user.png";
            }
            else
            {
                return image;
            }
        }
    }

    public static string isDriver(string username)
    {


        string localPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory)) + @"App_Data\Database.mdf";
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + localPath + "; Integrated Security = True";
        conn.Open();

        string sql = "SELECT [isDriver] FROM dbo.CARPOOLUSER WHERE [Username] = @uname";
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@uname", username);
            string result = cmd.ExecuteScalar()?.ToString();
            return result;
        }
    }

    public static List<string> Notifications(string username)
    {
        List<string> notifs = new List<string>();

        string localPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory)) + @"App_Data\Database.mdf";
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + localPath + "; Integrated Security = True";
        conn.Open();

        string sql = "SELECT [Message] FROM dbo.NOTIFICATION WHERE [Recipient] = @uname AND [Read] = @f";
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@uname", username);
            cmd.Parameters.AddWithValue("@f", false);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    notifs.Add(reader.GetString(0));
                }
            }
        }
        return notifs;
    }
}