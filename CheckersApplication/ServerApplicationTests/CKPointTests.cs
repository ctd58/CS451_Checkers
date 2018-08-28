using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests {
    [TestClass()]
    public class CKPointTests {

        private CKPoint pTest = new CKPoint(1, 1);

        [TestMethod()]
        public void CKPointTest() {
            Assert.AreNotEqual(pTest, null);
        }

        [TestMethod()]
        public void GetRowTest() {
            Assert.AreEqual(pTest.GetRow(), 1);
        }

        [TestMethod()]
        public void GetColumnTest() {
            Assert.AreEqual(pTest.GetColumn(), 1);
        }
    }
}