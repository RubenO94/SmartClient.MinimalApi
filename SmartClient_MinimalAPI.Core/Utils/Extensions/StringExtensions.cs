using HtmlAgilityPack;
using System.Web;

namespace SmartClient.MinimalAPI.Core.Utils.Extensions
{
    public static class StringExtensions
    {
        public static string TextFromHtml(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;

            var result = "";
            HtmlDocument doc = new()
            {
                OptionFixNestedTags = true
            };
            doc.LoadHtml(value);

            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//text()"))
                result += HttpUtility.HtmlDecode(node.InnerText) + Environment.NewLine;

            return result;
        }
    }
}
