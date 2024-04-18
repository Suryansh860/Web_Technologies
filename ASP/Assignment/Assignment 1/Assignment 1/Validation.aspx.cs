using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment_1
{
    public partial class Validation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Label1.Text = "Data Validated Successfully";
                Label1.ForeColor = System.Drawing.Color.Green;
            }
        }
        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}