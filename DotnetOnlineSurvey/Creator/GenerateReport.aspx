<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenerateReport.aspx.cs" Inherits="Creator_GenerateReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>

    <script type="text/javascript" charset="utf8" src="../Scripts/jquery-1.9.1.js"></script>

    <%-- Chart tags --%>
    <script type="text/javascript" src="../Scripts/google_chart_js.js"></script>
    <script type="text/javascript">
        google.load('visualization', '1', { packages: ['corechart'] });
    </script>
    <%-- ExportPDF tags --%>
    <script type="text/javascript"
        src="http://canvg.googlecode.com/svn/trunk/canvg.js"></script>
    <script type="text/javascript"
        src="http://canvg.googlecode.com/svn/trunk/rgbcolor.js"></script>
    <script type="text/javascript"
        src="http://canvg.googlecode.com/svn/trunk/StackBlur.js"></script>
    <script src="../Scripts/html2canvas.js"></script>
    <script src="../Scripts/FileSaver.js"></script>
    <script src="../Scripts/jspdf.js"></script>
    <script src="../Scripts/jspdf.plugin.addimage.js"></script>

    <script type="text/javascript">

        function export_PDF(chartContainer, imgContainer) {

            var count = $("#divGraphContainer").children().size();
            //main Div Hide
            var el = document.getElementById('divGraphContainer');
            el.parentNode.removeChild(el);

            //Chart to Image

            for (var i = 0; i < count; i++) {

                var doc = chartContainer.ownerDocument;
                var img = doc.createElement('img');
                img.src = getImgData(chartContainer, i);

                //while (imgContainer.firstChild) {
                //   // imgContainer.removeChild(imgContainer.firstChild);
                //}

                imgContainer.appendChild(img);

                //Pdf Creation
            }


            var divElements = document.getElementById('expotPdfDiv').innerHTML;
            //Get the HTML of whole page
            var oldPage = document.body.innerHTML;

            //Reset the page's HTML with div's HTML only
            document.body.innerHTML =
              "<html><head><title></title></head><body>" + divElements + "</body>";

            //convert whole html page to canvas

            html2canvas(document.body, {
                onrendered: function (canvas) {

                    // canvas is the final rendered <canvas> element

                    var myImage = canvas.toDataURL("image/JPEG").slice('data:image/jpeg;base64,'.length);

                    // Convert the data to binary form
                    myImage = atob(myImage)

                    //new object of jspdf and save image to pdf.
                    var doc = new jsPDF();
                    doc.addImage(myImage, 'JPEG', 0, 0, 300, 300);
                    doc.save('pdfName.pdf');
                }
            });
        }

        function getImgData(chartContainer, i) {

            var chartArea = chartContainer.getElementsByTagName('svg')[i].parentNode;
            var svg = chartArea.innerHTML;
            var doc = chartContainer.ownerDocument;
            var canvas = doc.createElement('canvas');

            canvas.setAttribute('width', chartArea.offsetWidth);
            canvas.setAttribute('height', chartArea.offsetHeight);

            canvas.setAttribute(
                'style',
                'position: absolute; ' +
                'top: ' + (-chartArea.offsetHeight * 2) + 'px;' +
                'left: ' + (-chartArea.offsetWidth * 2) + 'px;');

            doc.body.appendChild(canvas);

            canvg(canvas, svg);

            var imgData = canvas.toDataURL("image/JPEG");
            var data = canvas.toDataURL('image/JPEG').slice('data:image/JPEG;base64,'.length);

            // Convert the data to binary form
            data = atob(data)


            canvas.parentNode.removeChild(canvas);
            return imgData;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblError" ForeColor="Red" runat="server" Text=""></asp:Label>
        </div>
        <div id="expotPdfDiv">
            <div>
                <asp:Literal ID="lt" runat="server"></asp:Literal>
            </div>
            <div id="divGraphContainer" runat="server"></div>
            <%--<div id="visualization1" style="width: 600px; height: 350px;">
    </div>--%>
            <div id="chart_image" style="width: 600px; height: 350px;"></div>
        </div>
        <button onclick=" export_PDF(document.getElementById('divGraphContainer'), document.getElementById('chart_image')); ">Export PDF</button>
    </form>
</body>
</html>
