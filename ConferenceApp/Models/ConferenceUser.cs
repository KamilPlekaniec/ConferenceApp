using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Models
{
    public enum ConferenceType
    {
        Workshop,
        Lecture,
        Discussion,
    }
    public class ConferenceUser
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ConferenceType ConferenceType { get; set; }
        public string Photo { get; set; }
    }
}
