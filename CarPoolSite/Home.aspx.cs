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
    protected void Page_Load(object sender, EventArgs e)
    {
        string username = "";
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
    public static string requestRide()
    {
        return "This string is from Code behind";
    }
    
}