using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AdminApp.Model
{
    public class FeedParser
    {


        public static Channel[] FetchChannels(string url)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(url);

            string rootName = getRoot(xmlDocument);



            if (rootName == "rss")
            {
                return rssToChannels(xmlDocument.SelectSingleNode(rootName));
            }
            else if (rootName == "feed")
            {
                var nmmgr = new XmlNamespaceManager(xmlDocument.NameTable);
                nmmgr.AddNamespace("x", @"http://www.w3.org/2005/Atom");

                return atomToChannels(xmlDocument.SelectSingleNode(rootName), nmmgr);
            }
            else
            {
                return new Channel[0];
            }

        }

        protected static string getRoot(XmlDocument xmlDocument)
        {
            return xmlDocument.DocumentElement.Name;
        }


        protected static Channel[] atomToChannels(XmlNode xmlNode, XmlNamespaceManager nmmgr)
        {
            return getAtomChannels(xmlNode, nmmgr);
        }

        protected static Channel[] getAtomChannels(XmlNode xmlNode, XmlNamespaceManager nmmgr)
        {
            var channels = new List<Channel>();

            var nodes = xmlNode.SelectNodes("x:entry", nmmgr);
            Item[] items = nodes != null ? getAtomItems(nodes, nmmgr) : new Item[0];

            string link = xmlNode.SelectSingleNode("//x:link[@type='text/html']", nmmgr).Attributes.GetNamedItem("href").InnerText;

            channels.Add(new Channel
            {
                Title = getStringFromXmlNode(xmlNode, "x:title", nmmgr),
                Copyright = "",
                Description = getStringFromXmlNode(xmlNode, "x:subtitle", nmmgr),
                Link = link,
                Language = "",
                LastBuildDate = getDateFromXmlNode(xmlNode, "x:updated", nmmgr),
                PubDate = new DateTime(),
                Items = items,
            });

            return channels.ToArray();
        }


        protected static Channel[] rssToChannels(XmlNode xmlNode)
        {
            return getRssChannels(xmlNode.SelectNodes("channel"));
        }




        protected static Channel[] getRssChannels(XmlNodeList xmlNodeList)
        {
            var channels = new List<Channel>();

            foreach (XmlNode channel in xmlNodeList)
            {
                var nodes = channel.SelectNodes("item");
                Item[] items = nodes != null ? getRssItems(nodes) : new Item[0];

                channels.Add(new Channel
                {
                    Copyright = getStringFromXmlNode(channel, "copyright"),
                    Description = getStringFromXmlNode(channel, "description"),
                    Items = items,
                    Language = getStringFromXmlNode(channel, "language"),
                    LastBuildDate = getDateFromXmlNode(channel, "lastBuildDate"),
                    Link = getStringFromXmlNode(channel, "link"),
                    PubDate = getDateFromXmlNode(channel, "pubDate"),
                    Title = getStringFromXmlNode(channel, "title")
                });
            }

            return channels.ToArray();
        }



        protected static Item[] getAtomItems(XmlNodeList xmlNodeList, XmlNamespaceManager nmmgr)
        {
            var items = new List<Item>();

            foreach (XmlNode item in xmlNodeList)
            {
                if (item.ChildNodes.Count == 0)
                {
                    continue;
                }

                string link = item.SelectSingleNode("//x:link[@type='text/html']", nmmgr).Attributes.GetNamedItem("href").InnerText;

                items.Add(new Item
                {
                    Title = item.SelectSingleNode("x:title", nmmgr).InnerText,
                    Description = item.SelectSingleNode("x:summary", nmmgr).InnerText,
                    Link = link,
                    PubDate = getRssDate(item.SelectSingleNode("x:updated", nmmgr).InnerText),
                });
            }

            return items.ToArray();
        }


        protected static Item[] getRssItems(XmlNodeList xmlNodeList)
        {
            var items = new List<Item>();

            foreach (XmlNode item in xmlNodeList)
            {
                if (item.ChildNodes.Count == 0)
                {
                    continue;
                }

                items.Add(new Item
                {
                    Description = item.SelectSingleNode("description").InnerText,
                    Link = item.SelectSingleNode("link").InnerText,
                    PubDate = getRssDate(item.SelectSingleNode("pubDate").InnerText),
                    Title = item.SelectSingleNode("title").InnerText
                });
            }

            return items.ToArray();
        }

        protected static DateTime getRssDate(string innerText)
        {
            return DateTime.Parse(innerText, null, System.Globalization.DateTimeStyles.RoundtripKind);
        }

        protected static DateTime getDateFromXmlNode(XmlNode node, string label, XmlNamespaceManager nmmgr = null)
        {
            XmlNode child;

            if (nmmgr != null)
            {
                child = node.SelectSingleNode(label, nmmgr);
            }
            else
            {
                child = node.SelectSingleNode(label);
            }

            return child != null ? getRssDate(child.InnerText) : new DateTime();
        }

        protected static string getStringFromXmlNode(XmlNode node, string label, XmlNamespaceManager nmmgr = null)
        {
            XmlNode child;

            if (nmmgr != null)
            {
                child = node.SelectSingleNode(label, nmmgr);
            }
            else
            {
                child = node.SelectSingleNode(label);
            }

            return child != null ? child.InnerText : "";
        }












    }
}
