using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace RijndaelCodec.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class RijndaelUnitTests
    {
        public RijndaelUnitTests()
        {
            Key = new byte[32] {  0x40, 0x04, 0x01, 0xC0, 0x22, 0x11, 0x55, 0x33,
                                       0x11, 0xff, 0x55, 0x76, 0x14, 0x88, 0x7f, 0x11,
                                       0xAC, 0xBB, 0x71, 0x2C, 0xD1, 0x16, 0x07, 0x13,
                                       0xEE, 0x32, 0x20, 0x85, 0x25, 0x38, 0x25, 0x26};
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        [TestInitialize()]
        public void MyTestInitialize()
        {
            Coder = new RijndaelCodec.Services.Implementation.Encoder(Key);
            Decoder = new RijndaelCodec.Services.Implementation.Encoder(Key);
        }
        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        [TestMethod]
        public void CanEncodeFile()
        {
            const string file1 = @"testinput.exx";
            var dataToEncode = File.ReadAllText(file1);
            var result = Coder.Encode(dataToEncode);
            var decoded = Decoder.Decode(result);
            int comparisonResult = string.Compare(dataToEncode, decoded);
            Assert.AreEqual(comparisonResult, 0);
        }
      

        private byte[] Key { get; set; }

        public Services.Contract.ICoder Coder { get; set; }
        public Services.Contract.IDecoder Decoder { get; set; }
    }
}
