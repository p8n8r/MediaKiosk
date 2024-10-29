﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaKiosk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaKiosk.Models.Tests
{
    [TestClass()]
    public class UserTests
    {
        [TestMethod()]
        public void UserTest1()
        {
            User user = new User();

            Assert.IsNotNull(user.Rentals);
            Assert.IsNotNull(user.Purchases);
        }

        [TestMethod()]
        public void UserTest2()
        {
            string username = "user", password = "pass";
            User user = new User(username, password);

            Assert.AreEqual(username, user.Username);
            Assert.AreEqual(password, user.Password);
            Assert.IsNotNull(user.Rentals);
            Assert.IsNotNull(user.Purchases);
        }

        [TestMethod()]
        public void UpdateEncryptedPasswordTest()
        {
            string originalPassword = "password";
            User user = new User() { Password = originalPassword };

            PrivateObject privUser = new PrivateObject(user);
            privUser.Invoke("UpdateEncryptedPassword");

            string unencryptedPassword = Cryptography.DecryptString(user.EncryptedPassword);

            Assert.AreEqual(originalPassword, unencryptedPassword);
            Assert.AreEqual(user.Password, unencryptedPassword);
        }
    }
}