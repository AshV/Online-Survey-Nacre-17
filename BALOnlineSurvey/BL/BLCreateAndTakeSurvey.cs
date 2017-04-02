using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALOnlineSurvey;
using BALOnlineSurvey.BE;
using MySql.Data.MySqlClient;
using System.Data;

namespace BALOnlineSurvey.BL
{
    public class BLCreateAndTakeSurvey
    {
        public void surveyStarted(string uniqueId)
        {
            try
            {
                ConnectionFactory.ExecuteStoredProcedure("sp_surveyStarted", new MySqlParameter("_uniqueId", uniqueId));
            }
            catch { throw; }
        }

        public BE_Survey GetSurveyDetailsById(int surveyId)
        {
            try
            {
                MySqlDataReader objMySqlDataReader = ConnectionFactory.ExecuteStoredProcedure("sp_GetSurveyDetails", new MySqlParameter("_surveyId", surveyId));
                objMySqlDataReader.Read();
                return new BE_Survey { SurveyId = Convert.ToInt32(objMySqlDataReader[0]), CategoryId = Convert.ToInt32(objMySqlDataReader[1]), Introduction = objMySqlDataReader[2].ToString(), SurveyCreatedDate = Convert.ToDateTime(objMySqlDataReader[3]), SurveyCreatorId = Convert.ToInt32(objMySqlDataReader[4]), SurveyExpiryDate = Convert.ToDateTime(objMySqlDataReader[5]), SurevyLogo = objMySqlDataReader[6].ToString(), Title = objMySqlDataReader[7].ToString() };
            }
            catch { throw; }
        }

        public string GetQuestionListAccordingToSurvey(int surveyID)
        {
            try
            {
                BLGenerateQuestionList objBLGenerateQuestionList = new BLGenerateQuestionList();
                string surveyQuestionHtml = "";

                MySqlParameter[] objMySqlParameter = { new MySqlParameter("_surveyID", surveyID) };
                MySqlDataReader objMySqlDataReader = ConnectionFactory.ExecuteCommand("sp_GetAllQuestionAccordingTosurvey", CommandType.StoredProcedure, objMySqlParameter);
                int QuestionSrNo = 0;
                string[] Options = new string[8];
                string Question = "";
                string Required = "";
                int minVal = 0;
                int maxVal = 0;
                int intrval = 0;
                while (objMySqlDataReader.Read())
                {
                    QuestionSrNo++;
                    Question = objMySqlDataReader[1].ToString();
                    Required = objMySqlDataReader[2].ToString();
                    minVal = Convert.ToInt32(objMySqlDataReader[3]);
                    maxVal = Convert.ToInt32(objMySqlDataReader[4]);
                    intrval = Convert.ToInt32(objMySqlDataReader[5]);
                    Options[0] = objMySqlDataReader[6].ToString();
                    Options[1] = objMySqlDataReader[7].ToString();
                    Options[2] = objMySqlDataReader[8].ToString();
                    Options[3] = objMySqlDataReader[9].ToString();
                    Options[4] = objMySqlDataReader[10].ToString();
                    Options[5] = objMySqlDataReader[11].ToString();
                    Options[6] = objMySqlDataReader[12].ToString();
                    Options[7] = objMySqlDataReader[13].ToString();

                    switch (Convert.ToInt32(objMySqlDataReader[0]))
                    {
                        case 1:
                            surveyQuestionHtml += objBLGenerateQuestionList.Single(QuestionSrNo, Question, Options, Required);
                            break;
                        case 2:
                            surveyQuestionHtml += objBLGenerateQuestionList.Multiple(QuestionSrNo, Question, Options, Required);
                            break;
                        case 3:
                            surveyQuestionHtml += objBLGenerateQuestionList.Text(QuestionSrNo, Question, Required);
                            break;
                        case 4:
                            surveyQuestionHtml += objBLGenerateQuestionList.Ranking(QuestionSrNo, Question, minVal, maxVal, intrval, Required);
                            break;
                    }
                }
                return surveyQuestionHtml;
            }
            catch { throw; }
        }
        
    }

    public class BLGenerateQuestionList
    {
        string questionText;
        int time;
        public string Single(int QuestionNo, string Question, string[] Options, string Required)
        {
            try
            {
                questionText = "";
                time = DateTime.Now.Millisecond;
                questionText = "<div id='" + DateTime.Now.Millisecond + QuestionNo + "' class='qBox' data-qType='Single'><br/><input type='checkbox' class='required' name='required' value='required' " + Required + " />Mark as Required<br/><img src='../images/Delete.png' class='btnDelete' style='float:right;right:0px;height:50px;width:50px' /><img class='btnEdit' src='../images/Edit.png' style='float:right;right:0px;height:50px;width:50px' /><b>Question : </b><span class='qText'>" + Question + "</span>";
                foreach (string opt in Options)
                {
                    if (opt != "")
                        questionText += "<br /><input type='radio' name='" + time + QuestionNo + "' value='" + opt + "' />" + opt;
                }
                return questionText + "</div>";
            }
            catch { throw; }
        }

        public string Multiple(int QuestionNo, string Question, string[] Options, string Required)
        {
            try
            {
                questionText = "";
                time = DateTime.Now.Millisecond;
                questionText = "<div id='" + time + QuestionNo + "' class='qBox' data-qType='Multiple'><br/><input type='checkbox' class='required' name='required' value='required' " + Required + " />Mark as Required<br/><img src='../images/Delete.png' class='btnDelete' style='float:right;right:0px;height:50px;width:50px' /><img class='btnEdit' src='../images/Edit.png' style='float:right;right:0px;height:50px;width:50px' /><b>Question : </b><span class='qText'>" + Question + "</span>";
                foreach (string opt in Options)
                {
                    if (opt != "")
                        questionText += "<br /><input type='checkbox' name='" + time + QuestionNo + "' value='" + opt + "' />" + opt;
                }
                return questionText + "</div>";
            }
            catch
            {
                throw;
            }
        }

        public string Text(int QuestionNo, string Question, string Required)
        {
            try
            {
                questionText = "";
                questionText = "<div id='" + DateTime.Now.Millisecond + QuestionNo + "' class='qBox' data-qType='Text'><br/><input type='checkbox' class='required' name='required' value='required' " + Required + " />Mark as Required<br/><img src='../images/Delete.png' class='btnDelete' style='float:right;right:0px;height:50px;width:50px' /><img class='btnEdit' src='../images/Edit.png' style='float:right;right:0px;height:50px;width:50px' /><b>Question : </b><span class='qText'>" + Question + "</span>";
                return questionText + "<br/><textarea></textarea></div>";
            }
            catch { throw; }
        }

        public string Ranking(int QuestionNo, string Question, int MinVal, int MaxVal, int Intrval, string Required)
        {
            try
            {
                questionText = "";
                time = DateTime.Now.Millisecond;
                questionText = "<div id='" + DateTime.Now.Millisecond + QuestionNo + "' class='qBox' data-qType='Ranking'><br/><input type='checkbox' class='required' name='required' value='required' " + Required + " />Mark as Required<br/><img src='Delete.png' class='btnDelete' style='float:right;right:0px;height:50px;width:50px' /><img class='btnEdit' src='../images/Edit.png' style='float:right;right:0px;height:50px;width:50px' /><b>Question : </b><span class='qText'>" + Question + "</span><div class='rating'></div>";
                questionText += "<input type='range' /><br>Min=<span class='min'>" + MinVal + "  </span>Max=<span class='max'>" + MaxVal + "</span>Interval=<span class='int'>" + Intrval + "</span>";
                return questionText + "</div>";
            }
            catch { throw; }
        }
    }




    public class BLHtmlControlCreator
    {
        public string GetHtml(int QuestionTypeId, int QuestionId, bool Required, string Question, int Min, int Max, int Intrval, string Option1, string Option2, string Option3, string Option4, string Option5, string Option6, string Option7, string Option8)
        {
            string QuestionHtml = "";
            switch (QuestionTypeId)
            {
                case 1:
                    QuestionHtml = RadioButtons(QuestionId, Required, Question, Option1, Option2, Option3, Option4, Option5, Option6, Option7, Option8);
                    break;
                case 2:
                    QuestionHtml = CheckBoxes(QuestionId, Required, Question, Option1, Option2, Option3, Option4, Option5, Option6, Option7, Option8);
                    break;
                case 3:
                    QuestionHtml = Textbox(QuestionId, Required, Question);
                    break;
                case 4:
                    QuestionHtml = Ranking(QuestionId, Required, Question, Min, Max, Intrval);
                    break;
            }
            return QuestionHtml;
        }

        public string RadioButtons(int QuestionId, bool Required, string Question, string Option1, string Option2, string Option3, string Option4, string Option5, string Option6, string Option7, string Option8)
        {
            string rbRequired = Required ? "data-required='Yes'" : "data-required='No'";
            string star = Required ? "*  " : "";
            string QuestionRadioButton = "<div id='" + QuestionId + "' class='qBox' data-qType='Single' " + rbRequired + "><b>Question : </b><span class='qText'>" + star + Question + "</span>";

            if (Option1 != "")
                QuestionRadioButton += "<br /><input type='radio' name='opt" + QuestionId + "' value='" + Option1 + "' />" + Option1;
            if (Option2 != "")
                QuestionRadioButton += "<br /><input type='radio' name='opt" + QuestionId + "' value='" + Option2 + "' />" + Option2;
            if (Option3 != "")
                QuestionRadioButton += "<br /><input type='radio' name='opt" + QuestionId + "' value='" + Option3 + "' />" + Option3;
            if (Option4 != "")
                QuestionRadioButton += "<br /><input type='radio' name='opt" + QuestionId + "' value='" + Option4 + "' />" + Option4;
            if (Option5 != "")
                QuestionRadioButton += "<br /><input type='radio' name='opt" + QuestionId + "' value='" + Option5 + "' />" + Option5;
            if (Option6 != "")
                QuestionRadioButton += "<br /><input type='radio' name='opt" + QuestionId + "' value='" + Option6 + "' />" + Option6;
            if (Option7 != "")
                QuestionRadioButton += "<br /><input type='radio' name='opt" + QuestionId + "' value='" + Option7 + "' />" + Option7;
            if (Option8 != "")
                QuestionRadioButton += "<br /><input type='radio' name='opt" + QuestionId + "' value='" + Option8 + "' />" + Option8;
            return QuestionRadioButton + "</div>";
        }

        public string CheckBoxes(int QuestionId, bool Required, string Question, string Option1, string Option2, string Option3, string Option4, string Option5, string Option6, string Option7, string Option8)
        {
            string cbRequired = Required ? "data-required='Yes'" : "data-required='No'";
            string star = Required ? "*  " : "";
            string QuestionCheckBox = "<div id='" + QuestionId + "' class='qBox'  data-qType='Multiple' " + cbRequired + "><b>Question : </b><span class='qText'>" + star + Question + "</span>";

            if (Option1 != "")
                QuestionCheckBox += "<br /><input type='checkbox' name='opt" + QuestionId + "' value='" + Option1 + "' />" + Option1;
            if (Option2 != "")
                QuestionCheckBox += "<br /><input type='checkbox' name='opt" + QuestionId + "' value='" + Option2 + "' />" + Option2;
            if (Option3 != "")
                QuestionCheckBox += "<br /><input type='checkbox' name='opt" + QuestionId + "' value='" + Option3 + "' />" + Option3;
            if (Option4 != "")
                QuestionCheckBox += "<br /><input type='checkbox' name='opt" + QuestionId + "' value='" + Option4 + "' />" + Option4;
            if (Option5 != "")
                QuestionCheckBox += "<br /><input type='checkbox' name='opt" + QuestionId + "' value='" + Option5 + "' />" + Option5;
            if (Option6 != "")
                QuestionCheckBox += "<br /><input type='checkbox' name='opt" + QuestionId + "' value='" + Option6 + "' />" + Option6;
            if (Option7 != "")
                QuestionCheckBox += "<br /><input type='checkbox' name='opt" + QuestionId + "' value='" + Option7 + "' />" + Option7;
            if (Option8 != "")
                QuestionCheckBox += "<br /><input type='checkbox' name='opt" + QuestionId + "' value='" + Option8 + "' />" + Option8;
            return QuestionCheckBox + "</div>";
        }

        public string Textbox(int QuestionId, bool Required, string Question)
        {

            string tbRequired = Required ? "data-required='Yes'" : "data-required='No'";
            string star = Required ? "*  " : "";
            return "<div id='" + QuestionId + "' class='qBox'  data-qType='Text' " + tbRequired + "><b>Question : </b><span class='qText'>" + star + Question + "</span><br/><textarea id='txtarea" + QuestionId + "'></textarea></div>";

        }

        public string Ranking(int QuestionId, bool Required, string Question, int Min, int Max, int Intrval)
        {
            string rkRequired = Required ? "data-required='Yes'" : "data-required='No'";
            string star = Required ? "*  " : "";
            string questionText = "";
            questionText = "<div id='" + QuestionId + "' class='qBox'  data-qType='Ranking' " + rkRequired + "><b>Question : </b><span class='qText'>" + star + Question + ";/span><br/><div class='rating'></div>";
            for (int i = Max; i >= Max; i -= Intrval)
            {
                questionText += " <span><input type='radio' name='rating' id='" + QuestionId + i + "' value='" + i + "'><label for='" + QuestionId + i + "'/></label></span>";
            }
            questionText += "<br>Min=<span class='min'>" + Min + "  </span>Max=<span class='max'>" + Max + "</span>Interval=<span class='int'>" + Intrval + "</span>";
            return questionText + "</div>";
        }
    }
    
}
