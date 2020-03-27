using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for User
/// </summary>
public class User
{
    private int id;
    private string username;
    private string password;
    private int imageId;

    private string fName;
    private string sName;
    private string gender;
    private string course;

    private bool isDriver = false;

    public int ID
    {
        get { return id; }

        set { id = value; }
    }
    public string USERNAME
    {
        get { return username; }

        set { username = value; }
    }
    public string PASSWORD
    {
        get { return password; }

        set { password = value; }
    }
    public int IMAGEID
    {
        get { return imageId; }

        set { imageId = value; }
    }

    public string FNAME
    {
        get { return fName; }

        set { fName = value; }
    }
    public string SNAME
    {
        get { return sName; }

        set { sName = value; }
    }
    public string GENDER
    {
        get { return gender; }

        set { gender = value; }
    }
    public string COURSE
    {
        get { return course; }

        set { course = value; }
    }

    public bool ISDRIVER
    {
        get { return isDriver; }

        set { isDriver = value; }
    }


    public User(string Username, string Password, string Firstname, string Surname, string Gender)
    {
        Random rnd = new Random();
        ID = rnd.Next(0, 999999);
        USERNAME = username;
        PASSWORD = password;
        FNAME = Firstname;
        SNAME = Surname;
        GENDER = Gender;

    }

    
}