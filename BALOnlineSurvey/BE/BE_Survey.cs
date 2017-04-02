using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BALOnlineSurvey.BE
{
    public class BE_Survey
    {
        private int _surveyId;
        private string _introduction;
        private string _title;
        private DateTime ? _surveyCreatedDate;
        private DateTime _surveyExpiryDate;
        private int _categoryId;
        private int _surveyCreatorId;
        public string SurevyLogo { get; set; }
        public int SurveyId
        {
            get { return _surveyId; }
            set { _surveyId = value; }
        }
        public string Introduction
        {
            get { return _introduction; }
            set { _introduction = value; }
        }
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        public DateTime ? SurveyCreatedDate
        {
            get { return _surveyCreatedDate; }
            set { _surveyCreatedDate = value; }
        }
        public DateTime SurveyExpiryDate
        {
            get { return _surveyExpiryDate; }
            set { _surveyExpiryDate = value; }
        }
        public int CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; }
        }
        public int SurveyCreatorId
        {
            get { return _surveyCreatorId; }
            set { _surveyCreatorId = value; }
        }
    }
}
