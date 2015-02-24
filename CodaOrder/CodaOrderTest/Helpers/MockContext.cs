using Moq;
using MvcRouteTester.Assertions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using NUnit.Framework;

namespace CodaOrderTest.MockContext
{
    public class NunitAssertEngine : IAssertEngine
    {
        public void Fail(string message)
        {
            Assert.Fail(message);
        }

        public void StringsEqualIgnoringCase(string s1, string s2, string message)
        {
            if (string.IsNullOrEmpty(s1) && string.IsNullOrEmpty(s2))
            {
                return;
            }

            StringAssert.AreEqualIgnoringCase(s1, s2, message);
        }
    }
}
