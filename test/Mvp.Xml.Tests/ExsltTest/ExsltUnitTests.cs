using System;
using Mvp.Xml.Exslt;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using Mvp.Xml.Common.Xsl;
#if !NUNIT
using Microsoft.VisualStudio.TestTools.UnitTesting;
#else
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
#endif


namespace ExsltTest
{
    /// <summary>
    /// Superclass for unit tests.
    /// </summary>
    public class ExsltUnitTests
    {
        protected virtual string TestDir => "../../../ExsltTest/tests/EXSLT/Common/";

        protected virtual string ResultsDir => "../../../ExsltTest/results/EXSLT/Common/";

        protected void RunAndCompare(string source, string stylesheet, 
            string result) 
        {
            var doc = new XPathDocument(TestDir + source);
            var res = new StringWriter();

            // deprecated
            //ExsltTransform exslt = new ExsltTransform();
            //exslt.Load(TestDir + stylesheet);
            //exslt.Transform(doc, null, res);

            var transform = new MvpXslTransform();
            transform.Load(TestDir + stylesheet);
            transform.Transform(new XmlInput(doc), null, new XmlOutput(res));

            var sr = new StreamReader(ResultsDir + result);
            string expectedResult = sr.ReadToEnd();
            sr.Close();
            string actualResult = res.ToString();
			if (actualResult != expectedResult)
			{
				Console.WriteLine(@"Actual Result was {0}", actualResult);
				Console.WriteLine(@"Expected Result was {0}", expectedResult);
			}
            Assert.AreEqual(expectedResult, actualResult);
        }        
    }
}
