using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Rdt.CourseFinder.Services
{
    public class HtmlComposer
    {
        StringBuilder _text = new StringBuilder();

        public string Text
        {
            get { return _text.ToString(); }
        }

        public HtmlComposer()
        {
        }

        public static string Link(string text, string link, bool isHead = false)
        {
            if (isHead)
            {
                return string.Format("<a style='{2}' href='{0}'>{1}</a>", link, text, BoldLarge());
            }
            return string.Format("<a href='{0}'>{1}</a>", link, text);
        }

        public static string BoldLarge()
        {
            return "font-weight:bold; font-size:13px;";
        }

        public HtmlComposer AppendLink(string text, string link)
        {
            _text.Append(Link(text, link));
            return this;
        }

        public HtmlComposer AppendLinkHead(string text, string link)
        {
            _text.Append(Link(text, link, true));
            return this;
        }

        public HtmlComposer AppendHead(string text)
        {
            _text.Append(Head(text));
            return this;
        }

        public string Head(string text)
        {
            return string.Format("<div style='font-size: 14px;font-family:Arial;font-weight: bold;padding: 6px 0;color:#36a;'>{0}</div>", text);
        }

        public static string DivEm(string text)
        {
            return string.Format("<div><em>{0}</em></div>", text);
        }

        public static string Div(string text)
        {
            return string.Format("<div style='font-family:Arial;'>{0}</div>", text);
        }

        public HtmlComposer AppendDiv(string text, bool isEm = false)
        {
            if (isEm) _text.Append(DivEm(text));
            else _text.Append(Div(text));
            return this;
        }

        public HtmlComposer AppendBr()
        {
            _text.Append("<br/>");
            return this;
        }

        public HtmlComposer AppendHr()
        {
            _text.Append("<hr style='border-top: 1px solid #ccc;border-bottom: 1px solid #f9f9f9;'/>");
            return this;
        }

        public HtmlComposer AppendHrDotted()
        {
            _text.Append("<hr style='border-top: 1px solid #e0e0e0;border-bottom: 0px solid #fff;'/>");
            return this;
        }

        public HtmlComposer AppendRaw(string text)
        {
            _text.Append(text);
            return this;
        }
    }

}