<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GameView.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HIT-the-DUCK - ניהול משחקים</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="HIT-the-DUCK" />
    <meta name="keywords" content="" />
    <meta name="author" content="Gil Mor, Anna Strijko" />
    <link rel="shortcut icon" type="image/x-icon" href="images/favicon.ico" />

    <script src="jscripts/jquery-1.12.0.min.js"></script>

    <%--    <script type="text/javascript">
        if ($('.passCB').attr('checkPublish') == true) {
            $("input.passCB:checkbox").removeAttr("disabled");
        } else {
            $("input.passCB:checkbox").attr("disabled", true);
        }

        $(document).ready(function () {
            if ($('.passCB').attr('checkPublish') == true) {
                $("input.passCB:checkbox").removeAttr("disabled");
            } else {
                $("input.passCB:checkbox").attr("disabled", true);
            }
        }
    </script>--%>
    <script src="https://kit.fontawesome.com/23e1c85578.js" crossorigin="anonymous"></script>
    <script src="jscripts/myScript.js"></script>
    <link href="styles/myStyle.css" rel="stylesheet" />

    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.0/css/all.css" integrity="sha384-lZN37f5QGtY3VHgisS14W3ExzMWZxybE1SJSEsQp9S+oqd12jhcu+A56Ebc1zFSJ" crossorigin="anonymous">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">

    <style>
        body {
            direction: rtl;
            font-family: Arial;
            text-align: right;
            margin: 2%;
        }

        #GridView1 {
            text-align: center;
            margin: 5%;
            width: 90%;
        }

        .tooltip2 {
            position: relative;
            display: inline-block;
            /* border-bottom: 1px dotted black; */
        }

            .tooltip2 .tooltiptext {
                visibility: hidden;
                font-family: arial;
                width: 120px;
                background-color: black;
                color: #fff;
                text-align: center;
                border-radius: 6px;
                padding: 5px 0;
                position: absolute;
                z-index: 1;
                bottom: 150%;
                left: 50%;
                margin-left: -60px;
            }

                .tooltip2 .tooltiptext::after {
                    content: "";
                    position: absolute;
                    top: 100%;
                    left: 50%;
                    margin-left: -5px;
                    border-width: 5px;
                    border-style: solid;
                    border-color: black transparent transparent transparent;
                }

            .tooltip2:hover .tooltiptext {
                visibility: visible;
            }

        .countTooltip {
            text-align: left;
            float: left;
            display: block;
        }

        .myGroup {
            display: inline-block;
        }
    </style>
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
    <div class="forNav">
        <div id="aboutDiv" class="popUp bounceInDown hide">
            <a class="closeAbout">X</a>

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
    <h1>המשחקים שלי</h1>
    <form id="form1" runat="server">
        <div>
            <div style="text-align: right; margin: 1%;" class="form-group">
                <div style="text-align: right; margin: 1%;">שם המשחק (1-20 תווים)</div>
                <div class="form-group myGroup">
                    <asp:TextBox ID="addNameTB" placeholder="שם המשחק" runat="server" item="1" MaxLength="20" CharacterLimit="20" CssClass="form-control CharacterCount" Style="display: inline;"></asp:TextBox>
                    <asp:Label ID="LabelCounter1" CssClass="countTooltip" runat="server" Text=""></asp:Label>
                </div>
                <div class="form-group myGroup">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/add.png" OnClick="ImageButton1_Click" Visible="False" />
                    <asp:Button ID="creatGameButton" CssClass="btn btn-primary" Text="יצירת משחק" runat="server" OnClick="creatGameButton_Click" />
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" ControlToValidate="addNameTB" ErrorMessage="נא להוסיף תוכן למסיח"></asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server" />
            </div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="XmlDataSource1" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnRowCommand="GridView1_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="שם המשחק">
                        <ItemTemplate>
                            <asp:Label ID="NameLabel" runat="server" Text='<%#Server.UrlDecode(XPathBinder.Eval(Container.DataItem, "@name").ToString())%>'> </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="קוד משחק">
                        <ItemTemplate>
                            <asp:Label ID="idLabel" runat="server" Text='<%#XPathBinder.Eval(Container.DataItem, "@gamecode")%>'> </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="עריכה">
                        <ItemTemplate>
                            <asp:ImageButton ID="editImageButton" CausesValidation="false" CommandName="editRow" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@id")%>' runat="server" ImageUrl="~/images/edit.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="מחיקה">
                        <ItemTemplate>
                            <asp:ImageButton ID="deleteImageButton" CausesValidation="false" CommandName="deleteRow" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@id")%>' runat="server" ImageUrl="~/images/delete.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <div>
                                <span>פרסום</span>
                                <%--                                <img src="images/question.png" width="15px" alt="Alternate Text" />--%>
                                <div class="far fa-question-circle tooltip2">
                                    <span class="tooltiptext">תנאי סף לפרסום: לפחות 18 פריטים, לפחות 9 פריטים בכל עמודה והנחיה לזיהוי
                                    </span>
                                </div>
                            </div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox CssClass="passCB" ID="isPassCheckBox" runat="server" AutoPostBack="true" OnCheckedChanged="isPassCheckBox_CheckedChanged" Checked='<%#Convert.ToBoolean(XPathBinder.Eval(Container.DataItem,"@isPublished"))%>' theItemId='<%#XPathBinder.Eval(Container.DataItem,"@id")%>' checkPublish='<%#CheckIfCanPublish(XPathBinder.Eval(Container.DataItem,"@id").ToString())%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField>
                        <HeaderTemplate>
                            <div>
                                <span>שיתוף</span>
                                <div class="far fa-question-circle tooltip">
                                    <span class="tooltiptext">שיתוף משחק מאפשר לעורכים אחרים להעתיק את המשחק שלכם
                                    </span>
                                </div>
                            </div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="isShareCheckBox" runat="server" AutoPostBack="true" OnCheckedChanged="isShareCheckBox_CheckedChanged" Checked="false" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@id")%>' checkPublish='<%#CheckIfCanPublish(XPathBinder.Eval(Container.DataItem,"@id").ToString())%>' />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <HeaderStyle BackColor="#d8e2f3ff" Font-Bold="True" ForeColor="black" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#242121" />
            </asp:GridView>

            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/XMLFiles/games.xml" XPath="/HIT-the-Duck/game"></asp:XmlDataSource>
            <br />
        </div>
        <asp:Panel ID="DeleteConfPopUp" CssClass="PopUpBackground" Visible="false" runat="server">
            <asp:Panel ID="DeleteConfPopUp2" CssClass="PopUp" runat="server">
                <asp:Label ID="Label3" runat="server" Text="האם אתה בטוח?"></asp:Label>
                <!-- כפתור יציאה - יש לשים לב שהוא מפנה בלחיצה לאותה פונקציה של הכפתור יציאה השני -->
                <asp:Button ID="ExitDeleteConf" CausesValidation="false" CssClass="Exit" runat="server" Text="לא אל תמחק" OnClick="ExitButton_Click" />
                <asp:Button ID="DeleteGame" CausesValidation="false" CssClass="Exit" runat="server" Text="כן מחק משחק" OnClick="DeleteQ_Click" />
                <!-- תווית להצגת הודעה למשתמש -->
            </asp:Panel>
        </asp:Panel>
    </form>
</body>
</html>
