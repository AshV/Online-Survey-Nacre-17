using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BALOnlineSurvey.BE
{
    public class BE_Company
    {
        private int _companyId;
        private string _companyName;

        public int CompanyId
        {
            get { return _companyId; }
            set { _companyId = value; }
        }
        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }
    }
}
