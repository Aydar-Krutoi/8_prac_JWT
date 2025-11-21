namespace _8_prac_JWT.Requests
{
    public class PutCategoryRequest
    {
        public int Id { get; set; }
        public string Category_name { get; set; }
        public int User_id { get; set; }
    }
}
