using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using MongoDB.Bson;
using RssReaders.Core.Model;
using RssReaders.Core.Repositories;

namespace RssReaders.Infrastructure.Services {
    public class ParserService : IParserService
    {
        private readonly IChannelRepository _channelRepository;
        private readonly IAddressRepository _addressRepository;
        public ParserService(IChannelRepository channelRepository, IAddressRepository addressRepository)
        {
            _channelRepository = channelRepository;
            _addressRepository = addressRepository;
        }

        public async Task CreateChannel(ObjectId id, string title, string description, string link, string language, 
            string copyright, DateTime lastBuidlDate, DateTime pubDate, ICollection<Item> items)
        {
            var url = _addressRepository.GetLink(id);
            var temp = ParseRss(url);

        }

        public IEnumerable<Item> ParseRss (string url) {

            XmlDocument xmlDocument = new XmlDocument ();
            xmlDocument.Load (url);

            string rootName = getRoot (xmlDocument);

            if (rootName == "rss") {
                return rssToFeed (xmlDocument.SelectSingleNode (rootName));
            } else if (rootName == "feed") {
                var nmmgr = new XmlNamespaceManager (xmlDocument.NameTable);
                nmmgr.AddNamespace ("x", @"http://www.w3.org/2005/Atom");

                return atomToFeed (xmlDocument.SelectSingleNode (rootName), nmmgr);
            } else {
                return new List<Item>();
            }
        }

        public string getRoot (XmlDocument xmlDocument) 
            =>xmlDocument.DocumentElement.Name;
        

        public IEnumerable<Item> atomToFeed (XmlNode xmlNode, XmlNamespaceManager nmmgr) 
            => getAtomChannels (xmlNode, nmmgr).SelectMany (x => x.Items);
        

        public Channel[] getAtomChannels (XmlNode xmlNode, XmlNamespaceManager nmmgr) {
            var channels = new List<Channel> ();

            var nodes = xmlNode.SelectNodes ("x:entry", nmmgr);
            Item[] items = nodes != null ? getAtomItems (nodes, nmmgr) : new Item[0];

            string link = xmlNode.SelectSingleNode ("//x:link[@type='text/html']", nmmgr).Attributes.GetNamedItem ("href").InnerText;

            channels.Add (new Channel(ObjectId.GenerateNewId(), 
            getStringFromXmlNode (xmlNode, "x:title", nmmgr),
            getStringFromXmlNode (xmlNode, "x:subtitle", nmmgr),
            link, "", "",
            getDateFromXmlNode (xmlNode, "x:updated", nmmgr), 
            new DateTime (),
            items));

            return channels.ToArray ();
        }

        public IEnumerable<Item> rssToFeed (XmlNode xmlNode) 
            => getRssChannels (xmlNode.SelectNodes ("channel")).SelectMany (x => x.Items);

        public IEnumerable<Channel> getRssChannels (XmlNodeList xmlNodeList) 
        {
            var channels = new List<Channel> ();

            foreach (XmlNode channel in xmlNodeList) {
                var nodes = channel.SelectNodes ("item");
                Item[] items = nodes != null ? getRssItems (nodes) : new Item[0];

                channels.Add (new Channel (ObjectId.GenerateNewId(),
                getStringFromXmlNode (channel, "title"), 
                getStringFromXmlNode (channel, "description"),
                getStringFromXmlNode (channel, "link"), 
                getStringFromXmlNode (channel, "language"), 
                getStringFromXmlNode (channel, "copyright"), 
                getDateFromXmlNode (channel, "lastBuildDate"), 
                getDateFromXmlNode (channel, "pubDate"),items ));
            }

            return channels;
        }

        public Item[] getAtomItems (XmlNodeList xmlNodeList, XmlNamespaceManager nmmgr)
        {
            var items = new List<Item> ();

            foreach (XmlNode item in xmlNodeList) {
                if (item.ChildNodes.Count == 0) {
                    continue;
                }

                string link = item.SelectSingleNode ("//x:link[@type='text/html']", nmmgr).Attributes.GetNamedItem ("href").InnerText;

                items.Add(new Item(ObjectId.GenerateNewId(), 
                item.SelectSingleNode ("x:title", nmmgr).InnerText,
                item.SelectSingleNode ("x:summary", nmmgr).InnerText, 
                link, 
                getRssDate (item.SelectSingleNode ("x:updated", nmmgr).InnerText)));
            }
            return items.ToArray ();
        }

        public Item[] getRssItems (XmlNodeList xmlNodeList) 
        {
            var items = new List<Item> ();

            foreach (XmlNode item in xmlNodeList) {
                if (item.ChildNodes.Count == 0) {
                    continue;
                }

                items.Add( new Item (ObjectId.GenerateNewId(), 
                item.SelectSingleNode ("title").InnerText,
                item.SelectSingleNode ("link").InnerText,
                item.SelectSingleNode ("description").InnerText,
                getRssDate (item.SelectSingleNode ("pubDate").InnerText)));
            }
            return items.ToArray ();
        }

        public DateTime getRssDate (string innerText) 
            => DateTime.Parse (innerText, null, System.Globalization.DateTimeStyles.RoundtripKind);
        

        public DateTime getDateFromXmlNode (XmlNode node, string label, XmlNamespaceManager nmmgr = null) {
            XmlNode child;

            if (nmmgr != null) {
                child = node.SelectSingleNode (label, nmmgr);
            } else {
                child = node.SelectSingleNode (label);
            }

            return child != null ? getRssDate (child.InnerText) : new DateTime ();
        }

        public string getStringFromXmlNode (XmlNode node, string label, XmlNamespaceManager nmmgr = null) {
            XmlNode child;

            if (nmmgr != null) {
                child = node.SelectSingleNode (label, nmmgr);
            } else {
                child = node.SelectSingleNode (label);
            }

            return child != null ? child.InnerText : "";
        }

       
    }
}