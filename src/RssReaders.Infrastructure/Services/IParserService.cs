using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using MongoDB.Bson;
using RssReaders.Core.Model;

namespace RssReaders.Infrastructure.Services
{
    public interface IParserService
    {
        Task CreateChannel(ObjectId id, string title, string description, string link, string language, string copyright, DateTime lastBuidlDate,
            DateTime pubDate, ICollection<Item> items);
        IEnumerable<Item> ParseRss (string url);
        string getRoot (XmlDocument xmlDocument);
        IEnumerable<Item> atomToFeed (XmlNode xmlNode, XmlNamespaceManager nmmgr);
        Channel[] getAtomChannels (XmlNode xmlNode, XmlNamespaceManager nmmgr);
        IEnumerable<Item> rssToFeed (XmlNode xmlNode);
        IEnumerable<Channel> getRssChannels (XmlNodeList xmlNodeList);
        Item[] getAtomItems (XmlNodeList xmlNodeList, XmlNamespaceManager nmmgr);
        Item[] getRssItems (XmlNodeList xmlNodeList);
        DateTime getRssDate (string innerText);
        DateTime getDateFromXmlNode (XmlNode node, string label, XmlNamespaceManager nmmgr = null);
        string getStringFromXmlNode (XmlNode node, string label, XmlNamespaceManager nmmgr = null); 
    }
}