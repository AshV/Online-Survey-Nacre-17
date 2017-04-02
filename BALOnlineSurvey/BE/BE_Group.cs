using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BALOnlineSurvey.BE
{
    public class BE_Group
    {
            private int _groupId;
            private string _groupName;
            private string _groupDescription;
            private int _surveyCreatorId;

            public int GroupId
            {
                get { return _groupId; }
                set { _groupId = value; }
            }
            public string GroupName
            {
                get { return _groupName; }
                set { _groupName = value; }
            }
            public string GroupDescription
            {
                get { return _groupDescription; }
                set { _groupDescription = value; }
            }
            
            public int SurveyCreatorID
            {
                get { return _surveyCreatorId; }
                set { _surveyCreatorId = value; }
            }
        }
}
