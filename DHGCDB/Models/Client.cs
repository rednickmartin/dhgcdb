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
      Products = new List<Product>();
    }

    public int ID { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Person> Persons { get; set; }

    public virtual Address Address { get; set; }

    public string Individuals { get { return string.Join(" & ", Persons.Select(x => x.ToString())); } }

    public virtual ICollection<Review> Reviews { get; set; }

    public virtual ICollection<Product> Products { get; set; }

    public IEnumerable<Product> JointProducts
    {
      get
      {
        return Products.Where(x => x.Person == null);
      }
    }
  }
}