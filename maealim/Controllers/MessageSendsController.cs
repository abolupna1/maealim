using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using maealim.Data;
using maealim.Models;
using Microsoft.AspNetCore.Authorization;
using maealim.Data.Repositories;
using maealim.Extensions.Alerts;
using maealim.ModelViews.MessageSends;
using System.Security.Claims;

namespace maealim.Controllers
{
    [Route("MessageSends")]
    [Authorize(Roles = "Admin,ProgramsSupervisor")]
    public class MessageSendsController : Controller
    {
        private readonly IMaealimRepository _repository;
        string mContryNotExixit = "لاتوجد دولة بهاذه البيانات ";
        public MessageSendsController(IMaealimRepository repository)
        {
            _repository = repository;
        }


        // GET: MessageSends
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetCountries());
        }

        [Route("Create/{countryId:int}")]
        public async Task<IActionResult> Create(int countryId)
        {
            var country = await _repository.GetCountry(countryId);
            if (country== null)
            {
                return RedirectToAction(nameof(Index)).WithDanger("danger",mContryNotExixit);
            }
            var messageSend =new MessageSendCMV{
                CountryId=country.Id,
                CountryName=country.Name,
                AppUserId= getUserId(),

                
            };
            ViewData["TypesMessageId"] = new SelectList(await _repository.GetTypesMessages(),"Id","Name");
            ViewData["WjhaaMessageId"] = new SelectList(await _repository.GetWjhaaMessages(), "Id", "Message");
            
            return View(messageSend);
        }

        [Route("Create/{countryId:int}")]
        [HttpPost]
        public async Task<IActionResult> Create(int countryId, MessageSendCMV messageSendCMV)
        {
            var country = await _repository.GetCountry(countryId);
            if (country == null)
            {
                return RedirectToAction(nameof(Index)).WithDanger("danger", mContryNotExixit);
            }
          
            return View(await _repository.GetCountries());
        }
        private int getUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
