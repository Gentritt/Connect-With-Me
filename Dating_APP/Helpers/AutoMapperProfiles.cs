using AutoMapper;
using Dating_APP.Dtos;
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
				; //from app user to memberdto mapping
			CreateMap<Photo, PhotoDto>();
		}
	}
}
