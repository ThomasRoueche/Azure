cible : http://feeds.feedburner.com/nofrag_com


## Code
System.ServiceModel

string url = "http://fooblog.com/feed";
XmlReader reader = XmlReader.Create(url);
SyndicationFeed feed = SyndicationFeed.Load(reader);
reader.Close();
foreach (SyndicationItem item in feed.Items)
{
    String subject = item.Title.Text;    
    String summary = item.Summary.Text;
    ...                
}



## Ref
https://stackoverflow.com/questions/10399400/best-way-to-read-rss-feed-in-net-using-c-sharp
https://msdn.microsoft.com/en-us/library/system.servicemodel.syndication.syndicationfeed.aspx