using Foodie.Emails.Utils;

namespace Foodie.Api.Tests {
	[TestClass]
	public class EncodingUtilsTests {
		[TestMethod]
		public void EncodingUtils_EncodeAccountConfirmationToken_ReturnsCorrectValues()
		{
			//arrange
			string email = Guid.NewGuid().ToString();
			string accountConfirmationToken = Guid.NewGuid().ToString();

			//act
			var token = EncodingUtils.EncodeAccountConfirmationToken(email, accountConfirmationToken);
			var decodeResult = EncodingUtils.DecodeAccountConfirmationToken(token);

			//assert
			Assert.AreEqual(email, decodeResult.email);
			Assert.AreEqual(accountConfirmationToken, decodeResult.accountConfirmationToken);
		}
	}
}