using HtmlPareser.Core;
using HtmlPareser.Core.Habra;
using System;

namespace HtmlPareser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ParserWorker<string[]> parser = new ParserWorker<string[]>(new HabraParser());
            int startPoint, endPoint;

            Int32.TryParse(Console.ReadLine(), out int i1);
            Int32.TryParse(Console.ReadLine(), out int i2);
            startPoint = i1;
            endPoint = i2;

            parser.Settings = new HabraSettings(i1, i2);
            parser.OnComplete += Parser_OnComplete;
            parser.OnNewData += Parser_OnNewData;
            parser.Start();
            Console.ReadKey();
        }

        private static void Parser_OnNewData(object arg1, string[] arg2)
        {
            foreach(string arg in arg2)
            {
                Console.WriteLine(arg);
            } 
        }

        private static void Parser_OnComplete(object obj)
        {
            Console.WriteLine();
        }
    }
}
