using Microsoft.Data.SqlClient;
using Warehouse.DTO;
using Warehouse.Services.Exceptions;
using Warehouse.Services.Interfaces.Repositories;
using Warehouse.Services.Interfaces.Services;
using Warehouse.Services.Models;

namespace Warehouse.Services.Tests
{
    public class UserServiceTests : BaseServiceTests
    {
        private IUserService? _service;

        [SetUp]
        public void Setup()
        {
            _service = new UserService(_unitOfWork!);
        }

        [Test]
        public void TestAddUser_ShouldAddUser()
        {
            User newUser = new()
            {
                UserId = 4,
                Username = "Test User",
                Password = "Test Password",
                UserRole = 2,
            };

            Assert.DoesNotThrow(() => _service!.AddUser(newUser));
            Assert.That(newUser.UserId, Is.GreaterThan(0));

            User? insertedUser = _service!.GetUser(newUser.UserId);
            Assert.That(insertedUser, Is.Not.Null);
            Assert.That(insertedUser!.Username, Is.EqualTo(newUser.Username));
            Assert.That(insertedUser.UserRole, Is.EqualTo(newUser.UserRole));
        }

        [Test]
        public void TestAddUser_ShouldNotAddUser()
        {
            User user = new()
            {
                Username = "Test User",
                Password = null!,
                UserRole = 1,
            };
            Assert.Throws<SqlException>(() => _service!.AddUser(user));
        }

        [Test]
        public void TestEditUser_ShouldEditUser()
        {
            User? current = _service!.GetUser(3);
            Assert.IsNotNull(current);

            current.UserId = 3;
            current.UserRole = 3;
            current!.Password = "123Pass++";
            current.Username = "Updated " + current.Username;

            _service!.EditUser(current);

            User? updated = _service.GetUser(current.UserId);
            Assert.IsNotNull(updated);
            Assert.That(current.UserId, Is.EqualTo(updated!.UserId));
            Assert.That(current.Username, Is.EqualTo(updated!.Username));
            Assert.That(current.UserRole, Is.EqualTo(updated!.UserRole));
        }

        [Test]
        public void TestEditUser_ShouldNotEditUser()
        {
            User? current = _service!.GetUser(3);
            Assert.IsNotNull(current);

            current.UserId = 3;
            current!.UserRole = 3;
            current!.Password = "123Pass++";
            current.Username = null!;
            Assert.Throws<SqlException>(() => _service!.EditUser(current));
        }

        [Test]
        public void TestDeleteUser_ShouldDeleteUser()
        {
            User? current = _service!.GetUser(2);
            Assert.IsNotNull(current);

            Assert.DoesNotThrow(() => _service!.DeleteUser(current!.UserId));
            User? deleted = _service.GetUser(current!.UserId);
            Assert.IsNull(deleted);
        }

        [Test]
        public void TestDeleteUser_ShouldNotDeleteUser()
        {
            User? current = _service!.GetUser(2);
            if (current == null)
            {
                Assert.Throws<SqlException>(() => _service!.DeleteUser(2));
                return;
            }
            Assert.IsNotNull(current);

            Assert.DoesNotThrow(() => _service!.DeleteUser(current!.UserId));
            User? deleted = _service.GetUser(current!.UserId);
            Assert.IsNull(deleted);
            Assert.Throws<SqlException>(() => _service!.DeleteUser(current.UserId));
        }

        [Test]
        public void TestGetUser_shouldReturnUser()
        {
            User? user = _service!.GetUser(1);
            Assert.IsNotNull(user);
        }

        [Test]
        public void TestGetUser_ShouldNotReturnUser()
        {
            User? user = _service!.GetUser(0);
            Assert.IsNull(user);
        }

        [Test]
        public void TestGetUsers_ShouldReturnUsers()
        {
            IEnumerable<User> users = _service!.GetUsers();
            Assert.IsNotEmpty(users);
            foreach (var item in users)
            {
                Assert.IsNotNull(item);
                Assert.IsNotEmpty(item.Username);
            }
        }

        [Test]
        public void TestGetUsers_ShouldNotReturnUsers()
        {
            IEnumerable<User> users = _service!.GetUsers("NonExistentUser");
            Assert.IsEmpty(users);
        }

        [Test]
        public void TestLogin_ShouldLoginUser()
        {
            int result = _service!.LoginUser("admin", "admin123");
            Assert.Greater(result, 0, "Login failed.");
        }

        [Test]
        public void TestLogout_ShouldNotLoginUser()
        {
            Assert.Throws<LoginException>(() => _service!.LoginUser("admin", "wrongpassword"));
        }
    }
}