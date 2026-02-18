namespace ASPGymCentre.Models
{
    public class Plan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryID { get; set; }
        public PlanCategory PlansCategories { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double PriceSingleWorkout { get; set; }
        public DateTime RegisteredDate { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
    }
}
