using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DHGCDB.Models
{
  public class FundSelection
  {
    public FundSelection()
    {
      Funds = new List<Fund>();
    }

    public int ID { get; set; }

    public string Name { get; set; }

    [Display(Name = "Date Created")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime DateCreated { get; set; }

    public virtual ICollection<Fund> Funds { get; set; }
  }
}