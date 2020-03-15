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
using AutoMapper;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace maealim.Controllers
{
    [Route("MessageSends")]
    [Authorize(Roles = "Admin,ProgramsSupervisor")]
    public class MessageSendsController : Controller
    {
        private readonly IMaealimRepository _repository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        string mContryNotExixit = "لاتوجد دولة بهاذه البيانات ";
        string sMessage = "تم ارسال الايميلات بنجاح";
        public MessageSendsController(IMaealimRepository repository,
            IMapper mapper, IEmailSender emailSender)
        {
            _repository = repository;
            _mapper = mapper;
            _emailSender = emailSender;
        }


        // GET: MessageSends
        public async Task<IActionResult> Index()
        {
            var countryMessages = new List<CountryMessages>();
           var countries = await _repository.GetCountries();
            foreach (var country in countries)
            {
                var c = new CountryMessages {
                    CountryId = country.Id,
                    CountryName = country.Name,
                    MesagesSentCount = await _repository.CountMessagesSentsToCountry(country.Id)
                };
            countryMessages.Add(c);
            }
            return View(countryMessages);
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

            if (ModelState.IsValid) 
            {
                var message = _mapper.Map<MessageSend>(messageSendCMV);
                var wjahs = await _repository.GetNotablesByCountry(countryId);
                var typeMessage = await _repository.GetTypesMessage(messageSendCMV.TypesMessageId);
                var messageSend = await _repository.GetWjhaaMessage(messageSendCMV.WjhaaMessageId);
                foreach (var wjeeh in wjahs)
                {
                    message.NotableId = wjeeh.Id;
                    message.AppUserId = getUserId();
                    _repository.Add<MessageSend>(message);
                    await _emailSender.SendEmailAsync(wjeeh.Email, "رسالة" + typeMessage.Name, messageSend.Message);

                }
                await _repository.SavaAll();
                return RedirectToAction(nameof(Index)).WithSuccess("success", sMessage);

            }

            return View(await _repository.GetCountries());
        }
        private int getUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
