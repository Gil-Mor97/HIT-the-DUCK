using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("login.aspx");
        }
        LoadDisabled();
    }

    protected void creatGameButton_Click(object sender, EventArgs e)
    {
        XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument();
        int myId = Convert.ToInt16(xmlDoc.SelectSingleNode("//idCounter").InnerXml);
        myId++;
        string myNewId = myId.ToString();
        xmlDoc.SelectSingleNode("//idCounter").InnerXml = myNewId;

        // יצירת ענף משחק     
        XmlElement myNewGametNode = xmlDoc.CreateElement("game");
        myNewGametNode.SetAttribute("id", myNewId);
        myNewGametNode.SetAttribute("name", Server.HtmlEncode(addNameTB.Text));
        myNewGametNode.SetAttribute("isPublished", "false");
        myNewGametNode.SetAttribute("gamecode", (1000 + int.Parse(myNewId)).ToString());

        XmlElement newCounter = xmlDoc.CreateElement("idCounter");
        XmlElement newQuestions = xmlDoc.CreateElement("question");

        XmlText newCounterId = xmlDoc.CreateTextNode("1");
        newCounter.AppendChild(newCounterId);

        XmlText newQuestion = xmlDoc.CreateTextNode("");
        newQuestions.AppendChild(newQuestion);

        myNewGametNode.AppendChild(newCounter);
        myNewGametNode.AppendChild(newQuestions);


        XmlNode FirstGame = xmlDoc.SelectNodes("/HIT-the-Duck/game").Item(0);
        XmlNode LastGame = xmlDoc.SelectNodes("/HIT-the-Duck").Item(0).LastChild;
        xmlDoc.SelectSingleNode("/HIT-the-Duck").InsertBefore(myNewGametNode, FirstGame);
        XmlDataSource1.Save();
        GridView1.DataBind();
        LoadDisabled();
        addNameTB.Text = "";
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument();
        int myId = Convert.ToInt16(xmlDoc.SelectSingleNode("//idCounter").InnerXml);
        myId++;
        string myNewId = myId.ToString();
        xmlDoc.SelectSingleNode("//idCounter").InnerXml = myNewId;

        // יצירת ענף משחק     
        XmlElement myNewGametNode = xmlDoc.CreateElement("game");
        myNewGametNode.SetAttribute("id", myNewId);
        myNewGametNode.SetAttribute("name", Server.HtmlEncode(addNameTB.Text));
        myNewGametNode.SetAttribute("isPublished", "false");
        myNewGametNode.SetAttribute("gamecode", (1000 + int.Parse(myNewId)).ToString());

        XmlElement newCounter = xmlDoc.CreateElement("idCounter");
        XmlElement newQuestions = xmlDoc.CreateElement("question");

        XmlText newCounterId = xmlDoc.CreateTextNode("1");
        newCounter.AppendChild(newCounterId);

        XmlText newQuestion = xmlDoc.CreateTextNode("");
        newQuestions.AppendChild(newQuestion);

        myNewGametNode.AppendChild(newCounter);
        myNewGametNode.AppendChild(newQuestions);


        XmlNode FirstGame = xmlDoc.SelectNodes("/HIT-the-Duck/game").Item(0);
        XmlNode LastGame = xmlDoc.SelectNodes("/HIT-the-Duck").Item(0).LastChild;
        xmlDoc.SelectSingleNode("/HIT-the-Duck").InsertBefore(myNewGametNode, FirstGame);
        XmlDataSource1.Save();
        GridView1.DataBind();
        LoadDisabled();
        addNameTB.Text = "";
    }

    //פונקציה זו תרוץ בעת לחיצה על כפתור הפרסום
    protected void isPassCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        // טעינה של העץ
        XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument();
        // תחילה אנו מבררים מהו ה -אי די- של הפריט בעץ ה אקס אם אל
        CheckBox myCheckBox = (CheckBox)sender;
        // מושכים את האי די של הפריט באמצעות המאפיין שהוספנו באופן ידני לתיבה
        string theId = myCheckBox.Attributes["theItemId"];
        //שאילתא למציאת הסטודנט שברצוננו לעדכן
        XmlNode theGame = xmlDoc.SelectSingleNode("/HIT-the-Duck/game[@id=" + theId + "]");

        //קבלת הערך החדש של התיבה לאחר הלחיצה
        bool NewIsPass = myCheckBox.Checked;
        if (NewIsPass) //אם משנים לערך אמת
        {
            XmlNodeList theGameTrue = xmlDoc.SelectNodes("/HIT-the-Duck/game[@id='" + theId + "']/answer[@isCorrect='true']");
            XmlNodeList theGameFalse = xmlDoc.SelectNodes("/HIT-the-Duck/game[@id='" + theId + "']/answer[@isCorrect='false']");
            if (theGameTrue.Count >= 9 && theGameFalse.Count >= 9)
            {
                theGame.Attributes["isPublished"].InnerText = NewIsPass.ToString(); //עדכון שניתן לשתף
            }
            else
            {
                ((CheckBox)sender).Checked = false;
            }
        }
        else
        {
            theGame.Attributes["isPublished"].InnerText = NewIsPass.ToString();
        }

        //שמירה בעץ והצגה
        XmlDataSource1.Save();
        GridView1.DataBind();
        LoadDisabled();
    }

    protected bool CheckIfCanPublish(string itemID)
    {
        XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument();

        XmlNodeList theGameTrue = xmlDoc.SelectNodes("/HIT-the-Duck/game[@id='" + itemID + "']/answer[@isCorrect='true']");
        XmlNodeList theGameFalse = xmlDoc.SelectNodes("/HIT-the-Duck/game[@id='" + itemID + "']/answer[@isCorrect='false']");
        if (theGameTrue.Count < 9 || theGameFalse.Count < 9) //בדיקה אם אין מספיק פריטים
        {
            return false;
        }

        string myInstructions = Server.HtmlDecode(xmlDoc.SelectNodes("/HIT-the-Duck/game[@id='" + itemID + "']/question").Item(0).InnerXml.ToString());
        if (string.IsNullOrEmpty(myInstructions)) //בדיקה אם אין הנחיית זיהוי
        {
            return false;
        }

        return true;
    }

    protected void LoadDisabled()
    {
        foreach (GridViewRow row in GridView1.Rows)
        {

            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox myCbs = row.FindControl("isPassCheckBox") as CheckBox;
                string checkPub = myCbs.Attributes["checkPublish"];

                CheckBox myCbsShare = row.FindControl("isShareCheckBox") as CheckBox;


                bool isPublish = bool.Parse(checkPub);
                if (isPublish)
                {
                    myCbs.Enabled = true;
                    //myCbsShare.Enabled = true;
                }
                else
                {
                    myCbs.Enabled = false;
                    //myCbsShare.Enabled = false;
                }
            }
        }
    }

    protected void isShareCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        // טעינה של העץ
        XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument();
        // תחילה אנו מבררים מהו ה -אי די- של הפריט בעץ ה אקס אם אל
        CheckBox myCheckBox = (CheckBox)sender;
        // מושכים את האי די של הפריט באמצעות המאפיין שהוספנו באופן ידני לתיבה
        string theId = myCheckBox.Attributes["theItemId"];
        //שאילתא למציאת הסטודנט שברצוננו לעדכן
        XmlNode theStudents = xmlDoc.SelectSingleNode("/HIT-the-Duck/game[@id=" + theId + "]");

        //קבלת הערך החדש של התיבה לאחר הלחיצה
        bool NewIsPass = myCheckBox.Checked;
        if (bool.Parse(theStudents.Attributes["isPublished"].InnerText))
            //עדכון של המאפיין בעץ
            theStudents.Attributes["isPublished"].InnerText = NewIsPass.ToString();
        //שמירה בעץ והצגה
        XmlDataSource1.Save();
        GridView1.DataBind();
        LoadDisabled();

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        // תחילה אנו מבררים מהו ה -אי די- של הפריט בעץ ה אקס אם אל
        ImageButton i = (ImageButton)e.CommandSource;
        // אנו מושכים את המזהה של הפריט באמצעות מאפיין לא שמור במערכת שהוספנו באופן ידני לכפתור-תמונה
        string theId = i.Attributes["theItemId"];
        Session["theItemIdSession"] = i.Attributes["theItemId"];

        // עלינו לברר איזו פקודה צריכה להתבצע - הפקודה רשומה בכל כפתור             
        switch (e.CommandName)
        {
            //אם נלחץ על כפתור מחיקה יקרא לפונקציה של מחיקה                    
            case "deleteRow":
                DeleteRow(theId);
                break;

            //אם נלחץ על כפתור עריכה (העפרון) נעבור לדף עריכה                    
            case "editRow":
                Response.Redirect("/Edit.aspx");
                break;
        }
    }

    //מחיקת משחק
    void DeleteRow(string theItemId)
    {
        ViewState["mySelectedId"] = theItemId;
        DeleteConfPopUp.Visible = true;        
    }

    protected void DeleteQ_Click(object sender, EventArgs e)
    {
        string myID = ViewState["mySelectedId"].ToString();

        //הסרת ענף של משחק קיים באמצעות זיהוי האיי דיי שניתן לו על ידי לחיצה עליו מתוך הטבלה
        //שמירה ועדכון לתוך העץ ולגריד ויו
        XmlDocument Document = XmlDataSource1.GetXmlDocument();
        XmlNode node = Document.SelectSingleNode("/HIT-the-Duck/game[@id='" + myID + "']");
        node.ParentNode.RemoveChild(node);

        XmlDataSource1.Save();
        GridView1.DataBind();
        LoadDisabled();
        DeleteConfPopUp.Visible = false;
    }

    protected void ExitButton_Click(object sender, EventArgs e)
    {
        DeleteConfPopUp.Visible = false;
    }



}