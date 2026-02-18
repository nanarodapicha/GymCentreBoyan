namespace ASPGymCentre.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public Client Clients { get; set; }
        public int  ExerciseId { get; set; }
        public Exercise Exercises { get; set; }
        public DateTime RegisteredDate { get; set; }
    }
}
