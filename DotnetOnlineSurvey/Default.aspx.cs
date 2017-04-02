using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         
                string Creatorname = Session["Name"].ToString();
                string Uname = Session["UName"].ToString();
                lblCreatorName.Text ="Welcome "+ Creatorname;
                lblUname.Text = Uname;
    
    
    }
}