namespace ServerTests.Tests.Services.TransactionService
{
    public class TransactionServiceTest : IClassFixture<TransactionFixture>
    {
        private readonly TransactionFixture _fixture;

        public TransactionServiceTest(TransactionFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void TryExecuteTransaction_ShouldBeFalse_WhenCurrencyCodeIsNotSupported()
        {
            var unsupportedCurrencyCode = "234";
            var userId = _fixture.Users[0].UserId;
            Assert.False(_fixture.ServiceUnderTest.TryExecuteTransaction(userId, unsupportedCurrencyCode, 32));
        }

        [Fact]
        public void TryExecuteTransaction_ShouldBeFalse_WhenUserHasNoAccounts()
        {
            var userId = _fixture.Users[1].UserId;
            Assert.False(_fixture.ServiceUnderTest.TryExecuteTransaction(userId, "GBP", 25));
        }

        [Fact]
        public void TryExecuteTranscation_ShouldBeTrue_WhenUserHasAccountWithSameCurrencyAndEnoughBalance()
        {
            var userId = _fixture.Users[0].UserId;
            var account = _fixture.Accounts[0];
            var currency = account.Currency;
            account.Balance = 1000;
            Assert.True(_fixture.ServiceUnderTest.TryExecuteTransaction(userId, currency, 25));
        }

        [Fact]
        public void TryExecuteTransaction_ShouldBeFalse_WhenUserHasNoAccountWithEnoughBalance()
        {
            var userId = _fixture.Users[0].UserId;
            var account1 = _fixture.Accounts[0];
            var account2 = _fixture.Accounts[1];
            account1.Balance = 0;
            account2.Balance = 0;
            account1.Currency = account2.Currency;
            Assert.False(_fixture.ServiceUnderTest.TryExecuteTransaction(userId, account1.Currency, -25));
        }

        [Fact]
        public void TryExecuteTranscation_ShouldBeTrue_WhenValueIsNegativeAndUserHasMatchingAccountWithEnoughBalance()
        {
            var userId = _fixture.Users[0].UserId;
            var account1 = _fixture.Accounts[0];
            var account2 = _fixture.Accounts[1];
            account1.Balance = 50;
            Assert.True(_fixture.ServiceUnderTest.TryExecuteTransaction(userId, account1.Currency, -25));
        }

        [Fact]
        public void TryExecuteTransaction_ShouldBeTrue_WhenUserHasAccountWithoutMatchingCurrencyWithEnoughBalance()
        {
            var userId = _fixture.Users[0].UserId;
            var account1 = _fixture.Accounts[0];
            account1.Balance = 1000;
            Assert.True(_fixture.ServiceUnderTest.TryExecuteTransaction(userId, "CZK", -25));
        }
    }
}
