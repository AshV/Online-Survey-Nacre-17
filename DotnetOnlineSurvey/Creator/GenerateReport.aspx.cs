using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using MySql.Data.MySqlClient;
using System.Text;
using BALOnlineSurvey.BL;

public partial class Creator_GenerateReport : System.Web.UI.Page
{
    StringBuilder str = new StringBuilder();
    StringBuilder divHTML = new StringBuilder();
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();
    int counter = 1, c = 1, q = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            MySqlDataReader objGetSurveyQuestions =  BLReport.GetSurveyQuestions(Convert.ToInt32(Request.QueryString["ID"]));
            dt.Load(objGetSurveyQuestions);
            ds.Tables.Add(dt);
            counter = ds.Tables[0].Rows.Count;
            divGraphContainer.InnerHtml = "/n /r";
            for (int i = 1; i <= counter; i++)
            {
                divHTML.Append("<div id='divGraph" + i.ToString() + "' style=width: 600px; height: 350px;></div>");
            }
            divGraphContainer.InnerHtml = divHTML.ToString();
            for (int i = 0; i < counter; i++)
            {
                BindChart(Convert.ToInt32(ds.Tables[0].Rows[i][0]), ds.Tables[0].Rows[i][1].ToString());
                c++;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    private MySqlDataReader GetResoponsesToQuestions(int surveyId, int questionId)
    {

            MySqlDataReader objGetResoponsesToQuestions = BLReport.GetSurveyResponsesToQuestions(surveyId, questionId);
            return objGetResoponsesToQuestions;
    }
    private void BindChart(int questionId, string question)
    {
        DataTable dt = new DataTable();
        try
        {

            MySqlDataReader objBindChart = GetResoponsesToQuestions(1, questionId);
            dt.Load(objBindChart);
            str.Append(@"<script type=text/javascript> google.load( *visualization*, *1*, {packages:[*corechart*]});
                       google.setOnLoadCallback(drawChart);
                       function drawChart() {
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'Answer');
        data.addColumn('number', 'AnswerCount');      

        data.addRows(" + dt.Rows.Count + ");");

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                str.Append("data.setValue( " + i + "," + 0 + "," + "'" + dt.Rows[i]["response"].ToString() + "');");
                str.Append("data.setValue(" + i + "," + 1 + "," + dt.Rows[i]["count"].ToString() + ") ;");
            }

            str.Append(" var chart = new google.visualization.PieChart(document.getElementById('divGraph" + c.ToString() + "'));");
            str.Append(" chart.draw(data, {width: 550, height: 300, title: '" + q + ". " + question + " ',titleTextStyle: {color: 'Orange'},");
            str.Append("hAxis: {title: 'Options', titleTextStyle: {color: 'green'}}");
            str.Append("}); }");
            str.Append("</script>");
            q++;
            lt.Text = str.ToString().TrimEnd(',').Replace('*', '"');
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}