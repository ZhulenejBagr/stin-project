namespace STINProject.Shared
{
    public class TransactionViewModel
    {
        public Guid TranscationId { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
        public TransactionViewModel() { }
    }
}
