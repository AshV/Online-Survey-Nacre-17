using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BALOnlineSurvey.BE
{
    public class BE_QuestionType
    {
        private int _questionTypeId;
        private string _questionType;

        public int QuestionTypeId
        {
            get { return _questionTypeId; }
            set { _questionTypeId = value; }
        }
        public string QuestionType
        {
            get { return _questionType; }
            set { _questionType = value; }
        }
        
    }
}
