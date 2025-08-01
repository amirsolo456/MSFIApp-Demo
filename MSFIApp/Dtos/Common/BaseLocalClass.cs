namespace MSFIApp.Dtos.Common
{
    public record ExpansionPannelRow
    {
        public int? ID { get; set; }
        public string? Text { get; set; }
        public event EventHandler<ExpansionPannelRow> OnClick;
    }
}
