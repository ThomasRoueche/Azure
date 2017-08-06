using System.Net;
using System.ServiceModel.Syndication;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");

    // parse query parameter
    string name = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
        .Value;

    // Get request body
    dynamic data = await req.Content.ReadAsAsync<object>();

    // Set name to query string or body data
    name = name ?? data?.name;

    string url = "http://feeds.feedburner.com/nofrag_com";
    XmlReader reader = XmlReader.Create(url);
    SyndicationFeed feed = SyndicationFeed.Load(reader);
    reader.Close();
    foreach (SyndicationItem item in feed.Items)
    {
        name += " ";
        name += item.Title.Text;
    }




    return name == null
        ? req.CreateResponse(HttpStatusCode.BadRequest, "Tom, Please pass a name on the query string or in the request body")
        : req.CreateResponse(HttpStatusCode.OK, "Tom, Hello " + name);
}
