using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BALOnlineSurvey.BE
{
    public class BE_GroupMember
    {
        private int _groupId;
        private int _surveyTakerId;

        public int GroupId
        {
            get { return _groupId; }
            set { _groupId = value; }
        }
        public int SurveyTakerId
        {
            get { return _surveyTakerId; }
            set { _surveyTakerId = value; }
        }
    }
}
