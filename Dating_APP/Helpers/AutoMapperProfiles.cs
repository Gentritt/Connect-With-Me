﻿using AutoMapper;
using Dating_APP.Dtos;
using Dating_APP.Extensions;
using Dating_APP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating_APP.Helpers
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<AppUser, MemberDto>()
				.ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom
				(src => src.Photos.FirstOrDefault
				(x => x.IsMain).Url))
				.ForMember(dest=> dest.Age, opt=> opt.MapFrom(src=> src.DateOfBirth.CalculateAge())); //from app user to memberdto mapping
			CreateMap<Photo, PhotoDto>();
		}
	}
}
