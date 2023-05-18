<<<<<<< HEAD
﻿using Google.Authenticator;
using Moq;
=======
﻿using Moq;
>>>>>>> b1c9a11afcfb58fe9db69cd7590fb0769dd3447a
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
<<<<<<< HEAD
        private readonly SessionStorage _sessionStorage;
        private readonly TwoFactorService _twoFactorService;
=======
>>>>>>> b1c9a11afcfb58fe9db69cd7590fb0769dd3447a
        public IEnumerable<User> Users { get; init; }

        public LoginFixture()
        {
            Users = ContextMockingTools.SampleUsers(3);

<<<<<<< HEAD
            var persistenceMock = new Mock<IPersistenceService>();
            persistenceMock
                .Setup(x => x.GetUser(It.IsAny<string>()))
                .Returns((string username) => Users.FirstOrDefault(x => x.Username == username));

            var 

            _persistenceService = persistenceMock.Object;
            _sessionStorage = new SessionStorage();
            var authenticator = new TwoFactorAuthenticator();
            _twoFactorService = new TwoFactorService(authenticator);
            
            
=======
            var loginMock = new Mock<IPersistenceService>();
            loginMock
                .Setup(x => x.GetUser(It.IsAny<string>()))
                .Returns((string username) => Users.FirstOrDefault(x => x.Username == username));
>>>>>>> b1c9a11afcfb58fe9db69cd7590fb0769dd3447a
        }
    }
}
