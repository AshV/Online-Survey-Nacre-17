<%@ Page Title="" Language="C#" MasterPageFile="~/Creator/ProfileManagementMaster.master" AutoEventWireup="true" CodeFile="CreatorHomePage.aspx.cs" Inherits="Creator_CreatorHomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="nestedcontent" Runat="Server">
       <script src="../Scripts/jquery-1.9.1.js"></script>
       <script type="text/javascript" charset="utf8" src="../Scripts/jquery.dataTables.js"></script>
    <link href="../css/ReportPageStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery-migrate-1.2.1.min.js"></script>
	<script type="text/javascript" src="../Scripts/jquery-ui.js"></script>			
	<script type="text/javascript" src="../Scripts/jquery.selectbox-0.2.min.js"></script>
		<script type="text/javascript" src="../Scripts/custom.js"></script>
    <script type="text/javascript" src="../Scripts/google_chart_js.js"></script>
    <script type="text/javascript">
        google.load('visualization', '1', { packages: ['corechart'] });
    </script>
     <script type="text/javascript">
         $(document).ready(function () {
             displayChart();
         });
         function displayChart() {
             $.ajax({
                 type: 'POST',
                 dataType: 'json',
                 contentType: 'application/json',
                 url: 'CreatorHomePage.aspx/DisplayChartData',
                 data: '{}',
                 success:
                     function (response) {
                         drawVisualization(response.d);
                     }

             });
         }
         function drawVisualization(dataValues) {
             var data = new google.visualization.DataTable();
             var chart = document.getElementById("ddlCharts").selectedIndex;
             data.addColumn('string', 'Column Name');
             data.addColumn('number', 'Column Value');

             for (var i = 0; i < dataValues.length; i++) {
                 data.addRow([dataValues[i].surveyName, dataValues[i].totalResponses]);
             } 
             if (chart == 1||chart==0) {
                 new google.visualization.PieChart(document.getElementById('visualization')).
                     draw(data, { title: "Google Chart demo" });
             }

             if (chart == 2) {
                 new google.visualization.BarChart(document.getElementById('visualization')).
                    draw(data, { title: "Google Chart demo" });
             }

             if (chart == 3) {
                 new google.visualization.LineChart(document.getElementById('visualization')).
                    draw(data, { title: "Google Chart demo" });
             }
        }
     </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <div>
                <div>
                    <asp:Label ID="lblError" ForeColor="Red" runat="server" Text=""></asp:Label>
                </div>
                <div style="text-align:right">
                Welcome <asp:Label ID="lblCreatorName" runat="server" Text=""></asp:Label>
                </div>
                    <div style="margin-left:70px;"><select class="select" id="ddlCharts" onchange="displayChart()">
                        <option selected="selected" value="0" />Choose Option 
                        <option value="1"/>Pie Chart
					    <option value="2"/>Barchart
					    <option value="3"/>Line Graph
	   			    </select></div>
                <div id="visualization" style="width: 600px; height: 350px;"></div>
            </div>

       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

