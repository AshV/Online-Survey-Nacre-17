using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BALOnlineSurvey.BE
{
    public class BE_SurveyQuestion
    {
        private int _surveyId;
        private int _questionId;
        private int _required;
        private int _minValue;
        private int _maxValue;
        private int _interval;
        private int _questionSrNo;

        public int SurveyId
        {
            get { return _surveyId; }
            set { _surveyId = value; }
        }
        public int QuestionId
        {
            get { return _questionId; }
            set { _questionId = value; }
        }
        public int Required
        {
            get { return _required; }
            set { _required = value; }
        }
        public int MinValue
        {
            get { return _minValue; }
            set { _minValue = value; }
        }
        public int MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }
        public int Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }
        public int QuestionSrNo
        {
            get { return _questionSrNo; }
            set { _questionSrNo = value; }
        }
    }
}
