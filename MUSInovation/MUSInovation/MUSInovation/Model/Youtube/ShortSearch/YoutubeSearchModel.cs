using System;
using System.Collections.Generic;
using System.Text;

namespace MUSInovation.Model.Youtube.ShortSearch
{
    /// <summary>
    /// Author :        Alexandre Pouliot
    /// Description :   This class is responsible for deserialized upcoming movies from TMBd API JSON
    /// Date :          2020-11-19
    /// </summary>

    public class PageInfo
    {
        public int totalResults { get; set; }
        public int resultsPerPage { get; set; }
    }

    public class Id
    {
        public string kind { get; set; }
        public string videoId { get; set; }
    }

    public class Item
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public Id id { get; set; }
    }

    public class YoutubeSearchModel
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string nextPageToken { get; set; }
        public string regionCode { get; set; }
        public PageInfo pageInfo { get; set; }
        public IList<Item> items { get; set; }
    }
}
