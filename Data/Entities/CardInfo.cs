using System;

namespace Data.Entities
{
    public class CardInfo
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string PinCode { get; set; }
        public long Balance { get; set; }
        public bool Active { get; set; }
    }
}
