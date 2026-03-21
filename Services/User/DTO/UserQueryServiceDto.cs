
    using Microsoft.AspNetCore.Mvc;
    public class UserQueryServiceDto
    {
        public int Count { get; set; } = 50;

        public int Offset { get; set; } = 0;

        public string? Search { get; set; }

        public short? RoleId { get; set; }  
        public short? MinAge { get; set; }      
        public short? MaxAge { get; set; }      

        public string? SortBy { get; set; }      
        public bool IsDescending { get; set; }
        
    }