using Moq;
using ServerTests.Tests.Services.PersistenceService;
using STINProject.Server.Services.LoginService;
using STINProject.Server.Services.PersistenceService;
using STINProject.Server.Services.PersistenceService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerTests.Tests.Services.LoginService
{
    internal class LoginFixture
    {
        public ILoginService ServiceUnderTest { get; init; }
        private readonly IPersistenceService _persistenceService;
        public IEnumerable<User> Users { get; init; }

        public LoginFixture()
        {
            Users = ContextMockingTools.SampleUsers(3);

            var loginMock = new Mock<IPersistenceService>();
            loginMock
                .Setup(x => x.GetUser(It.IsAny<string>()))
                .Returns((string username) => Users.FirstOrDefault(x => x.Username == username));
        }
    }
}
