using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPADesign.Helpers
{
    public enum PartWingNameType
    {
        NumberStartWith0,
        NumberStartWith1,
        Alphabet
    }
    public static class PartWingName
    {
        public static string getPartWingName(int index, PartWingNameType nameType)
        {
            string result = "";
            switch(nameType)
            {
                case PartWingNameType.NumberStartWith0:
                    result += index.ToString() + "番";
                    break;
                case PartWingNameType.NumberStartWith1:
                    result += (index + 1).ToString() + "番";
                    break;
                case PartWingNameType.Alphabet:
                    result += ('A' + index);
                    break;
            }
            result += "翼";
            return result;
        }
    }
}
