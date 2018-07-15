using Moq;
using NUnit.Framework;
using TigerSpikeLandMarks.Entities;
using TigerSpikeLandMarks.Repositories;
using TigerSpikeLandMarks.DBContexts;
using Microsoft.EntityFrameworkCore;

namespace LandMark_Test
{
    [TestFixture]
    public class UsersTests
    {
        private Mock<IDataRepository> mockDataRepo;
        private Mock<SQLDBContext> sqlDBContext;

        [SetUp]
        public void Setup()
        {
            this.mockDataRepo = new Mock<IDataRepository>();
        }

        [Test]
        public void AddUserTest()
        {
            var builder = new DbContextOptionsBuilder<SQLDBContext>();
            builder.UseInMemoryDatabase();
            var options = builder.Options;
            
            using (var context = new SQLDBContext(options))
            {
                var userManager = new TigerSpikeLandMarks.Managers.UserManager.UserManager(context, mockDataRepo.Object); ;
                var allUsers = userManager.Create(new User { Id = 1, Username = "username1", FirstName = "firstName1", LastName = "lastname1", PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }}, "password");
                context.SaveChanges();
            }

            using (var context = new SQLDBContext(options))
            {
                var userManager = new TigerSpikeLandMarks.Managers.UserManager.UserManager(context, mockDataRepo.Object); ;
                var user = userManager.GetById(1);
                context.SaveChanges();
                Assert.IsTrue(1 == user.Id);
            }
        }


        [Test]
        public void UpdateUserTest()
        {
            var builder = new DbContextOptionsBuilder<SQLDBContext>();
            builder.UseInMemoryDatabase();
            var options = builder.Options;

            using (var context = new SQLDBContext(options))
            {
                var userManager = new TigerSpikeLandMarks.Managers.UserManager.UserManager(context, mockDataRepo.Object); ;
                var allUsers = userManager.Create(new User { Id = 2, Username = "username2", FirstName = "firstName2", LastName = "lastname2", PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }, PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 } }, "password");
                context.SaveChanges();
            }

            using (var context = new SQLDBContext(options))
            {
                var userManager = new TigerSpikeLandMarks.Managers.UserManager.UserManager(context, mockDataRepo.Object); ;
                var user = userManager.GetById(1);
                user.FirstName = "updated";
                context.SaveChanges();
            }

            using (var context = new SQLDBContext(options))
            {
                var userManager = new TigerSpikeLandMarks.Managers.UserManager.UserManager(context, mockDataRepo.Object); ;
                var user = userManager.GetById(1);
                Assert.IsTrue("updated" == user.FirstName);
            }
        }
    }
}
