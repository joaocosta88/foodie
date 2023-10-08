using System.Text;
using System.Web;

namespace Foodie.Emails.Utils {
	public static class EncodingUtils {
		public static string EncodeAccountEditToken(string email, string accountConfirmationToken)
		{
			byte[] texAsBytes = Encoding.ASCII.GetBytes($"{email}:{accountConfirmationToken}");
			string base64 = Convert.ToBase64String(texAsBytes);
			return HttpUtility.UrlEncode(base64);
		}

		public static (string email, string accountConfirmationToken) DecodeAccountEditToken(string token)
		{
			var urlDecoded = HttpUtility.UrlDecode(token);
			byte[] base64Decoded = Convert.FromBase64String(urlDecoded);
			string text = Encoding.ASCII.GetString(base64Decoded);
			
			return (text.Split(":")[0], text.Split(":")[1]);
		}
	}
}
