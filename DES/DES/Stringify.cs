using System;
using System.Collections.Generic;
namespace MyUtils {
    public partial class Utils
    {
        public static string ListToString(List<object> source) {
            string text = "";

            foreach (object l in source) {
                text += l.ToString() + ", ";
            }

            return text;
        }

        public static string ListToString(object[] source) {
            string text = "";

            foreach (object l in source)
            {
                text += l.ToString() + ", ";
            }

            return text;
        }

        public static string ListToString(List<string> keywords)
        {
            return ListToString(keywords.ToArray());
        }
    }
}
