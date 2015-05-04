using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for cErrorLog
/// </summary>
public class cErrorLog
{
    string sFileName = "";
    string sFilePath = "";
    public cErrorLog()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void LogError(Exception ex)
    {
        try
        {
            cErrorLog obj = new cErrorLog();
            obj.CreateLogFile();
            obj.WriteLog(ex.Message, ex.StackTrace);
        }
        catch (Exception e)
        {
            HttpContext.Current.Response.Write(e.Message);
        }
    }
    public static void LogError(string strMessage)
    {
        try
        {
            cErrorLog obj = new cErrorLog();
            obj.CreateLogFile();
            obj.WriteLog(strMessage, "");
        }
        catch (Exception e)
        {
            HttpContext.Current.Response.Write(e.Message);
        }
    }
    public static void LogError(string strMessage, string strStackTrace)
    {
        try
        {
            cErrorLog obj = new cErrorLog();
            obj.CreateLogFile();
            obj.WriteLog(strMessage, strStackTrace);
        }
        catch (Exception e)
        {
            HttpContext.Current.Response.Write(e.Message);
        }
    }

    public void CreateLogFile()
    {
        sFileName = "Try" + DateTime.Now.ToShortDateString().Replace("/", "") + ".html";
        sFilePath = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);
        sFilePath = sFilePath + "/" + sFileName;
        if (!File.Exists(sFilePath))
        {
            try
            {
                File.Create(sFilePath);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message);
            }
        }
    }

    public void WriteLog(string strErrorMessage,string strStackTrace)
    {
        try
        {
            //Read Old Data for Appending to end
            StreamReader sr = new StreamReader(sFilePath);
            string strOldDate = sr.ReadToEnd();
            sr.Close();

            //Create Table
            StringBuilder sb = new StringBuilder();            
            sb.Append("<table>");
            //Message   
            sb.Append("<tr><td>Message</td><td>" + strErrorMessage + "</td></tr>");
            //Stack Trace
            sb.Append("<tr><td>Stack Trace</td><td>" + strStackTrace + "</td></tr>");
            //Date Time
            sb.Append("<tr><td>Date Time</td><td>" + DateTime.Now.ToString() + "</td></tr>");
            //IP Address
            sb.Append("<tr><td>IP Address</td><td>" + HttpContext.Current.Request.UserHostAddress + "</td></tr>");
            //Page URL
            sb.Append("<tr><td>Page URL</td><td>" + HttpContext.Current.Request.RawUrl + "</td></tr>");
            sb.Append("</table>");
            //Break line
            sb.Append("<hr/>");

            //Write to file
            StreamWriter sw = new StreamWriter(sFilePath);
            sw.Write(sb.ToString());
            sw.Write(strOldDate);
            sw.Close();
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Write(ex.Message);
        }
    }
}
