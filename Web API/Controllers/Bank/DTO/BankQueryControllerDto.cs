
    using Microsoft.AspNetCore.Mvc;
    public class BankQueryControllerDto
    {
        public int Count { get; set; } = 50;

        public int Offset { get; set; } = 0;
        
    }