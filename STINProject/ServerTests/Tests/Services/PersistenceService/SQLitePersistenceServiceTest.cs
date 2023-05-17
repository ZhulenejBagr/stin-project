namespace ServerTests.Tests.Services.PersistenceService
{
    [TestCaseOrderer("ServerTests.Tests.PriorityOrderer", "ServerTests")]
    public class SQLitePersistenceServiceTest : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;

        public SQLitePersistenceServiceTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact, TestPriority(1)]
        public void AddUser_ShouldAddUser()
        {
            Assert.True(_fixture.InMemoryService.AddUser(_fixture.TestUser));
        }


        [Fact, TestPriority(2)]
        public void GetUser_ShouldGetUser()
        {
            Assert.Equal(_fixture.TestUser, _fixture.InMemoryService.GetUser(_fixture.TestUser.UserId));
        }

        [Fact, TestPriority(3)]
        public void AddAccount_ShouldAddAccounts()
        {
            bool failed = false;
            foreach (var account in _fixture.TestAccounts)
            {
                failed &= _fixture.InMemoryService.AddAccount(account);
            }

            Assert.True(!failed);
        }

        [Fact, TestPriority(4)]
        public void GetAccount_ShouldGetAccount()
        {
            Assert.Equal(_fixture.TestAccounts, _fixture.InMemoryService.GetAccounts(_fixture.TestUser.UserId));
        }


        [Fact, TestPriority(5)]
        public void AddTransaction_ShouldAddTransactions()
        {
            bool failed = false;
            foreach (var transaction in _fixture.TestTransactions)
            {
                failed &= _fixture.InMemoryService.AddTransaction(transaction);
            }

            Assert.True(!failed);
        }

        [Fact, TestPriority(6)]
        public void GetTransactions_ShouldGetTransactions()
        {
            Assert.Equal(_fixture.TestTransactions, _fixture.InMemoryService.GetTransactions(_fixture.TestTransactions.First().AccountID));
        }

        [Fact, TestPriority(7)]
        public void AddUser_ShouldNotAddDuplicate()
        {
            Assert.False(_fixture.InMemoryService.AddUser(_fixture.TestUser));
        }

        [Fact, TestPriority(8)]
        public void AddAccount_ShouldNotAddDuplicate()
        {
            Assert.False(_fixture.InMemoryService.AddAccount(_fixture.TestAccounts.First()));
        }

        [Fact, TestPriority(9)]
        public void AddTransaction_ShouldNotAddDuplicate()
        {
            Assert.False(_fixture.InMemoryService.AddTransaction(_fixture.TestTransactions.First()));
        }

        [Fact, TestPriority(10)]
        public void AddAccount_ShouldNotAddAccountWithUnknownUser()
        {
            var unknownUser = ContextMockingTools.SampleUsers(1).First();
            var account = ContextMockingTools.SampleAccounts(1, unknownUser.UserId).First();
            Assert.False(_fixture.InMemoryService.AddAccount(account));
        }

        [Fact, TestPriority(11)]
        public void AddTransaction_ShouldNotAddTransactionWithUnknownAccount()
        {
            var unknownAccount = ContextMockingTools.SampleAccounts(1, _fixture.TestUser.UserId).First();
            var transaction = ContextMockingTools.SampleTransactions(1, unknownAccount.AccountId).First();
            Assert.False(_fixture.InMemoryService.AddTransaction(transaction));
        }

        [Fact, TestPriority(12)]
        public void GetUser_ShouldGetUser_WhenCorrectNameIsProvided()
        {
            var user = _fixture.TestUser;
            var username = user.Username;
            Assert.Equal(user, _fixture.InMemoryService.GetUser(username));
        }

        [Fact, TestPriority(13)]
        public void GetUser_ShouldReturnNull_WhenUnknownUserIdIsProvided()
        {
            var unknownId = Guid.Empty;
            Assert.Null(_fixture.InMemoryService.GetUser(unknownId));
        }

        [Fact, TestPriority(14)]
        public void GetUser_ShouldReturnNull_WhenUnknownUsernameIsProvided()
        {
            var unknownUsername = string.Empty;
            Assert.Null(_fixture.InMemoryService.GetUser(unknownUsername));
        }
    }
}
