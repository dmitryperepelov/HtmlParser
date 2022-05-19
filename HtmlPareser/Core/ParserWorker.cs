using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlPareser.Core
{
    class ParserWorker<T> where T : class
    {
        IParser<T> parser;
        IParserSettings parserSettings;

        HtmlLoader htmlLoader;

        bool isActive;

        #region Proerties

        public IParser<T> Parser { get; set; }

        public IParserSettings Settings
        {
            get => parserSettings;
            set
            {
                parserSettings = value;
                htmlLoader = new HtmlLoader(value);
            }
        }

        public bool IsActive => isActive;

        #endregion

        public event Action<object, T> OnNewData;

        public event Action<object> OnComplete;

        public ParserWorker(IParser<T> parser)
        {
            this.parser = parser;
        }

        public ParserWorker(IParser<T> parser, IParserSettings parserSettings) : this(parser)
        {
            this.parserSettings = parserSettings;
        }

        public void Start()
        {
            isActive = true;
            Worker();
        }

        public void Abort()
        {
            isActive = false;
        }

        private async void Worker()
        {
            for(int i = parserSettings.StartPoint; i <= parserSettings.EndPoint; i++)
            {
                if (!isActive) 
                {
                    OnComplete?.Invoke(this);
                    return;
                }

                var source = await htmlLoader.GetSourceByPageId(i);
                var domParser = new HtmlParser();

                var document = await domParser.ParseDocumentAsync(source);

                var result = parser.Parse(document);

                OnNewData?.Invoke(this, result);
            }

            OnComplete?.Invoke(this);
            isActive = false;
        }
    }
}
