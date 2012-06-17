using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;

namespace RSSReader.Models
{
    public class Rss
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public static class RssReader
    {
        private static string _blogRssUrl = "http://pieux.diandian.com/rss";

        public static IEnumerable<Rss> GetRssFeed()
        {
            XDocument feedXml = XDocument.Load(_blogRssUrl);
            var feeds = from feed in feedXml.Descendants("item")
                        select new Rss
                        {
                            Title = feed.Element("title").Value,
                            Link = feed.Element("link").Value,
                            Description = Regex.Match(feed.Element("description").Value, @"^.{1,180}\b(?<!\s)").Value
                        };
            return feeds;
        }
    }
}