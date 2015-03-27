using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebApplication1.Tests
{
    [TestFixture]
    public class Test2
    {
        [Test]
        public void CalYear()
        {
            TestClass.Calculate cal = new TestClass.Calculate();
            int inputAge = 30;
            int expect = 1985;//true
            //int expect = 1988;//false

            Assert.AreEqual(expect, cal.GetBirthYear(inputAge, 1));
        }
    }
}
