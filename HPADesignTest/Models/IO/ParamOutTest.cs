using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HPADesign.Helpers;

namespace HPADesignTest.Models.IO
{
    [TestClass]
    public class ParamOutTest
    {  
        [TestMethod]
        public void パラメーターを渡してDXF形式になっているか()
        {
            var param = new List<KeyValuePair<int, string >>();
            param.Add(new KeyValuePair<int, string>(0, "SECTION"));
            param.Add(new KeyValuePair<int, string>(2, "HEADER"));
            param.Add(new KeyValuePair<int, string>(0, "ENDSEC"));
            param.Add(new KeyValuePair<int, string>(0, "SECTION"));
            param.Add(new KeyValuePair<int, string>(2, "ENTITIES"));
            param.Add(new KeyValuePair<int, string>(0, "ENDSEC"));
            param.Add(new KeyValuePair<int, string>(0, "EOF"));

            StreamReader sr = new StreamReader("Resource/DxfMin.dxf");

            string except = sr.ReadToEnd();
            string actual = DXF.ParamToString(param);
            sr.Close();
            Assert.AreEqual(except, actual, true);
        }

        [TestMethod]
        public void DXFの最小構成を構築()
        {
            StreamReader sr = new StreamReader("Resource/DxfMin.dxf");

            string except = sr.ReadToEnd();
            string actual = DXF.Header + DXF.Footer;
            sr.Close();
            Assert.AreEqual(except, actual, true);
        }
    }
}
