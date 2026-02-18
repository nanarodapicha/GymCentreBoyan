namespace ASPGymCentre.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegisteredDate { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
    }
}
