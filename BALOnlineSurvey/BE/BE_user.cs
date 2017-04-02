using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BALOnlineSurvey.BE
{
    public class BE_user
    {
        private int _surveyTakerId;
        private string _surveyTakerName;
        private string _surveyTakerEmailID;
        private string _mobileNumber;
        private string _alternateEmailID;
        private int _companyId;

        public int SurveyTakerId
        {
            get { return _surveyTakerId; }
            set { _surveyTakerId = value; }
        }
        public string SurveyTakerName
        {
            get { return _surveyTakerName; }
            set { _surveyTakerName = value; }
        }
        public string SurveyTakerEmailID
        {
            get { return _surveyTakerEmailID; }
            set { _surveyTakerEmailID = value; }
        }
        public string MobileNumber
        {
            get { return _mobileNumber; }
            set { _mobileNumber = value; }
        }
        public string AlternateEmailID
        {
            get { return _alternateEmailID; }
            set { _alternateEmailID = value; }
        }
        public int CompanyId
        {
            get { return _companyId; }
            set { _companyId = value; }
        }
    }
}
