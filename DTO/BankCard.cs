namespace DTO
{
    public class BankCard
    {
        public int BankCardId { get; set; }
        public int OwnerId { get; set; }
        public string Number { get; set; }
        public int CVV { get; set; }
        public int PIN { get; set; }
    }
}
