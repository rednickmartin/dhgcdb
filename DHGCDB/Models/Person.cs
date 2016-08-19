using System.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DHGCDB.Models
{
  public class Person
  {
    public int ID { get; set; }
    
    public virtual Client Client { get; set; }

    public string Title { get; set; }

    public string FirstName { get; set; }

    public string Surname { get; set; }

    public string Gender { get; set; }

    public bool IsPrimary { get; set; }

    [Display(Name = "Birth Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime BirthDate { get; set; }

    public override string ToString()
    {
      return string.Format("{0} {1} {2}", Title, FirstName, Surname);
    }
  }

  public class PersonDBContext : DbContext
  {
    public DbSet<Person> People { get; set; }
  }
}