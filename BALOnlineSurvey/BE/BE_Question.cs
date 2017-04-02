using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BALOnlineSurvey.BE
{
    public class BE_Question
    {
        private int _questionId;
        private string _question;
        private int _questionTypeId;
        private string _option1;
        private string _option2;
        private string _option3;
        private string _option4;
        private string _option5;
        private string _option6;
        private string _option7;
        private string _option8;
        private int _categoryId;
        private int _creatorId;

        public int QuestionId
        {
            get { return _questionId; }
            set { _questionId = value; }
        }
        public string Question
        {
            get { return _question; }
            set { _question = value; }
        }
        public int QuestionTypeId
        {
            get { return _questionTypeId; }
            set { _questionTypeId = value; }
        }
        public string Option1
        {
            get { return _option1; }
            set { _option1 = value; }
        }
        public string Option2
        {
            get { return _option2; }
            set { _option2 = value; }
        }
        public string Option3
        {
            get { return _option3; }
            set { _option3 = value; }
        }
        public string Option4
        {
            get { return _option4; }
            set { _option4 = value; }
        }
        public string Option5
        {
            get { return _option5; }
            set { _option5 = value; }
        }
        public string Option6
        {
            get { return _option6; }
            set { _option6 = value; }
        }
        public string Option7
        {
            get { return _option7; }
            set { _option7 = value; }
        }
        public string Option8
        {
            get { return _option8; }
            set { _option8 = value; }
        }
        public int CategoryID
        {
            get { return _categoryId; }
            set { _categoryId = value; }
        }
        public int CreatorID
        {
            get { return _creatorId; }
            set { _creatorId = value; }
        }
    }
}
