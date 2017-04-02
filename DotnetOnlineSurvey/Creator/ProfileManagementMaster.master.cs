using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BALOnlineSurvey.BE;
using BALOnlineSurvey.BL;

public partial class Creator_ProfileManagementMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    BE_Group groupobj = new BE_Group();
    BLProfileManagement obj = new BLProfileManagement();
    protected void btnAdd_OnClick(object sender, EventArgs e)
    {
        try
        {

            groupobj.GroupName = txtgroup.Text;
            groupobj.GroupDescription = txtdesc.Text;
            groupobj.SurveyCreatorID = 100;
            int n = obj.CreateGroup(groupobj);
            Response.Write("group inserted sucessfully");

        }
        catch (Exception ex)
        {
            lblError.Text= ex.Message;
        }
    }
}
