namespace _8_prac_JWT.Requests
{
    public class GetAllProductRequest
    {
        public string Filter_by_category { get; set; }
        public string Sort_by_price { get; set; }
        public string Sort_by_date { get; set; }
        public int Min_price { get; set; }
        public int Max_price { get; set; }
        public bool In_stock { get; set; }
    }
}
