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

    [Required]
    public string Title { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string Surname { get; set; }

    [Required]
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
}