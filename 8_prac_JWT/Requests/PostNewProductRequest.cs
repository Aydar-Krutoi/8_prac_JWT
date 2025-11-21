namespace _8_prac_JWT.Requests
{
    public class PostNewProductRequest
    {
        public string Product_name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool Is_active { get; set; }
        public int Stock { get; set; }
        public int Category_id { get; set; }
        public int User_id { get; set; }
    }
}
