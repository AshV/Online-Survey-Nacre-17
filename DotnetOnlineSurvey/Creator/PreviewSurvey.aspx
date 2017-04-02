<%@ Page Title="" Language="C#" MasterPageFile="~/Creator/CreatorMaster.master" AutoEventWireup="true" CodeFile="PreviewSurvey.aspx.cs" Inherits="Creator_PreviewSurvey" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/CreateTakeA.css" rel="stylesheet" />
    <script src="../AJs/jquery-2.1.0.min.js"></script>
    <script src="../AJs/jquery-ui-1.10.4.min.js"></script>
  <%--  <script type="text/javascript">
        var surveyId = 1;
        var userId = 1;

        $(document).ready(function () {

            //Ranking Code

            //$('input:radio').change(function () {
            //    alert($(this).val());
            //});
            var userRating;
            $(".rating input:radio").attr("checked", false);
            $('.rating input').click(function () {
                $(".rating span").removeClass('checked');
                $(this).parent().addClass('checked');
            });

            $('input:radio').change(
            function () {
                userRating = this.value;
                $(this).parent().parent().children(".rating").html(userRating);
            });



            $("#Submit").click(function () {

                //var i = 0;
                //$(".qBox").each(function () {
                //    if ($(this).attr("data-required") == "Yes") {
                //     i = 0;
                //        if ($(this).attr("data-qType") == "Single") {
                //            $(this).children(":radio").each(function () {
                //                if ($(this).is(":checked")) {
                //                    i++;
                //                }
                //            });
                //        }
                //        if ($(this).attr("data-qType") == "Multiple") {
                //            $(this).children(":checkbox").each(function () {
                //                if ($(this).is(":checked")) {
                //                    i++;
                //                }
                //            });
                //        }
                //        //if (i == 0)
                //        //    alert("please attempt required questions !!!");
                //    }
                //});

                //if (i != 0)
                //    alert("A" + i);

                if (true) {
                    $(".qBox").each(function () {

                        var qId = $(this).attr("id");
                        var res = "";
                        if ($(this).attr("data-qType") == "Single") {
                            $(this).children(":radio").each(function () {
                                if ($(this).is(":checked")) {
                                    res = $(this).val();
                                }
                            });
                        }

                        if ($(this).attr("data-qType") == "Multiple") {
                            $(this).children(":checkbox").each(function () {
                                if ($(this).is(":checked")) {
                                    res += $(this).val() + ",";
                                }
                            });
                        }

                        if ($(this).attr("data-qType") == "Text") {
                            res = $(this).children("textarea").val();
                        }

                        if ($(this).attr("data-qType") == "Ranking") {
                            res = $(this).children().children(".rating").html();
                            //    res = userRating;
                        }

                        if (res != "") {
                            //$.ajax({
                            //    type: "POST",
                            //    url: "../WebMethods/CreateTakeSurvey.asmx/TakeUserResponse",
                            //    data: "{'surveyId':'" + surveyId + "','userId':'" + userId + "','questionId':'" + qId + "','response':'" + res + "'}",
                            //    contentType: "application/json",
                            //    dataType: "JSON",
                            //    success: function (response) {
                            //    },
                            //    failure: function (msg) {
                            //        alert(msg);
                            //    }

                            //});
                            // alert("{'surveyId':'" + surveyId + "','userId':'" + userId + "','questionId':'" + qId + "','response':'" + res + "'}");
                        }
                    });

                    if (confirm("Do you want to submit your feedback?(Y/N)"));
                    alert("This was Preview!");
                    
                }
            });
        });

    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Literal ID="ltAllQuestion" runat="server"></asp:Literal>
    <a href="#" id="Submit" class="myButton" onclick="alert('This was Preview Version')">Submit Feedback!</a>
</asp:Content>

