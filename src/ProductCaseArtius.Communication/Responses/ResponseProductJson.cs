namespace ProductCaseArtius.Communication.Responses
{
    public class ResponseProductJson
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}
