using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Service.Domain;
using NUnit.Framework;

namespace LMS.DAL.Test
{
    [TestFixture]
    public class UserDalManagerTester
    {
        private UserDalManager userDalManager;

        [SetUp]
        public void SetUp()
        {
            this.userDalManager = new UserDalManager();
        }

        [Test]
        public void GetUserOnLoginBasicTest()
        {
            string login = "admin";
            User user = userDalManager.GetUserOnLogin(login);
            Assert.That(user.Login, Is.EqualTo("admin"));
        }

        [Test]
        public void CreateUserBasicTest()
        {
            User newUser = new User();
            newUser.Login = "qwerty";
            newUser.Password = "qwerty";
            newUser.TimeCreated = DateTime.Now;
            newUser.NumberOfBooksIssued = 0;

            User returnedUser = userDalManager.CreateUser(newUser);
            Console.WriteLine("Create user with Login - " + returnedUser.Login);

            userDalManager.DeleteUser(newUser.Login);
            Console.WriteLine("Delete user with Login - " + newUser.Login + " on CreateUserBasicTest");
        }

        [Test]
        public void DeteleUserBasicTest()
        {
            User newUser = new User();
            newUser.Login = "ChiChi";
            newUser.Password = "ChiChi";
            newUser.TimeCreated = DateTime.Now;
            newUser.NumberOfBooksIssued = 2;

            User returnedUser = userDalManager.CreateUser(newUser);

            userDalManager.DeleteUser(returnedUser.Login);
        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}
