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
        public static string ParseString(TextBox tb, bool required = false, bool returnNull = true)
        {
            if(required && tb.Text == "")
            {
                throw new ArgumentNullException();
            }
            return tb.Text == "" ? (returnNull ? null : tb.Text) : tb.Text;
        }
        public static int ParseInt(TextBox tb, bool required = false)
        {
            try
            {
                return int.Parse(tb.Text);
            }
            catch (FormatException e)
            {
                if (required)
                    throw e;
                return -1;
            }
        }
    }
}
