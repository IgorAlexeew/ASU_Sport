namespace ASUSport.Models
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual SportObject SportObject { get; set; }
        public int Duration { get; set; }
    }
}
