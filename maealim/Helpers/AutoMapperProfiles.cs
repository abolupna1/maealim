using AutoMapper;
using maealim.Models;
using maealim.ModelViews.WorkSeasons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maealim.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<WorkSeason, WorkSeasonEMV>().ReverseMap();
         

        }
    }
}
