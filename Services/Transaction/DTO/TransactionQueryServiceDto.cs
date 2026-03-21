
    using Microsoft.AspNetCore.Mvc;
    public class TransactionQueryServiceDto
    {
        public int Count { get; set; } = 50;

        public int Offset { get; set; } = 0;
        
    }