namespace ASPGymCentre.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public int PlanId { get; set; }//fk
        public Plan Plans { get; set; }//fk
        public string Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int InstructorId { get; set; }//fk
        public Instructor Instructors { get; set; }//fk
        public DateTime RegisteredDate { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
