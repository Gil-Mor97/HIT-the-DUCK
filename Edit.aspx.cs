using Habanero.Faces.Base;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Xceed.Wpf.Toolkit;

public partial class _Default : System.Web.UI.Page
{
    XmlDocument myDoc = new XmlDocument();
    string gameid;
    string imagesLibPath = "uploadedFiles/";//הגדרת נתיב לשמירת הקובץ

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("login.aspx");
        }

        if (Session["theItemIdSession"] == null)
        {
            Response.Redirect("GameView.aspx");
        }

        gameid = Session["theItemIdSession"].ToString();
        //gameid = "1";

        myDoc.Load(Server.MapPath("/XMLFiles/games.xml"));

        XmlDataSource_Correct.XPath = "/HIT-the-Duck/game[@id='" + gameid + "']/answer[@isCorrect='true']";
        XmlDataSource_Incorrect.XPath = "/HIT-the-Duck/game[@id='" + gameid + "']/answer[@isCorrect='false']";

        gamename.Text = Server.HtmlDecode(myDoc.SelectNodes("/HIT-the-Duck/game[@id='" + gameid + "']")[0].Attributes["name"].Value);
        if (!string.IsNullOrEmpty(gamename.Text))
        {
            myH1.InnerHtml = "עריכת המשחק \"" + gamename.Text + "\"";
        }

        //הצגת הנחייה לזיהוי
        if(myDoc.SelectNodes("/HIT-the-Duck/game[@id='" + gameid + "']/question").Item(0) != null)
        {
            instructions.Text = Server.HtmlDecode(myDoc.SelectNodes("/HIT-the-Duck/game[@id='" + gameid + "']/question").Item(0).InnerXml.ToString());

        }
        else
        {
            instructions.Text = "";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("login.aspx");
        }
        LoadDLItems(DataListCorrect);
        LoadDLItems(DataListIncorrect);
    }

    protected void LoadDLItems(DataList dl)
    {

        //הפונקציה שאחראית על הצגת התמונות והטקסטים בטבלה של המסיחים הנכונים
        foreach (DataListItem dli in dl.Items)
        {
            //הפנייה בשורה הבאה תיעשה לאחד מהפקדים בגריד וויו שיש להם אפשרות לזהות את האיי-די של המסיח שאליו מתייחסת השורה 
            string myID = ((LinkButton)dli.FindControl("lnkSelect")).Attributes["theItemId"];

            //שליפת הענף שמכיל את השם (בטקסט או בתמונה) של המסיח
            XmlNode myRightQ = myDoc.SelectSingleNode("/HIT-the-Duck/game[@id='" + gameid + "']/answer[@id='" + myID + "']");

            //המאפיין המגדיר האם יש או אין תמונה נמצא בענף ההורה של הענף ששלפנו
            //אם יש תמונה למסיח
            if (myRightQ.Attributes["qType"].Value == "img")
            {
                ((Label)dli.FindControl("NameLabel")).Visible = false;
                //הצגת פקד התמונה
                ((System.Web.UI.WebControls.Image)dli.FindControl("ImageQR")).Visible = true;

                //הצגת התמונה עצמה בתוך הפקד
                ((System.Web.UI.WebControls.Image)dli.FindControl("ImageQR")).ImageUrl = "~/uploadedFiles/" + Server.UrlDecode(myRightQ.InnerXml);
            }
            else
            {
                //הצגת פקד הלייבל
                ((Label)dli.FindControl("NameLabel")).Visible = true;
                ((System.Web.UI.WebControls.Image)dli.FindControl("ImageQR")).Visible = false;


                //הכנסה של שם המסיח לטקסט שבפקד הלייבל
                ((Label)dli.FindControl("NameLabel")).Text = Server.UrlDecode(myRightQ.InnerXml);
            }
        }

        CheckRequierments();
    }

    protected void CheckFields()
    {
        
    }

    protected void ItemSelect(object sender, EventArgs e)
    {
        //חסימת שימוש בכפתורים
        EnabledUploading(false);


        string myID = ((LinkButton)((DataList)sender).SelectedItem.FindControl("lnkSelect")).Attributes["theItemId"];
        myDoc.Load(Server.MapPath("/XMLFiles/games.xml"));
        //string gameid = "1";/* Session["theItemIdSession"].ToString();*/
        ViewState["mySelectedId"] = myID;


        XmlNode BTE = myDoc.SelectNodes("/HIT-the-Duck/game[@id='" + gameid + "']/answer[@id='" + myID + "']").Item(0);

        if (BTE.Attributes["isCorrect"].Value == "true")
        {
            ansRBL.SelectedIndex = 0;
            DataListIncorrect.SelectedIndex = -1;
        }
        else
        {
            ansRBL.SelectedIndex = 1;
            DataListCorrect.SelectedIndex = -1;
        }

        if (BTE.Attributes["qType"].Value == "img")
        {
            itemIMG.ImageUrl = "~/uploadedFiles/" + Server.UrlDecode(BTE.InnerXml);
            itemTB.Visible = false;
            itemIMG.Visible = true;
            LabelCounter3.Visible = false;
        }
        else
        {
            itemTB.Text = BTE.InnerXml;
            itemIMG.Visible = false;
            itemTB.Visible = true;
            LabelCounter3.Visible = true;
        }

        //myDoc.Save(Server.MapPath("/XMLFiles/games.xml"));
        itemUpdateButton.Visible = true;

        itemCancelButton.Visible = true;
    }

    protected void itemUpdateButton_Click(object sender, EventArgs e)
    {
        string myID;
        if (DataListCorrect.SelectedItem != null)
        {
            myID = ((LinkButton)DataListCorrect.SelectedItem.FindControl("lnkSelect")).Attributes["theItemId"];
        }
        else
        {
            myID = ((LinkButton)DataListIncorrect.SelectedItem.FindControl("lnkSelect")).Attributes["theItemId"];
        }

        DataListCorrect.SelectedIndex = -1;
        DataListIncorrect.SelectedIndex = -1;

        myDoc.Load(Server.MapPath("/XMLFiles/games.xml"));

        XmlNode BTE = myDoc.SelectNodes("/HIT-the-Duck/game[@id='" + gameid + "']/answer[@id='" + myID + "']").Item(0);
        if (ansRBL.SelectedItem.Text == "נכון") //בדיקה האם נבחר "נכון" או "לא נכון" בעריכת הפריט
        {
            BTE.Attributes["isCorrect"].Value = "true";
        }
        else
        {
            BTE.Attributes["isCorrect"].Value = "false";
        }
        if (!string.IsNullOrEmpty(itemTB.Text))
        {
            BTE.InnerXml = Server.HtmlEncode(itemTB.Text); //עדכון תוכן הפריט
        }
        myDoc.Save(Server.MapPath("/XMLFiles/games.xml"));

        myDoc.Load(Server.MapPath("/XMLFiles/games.xml"));
        DataListCorrect.DataBind();
        DataListIncorrect.DataBind();
        LoadDLItems(DataListCorrect);
        LoadDLItems(DataListIncorrect);

        itemIMG.Visible = true;
        itemIMG.ImageUrl = "data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==";
        itemTB.Visible = false;
        LabelCounter3.Visible = false;

        itemTB.Text = "";
        itemUpdateButton.Visible = false;
        itemCancelButton.Visible = false;

        EnabledUploading(true);
    }

    protected void back_Click(object sender, EventArgs e)
    {
        Response.Redirect("/GameView.aspx");
    }
    protected void txtUpload_Click(object sender, ImageClickEventArgs e)
    {
        //itemIMG.Visible = false;
        itemTB.Visible = true;
        LabelCounter3.Visible = true;
        //ItemAddButton.Visible = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowText()", true);

    }

    protected void imgUpload_Click(object sender, ImageClickEventArgs e)
    {
        //itemIMG.Visible = true;
        //itemTB.Visible = false;
        //LabelCounter3.Visible = false;
        //ItemAddButton.Visible = true;
        
        Page.ClientScript.RegisterStartupScript(this.GetType(),"CallMyFunction", "ShowText()", true);

    }

    

    protected void UpadeInfo_Click(object sender, EventArgs e)
    {
        XmlNode BTE = myDoc.SelectNodes("/HIT-the-Duck/game[@id='" + gameid + "']")[0];
        BTE.Attributes["name"].Value = Server.HtmlEncode(gamename.Text);
        myDoc.Save(Server.MapPath("/XMLFiles/games.xml"));
        myDoc.Load(Server.MapPath("/XMLFiles/games.xml"));

        BTE = myDoc.SelectNodes("/HIT-the-Duck/game[@id='" + gameid + "']/question").Item(0);
        BTE.InnerText = Server.HtmlEncode(instructions.Text);
        myDoc.Save(Server.MapPath("/XMLFiles/games.xml"));
        myDoc.Load(Server.MapPath("/XMLFiles/games.xml"));

        if (instructions.Text.Length > 0)
        {
            instuction_ok.Attributes["class"] = "fas fa-check-circle";
        }
        else
        {
            instuction_ok.Attributes["class"] = "fas fa-exclamation-circle";
        }
    }

    protected void itemCancelButton_Click(object sender, EventArgs e)
    {
        itemCancelButton.Visible = false;
        itemUpdateButton.Visible = false;


        DeleteConfPopUp.Visible = true;

    }

    protected void DeleteQ_Click(object sender, EventArgs e)
    {
        string myID = ViewState["mySelectedId"].ToString();

        DataListCorrect.SelectedIndex = -1;
        DataListIncorrect.SelectedIndex = -1;

        XmlNode node = myDoc.SelectNodes("/HIT-the-Duck/game[@id='" + gameid + "']/answer[@id='" + myID + "']").Item(0);

        node.ParentNode.RemoveChild(node);
        myDoc.Save(Server.MapPath("/XMLFiles/games.xml"));
        myDoc.Load(Server.MapPath("/XMLFiles/games.xml"));

        itemIMG.Visible = true;
        itemIMG.ImageUrl = "data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==";
        itemTB.Visible = false;
        LabelCounter3.Visible = false;

        DataListCorrect.DataBind();
        DataListIncorrect.DataBind();
        LoadDLItems(DataListCorrect);
        LoadDLItems(DataListIncorrect);

        DeleteConfPopUp.Visible = false;
        itemCancelButton.Visible = false;
        itemUpdateButton.Visible = false;

        EnabledUploading(true);
    }

    protected void ExitButton_Click(object sender, EventArgs e)
    {
        DeleteConfPopUp.Visible = false;
        itemCancelButton.Visible = true;
        itemUpdateButton.Visible = true;

        EnabledUploading(false);
    }

    protected void EnabledUploading(bool myFlag)
    {
        imgUpload.Enabled = myFlag;
        txtUpload.Enabled = myFlag;
    }

    protected void ItemAddButton_Click(object sender, EventArgs e)
    {
        int myId = Convert.ToInt16(myDoc.SelectSingleNode("/HIT-the-Duck/game[@id='" + gameid + "']//idCounter").InnerXml);
        myId++;
        string myNewId = myId.ToString();
        myDoc.SelectSingleNode("/HIT-the-Duck/game[@id='" + gameid + "']//idCounter").InnerXml = myNewId;

        // יצירת ענף משחק     
        XmlElement myNewGametNode = myDoc.CreateElement("answer");
        myNewGametNode.SetAttribute("id", myNewId);

        Button btn1 = ((Button)sender);
        if (btn1.ID == "ItemAddButtonIMG")
        {
            string imgName = SaveImg(); //שמירת התמונה
            myNewGametNode.InnerXml = Server.HtmlEncode(imgName);
            myNewGametNode.SetAttribute("qType", "img");
            myNewGametNode.SetAttribute("isCorrect", ansRBL.SelectedValue);
        }
        else if(btn1.ID == "ItemAddButtonTXT")
        {
            myNewGametNode.InnerXml = Server.HtmlEncode(itemTB.Text);
            myNewGametNode.SetAttribute("qType", "txt");
            myNewGametNode.SetAttribute("isCorrect", ansRBL.SelectedValue);
        }

        XmlNode lastAnswer = myDoc.SelectNodes("/HIT-the-Duck/game[@id='" + gameid + "']").Item(0).LastChild;
        myDoc.SelectSingleNode("/HIT-the-Duck/game[@id='" + gameid + "']").InsertAfter(myNewGametNode, lastAnswer);
        myDoc.Save(Server.MapPath("/XMLFiles/games.xml"));
        myDoc.Load(Server.MapPath("/XMLFiles/games.xml"));

        itemIMG.Visible = true;
        itemIMG.ImageUrl = "data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==";
        itemTB.Visible = false;
        itemTB.Text = "";
        LabelCounter3.Visible = false;

        DataListCorrect.DataBind();
        DataListIncorrect.DataBind();
        LoadDLItems(DataListCorrect);
        LoadDLItems(DataListIncorrect);
    }

    protected void CheckRequierments()
    {
        if(DataListCorrect.Items.Count + DataListIncorrect.Items.Count >= 18)
        {
            overall_ok.Attributes["class"] = "fas fa-check-circle";
        }
        else
        {
            overall_ok.Attributes["class"] = "fas fa-exclamation-circle";
            XmlNode theGame = myDoc.SelectSingleNode("/HIT-the-Duck/game[@id=" + gameid + "]");
            theGame.Attributes["isPublished"].InnerText = "false"; //עדכון שלא ניתן לשתף
        }
        if (DataListCorrect.Items.Count >= 9 && DataListIncorrect.Items.Count >= 9)
        {
            column_ok.Attributes["class"] = "fas fa-check-circle";
        }
        else
        {
            column_ok.Attributes["class"] = "fas fa-exclamation-circle";
            XmlNode theGame = myDoc.SelectSingleNode("/HIT-the-Duck/game[@id=" + gameid + "]");
            theGame.Attributes["isPublished"].InnerText = "false"; //עדכון שלא ניתן לשתף
        }
        if (instructions.Text.Length > 0)
        {
            instuction_ok.Attributes["class"] = "fas fa-check-circle";
        }
        else
        {
            instuction_ok.Attributes["class"] = "fas fa-exclamation-circle";
        }

    }

    protected string SaveImg()
    {
        string filetype = FileUpload1.PostedFile.ContentType;//איתור סוג הקובץ 

        //בדיקה האם הקובץ הנקלט הוא מסוג תמונה
        if (filetype.Contains("image"))
        {
            //איתור שם הקובץ
            string fileName = FileUpload1.PostedFile.FileName;
            //איתור סיומת הקובץ
            string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));
            //איתור זמן העלת הקובץ
            string myTime = DateTime.Now.ToString("dd_MM_yy_HH_mm_ss");
            //הגדרת שם חדש לקובץ
            string imageNewName = myTime + endOfFileName;


            // Bitmap המרת הקובץ שיתקבל למשתנה מסוג
            System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(FileUpload1.PostedFile.InputStream);

            //קריאה לפונקציה המקטינה את התמונה
            //אנו שולחים לה את התמונה שלנו בגירסאת הביטמאפ ואת האורך והרוחב שאנו רוצים לתמונה החדשה
            System.Drawing.Image objImage = FixedSize(bmpPostedImage, 300, 300);



            //שמירת הקובץ בגודלו החדש בתיקייה
            objImage.Save(Server.MapPath(imagesLibPath) + imageNewName);
            return imageNewName;
        }
        else
        {
            Response.Write("<script LANGUAGE='JavaScript' >alert('הקובץ אינו תמונה ולכן לא ניתן להעלות אותו')</script>");
            return null;
        }
    }

    static System.Drawing.Image FixedSize(System.Drawing.Image imgPhoto, int Width, int Height)
    {
        int sourceWidth = Convert.ToInt32(imgPhoto.Width);
        int sourceHeight = Convert.ToInt32(imgPhoto.Height);

        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)Width / (float)sourceWidth);
        nPercentH = ((float)Height / (float)sourceHeight);
        if (nPercentH < nPercentW)
        {
            nPercent = nPercentH;
            destX = System.Convert.ToInt16((Width -
                          (sourceWidth * nPercent)) / 2);
        }
        else
        {
            nPercent = nPercentW;
            destY = System.Convert.ToInt16((Height -
                          (sourceHeight * nPercent)) / 2);
        }

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        System.Drawing.Bitmap bmPhoto = new System.Drawing.Bitmap(Width, Height,
                          PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                         imgPhoto.VerticalResolution);

        System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto);
        grPhoto.Clear(System.Drawing.Color.White);
        grPhoto.InterpolationMode =
                InterpolationMode.HighQualityBicubic;

        grPhoto.DrawImage(imgPhoto,
            new System.Drawing.Rectangle(destX, destY, destWidth, destHeight),
            new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
            System.Drawing.GraphicsUnit.Pixel);

        grPhoto.Dispose();
        return bmPhoto;
    }

    
}
