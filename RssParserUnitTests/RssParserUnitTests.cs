using RssParser.RssParserModel.FeedMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Xunit;

namespace RssParserUnitTests
{
    public class RssParserUnitTests : RssParser.RssParserModel.RssParser
    {
        string _rssFeedPathTvn = @"D:\Studia\INFORMATYKA 6 semestr\Projekt - zespo³owe przedsiêwziêcie programistyczne\Projekt 1\RssReader\RssParserUnitTests\TestData\RssFeed_tvn24.xml";
        string _atomFeedPath = @"D:\Studia\INFORMATYKA 6 semestr\Projekt - zespo³owe przedsiêwziêcie programistyczne\Projekt 1\RssReader\RssParserUnitTests\TestData\AtomFeed_wikipedia.xml";
        string _rssFeedPathRmf = @"D:\Studia\INFORMATYKA 6 semestr\Projekt - zespo³owe przedsiêwziêcie programistyczne\Projekt 1\RssReader\RssParserUnitTests\TestData\rmf_feed.xml";
        string _rssFeedPathTlumaczenia = @"D:\Studia\INFORMATYKA 6 semestr\Projekt - zespo³owe przedsiêwziêcie programistyczne\Projekt 1\RssReader\RssParserUnitTests\TestData\tlumaczenia.rss";


        [Fact]
        public void getRssItemDateTest_parseDateString_excerptYear_areEqual_true()
        {
            string stringDate = @"Tue, 02 Apr 19 13:42:00 +0200";

            var date = getRssDate(stringDate);

            int actual = date.Year;
            int expected = 2019;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void getRssItemDateTest_parseDateString_excerptDay_areEqual_true()
        {
            string stringDate = @"Wed, 03 Apr 2019 10:41:11 +0200";

            var date = getRssDate(stringDate);

            int actual = date.Day;
            int expected = 3;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void xmlDocumentToRssFeedTest_getTitle_equalTrue()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(_rssFeedPathTvn);

            var rssFeed = rssToFeed(xmlDocument.SelectSingleNode("rss"));

            string actual = rssFeed.Channels[0].Title;
            string expected = "TVN24.pl - biznes gospodarka";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void xmlDocumentToRssFeedTest_getItemsNumber_true()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(_rssFeedPathTvn);

            var rssFeed = rssToFeed(xmlDocument.SelectSingleNode("rss"));

            int numberOfItems = rssFeed.Channels[0].Items.Count;
            bool condition = numberOfItems > 0;

            Assert.True(condition);
        }

        [Fact]
        public void xmlDocumentToRssFeedTest_get2ItemTitle_equalTrue()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(_rssFeedPathTvn);

            var rssFeed = rssToFeed(xmlDocument.SelectSingleNode("rss"));

            string title = rssFeed.Channels[0].Items.ToList()[1].Title;

            string actual = title;
            string expected = "Dwóch turystów na jednego mieszkañca. Miasto ogranicza wynajem";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void xmlDocumentToRssFeedTest_get1ItemPubDate_equalTrue()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(_rssFeedPathTvn);

            var rssFeed = rssToFeed(xmlDocument.SelectSingleNode("rss"));

            var pubDate = rssFeed.Channels[0].Items.ToList()[1].PubDate;

            var actual = pubDate;
            var expected = DateTime.Parse("Thu, 28 Mar 19 03:57:35 +0100");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void xmlDocumentToRssFeedTest_get3rdItemLink_equalTrue()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(_rssFeedPathTvn);

            var rssFeed = rssToFeed(xmlDocument.SelectSingleNode("rss"));

            string link = rssFeed.Channels[0].Items.ToList()[2].Link;

            string actual = link;
            string expected = @"https://www.tvn24.pl/wiadomosci-z-kraju,3/milion-zlotych-kary-za-naruszenie-rodo,921776.html";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void getRoot_getAtomFeed_equal_true()
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(_atomFeedPath);

            string actual = getRoot(xmlDocument);
            string expected = "feed";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void getRoot_getRssFeed_equal_true()
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(_rssFeedPathTvn);

            string actual = getRoot(xmlDocument);
            string expected = "rss";

            Assert.Equal(expected, actual);
        }
                          
        [Fact]
        public void xmlDocumentToAtomFeedTest_getItemsNumber_true()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(_atomFeedPath);

            var nmmgr = new XmlNamespaceManager(xmlDocument.NameTable);
            nmmgr.AddNamespace("x", @"http://www.w3.org/2005/Atom");

            var nodes = xmlDocument.SelectSingleNode("x:feed", nmmgr);

            var atomFeed = atomToFeed(nodes, nmmgr);

            int numberOfItems = atomFeed.Channels[0].Items.Count;
            bool condition = numberOfItems > 0;

            Assert.True(condition);
        }

        [Fact]
        public void xmlDocumentToAtomFeedTest_get1ItemTitle_equalTrue()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(_atomFeedPath);

            var nmmgr = new XmlNamespaceManager(xmlDocument.NameTable);
            nmmgr.AddNamespace("x", @"http://www.w3.org/2005/Atom");

            var feedNode = xmlDocument.SelectSingleNode("x:feed", nmmgr);

            var atomFeed = atomToFeed(feedNode, nmmgr);

            string title = atomFeed.Channels[0].Items.ToList()[0].Title;

            string actual = title;
            string expected = "Tytu³";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void xmlDocumentToAtomFeedTest_get1ItemPubDate_equalTrue()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(_atomFeedPath);

            var nmmgr = new XmlNamespaceManager(xmlDocument.NameTable);
            nmmgr.AddNamespace("x", @"http://www.w3.org/2005/Atom");

            var atomFeed = atomToFeed(xmlDocument.SelectSingleNode("x:feed", nmmgr), nmmgr);

            var pubDate = atomFeed.Channels[0].Items.ToList()[0].PubDate;

            var actual = pubDate;
            var expected = DateTime.Parse("2005-06-13T16:20:02Z", null, System.Globalization.DateTimeStyles.RoundtripKind);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void xmlDocumentToAtomFeedTest_get1rdItemLink_equalTrue()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(_atomFeedPath);

            var nmmgr = new XmlNamespaceManager(xmlDocument.NameTable);
            nmmgr.AddNamespace("x", @"http://www.w3.org/2005/Atom");

            var rssFeed = atomToFeed(xmlDocument.SelectSingleNode("x:feed", nmmgr), nmmgr);

            string link = rssFeed.Channels[0].Items.ToList()[0].Link;

            string actual = link;
            string expected = @"http://przyklad.pl/2003/12/13/atom03.html";

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void xmlDocumentToAtomFeedTest_getChannelTitle_equalTrue()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(_atomFeedPath);

            var nmmgr = new XmlNamespaceManager(xmlDocument.NameTable);
            nmmgr.AddNamespace("x", @"http://www.w3.org/2005/Atom");

            var atomFeed = atomToFeed(xmlDocument.SelectSingleNode("x:feed", nmmgr), nmmgr);

            string title = atomFeed.Channels[0].Title;

            string actual = title;
            string expected = @"Przyk³ad kana³u";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void xmlDocumentToRssFeedTest_getChannelTitle_equalTrue()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(_rssFeedPathTvn);

            var rssFeed = rssToFeed(xmlDocument.SelectSingleNode("rss"));

            string title = rssFeed.Channels[0].Title;

            string actual = title;
            string expected = @"TVN24.pl - biznes gospodarka";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void fetchFeedTest_rfmFeed_numberOfItems_equalTrue()
        {
            var feed = ParseRss(_rssFeedPathRmf);

            int actual = feed.Channels[0].Items.Count;
            int expected = 10;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void fetchFeedTest_rfmFeed_2ndItemTitle_equalTrue()
        {
            var feed = ParseRss(_rssFeedPathRmf);

            string actual = feed.Channels[0].Items.ToList()[1].Title;
            string expected = "Ho³d dla wielkiego artysty. Za nami wernisa¿ wystawy \"Wajda\"";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void fetchFeedTest_tlumaczenia_5thItemLink_equalTrue()
        {
            var feed = ParseRss(_rssFeedPathTlumaczenia);

            string actual = feed.Channels[0].Items.ToList()[4].Link;
            string expected = @"http://teksciory.interia.pl/ashes-of-ares-you-know-my-name-chris-cornell-cover-tekst-piosenki,t,671140.html";

            Assert.Equal(expected, actual);
        }







    }
}
