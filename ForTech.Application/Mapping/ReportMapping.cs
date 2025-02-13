﻿using AutoMapper;
using FonTech.Domain.Dto.Report;
using FonTech.Domain.Entity;


namespace ForTech.Application.Mapping
{
    public class ReportMapping : Profile
    {
        public ReportMapping()
        {
            CreateMap<Report, ReportDto>().ReverseMap();
        }
    }
}
