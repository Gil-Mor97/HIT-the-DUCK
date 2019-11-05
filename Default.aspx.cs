using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Newtonsoft.Json;

public partial class _Default : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        {
            

            XmlDocument myDoc = new XmlDocument();
            myDoc.Load(Server.MapPath("~/XMLFiles/games.xml"));

            //string gameCode = Request["gameCode"]; // חשוב לשים לב שזה אותו שם משתנה כמו באנימייט
            string gameCode="1";
            XmlNode gameNode = myDoc.SelectSingleNode("//game[@id='" + gameCode + "']");

            if (gameNode != null)
            {
                string jsonText = JsonConvert.SerializeXmlNode(gameNode);


                TextBox1.Text = jsonText;
            }
            else
            {
                TextBox1.Text = "game not found";
            }
        }
}
}
