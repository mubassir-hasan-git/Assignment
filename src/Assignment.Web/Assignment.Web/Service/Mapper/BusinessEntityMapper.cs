using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment.Web.Models;
using Assignment.Web.Service.ViewModels;
using AutoMapper;

namespace Assignment.Web.Service.Mapper
{
    public class BusinessEntityMapper:Profile
    {
        public BusinessEntityMapper()
        {
            CreateMap<BusinessEntities, BusinessEntityAddViewModel>();
            CreateMap< BusinessEntityAddViewModel, BusinessEntities>();
        }
    }
}
