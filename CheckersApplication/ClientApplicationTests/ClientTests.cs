using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApplicationTests {
    [TestClass()]
    public class ClientTests
    {

        Game_Form form = new Game_Form(true);

        [TestMethod()]
        public void ClientTest()
        {
            Client clientTest = new Client(form);
            Assert.AreNotEqual(clientTest, null);
        }

        [TestMethod()]
        public void GetBoardTest()
        {
            Client clientTest = new Client(form);
            Assert.AreNotEqual(clientTest.GetBoard(), null);
        }
    }
}