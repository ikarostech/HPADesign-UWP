using HPADesign.IO.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Helpers
{
    public static class DXF
    {
        public static string ParamToString(List<KeyValuePair<int,string>> param)
        {
            return string.Join("", param.Select(x => x.Key.ToString() + Environment.NewLine + x.Value + Environment.NewLine).ToList());
        }
        private static string Header
        {
            get
            {
                var param = new List<KeyValuePair<int, string>>();
                param.Add(new KeyValuePair<int, string>(0, "SECTION"));
                param.Add(new KeyValuePair<int, string>(2, "HEADER"));
                param.Add(new KeyValuePair<int, string>(0, "ENDSEC"));
                param.Add(new KeyValuePair<int, string>(0, "SECTION"));
                param.Add(new KeyValuePair<int, string>(2, "ENTITIES"));

                return ParamToString(param);
            }
        }
        private static string Footer
        {
            get
            {
                var param = new List<KeyValuePair<int, string>>();
                param.Add(new KeyValuePair<int, string>(0, "ENDSEC"));
                param.Add(new KeyValuePair<int, string>(0, "EOF"));

                return ParamToString(param);
            }
        }

        public static string Construct(List<KeyValuePair<int, string>> param)
        {
            return Header + ParamToString(param) + Footer;
        }
        private static string Construct(string content)
        {
            return Header + content + Footer;
        }
        public static string Content(IPrintable printable)
        {
            return DXF.Construct(string.Join("", printable.Shapes.Select(x => x.Print()).ToList()));
        }
    }
}
