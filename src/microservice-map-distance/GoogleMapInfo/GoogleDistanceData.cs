namespace GoogleMapInfo
{
    public sealed class GoogleDistanceData
    {
        public string[] destination_addresses { get; set; }
        public string[] origin_addresses { get; set; }
        public Row[] rows { get; set; }
        public string status { get; set; }
    }

    public sealed class Row
    {
        public Element[] elements { get; set; }
    }

    public sealed class Element
    {
        public Distance distance { get; set; }
        public Duration duration { get; set; }
        public string status { set; get; }
    }

    public sealed class Distance
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public sealed class Duration
    {
        public string text { get; set; }
        public int value { get; set; }
    }
}
