export interface TagOption {
  emoji: string;
  text: string;
}

export const TRADE_TAGS: TagOption[] = [
  { emoji: "🎮", text: "Overtrading" },
  { emoji: "✂️", text: "Cut winner early" },
  { emoji: "🕳️", text: "Cut loser late" },
  { emoji: "🚀", text: "FOMO entry" },
  { emoji: "📉", text: "Didn’t follow plan" },
  { emoji: "✅", text: "Followed plan" },
  { emoji: "📈", text: "Too much size" },
  { emoji: "😤", text: "Revenge trade" },
  { emoji: "📝", text: "Journaled this trade" },
  { emoji: "🛡️", text: "Followed risk rules" },
];