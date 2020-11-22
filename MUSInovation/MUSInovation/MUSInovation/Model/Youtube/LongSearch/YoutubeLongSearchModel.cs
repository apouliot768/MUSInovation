using System;
using System.Collections.Generic;
using System.Text;

namespace MUSInovation.Model.Youtube.LongSearch
{
public class Snippet
{
    public DateTime publishedAt { get; set; }
    public string channelId { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public Thumbnails thumbnails { get; set; }
    public string channelTitle { get; set; }
    public string liveBroadcastContent { get; set; }
    public DateTime publishTime { get; set; }
}

public class Item
{
    public string kind { get; set; }
    public string etag { get; set; }
    public Id id { get; set; }
    public Snippet snippet { get; set; }
}

public class YoutubeLongSearchModel
{
    public string kind { get; set; }
    public string etag { get; set; }
    public string nextPageToken { get; set; }
    public string regionCode { get; set; }
    public PageInfo pageInfo { get; set; }
    public List<Item> items { get; set; }
}


}
