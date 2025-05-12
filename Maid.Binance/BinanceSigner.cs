using System.Security.Cryptography;
using System.Text;

namespace Maid.Binance
{
	public class BinanceSigner
	{
		public static string Sign(string data, string secret) {
			var keyBytes = Encoding.UTF8.GetBytes(secret);
			var dataBytes = Encoding.UTF8.GetBytes(data);

			using var hmac = new HMACSHA256(keyBytes);
			var hashBytes = hmac.ComputeHash(dataBytes);

			return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
		}
	}
}
