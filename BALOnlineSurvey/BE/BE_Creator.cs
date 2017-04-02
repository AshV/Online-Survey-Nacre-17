using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BALOnlineSurvey.BE
{
    public class BE_Creator
    {
        private int _surveyCreatorId;
        private string _creatorName;
        private string _emailID;
        private string _mobileNumber;
        private string _password;
        private string _username;
        private int _CompanyId;

        public int SurveyCreatorId
        {
            get { return _surveyCreatorId; }
            set { _surveyCreatorId = value; }
        }
        public string CreatorName
        {
            get { return _creatorName; }
            set { _creatorName = value; }
        }
        public string EmailID
        {
            get { return _emailID; }
            set { _emailID = value; }
        }
        public string MobileNumber
        {
            get { return _mobileNumber; }
            set { _mobileNumber = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }
        public int CompanyId
        {
            get { return _CompanyId; }
            set { _CompanyId = value; }
        }
    }
}
