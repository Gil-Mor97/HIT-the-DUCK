<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Xml;
using Newtonsoft.Json;

public class Handler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(context.Server.MapPath("~/XMLFiles/games.xml"));


        string gameCode = context.Request["gameCode"]; // חשוב לשים לב שזה אותו שם משתנה כמו באנימייט

        XmlNode gameNode = myDoc.SelectSingleNode("//game[@gamecode='" + gameCode + "']");

        if (gameNode != null)
        {
            if (bool.Parse(gameNode.Attributes["isPublished"].Value) == true)
            {
                string jsonText = JsonConvert.SerializeXmlNode(gameNode);

                context.Response.Write(jsonText);
            }
            else
            {
                context.Response.Write("notPublished"); //המשחק לא מפורסם
            }
        }
        else
        {
            context.Response.Write("noGame"); //לא נמצא משחק
        }
    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }
}
















