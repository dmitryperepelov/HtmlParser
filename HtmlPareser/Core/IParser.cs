using AngleSharp.Html.Dom;

namespace HtmlPareser.Core
{
    interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document);
    }
}
