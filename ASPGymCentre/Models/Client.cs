using Microsoft.AspNetCore.Identity;

namespace ASPGymCentre.Models
{
    public class Client : IdentityUser
    {
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public DateTime RegisteredDate { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
