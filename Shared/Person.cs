using System.ComponentModel.DataAnnotations;

namespace BlazorMovies.Shared
{
    public abstract class Person
    {
        [Key]
        public string PersonId { get; set; }
        public string Name { get; set; }
        public Person(string personId, string name)
        {
            PersonId = personId;
            Name = name;
        }
    }
}