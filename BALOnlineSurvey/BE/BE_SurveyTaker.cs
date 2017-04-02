using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BALOnlineSurvey.BE
{
    public class BE_SurveyTaker
    {
        private int _surveyId;
        private int _surveyTakerID;
        private int _completed;
        private int _started;

        string _surveyTakerEmailID;
        string _uniqueID;

        public string UniqueID
        {
            get { return _uniqueID; }
            set { _uniqueID = value; }
        }

        public string SurveyTakerEmailID
        {
            get { return _surveyTakerEmailID; }
            set { _surveyTakerEmailID = value; }
        }

        public int SurveyId
        {
            get { return _surveyId; }
            set { _surveyId = value; }
        }
        public int SurveyTakerID
        {
            get { return _surveyTakerID; }
            set { _surveyTakerID = value; }
        }
        public int Completed
        {
            get { return _completed; }
            set { _completed = value; }
        }
        public int Started
        {
            get { return _started; }
            set { _started = value; }
        }
    }
}
