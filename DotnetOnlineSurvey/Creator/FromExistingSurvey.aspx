<%@ Page Title="" Language="C#" MasterPageFile="~/Creator/CreatorMaster.master" AutoEventWireup="true" CodeFile="FromExistingSurvey.aspx.cs" Inherits="Creator_FromExistingSurvey" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script src="../AJs/mootools-core-1.4.5-nocompat.js"></script>
    <script src="../AJs/jquery-2.1.0.min.js"></script>
    <link href="../css/CreateTakeA.css" rel="stylesheet" />
    <link href="../css/CreateTakeY.css" rel="stylesheet" />
    <script src="../AJs/bopoup.js"></script>
    <script src="../AJs/jquery-ui-1.10.4.min.js"></script>


    <script type="text/javascript">
        //Code for Uploading & checking Excel file in Databse from Popup
        var categoryId = 1;
        var creatorID = '<%=Session["HomeCreatorId"]%>';
        var surveyID = '<%=Request.QueryString["surveyId"]%>';
        var newSurveyId = '<%=Request.QueryString["newSurveyId"]%>';

        window.addEvent('load', function () {
            var handleFileSelect = function (evt) {
                var files = evt.target.files;
                var file = files[0];

                var sFileName = file.name;
                var sFileExtension = sFileName.split('.')[sFileName.split('.').length - 1].toLowerCase();

                if (!(sFileExtension == 'xls' || sFileExtension == 'xlsx'))
                    alert('File must be an Excel Spreadsheet with .xls/.xlsx extension!');

                if (files && file && (sFileExtension == 'xls' || sFileExtension == 'xlsx')) {

                    var reader = new FileReader();

                    reader.onload = function (readerEvt) {
                        var binaryString = readerEvt.target.result;
                        //    alert(btoa(binaryString));

                        $.ajax({
                            type: "POST",
                            url: "../WebMethods/CreateTakeSurvey.asmx/GetExcelQuestions",
                            data: "{'base64FileString':'" + btoa(binaryString) + "', 'fileExtension':'" + sFileExtension + "'}",
                            contentType: "application/json",
                            dataType: "JSON",
                            success: function (response) {
                                $("#sortable").append(response.d);
                            },
                            failure: function (msg) {
                                alert(msg);
                            }
                        });
                        $("#Add_From_Excel").bPopup().close();
                    };
                    reader.readAsBinaryString(file);
                }
            };

            if (window.File && window.FileReader && window.FileList && window.Blob) {
                document.getElementById('filePicker').addEventListener('change', handleFileSelect, false);
            }
            else {
                alert('The File APIs are not fully supported in this browser.');
            }
        });


        jQuery(document).ready(function () {

            /////
            //For Adding Questions Manually
            //Add Question by type popup
            $(function () {
                $('#addQuestion').click(function () {
                    Answers = 2;
                    $("textarea[id$='question_question']").val("");
                    $("#q_text").hide();
                    $("#Answer_Option").empty();
                    $("#QuesType_dropDown").val(0);
                    $('#popupcreateques').bPopup({
                        easing: 'easeOutBack',
                        speed: 450,
                        transition: 'slideDown'
                    });
                });
            });

            //For Close Button beside Option Text Box
            function CloseClick(e) {
                alert($(e).html());
                $(e).closest('div').remove();
            }

            //To Limit No. of Options only upto 8
            //Ashish
            var Answers;
            $("#QuesType_dropDown").change(function () {
                $("#q_text").show();
                var var1 = $("#QuesType_dropDown option:selected").val();
                if (var1 == 1 || var1 == 2) {
                    $("#Answers_div").show();
                    $("#addanswer").show();
                    Answers = 2;
                    $("#Answer_Option").empty();
                    $("#Answer_Option").append('<div><input class="Answer_Option" id="Answer_Option1" style="float:left;width:70%" type="text" placeholder="Enter Your Text" /><input onclick="javascript:CloseClick(this);" style="float:right" type="button" value="Close" /><br/></div>');
                    $("#Answer_Option").append('<div><input class="Answer_Option" id="Answer_Option2" style="float:left;width:70%" type="text" placeholder="Enter Your Text" /><input onclick="javascript:CloseClick(this);" style="float:right" type="button" value="Close" /><br/></div>');
                }
                else if (var1 == 4) {
                    $("#Answers_div").show();
                    $("#addanswer").hide();
                    $("#Answer_Option").empty();
                    $("#Answer_Option").append("<input class='Answer_Option' type='text' placeholder='Enter Minimum Value' /><br/><input class='Answer_Option' type='text' placeholder='Enter Minimum Value' /><br/><input class='Answer_Option' type='text' placeholder='Enter Minimum Value' /><br/>");
                }
                else {
                    Answers = 0;
                    $("#Answers_div").hide();
                }
            });

            //add answer textbox
            $(document).ready(function () {

                $("#btnPopupSubmit").click(function () {
                    var i = 0, opt = "", ctype = "", typeDiv = "";
                    if ($("#QuesType_dropDown option:selected").val() == 1) {
                        ctype = "<input type='radio' name='" + i + "' value='";
                        typeDiv = "Single";
                    }
                    if ($("#QuesType_dropDown option:selected").val() == 2) {
                        ctype = "<input type='checkbox' name='" + i + " value='";
                        typeDiv = "Multiple";
                    }
                    if ($("#QuesType_dropDown option:selected").val() == 3) {
                        opt = "<br/><textarea></textarea>";
                        typeDiv = "Text";
                    }
                    if ($("#QuesType_dropDown option:selected").val() == 4) {
                        typeDiv = "Ranking";
                    }

                    var RankInput = 0;
                    $(".Answer_Option").each(function () {
                        RankInput++;
                        if (typeDiv != "Ranking") {
                            if ($(this).val() != "")
                                opt += "<br/>" + ctype + $(this).val() + "'/>" + $(this).val();
                        }
                        else {
                            switch (RankInput) {
                                case 1:
                                    opt += "<input type='range' /><br/>Min=<span class='min' >" + $(this).val() + "</span>";
                                    break;
                                case 2:
                                    opt += " Max=<span class='max' >" + $(this).val() + "</span>";
                                    break;
                                case 3:
                                    opt += " Interval=<span class='int' >" + $(this).val() + "</span>";
                                    break;
                            }
                        }
                    });
                    if ($("textarea[id$='question_question']").val() != "") {
                        $('#sortable').append('<div data-qType="' + typeDiv + '" class="qBox" id="questionDiv' + i + '"><br/><input class="required" type="checkbox" value="' + i + '" name="required" />Mark As Required<br/><img class="btnEdit" src="Edit.png" style="float:right;right:0px;height:50px;width:50px" /><img src="Delete.png" class="btnDelete" style="float:right;right:0px;height:50px;width:50px" /><span class="qText">' + $("textarea[id$='question_question']").val() + '</span>' + opt + '</div>');
                        i++;
                        $('#popupcreateques').bPopup().close();
                    }
                    else
                        alert("Enter Question!!!!!");
                });


                //adding more option
                $("#addanswer").click(function () {
                    if (Answers < 8) {
                        Answers++;
                        //        alert(Answers);
                        $("#Answer_Option").append('<div><input class="Answer_Option" id="Answer_Option' + Answers + '" style="float:left;width:70%" type="text" placeholder="Enter Your Text" /><input onclick="javascript:CloseClick(this);" style="float:right" type="button" value="Close" /><br/></div>');
                    }
                    else
                        alert("More than 8 Options are not allowed!");
                });
            });





            /////For Updating Single Question 
            var _DivId = "";
            var oldQ = "";
            var newQ = "";
            var fullText = "";
            var newText = "";
            $('#sortable').on('click', '.qBox .btnEdit', function () {
                $("#qTypeAndTextEdit").bPopup({
                    easing: 'easeOutBack',
                    speed: 450,
                    transition: 'slideDown'
                });
                _DivId = $(this).parent().attr("id");
                oldQ = $(this).parent().children(".qText").html();
                $("#qTypeAndTextEdit").children("#quesName").html(oldQ);
            });

            $("#btnUpdateQuestion").click(function () {
                newQ = $("textarea[id$='quesName']").val();
                fullText = $("#" + _DivId).html();
                newText = fullText.replace(oldQ, newQ);
                $("#" + _DivId).html(newText);
                $("#qTypeAndTextEdit").bPopup().close();
            });



            ////For Updating Single Question Div
            //var _DivId = "";
            //var _questionType = "";
            //var _question = "";
            //var _required = false;
            //var _minVal = 0;
            //var _maxVal = 0;
            //var _intrval = 0;
            //var _options = new Array();
            //var count = 0;

            //$('#sortable').on('click', '.qBox .btnEdit', function () {
            //    _DivId = $(this).parent().attr("id");
            //    _questionType = $(this).parent().attr("data-qType");
            //    _question = $(this).parent().children(".qText").html();
            //    _required = $(this).parent().children(".required").is(":checked");
            //    alert(_question);
            //    //var _minVal = 0;
            //    //var _maxVal = 0;
            //    //var _intrval = 0;
            //    //for (var i = 1; i <= 8; i++) {
            //    //    _options[i] = "";
            //    //}
            //    //if (_questionType == "Single") {
            //    //    count = 1;
            //    //    $(this).parent().children(":radio").each(function () {
            //    //        if ($(this).attr("value") != "") {
            //    //            _options[count] = $(this).attr("value");
            //    //            count++;
            //    //        }
            //    //    });
            //    //}
            //    //if (_questionType == "Multiple") {
            //    //    count = 1;
            //    //    $(this).parent().children(":checkbox").each(function () {
            //    //        if ($(this).val() != 'required' && $(this).val() != "") {
            //    //            _options[count] = $(this).attr("value");
            //    //            count++;
            //    //        }
            //    //    });
            //    //}
            //    //alert(_options[1] + " 1  " + _options[2] + " 2  " + _options[3]);
            //    alert($("#qTypeAndText").html());
            //    $("#qTypeAndText").appendTo($("#Edit_Question_Div"));
            //    $("#qTypeAndText").children("textarea").html(_question);
            //    $("#Edit_Question_Div").bPopup();
            //});


            //Updating Qustion Div from Popup
            $("#btnUpdateQuestion").click(function () {
                var newQ = $("textarea[id$='quesName']").val();
                var oldQ = $("#" + _DivId).children(".qText").html();
                var fullText = $("#" + _DivId).html();
                var newfullText = fullText.replace(oldQ, newQ);
                $("#" + _DivId).html(newfullText);
                $("#Edit_Question_Div").bPopup().close();
            });

            //Edit Type Radio Button Changed
            $(".qt").change(function () {
                _questionType = $('input:radio[name=qtype]:checked').val();
            });

            //Code for UpdateQuestion to update into Database
            $("#updateSurveyToDb").click(function () {
                var questionSrno = 0;
                $(".qBox").each(function () {
                    questionSrno++;
                    var question = $(this).children(".qText").text();
                    var questionType = $(this).attr("data-qType");
                    var questionTypeId = "";
                    var required = "";
                    var minVal = "0";
                    var maxVal = "0";
                    var intrval = "0";
                    var options = ",";
                    switch (questionType) {
                        case 'Single':
                            questionTypeId = 1;
                            break;
                        case 'Multiple':
                            questionTypeId = 2;
                            break;
                        case 'Text':
                            questionTypeId = 3;
                            break;
                        case 'Ranking':
                            questionTypeId = 4;
                    }
                    if ($(this).attr("data-qType") == "Single") {
                        if ($(this).children(":checkbox").is(":checked")) {
                            required = "true";
                        }
                        else {
                            required = "false";
                        }
                        $(this).children(":radio").each(function () {
                            options += $(this).val() + ",";
                        });
                    }
                    else if ($(this).attr("data-qType") == "Multiple") {
                        $(this).children(":checkbox").each(function () {
                            if ($(this).val() == 'required') {
                                if ($(this).is(":checked")) {
                                    required = "true";
                                }
                                else {
                                    required = "false";
                                }
                            }
                            if ($(this).val() != 'required') {
                                options += $(this).val() + ",";
                            }
                        });
                    }
                    else if ($(this).attr("data-qType") == "Text") {
                        if ($(this).children(":checkbox").is(":checked")) {
                            required = "true";
                        }
                        else {
                            required = "false";
                        }
                    }
                    else if ($(this).attr("data-qType") == "Ranking") {
                        if ($(this).children(":checkbox").is(":checked")) {
                            required = "true";
                        }
                        else {
                            required = "false";
                        }
                        $(this).children("span").each(function () {
                            switch ($(this).attr("class")) {
                                case 'min':
                                    minVal = $(this).text();
                                    break;
                                case 'max':
                                    maxVal = $(this).text();
                                    break;
                                case 'int':
                                    intrval = $(this).text();
                                    break;
                            }
                        });
                    }

                    //Calling WebMethod for addin One by one Qusetion to Database
                    if (true) {
                        $.ajax({
                            type: "POST",
                            url: "../WebMethods/CreateTakeSurvey.asmx/InsertQuestion",
                            data: "{'categoryID':'" + categoryId + "','creatorID':'" + creatorID + "','surveyID':'" + newSurveyId + "','questionTypeId':'" + questionTypeId + "','questionSrNo':'" + questionSrno + "','question':'" + question + "','required':'" + required + "','minVal':'" + minVal + "','maxVal':'" + maxVal + "','intrval':'" + intrval + "','options':'" + options + "'}",
                            contentType: "application/json",
                            dataType: "JSON",
                            success: function (response) {
                            },
                            failure: function (msg) {
                                alert(msg);
                            }
                        });
                    }
                    // alert("{'categoryID':'" + categoryId + "','creatorID':'" + creatorID + "','surveyID':'" + surveyID + "','questionTypeId':'" + questionTypeId + "','questionSrNo':'" + questionSrno + "','question':'" + question + "','required':'" + required + "','minVal':'" + minVal + "','maxVal':'" + maxVal + "','intrval':'" + intrval + "','options':'" + options + "'}");
                });
            });
            //End



            //Retrving and Showing Questions in Div According To category
            $("#addFromDB").click(function () {
                $('#Add_From_Database').empty();
                $.ajax({
                    type: "POST",
                    url: "../WebMethods/CreateTakeSurvey.asmx/GetCategoryWiseQuestions",
                    data: "{'categoryId':'" + categoryId + "'}",
                    contentType: "application/json",
                    dataType: "JSON",
                    success: function (response) {
                        $('#Add_From_Database').empty();
                        $("#Add_From_Database").append("<span class='button b-close'><span>X</span></span><a href='#' id='Add_QuesFrom_DB' class='myButton'>Add Selected To Survey!</a>");
                        $("#Add_From_Database").append(response.d);
                        $("#Add_From_Database").append("<a href='#' id='Add_QuesFrom_DB' class='myButton'>Add Selected To Survey!</a>");
                        $("#Add_From_Database").bPopup({
                            follow: [false, false],
                            position: [400, 100]
                        });
                    },
                    failure: function (msg) {
                        alert(msg);
                    }
                });
            });
            //End
            //Adding Selected Questions from Div to Page
            $('#Add_From_Database').on('click', '#Add_QuesFrom_DB', function () {
                $("#tempContainer").empty();
                $("#Add_From_Database").children(".selectQuestion").each(function () {
                    if ($(this).is(":checked"))
                        $("#tempContainer").append($(this).next(".qBox")).html();
                });
                $("#sortable").append($("#tempContainer").html());
                $("#tempContainer").empty();
                $("#Add_From_Database").bPopup().close();
            });
            //end

            //Add From Excel Popup Code
            $("#addFromExcel").click(function () {
                $("#Add_From_Excel").bPopup();
            });

            //Update Survey Name
            jQuery("#updateName").click(function () {
                $("#divUpdateName").bPopup();
                $("#txtSurveyName").val($("#<%=Survey_Title.ClientID%>").text());
            });
            $("#btnUpdateName").click(function () {
                jQuery("#<%=Survey_Title.ClientID%>").text($("#txtSurveyName").val());
                $("#divUpdateName").bPopup().close();
            });
            //End

            //For Sorting qBox (question Container) Divs
            $("#sortable").sortable();
            $("#sortable").disableSelection();
            //End

            //For Delete Image Button in qBox Div
            $('#sortable').on('click', '.qBox .btnDelete', function () {
                $(this).parent('div').remove();
            });
            //End
        });

    </script>
    <h1>
        <asp:Label ID="Survey_Title" runat="server">Ashish</asp:Label>
        <a href="#" id="updateName">Update Name</a>
    </h1>
    <div>
     <a href="#" id="addQuestion" class="myButton">Add Question Manually</a>
     <a href="#" id="addFromExcel" class="myButton">Add Question From Excel File</a>
     <a href="#" id="addFromDB" class="myButton">Add Question From Question Bank</a>
    </div>
    <!--Main Container for All questions-->
     <ul id="sortable">
         <asp:Literal ID="ltlAllQuestions" runat="server"></asp:Literal>
    </ul>
    <!--button to update Questions to Db-->
    <a href="#" id="updateSurveyToDb" class="myButton">Save Survey</a>

    <!--Popup To Update Survey Name-->
    <div id="divUpdateName" style="display:none">
        <span class="button b-close"><span>X</span></span>
        <input type="text" id="txtSurveyName" /><br />
        <a href="#" id="btnUpdateName">Update Name</a>
    </div>

    <!--QuestionTypeText Div inside Edit Question Div-->
    <div id="qTypeAndTextEdit"  style="display:none">      
         <span class="button b-close"><span>X</span></span>
        ashish
        <textarea id="quesName"></textarea>
        <br />
        <a href="#" id="btnUpdateQuestion">Update Question</a>
    </div>

    <!-- Add from Excel File Div -->
    <div id="Add_From_Excel" style="display:none">
        <span class="button b-close"><span>X</span></span>
        <label for="filePicker">Select an Excel File with appropriate format:</label><br/>
        <input type="file" id="filePicker" />
    </div>

    <!--Add Question Popup-->
    <div id="popupcreateques" style="display: none">
        <span class="button b-close"><span>X</span></span>
            <h2>Create Question</h2>
            <div id="q_type" style="padding: 2px 2px 2px 2px; margin-bottom: 4px">
                Select Question Type: 
                <select name="question[type]" id="QuesType_dropDown" size="1" onchange="javascript:OnChange();" >
                    <option value="0">Select question type...</option>
                    <option value="1">Single Choice</option>
                    <option value="2">Multiple Choice</option>
                    <option value="3">Descriptive</option>
                    <option value="4">Rank</option>
                </select>
                <!--<a href="/survey/help/question_types" class="small">Learn more about our question types...</a>-->

            </div>
            <div id="q_text" style="padding: 2px 2px 2px 2px;display:none" >
                Enter Question Text:<br />
                <textarea class="manual_editor" id='question_question' name="question[question]" rows="5" cols="60"></textarea>
            
            
            <div   id="Answers_div" style="display:none">
                <input type="button" id="addanswer" value="Add Answer" title="Add Answer" />
               <div id="Answer_Option">
            <%--  <div><input id="Answer_Option1" style="float:left;width:70%" type="text" placeholder="Enter Your Text" /><input onclick="javascript: CloseClick(this);" style="float:right" type="button" value="Close" /><br/></div> 
              <div><input id="Answer_Option2" style="float:left;width:70%" type="text" placeholder="Enter Your Text" /><input onclick="javascript: CloseClick(this);" style="float:right" type="button" value="Close" /><br/></div>
            --%></div></div>
        <input id="btnPopupSubmit" type="button" value="Save" />
                </div>
</div>

    <!--Edit Question Div-->
    <div id="Edit_Question_Div" style="display:none">
        <span class="button b-close"><span>X</span></span>
    </div>

     <!-- Add From Database Div -->
     <div id="Add_From_Database" style="display:none">   
     </div>

    <!--Temporary Question Container for Database Questions Before adding to Page-->
    <a id="tempContainer" style="display:none"></a> 

</asp:Content>


