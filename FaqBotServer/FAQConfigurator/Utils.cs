using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FAQConfigurator
{
    class Utils
    {
        public static string ParseString(TextBox tb, bool required = false)
        {
            return tb.Text == "" ? null : tb.Text;
        }
        public static int ParseInt(TextBox tb, bool required = false)
        {
            return int.Parse(tb.Text);
        }
    }
}
