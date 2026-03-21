
    using Microsoft.AspNetCore.Mvc;
    public class BudgetQueryServiceDto
    {
        public int Count { get; set; } = 50;
        public int Offset { get; set; } = 0;
        public string? Search { get; set; }
        public long? FamilyId { get; set; }
        public long? CreatedById { get; set; }
        public short? CategoryId { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; }
        
    }