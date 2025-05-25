namespace Maid.ChatGPT
{
	public static class TokenHelper
	{
		/// <summary>
		/// Rough estimate: 1 token ≈ 4 characters for English.
		/// </summary>
		public static int EstimateTokenCount(string text) {
			return (int)Math.Ceiling((double)text.Length / 4);
		}

		/// <summary>
		/// Calculates max_tokens allowed based on model limit and input size.
		/// </summary>
		/// <param name="input">Your full input string (prompt + image captions, etc.)</param>
		/// <param name="modelLimit">Max total tokens allowed (e.g. 128000 for GPT-4 Turbo)</param>
		public static int GetSafeMaxTokens(string input, int modelLimit = 8192, int reserve = 200) {
			var estimatedInputTokens = EstimateTokenCount(input);
			var available = modelLimit - estimatedInputTokens;

			// Leave a little reserve room to avoid cutoff
			return Math.Max(256, available - reserve);
		}
	}
}
