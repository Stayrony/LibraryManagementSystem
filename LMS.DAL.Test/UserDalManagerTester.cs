// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserDalManagerTester.cs" company="">
//   
// </copyright>
// <summary>
//   The user dal manager tester.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LMS.DAL.Test
{
    using System;

    using LMS.Service.Domain;

    using NUnit.Framework;

    /// <summary>
    /// The user dal manager tester.
    /// </summary>
    [TestFixture]
    public class UserDalManagerTester
    {
        /// <summary>
        /// The user dal manager.
        /// </summary>
        private UserDalManager userDalManager;

        /// <summary>
        /// The set up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.userDalManager = new UserDalManager();
        }

        /// <summary>
        /// The get user on login basic test.
        /// </summary>
        [Test]
        public void GetUserOnLoginBasicTest()
        {
            string login = "admin";
            User user = this.userDalManager.GetUserOnLogin(login);
            Assert.That(user.Login, Is.EqualTo("admin"));
        }

        /// <summary>
        /// The create user basic test.
        /// </summary>
        [Test]
        public void CreateUserBasicTest()
        {
            User newUser = new User();
            newUser.Login = "qwerty";
            newUser.Password = "qwerty";
            newUser.TimeCreated = DateTime.Now;

            User returnedUser = this.userDalManager.CreateUser(newUser);
            Console.WriteLine("Create user with Login - " + returnedUser.Login);

            this.userDalManager.DeleteUser(newUser.Login);
            Console.WriteLine("Delete user with Login - " + newUser.Login + " on CreateUserBasicTest");
        }

        /// <summary>
        /// The detele user basic test.
        /// </summary>
        [Test]
        public void DeteleUserBasicTest()
        {
            User newUser = new User();
            newUser.Login = "ChiChi";
            newUser.Password = "ChiChi";
            newUser.TimeCreated = DateTime.Now;

            User returnedUser = this.userDalManager.CreateUser(newUser);

            this.userDalManager.DeleteUser(returnedUser.Login);
        }

        /// <summary>
        /// The tear down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
        }
    }
}