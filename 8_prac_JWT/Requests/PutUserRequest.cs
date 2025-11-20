namespace _8_prac_JWT.Requests
{
    public class PutUserRequest
    {
        public int Id_check { get; set; }
        public string User_fullname { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Login_N { get; set; }
        public string Password_N { get; set; }
    }
}
