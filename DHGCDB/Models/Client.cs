using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHGCDB.Models
{
  public class Client
  {
    public Client()
    {
      Persons = new List<Person>();
      Reviews = new List<Review>();
    }

    public int ID { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Person> Persons { get; set; }

    public virtual Address Address { get; set; }

    public string Individuals { get { return string.Join(" & ", Persons.Select(x => x.ToString())); } }

    public virtual ICollection<Review> Reviews { get; set; }
  }
}