<%@ Page Title="" Language="C#" MasterPageFile="~/Creator/CreatorMaster.master" AutoEventWireup="true" CodeFile="SurveyReportPage.aspx.cs" Inherits="Creator_SurveyReportPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <link rel="stylesheet" type="text/css" href="../css/jquery.dataTables.css" />
    <link href="../css/jquery-ui.css" rel="stylesheet" />
    <link href="../css/ReportPageStyle.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.9.1.js"></script>
    <script type="text/javascript" charset="utf8" src="../ScriptS/jquery.dataTables.js"></script>

    <script type="text/javascript" src="../ScriptS/jquery-migrate-1.2.1.min.js"></script>
	<script type="text/javascript" src="../ScriptS/jquery-ui.js"></script>			
	<script type="text/javascript" src="../ScriptS/jquery.selectbox-0.2.min.js"></script>
		<script type="text/javascript" src="../ScriptS/custom.js"></script>

    <script type="text/javascript" src="../ScriptS/google_chart_js.js"></script>
    <script type="text/javascript">
        google.load('visualization', '1', { packages: ['corechart'] });
    </script>


    
    <script>
        $(document).ready(function () {
            //displayDataTable();
        });
        function displayDataTable() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "SurveyReportPage.aspx/BindDatatable",
                data: "{}",
                dataType: "json",
                success: function (data) {
                    var status;
                    var ddlstatusvalue = $('#<%= ddlSurveyStatus.ClientID %> option:selected').val();
                    if (ddlstatusvalue == "Choose") {
                        for (var i = 0; i < data.d.length; i++) {
                            if (data.d[i].surveyStatus < 1)
                                status = "ACTIVE";
                            else
                                status = "EXPIRED";
                            $("#example").append("<tr><td>" + (i + 1) + "</td><td><a onclientclick='drawVisualization1(this.id)' onclick='javascript:OpenPopUp(this.id)'  id='" + data.d[i].surveyID + "' >" + data.d[i].surveyName + "</a></td><td>" + data.d[i].totalResponses + "</td><td>" + data.d[i].creationDate + "</td><td>" + status + "</td></tr>");

                        }
                    }
                    else if (ddlstatusvalue == "ACTIVE") {
                        for (var i = 0; i < data.d.length; i++) {
                            if (data.d[i].surveyStatus < 1)
                                $("#example").append("<tr><td>" + (i + 1) + "</td><td><a onclientclick='drawVisualization1(this.id)' onclick='javascript:OpenPopUp(this.id)'  id='" + data.d[i].surveyID + "' >" + data.d[i].surveyName + "</a></td><td>" + data.d[i].totalResponses + "</td><td>" + data.d[i].creationDate + "</td><td>ACTIVE</td></tr>");
                        }
                    }
                    else {
                        for (var i = 0; i < data.d.length; i++) {
                            if (data.d[i].surveyStatus == 1)
                                $("#example").append("<tr><td>" + (i + 1) + "</td><td><a onclientclick='drawVisualization1(this.id)' onclick='javascript:OpenPopUp(this.id)'  id='" + data.d[i].surveyID + "' >" + data.d[i].surveyName + "</a></td><td>" + data.d[i].totalResponses + "</td><td>" + data.d[i].creationDate + "</td><td>EXPIRED</td></tr>");
                        }
                    }
                    $("#example").dataTable();
                    displayChart();
                }
            });
        }
        function OpenPopUp(id) {
            window.open("GenerateReport.aspx?ID=" + id + "", "mypage", null, null);
        }
        function drawVisualization1(id) {
            '<%Session["surveyID"] = "' + id + '"; %>';
            alert('<%=Session["surveyID"] %>');
        }
    </script>
    <script type="text/javascript">

        function displayChart() {
            $.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                url: 'SurveyReportPage.aspx/DisplayChartData',
                data: '{}',
                success:
                    function (response) {
                        drawVisualization(response.d);
                    }

            });
            drawVisualization(dataValues);
        }
        function drawVisualization(dataValues) {
            var data = new google.visualization.DataTable();
            var chart = document.getElementById("ddlCharts").selectedIndex;
            data.addColumn('string', 'Column Name');
            data.addColumn('number', 'Column Value');


                    for (var i = 0; i < dataValues.length; i++) {
                        data.addRow([dataValues[i].surveyName, dataValues[i].totalResponses]);
                    }
                

                if (chart == 0) {
                    new google.visualization.PieChart(document.getElementById('visualization')).
                        draw(data, { title: "Google Chart demo" });
                }

                if (chart == 1) {
                    new google.visualization.BarChart(document.getElementById('visualization')).
                       draw(data, { title: "Google Chart demo" });
                }

                if (chart == 2) {
                    new google.visualization.LineChart(document.getElementById('visualization')).
                       draw(data, { title: "Google Chart demo" });
                }


            }
     </script>
     <script>
         $(function () {
             $(".date_datepicker").datepicker();
         });
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div id="main">
		<form id="slider-form" action="#" class="main-form" >	
			<div id="book_car_content" class="content-form ">
                <div class="form-block car-type">
					<h4>Select Category</h4>
						<div class="car-type-select">
							<asp:DropDownList ID="ddlCategory"  runat="server" class="select" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
						</div>
				</div>
				<div class="form-block car-type">
					<h4>Select Group</h4>
						<div class="car-type-select">
					          <asp:DropDownList ID="ddlGroup" runat="server" class="select" AutoPostBack="True" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" ></asp:DropDownList>
						</div>
				</div>
                <div class="form-block car-type">
					<h4>Select Status</h4>
		    			<div class="car-type-select">
	           				<asp:DropDownList ID="ddlSurveyStatus" runat="server" class="select" AutoPostBack="True" OnSelectedIndexChanged="ddlSurveyStatus_SelectedIndexChanged" ></asp:DropDownList>
						</div>
				</div>
                <div class="form-block car-type">
					<h4>FROM DATE</h4>
			    		<div class="car-type-select">                           
                             <asp:TextBox ID="txtFromDate" runat="server" class="date_datepicker" AutoPostBack="True" OnTextChanged="txtFromDate_TextChanged" placeholder="Choose Date"></asp:TextBox>
	           				<%--<input type="text" id="txtFromDate" class="date_datepicker">--%>
						</div>
				</div>
                <div class="form-block car-type">
					<h4>TO DATE</h4>
						<div class="car-type-select">
                            <asp:TextBox ID="txtToDate" runat="server" class="date_datepicker" AutoPostBack="True" OnTextChanged="txtToDate_TextChanged" placeholder="Choose Date"></asp:TextBox>
	         				<%--<input type="text" id="txtToDate" class="date_datepicker">--%>
						</div>
                </div>
	        </div>					
			<div class="clear"></div>
		</form>	
    <%--tab control--%>
            		
        <div id="panal" class="widget widget-advertising">
	 		<form class="main-form admin-form" >	
	 			<div class="main_form_navigation">
                   <div id="tabular" class="title-form current"><a href="#">Tabular</a></div>	
		    	   <div id="graphical" class="title-form back"><a href="#">Graphical</a></div>				
			    </div>			
                <div id="tabular_content" class="content-form">
                    <div>
                        <table id="example">
                           <thead>
                                <tr><th class="site_name">Sl.No</th><th>Survey Name </th><th>Total Responses</th><th>Creation Date</th><th>Survey Status</th></tr>
                           </thead>
                           <tbody>  
                           </tbody>
                         </table>
					</div>
                </div>
				<div id="graphical_content" class="content-form hidden">
                  <select class="select" id="ddlCharts" onchange="displayChart()"> 
                    <option  selected="selected" value="0"/>Pie Chart
					<option value="1"/>Barchart
					<option value="2"/>Line Graph
	   			  </select>
                  <div id="visualization" style="width: 600px; height: 350px;">
                  </div>
                 </div>
	</div>
       </div>    
</asp:Content>

