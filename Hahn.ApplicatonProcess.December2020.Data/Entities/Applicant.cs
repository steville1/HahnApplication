using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hahn.ApplicatonProcess.December2020.Data.Entities
{
    public class Applicant:BusinessEntity
    {
        //[NotMapped]
        //public override int? ID { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public string FamilyName { get; set; } 
        public string Address { get; set; } 
        public string CountryOfOrigin { get; set; } 
        public string EMailAdress { get; set; } 
        public int Age { get; set; } 
        public bool Hired { get; set; } = false;


    }
}
