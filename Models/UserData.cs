namespace ASUSport.Models
{
    public class UserData
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public virtual User User { get;  set; }
    }
}
