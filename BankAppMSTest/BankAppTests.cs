
using BankApp;
using System.Linq;
using System.Security.Principal;
using System.Xml.Linq;
namespace BankAppMSTest
{
    [TestClass]
    public class BankAppTests
    {
        [TestClass]
        public class CurrencySelectionMenuTest
        {
            [TestMethod]
            public void CurrencySelection_currencySelection_1_Return_currency_Euro()
            {
                // Arrange
                var inputs = new List<string> { "1" };
                string expectedCurrency = "EURO";
                string expectedCurrencySymbol = "£";

                // Act
                Accounts.CurrencySelectionMenu(inputs, out string actualCurrency, out string actualCurrencySymbol);

                // Assert
                Assert.AreEqual(expectedCurrency, actualCurrency);
                Assert.AreEqual(expectedCurrencySymbol, actualCurrencySymbol);
            }

            [TestMethod]
            public void CurrencySelection_currencySelection_2_Return_currency_USD()
            {
                // Arrange
                var inputs = new List<string> { "2" };
                string expectedCurrency = "USD";
                string expectedCurrencySymbol = "$";

                // Act
                Accounts.CurrencySelectionMenu(inputs, out string actualCurrency, out string actualCurrencySymbol);

                // Assert
                Assert.AreEqual(expectedCurrency, actualCurrency);
                Assert.AreEqual(expectedCurrencySymbol, actualCurrencySymbol);
            }

            [TestMethod]
            public void CurrencySelection_currencySelection_3_Return_currency_KRONOR()
            {
                // Arrange
                var inputs = new List<string> { "3" };
                string expectedCurrency = "KRONOR";
                string expectedCurrencySymbol = "SEK";

                // Act
                Accounts.CurrencySelectionMenu(inputs, out string actualCurrency, out string actualCurrencySymbol);

                // Assert
                Assert.AreEqual(expectedCurrency, actualCurrency);
                Assert.AreEqual(expectedCurrencySymbol, actualCurrencySymbol);
            }

            [TestMethod]
            public void TestInvalidInputThenValidInput()
            {
                // Arrange
                var inputs = new List<string> { "0", "2" };
                string expectedCurrency = "USD";
                string expectedCurrencySymbol = "$";

                // Act
                Accounts.CurrencySelectionMenu(inputs, out string actualCurrency, out string actualCurrencySymbol);

                // Assert
                Assert.AreEqual(expectedCurrency, actualCurrency);
                Assert.AreEqual(expectedCurrencySymbol, actualCurrencySymbol);
            }
        }
        [TestClass]
        public class ConvertCurrencyTest
        {
            private Accounts CreateAccount(string currency)
            {
                return new Accounts(
                    accountName: "Test Account",
                    accountBalance: 1000.0,
                    currency: currency,
                    currencySymbol: ""
                );
            }

            [TestMethod]
            public void TestConvertCurrency_100_USD_Return_93_Euro_Fail() //this test should fail, i changed usdEurCur to equal 0.94
            {
                // Arrange
                //usdEurCur = 0.93
                double amount = 100;
                var fromAccount = CreateAccount("USD");
                var toAccount = CreateAccount("EURO");
                double expectedAmount = amount * Currency.USDEURCUR;

                // Act
                double actualAmount = Currency.ConvertCurrency(amount, fromAccount, toAccount);

                // Assert
                Assert.AreEqual(expectedAmount, actualAmount);
                Assert.AreEqual(93, actualAmount);
            }

            [TestMethod]
            public void TestConvertCurrency_USDToKronor()
            {
                // Arrange
                //private static double _usdKronorCur = 10.86;
                double amount = 100;
                var fromAccount = CreateAccount("USD");
                var toAccount = CreateAccount("KRONOR");
                double expectedAmount = amount * Currency.USDKRONORCUR;

                // Act
                double actualAmount = Currency.ConvertCurrency(amount, fromAccount, toAccount);

                // Assert
                Assert.AreEqual(expectedAmount, actualAmount);
                Assert.AreEqual(1086, actualAmount);
            }

            [TestMethod]
            public void TestConvertCurrency_100_Euro_Return_108_USD() //this test should pass (areNotEqual), chaanged eurUsdCur to 1.08
            {
                // Arrange
                // private static double _eurUsdCur = 1.07;
                double amount = 100;
                var fromAccount = CreateAccount("EURO");
                var toAccount = CreateAccount("USD");
                double expectedAmount = amount * Currency.EURUSDCUR;

                // Act
                double actualAmount = Currency.ConvertCurrency(amount, fromAccount, toAccount);

                // Assert
                Assert.AreEqual(expectedAmount, actualAmount);
                Assert.AreNotEqual(107, actualAmount);
            }

            [TestMethod]
            public void TestConvertCurrency_100_Euro_Return_1186_Kronor()
            {
                // Arrange
                //private static double _eurKronorCur = 11.68;
                double amount = 100;
                var fromAccount = CreateAccount("EURO");
                var toAccount = CreateAccount("KRONOR");
                double expectedAmount = amount * Currency.EURKRONORCUR;

                // Act
                double actualAmount = Currency.ConvertCurrency(amount, fromAccount, toAccount);

                // Assert
                Assert.AreEqual(expectedAmount, actualAmount);
                Assert.AreEqual(1168, actualAmount);
            }

            [TestMethod]
            public void TestConvertCurrency_100_Kronor_Return_10_USD()
            {
                // Arrange
                //private static double _kronorUsdCur = 0.1;
                double amount = 100;
                var fromAccount = CreateAccount("KRONOR");
                var toAccount = CreateAccount("USD");
                double expectedAmount = amount * Currency.KRONORUSDCUR;

                // Act
                double actualAmount = Currency.ConvertCurrency(amount, fromAccount, toAccount);

                // Assert
                Assert.AreEqual(expectedAmount, actualAmount);
                Assert.AreEqual(10, actualAmount);
            }

            [TestMethod]
            public void TestConvertCurrency_100_Kronor_Return_0_91Euro()
            {
                // Arrange
                //private static double _kronorEurCur = 0.091
                double amount = 100;
                var fromAccount = CreateAccount("KRONOR");
                var toAccount = CreateAccount("EURO");
                double expectedAmount = amount * Currency.KRONOREURCUR;

                // Act
                double actualAmount = Currency.ConvertCurrency(amount, fromAccount, toAccount);

                // Assert
                Assert.AreEqual(expectedAmount, actualAmount);
                Assert.AreEqual(9.1, actualAmount);
            }

            [TestMethod]
            public void TestConvertCurrency_SameCurrency_100_USD_Return_100_USD()
            {
                // Arrange
                double amount = 100;
                var fromAccount = CreateAccount("USD");
                var toAccount = CreateAccount("USD");
                double expectedAmount = amount;

                // Act
                double actualAmount = Currency.ConvertCurrency(amount, fromAccount, toAccount);

                // Assert
                Assert.AreEqual(expectedAmount, actualAmount);
                Assert.AreEqual(100, actualAmount);
            }
        }
        [TestClass]
        public class ISValidBorrowAmountTest
        {
            [TestMethod]
            public void TestIsValidBorrowAmount_ValidAmount_Return_True()
            {
                // Arrange
                double borrowAmount = 100;
                double remainingBorrowLimit = 200;

                // Act
                bool result = Currency.IsValidBorrowAmount(borrowAmount, remainingBorrowLimit);

                // Assert
                Assert.IsTrue(result);
            }

            [TestMethod]
            public void TestIsValidBorrowAmount_AmountLessThanOrEqualToZero()
            {
                // Arrange
                double borrowAmount = -3;
                double remainingBorrowLimit = 200;

                // Act
                bool result = Currency.IsValidBorrowAmount(borrowAmount, remainingBorrowLimit);

                // Assert
                Assert.IsFalse(result);
            }

            [TestMethod]
            public void TestIsValidBorrowAmount_AmountGreaterThanRemainingLimit()
            {
                // Arrange
                double borrowAmount = 300;
                double remainingBorrowLimit = 200;

                // Act
                bool result = Currency.IsValidBorrowAmount(borrowAmount, remainingBorrowLimit);

                // Assert
                Assert.IsFalse(result);
            }
        }
    }
}