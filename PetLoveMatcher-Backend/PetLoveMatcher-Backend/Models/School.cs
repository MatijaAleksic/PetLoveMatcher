using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using PetLoveMatcher_Backend.Models;

namespace DtoNetProject.Models
{
    public class School
    {
        //[ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        //ManyToOne
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public ICollection<User>? Students { get; set; }

        public School() { }

        public School(string name)
        {
            Name = name;

            Students = new List<User>();
        }
    }
}
