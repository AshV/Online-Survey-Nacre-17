using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BALOnlineSurvey.BE
{
    public class BE_Category
    {
        private int _categoryId;
        private string _categoryName;

        public int CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; }
        }
        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }
    }
}
