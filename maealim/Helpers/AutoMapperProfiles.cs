using AutoMapper;
using maealim.Models;
using maealim.ModelViews.EmployeeAttends;
using maealim.ModelViews.GuestReservations;
using maealim.ModelViews.GuidesAttends;
using maealim.ModelViews.MessageSends;
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
            CreateMap<MessageSend, MessageSendCMV>().ReverseMap();
            CreateMap<GuestReservation, AddEditShikh>().ReverseMap();
            CreateMap<Attend, EmployeeAttendCMV>().ReverseMap();
            CreateMap<Attend, GuidesAttendsCMV>().ReverseMap();


        }
    }
}
