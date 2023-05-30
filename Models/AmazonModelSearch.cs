// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);



    public class Price2
    {
        public string raw { get; set; }
        public string name { get; set; }
    }



    public class RequestMetadata
    {
        public string amazon_url { get; set; }
    }



    public class SearchAmazon
    {
        public RequestMetadata request_metadata { get; set; }
        public List<SearchResult> search_results { get; set; }//no tocar

    }

    public class SearchResult
    {
        public int position { get; set; }
        public string title { get; set; }
        public string link { get; set; }

    }



