using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaKiosk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.Models.Tests
{
    [TestClass()]
    public class CryptographyTests
    {
        [DataRow("test")]
        [DataRow("peyton")]
        [DataRow("abc123")]
        [TestMethod()]
        public void EncryptDecryptStringTest(string text)
        {
            string encryptedText = Cryptography.EncryptString(text);
            string decryptedText = Cryptography.DecryptString(encryptedText);

            Assert.AreEqual(text, decryptedText);
        }
    }
}