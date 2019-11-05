using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] != null)
        {
            Response.Redirect("GameView.aspx");
        }
    }

    protected void loginBtn_Click(object sender, EventArgs e)
    {
        if (usernameTB.Text == "admin" && passwordTB.Text == "admin")
        {
            Session["user"] = "admin";
            Response.Redirect("GameView.aspx");
        }
        else
        {
            Response.Write("<script LANGUAGE='JavaScript' >alert('שם משתמש או סיסמה שגויים')</script>");
        }
    }
}