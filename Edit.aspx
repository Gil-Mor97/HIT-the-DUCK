<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HIT-the-DUCK - עריכת משחק</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="HIT-the-DUCK" />
    <meta name="keywords" content="" />
    <meta name="author" content="Gil Mor, Anna Strijko" />
    <link rel="shortcut icon" type="image/x-icon" href="images/favicon.ico" />

    <script src="jscripts/jquery-1.12.0.min.js"></script>
    <script src="jscripts/myScript.js"></script>

    <link href="styles/myStyle.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.0/css/all.css" integrity="sha384-lZN37f5QGtY3VHgisS14W3ExzMWZxybE1SJSEsQp9S+oqd12jhcu+A56Ebc1zFSJ" crossorigin="anonymous" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
</head>
<body dir="rtl">
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
    <div class="notContainer">
        <h1 runat="server" id="myH1">עריכת המשחק</h1>
        <div>
            <form id="form1" runat="server">
                <div class="container">
                    <div id="infospace" class="form-group row">
                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server" Text="שם המשחק: (1-20 תווים)"></asp:Label><br />
                            <asp:TextBox ID="gamename" placeholder="שם המשחק" item="1" MaxLength="20" CharacterLimit="20" CssClass="CharacterCount" runat="server" value="שם המשחק"></asp:TextBox><br />
                            <asp:Label ID="LabelCounter1" CssClass="countTooltip" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label2" runat="server" Text="הנחייה לזיהוי: (1-50 תווים)"></asp:Label><br />
                            <asp:TextBox ID="instructions" placeholder="הנחייה לזיהוי" item="2" MaxLength="50" CharacterLimit="50" CssClass="CharacterCount" runat="server" value=""></asp:TextBox><br />
                            <asp:Label ID="LabelCounter2" CssClass="countTooltip" runat="server" Text=""></asp:Label>
                        </div>
                        <div id="updating">
                            <asp:Button ID="UpadeInfo" CausesValidation="false" Text="עדכון שם והנחיות" CssClass="btn btn-primary" runat="server" OnClick="UpadeInfo_Click" UseSubmitBehavior="False" />
                        </div>
                        <div>
                            <asp:Button ID="back" CausesValidation="false" runat="server" CssClass="btn btn-primary" Text="בחזרה לעמוד הראשי" Style="display: block; width: 100%;" OnClick="back_Click" />
                        </div>
                    </div>

                    <div class="form-group row">
                        <div class="form-group col-sm-5" style="float: right;">
                            <div class="checks">
                                <div><span runat="server" id="overall_ok" class="fa fa-exclamation-circle"></span><span>לפחות 18 פריטים</span></div>
                                <div><span runat="server" id="column_ok" class="fas fa-exclamation-circle"></span><span>לפחות 9 פריטים בכל עמודה</span></div>
                                <div><span runat="server" id="instuction_ok" class="fas fa-exclamation-circle"></span><span>הנחייה לזיהוי</span></div>
                            </div>
                            <asp:FileUpload ID="FileUpload1" runat="server" />

                            <div id="uploading">
                                <asp:ImageButton ID="imgUpload" ImageUrl="~/images/uploadPhoto.png" CausesValidation="false" runat="server" OnClick="imgUpload_Click" OnClientClick="openFileUploader1(); return false;" />
                                <asp:ImageButton ID="txtUpload" ImageUrl="~/images/uploadText.png" CausesValidation="false" runat="server" OnClick="txtUpload_Click" />
                            </div>
                            <div id="ducky">
                                <div id="myEdit" style="float: right; direction: rtl">
                                    <asp:TextBox ID="itemTB" runat="server" Visible="false" CausesValidation="false" placeholder="תוכן המסיח" Text="" item="3" MaxLength="40" TextMode="MultiLine" CharacterLimit="40" CssClass="CharacterCount"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="LabelCounter3" Visible="false" CssClass="countTooltip" runat="server" Text=""></asp:Label>
                                    <asp:Image ID="itemIMG" Visible="true" runat="server"  CausesValidation="false" ImageUrl="data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==" />
                                    <%--</div>
                        <div class="form-group">--%>
                                    <br />
                                    <asp:RadioButtonList ID="ansRBL" runat="server" CausesValidation="false" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="נכון" Value="true"></asp:ListItem>
                                        <asp:ListItem Text="לא נכון" Value="false"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <div id="buttons">
                                        <asp:Button ID="itemUpdateButton" CssClass="btn btn-primary" Visible="false" runat="server" Text="עדכון מסיח" OnClick="itemUpdateButton_Click" />
                                        <asp:Button ID="itemCancelButton" CausesValidation="false" CssClass="btn btn-danger" Visible="false" runat="server" Text="מחיקת מסיח" OnClick="itemCancelButton_Click" />
                                        <%--<script language="javascript" type="text/javascript">
                                            function fnConfirmDelete() {
                                                return confirm("Are you sure you want to delete this?");
                                                OnClientClick = "return fnConfirmDelete();"
                                            }
                                        </script>--%>
                                        <asp:Button ID="ItemAddButtonIMG" CssClass="btn btn-primary" runat="server" Text="הוספת מסיח" OnClick="ItemAddButton_Click" />
                                        <asp:Button ID="ItemAddButtonTXT" CssClass="btn btn-primary" runat="server" Text="הוספת מסיח" OnClick="ItemAddButton_Click" />
                                    </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" ControlToValidate="ansRBL" ErrorMessage="נא לסמן את סוג המסיח"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None" ControlToValidate="itemTB" ErrorMessage="נא להוסיף תוכן למסיח"></asp:RequiredFieldValidator>
                                        <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group col-sm-7" style="float: left; direction: rtl">
                            <asp:Label ID="totalAnswers" Text="סך כל הפריטים" runat="server" />
                            <div class="form-group row">
                                <asp:DataList ID="DataListCorrect" RepeatDirection="Horizontal" RepeatColumns="3" RepeatLayout="Flow" runat="server" DataSourceID="XmlDataSource_Correct" OnSelectedIndexChanged="ItemSelect">
                                    <HeaderTemplate>
                                        <asp:Label ID="correctHead" CssClass="tHeaders" Text="פריטים נכונים" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="datalistItem">
                                            <asp:LinkButton ID="lnkSelect" CausesValidation="false" runat="server" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@id")%>' CommandName="Select">
                                                <asp:Label ID="NameLabel" runat="server" Text='<%#XPath("text()")%>'></asp:Label>
                                                <asp:Image ID="ImageQR" Width="90px" Height="90px" runat="server" />
                                            </asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                    <%--                                    <ItemStyle VerticalAlign="Middle" Height="100px" Width="100px" HorizontalAlign="Center" CssClass="datalistItem" />--%>
                                    <SelectedItemStyle BackColor=""></SelectedItemStyle>
                                </asp:DataList>
                                <asp:XmlDataSource ID="XmlDataSource_Correct" runat="server" DataFile="~/XMLFiles/games.xml"></asp:XmlDataSource>
                                <asp:DataList ID="DataListIncorrect" RepeatDirection="Horizontal" RepeatLayout="Flow" runat="server" DataSourceID="XmlDataSource_Incorrect" OnSelectedIndexChanged="ItemSelect" RepeatColumns="3">
                                    <HeaderTemplate>
                                        <asp:Label ID="incorrectHead" CssClass="tHeaders" Text="פריטים לא נכונים" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="datalistItem">
                                            <asp:LinkButton ID="lnkSelect" CausesValidation="false" runat="server" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@id")%>' CommandName="Select">
                                                <asp:Label ID="NameLabel" runat="server" Text='<%#XPath("text()")%>'></asp:Label>
                                                <asp:Image ID="ImageQR" Width="90px" Height="90px" runat="server" />
                                            </asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                    <%--                                    <ItemStyle VerticalAlign="Middle" Height="100px" Width="100px" HorizontalAlign="Center" CssClass="datalistItem" />--%>
                                    <SelectedItemStyle BackColor=""></SelectedItemStyle>
                                </asp:DataList>
                                <asp:XmlDataSource ID="XmlDataSource_Incorrect" runat="server" DataFile="~/XMLFiles/games.xml"></asp:XmlDataSource>
                            </div>
                        </div>
                    </div>
                    <div class="form-group row">
                    </div>
                </div>
                <asp:Panel ID="DeleteConfPopUp" CssClass="PopUpBackground" Visible="false" runat="server">
                    <asp:Panel ID="DeleteConfPopUp2" CssClass="PopUp" runat="server">
                        <asp:Label ID="Label3" runat="server" Text="האם אתה בטוח?"></asp:Label>
                        <!-- כפתור יציאה - יש לשים לב שהוא מפנה בלחיצה לאותה פונקציה של הכפתור יציאה השני -->
                        <asp:Button ID="ExitDeleteConf" CssClass="Exit" runat="server" Text="לא אל תמחק" OnClick="ExitButton_Click" />
                        <asp:Button ID="DeleteGame" CssClass="Exit" runat="server" Text="כן מחק מסיח" OnClick="DeleteQ_Click" />
                        <!-- תווית להצגת הודעה למשתמש -->
                    </asp:Panel>
                </asp:Panel>
            </form>
        </div>
    </div>
</body>
</html>
