using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Project1.Data;
using Project1.Models;

namespace Project1.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // CreateMap<Student, StudentDTO>();
            // CreateMap<StudentDTO, Student>().ReverseMap().ForMember(n => n.StudentName, opt => opt.Ignore());
            // CreateMap<StudentDTO, Student>().ForMember(n => n.StudentName, opt => opt.MapFrom(x => x.Name)).ReverseMap();
            CreateMap<StudentDTO, Student>().ReverseMap();
        }
    }
}