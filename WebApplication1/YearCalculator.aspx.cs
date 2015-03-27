using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestClass;

namespace WebApplication1
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        protected void btnCal_Click(object sender, EventArgs e)
        {
            Calculate cal = new Calculate();
            int inputAge = 0;
            int inputFormat = 0;

            int.TryParse(txtAge.Text, out inputAge);
            int.TryParse(YearFormat.SelectedValue, out inputFormat);
            
            if (inputAge > 0)
            {
                lblYear.Text = cal.GetBirthYear(inputAge, inputFormat).ToString();
            }
        }
    }
}