<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HIT-the-DUCK - התחברות לעורך</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="HIT-the-DUCK" />
    <meta name="keywords" content="" />
    <meta name="author" content="Gil Mor, Anna Strijko" />
    <link rel="shortcut icon" type="image/x-icon" href="images/favicon.ico" />

    <script src="jscripts/jquery-1.12.0.min.js"></script>
    <script src="https://kit.fontawesome.com/23e1c85578.js" crossorigin="anonymous"></script>
    <script src="jscripts/myScript.js"></script>
    <link href="styles/myStyle.css" rel="stylesheet" />
    <style>
        .form-group {
            text-align: right;
            width: 32%;
            display: grid;
            margin: 0 auto;
        }

            .form-group #myspan {
                font-size: 0.7em;
                text-align: center;
            }

        h1 {
            text-align: center;
        }
    </style>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.0/css/all.css" integrity="sha384-lZN37f5QGtY3VHgisS14W3ExzMWZxybE1SJSEsQp9S+oqd12jhcu+A56Ebc1zFSJ" crossorigin="anonymous">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
</head>
<body>
    <header class="myNavigation">
        <!--קישור לדף עצמו כדי להתחיל את המשחק מחדש בלחיצה על הלוגו-->
        <!--תפריט הניווט בראש העמוד-->
        <nav>
            <a class="about">אודות</a>
            <a class="howToPlay">איך משחקים?</a>
            <a href="GameView.aspx">עורך</a>
            <div class="topnav-right">
                <a href="index.html">
                    <img id="logo" src="images/bduck.png" />
                    <!--הלוגו של המשחק שלכם-->
                    <p style="display: inline;">HIT-the-DUCK</p>
                </a>
            </div>
        </nav>
    </header>
    <h1>HIT-the-DUCK - התחברות לעורך</h1>
    <div class="forNav">
        <div id="aboutDiv" class="popUp bounceInDown hide">
            <a class="closeAbout" style="float: left;">X</a>

            <img id="logo" src="images/bduck.png" />
            <!--הלוגו של המשחק שלכם-->
            <b>HIT-the-DUCK</b>
            <p>
                אפיון ופיתוח:
                <span>אנה סטריז'קו וגיל מור</span>
            </p>
            <p>פותח במסגרת פרוייקט בקורסים:</p>
            <p>סביבות לימוד אינטראקטיביות 1 & אנימציה, תשע"ט</p>
            <br />
            <a href="https://www.hit.ac.il/telem/overview" target="_blank">הפקולטה לטכנולוגיות למידה</a>
            <p>מכון טכנולוגי חולון</p>
            <img id="hit" src="images/HIT-logo.jpg" width="100" height="100" />
        </div>
        <div id="howToPlayDiv" class="popUp bounceInDown hide">
            <a class="closeHowToPlay">X</a>
            <p>כאן יכנסו התמונות/הסרטון המסביר כיצד לשחק</p>
        </div>
    </div>
    <form id="form1" runat="server">
        <div class="container">
            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="שם משתמש"></asp:Label>
                <asp:TextBox ID="usernameTB" CssClass="form-control-sm" runat="server" placeholder="שם משתמש"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="Label2" runat="server" Text="סיסמה"></asp:Label>
                <asp:TextBox ID="passwordTB" TextMode="Password" runat="server" CssClass="form-control-sm" placeholder="סיסמה"></asp:TextBox>
            </div>
            <div class="form-group">
                <span id="myspan">שם משתמש: admin | סיסמה: admin</span>
            </div>
            <div class="form-group">
                <asp:Button ID="loginBtn" Text="התחברות" CssClass="btn btn-primary" runat="server" OnClick="loginBtn_Click" />
            </div>
        </div>
    </form>
</body>
</html>
