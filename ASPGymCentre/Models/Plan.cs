using System.ComponentModel.DataAnnotations.Schema;

namespace ASPGymCentre.Models
{
    public class Plan
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int PlanCategoryID { get; set; }
        public PlanCategory PlansCategories { get; set; }

        public string Description { get; set; }
        public string Image { get; set; }

        [Column(TypeName ="decimal(10,2)")]
        public decimal PriceSingleWorkout { get; set; }
        public DateTime RegisteredDate { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
    }
}
