using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Extensions
{
    public static class StringExt
    {
        public static string adjustMandatoryText(this string text, bool isMandatory)
        {
            return isMandatory ? $"{text}*" : text;
        }
    }
}
