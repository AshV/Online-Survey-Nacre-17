using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BALOnlineSurvey.BE
{
        public class EmailForm
        {

            string _to;
            string _from;
            string _pwd;
            string _subject;
            string _body;

            public string Pwd
            {
                get { return _pwd; }
                set { _pwd = value; }
            }

            public string To
            {
                get { return _to; }
                set { _to = value; }
            }

            public string From
            {
                get { return _from; }
                set { _from = value; }
            }

            public string Subject
            {
                get { return _subject; }
                set { _subject = value; }
            }

            public string Body
            {
                get { return _body; }
                set { _body = value; }
            }
        }
}
