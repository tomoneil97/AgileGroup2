using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Ride
/// </summary>
public class Ride
{
    //User ID = new User(UserName);
    //public int riderID 
    private int id;
    private string username;
    private string sLocation;
    private string destination;
    private string date;
    private string time;

    public int rID
    {
        get { return id; }

        set { id = value; }
    }
    public string rUsername
    {
        get { return username; }

        set { username = value; }
    }
    public string rStart
    {
        get { return sLocation; }

        set { sLocation = value; }
    }
    public string rDestination
    {
        get { return destination; }

        set { destination = value; }
    }
    public string rDate
    {
        get { return date; }

        set { date = value; }
    }
    public string rTime
    {
        get { return time; }

        set { time = value; }
    }

    public Ride(string username, string sLocation, string destination, string date, string time)
    {
        
    }
}