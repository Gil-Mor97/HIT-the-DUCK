//כאשר העמוד נטען
$(document).ready(function () {

    $(".about").click(function () {
        $("#aboutDiv").toggle();
    });

    $(".howToPlay").click(function () {
        $("#howToPlayDiv").toggle();
    });

    $(".closeAbout").click(function () {
        $("#aboutDiv").hide();
        $("#gameIframe")[0].contentWindow.focus();
    });

    $(".closeHowToPlay").click(function () {
        $("#howToPlayDiv").hide();
        $("#gameIframe")[0].contentWindow.focus();
    });

    $(".CharacterCount").each(function () {
        checkCharacter($(this)); //קריאה לפונקציה שבודקת את מספר התווים
    });

    //בהקלדה בתיבת הטקסט
    $(".CharacterCount").keyup(function () {
        checkCharacter($(this)); //קריאה לפונקציה שבודקת את מספר התווים
    });

    //בהעתקה של תוכן לתיבת הטקסט
    $(".CharacterCount").on("paste", function () {
        checkCharacter($(this));//קריאה לפונקציה שבודקת את מספר התווים
    });


    //פונקציה שמקבלת את תיבת הטקסט שבה מקלידים ובודקת את מספר התווים
    function checkCharacter(myTextBox) {

        //משתנה למספר התווים הנוכחי בתיבת הטקסט
        var countCurrentC = myTextBox.val().length;

        //משתנה המקבל את מספר תיבת הטקסט 
        var itemNumber = myTextBox.attr("item");

        //משתנה המכיל את מספר התווים שמוגבל לתיבה זו
        var CharacterLimitNum = myTextBox.attr("CharacterLimit");

        //בדיקה האם ישנה חריגה במספר התווים
        if (countCurrentC > CharacterLimitNum) {

            //מחיקת התווים המיותרים בתיבה
            myTextBox.val(myTextBox.val().substring(0, CharacterLimitNum));
            //עדכון של מספר התווים הנוכחי
            countCurrentC = CharacterLimitNum;

        }

        //הדפסה כמה תווים הוקלדו מתוך כמה
        $("#LabelCounter" + itemNumber).text(countCurrentC + "/" + CharacterLimitNum);
    }

});


function modalBox() {
    $(function () {
        $("#dialog-confirm").dialog({
            resizable: false,
            height: "auto",
            width: 400,
            modal: true,
            buttons: {
                "Delete all items": function () {
                    $(this).dialog("close");
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });
    });
}

//פעולה שמתרחשת בלחיצה על התמונה הראשונה ופותחת את חלון בחירת התמונה הראשונה
function openFileUploader1() {
    $('#FileUpload1').click();
}


$(document).ready(function () {
    //לאחר שלחצנו על התמונה שרצינו לבחור - תמונה מספר אחד
    $("#FileUpload1").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#itemIMG').attr('src', e.target.result);
                $('#itemIMG').attr('src', e.target.result);
                //document.getElementById("myP").style.visibility = "hidden";
                ShowImg();
            }
            reader.readAsDataURL(this.files[0]);
            document.getElementById('<%=itemTB.ClientID %>').value = " ";
        }
        //call C#
        //var id = '<%= imgUpload2.ClientID%>;
        //$('#' + id).click();
    });
});

function ShowImg() {
    $("#itemTB").css('display', 'none');
    $("#itemIMG").css('display', 'initial');
    $("#LabelCounter3").css('display', 'none');
    $("#ItemAddButton").css('display', 'block');
    $("#ItemAddButtonIMG").css('display', 'block');
    $("#ItemAddButtonTXT").css('display', 'none');
}

function ShowText() {
    $("#itemTB").css('display', 'initial');
    $("#itemIMG").css('display', 'none');
    $("#LabelCounter3").css('display', 'initial');
    $("#ItemAddButtonIMG").css('display', 'none');
    $("#ItemAddButtonTXT").css('display', 'block');
}
