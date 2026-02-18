namespace ASPGymCentre.Models
{
    public class PlanCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Plan> Plans { get; set; }
    }
}

