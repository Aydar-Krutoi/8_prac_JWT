namespace _8_prac_JWT.Requests
{
    public class PostNewOrder
    {
        public int user_id { get; set; }
        public int delivery_type_id { get; set; }
        public int payment_type_id { get; set; }
    }
}
