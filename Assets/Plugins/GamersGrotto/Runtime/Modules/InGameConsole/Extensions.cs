namespace GamersGrotto.Runtime.Modules.InGameConsole
{
    public static class Extensions
    {
        public static string Colorize(this string text, string color) => $"<color={color}>{text}</color>";

        public static string Bold(this string text) => $"<b>{text}</b>";

        public static string Italic(this string text) => $"<i>{text}</i>";
    }
}