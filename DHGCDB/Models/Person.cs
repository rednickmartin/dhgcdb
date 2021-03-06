﻿using System.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;

namespace DHGCDB.Models
{
  public class Person
  {
    public Person()
    {
      AttitudeToRiskHistory = new List<PersonsAttitudeToRisk>();
      PersonProducts = new List<Product>();
    }

    public int ID { get; set; }
    
    public virtual Client Client { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    public string Surname { get; set; }

    [Required]
    public string Gender { get; set; }

    public bool IsPrimary { get; set; }

    [Display(Name = "Individuals Products")]
    public virtual ICollection<Product> PersonProducts { get; set; }

    [Display(Name = "Birth Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime BirthDate { get; set; }

    public virtual ICollection<PersonsAttitudeToRisk> AttitudeToRiskHistory { get; set; }

    public IEnumerable<AttitudeToRiskCategory> AttitudeToRiskCategories
    {
      get
      {
        return AttitudeToRiskHistory.Select(x => x.AttitudeToRiskCategory).Distinct();
      }
    }

    public PersonsAttitudeToRisk CurrentPersonsAttitudeToRisk(AttitudeToRiskCategory category)
    {
      return AttitudeToRiskHistory.Where(x => x.AttitudeToRiskCategory.Equals(category)).OrderBy(x => x.FromDate).Last();
    }

    public IEnumerable<PersonsAttitudeToRisk> AllCurrentAttitudeToRisk
    {
      get
      {
        var currentATRs = new List<PersonsAttitudeToRisk>();
        foreach(var category in AttitudeToRiskCategories) {
          currentATRs.Add(CurrentPersonsAttitudeToRisk(category));
        }

        return currentATRs;
      }
    }

    public IEnumerable<PersonsAttitudeToRisk> PreviousAttitudeToRisk
    {
      get
      {
        return AttitudeToRiskHistory.Except(AllCurrentAttitudeToRisk);
      }
    }

    public bool MissingATR(out AttitudeToRiskCategory atrCategory)
    {
      foreach (var product in PersonProducts) {
        var thisAtrCat = product.AttitudeToRiskCategory;
        if(!AttitudeToRiskCategories.Contains(thisAtrCat)) {
          atrCategory = thisAtrCat;
          return true;
        }
      }

      // Cycled through all products, they all have an ATR for their category
      atrCategory = null;
      return false;
    }

    public override string ToString()
    {
      return string.Format("{0} {1} {2}", Title, FirstName, Surname);
    }
  }
}