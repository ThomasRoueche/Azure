using System.Net;
using System.Xml;

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
    
    while (reader.Read())
    {
        if (reader.IsStartElement())
        {
            if (reader.IsEmptyElement)
                Console.WriteLine("<{0}/>", reader.Name);
            else
            {
                Console.Write("<{0}> ", reader.Name);
                reader.Read(); // Read the start tag.
                if (reader.IsStartElement())  // Handle nested elements.
                    Console.Write("\r\n<{0}>", reader.Name);
                Console.WriteLine(reader.ReadString());  //Read the text content of the element.
            }
        }
    }


    return name == null
        ? req.CreateResponse(HttpStatusCode.BadRequest, "Tom, Please pass a name on the query string or in the request body")
        : req.CreateResponse(HttpStatusCode.OK, "Tom, Hello " + name);
}
