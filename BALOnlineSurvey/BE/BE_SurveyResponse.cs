using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BALOnlineSurvey.BE
{
    public class BE_SurveyResponse
    {
        private int _surveyId;
        private int _userID;
        private string _response;
        private DateTime _date;
        private int _questionId;

        public int SurveyId
        {
            get { return _surveyId; }
            set { _surveyId = value; }
        }
        public int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }
        public string Response
        {
            get { return _response; }
            set { _response = value; }
        }
        public DateTime date
        {
            get { return _date; }
            set { _date = value; }
        }
        public int QuestionId
        {
            get { return _questionId; }
            set { _questionId = value; }
        }
    }
}
