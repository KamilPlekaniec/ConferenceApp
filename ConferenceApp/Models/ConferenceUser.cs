using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Models
{
    public enum ConferenceType
    {
        [Display(Name = "Warsztaty")]
        Workshop,
        [Display(Name = "Wykłady")]
        Lecture,
        [Display(Name = "Dyskusje")]
        Discussion,
    }
    public class ConferenceUser
    {
        [Key]
        public int UserID { get; set; }
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        public string Email { get; set; }
        [Display(Name = "Typ konferencji")]
        public ConferenceType ConferenceType { get; set; }
        [Display(Name = "Zdjęcie")]
        public string Photo { get; set; }
    }
}
