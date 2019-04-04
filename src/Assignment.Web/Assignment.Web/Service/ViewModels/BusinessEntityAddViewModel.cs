using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Assignment.Web.Models;
using Microsoft.AspNetCore.Http;

namespace Assignment.Web.Service.ViewModels
{
    public class BusinessEntityAddViewModel
    {
       
        public string Code { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        public string Name { get; set; }


        public string Street { get; set; }

        [MaxLength(150)]
        public string City { get; set; }

        [MaxLength(150)]
        public string State { get; set; }

        [MaxLength(50)]
        public string Zip { get; set; }

        [MaxLength(150)]
        public string Country { get; set; }

        [MaxLength(50)]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        [MaxLength(50)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }


        public string ContactPerson { get; set; }

        [MaxLength(50)]
        public string ReferredBy { get; set; }


        public string Logo { get; set; }
        
        public IFormFile LogoFile { get; set; }

        public BusinessStatus Status { get; set; }

        [Range(0,Double.MaxValue)]
        public float Balance { get; set; }
        

        [MaxLength(50)]
        public string SecurityCode { get; set; }

        public List<string> FlightApi { get; set; }
        public List<string> AgentType { get; set; }

        [MaxLength(50)]
        public string SMTPServer { get; set; }

        public int SMTPPort { get; set; }

        [MaxLength(50)]
        public string SMTPUsername { get; set; }

        [MaxLength(50)]
        public string SMTPPassword { get; set; }

        [Range(0, Double.MaxValue)]
        public float CurrentBalance { get; set; }
    }

    public class CheckBoxModel
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public bool IsChecked { get; set; }
    }

}
