using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using TCRC.BusinessLogic.Services;
using TCRC.Domain.DTOs;
using TCRC.Models;
using AutoMapper;

namespace TCRC.Controllers
{
    public class HomeController : BaseController
    {
        #region Members
        private readonly IMemberService memberService;
        #endregion

        #region Methods
        /// <summary>
        /// Home controller constructor
        /// </summary>
        /// <param name="memberService">The member service</param>
        public HomeController(IMemberService memberService)
        {
            this.memberService = memberService;
        }

        /// <summary>
        /// Agency search view
        /// </summary>
        public ActionResult AgencySearch()
        {
            return View();
        }

        /// <summary>
        /// Search agencies
        /// </summary>
        /// <param name="model">The agency search view model</param>
        /// <returns>Returns the agency search view model</returns>
        [HttpPost]
        public ActionResult AgencySearch(AgencySearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                AgencySearchDTO agencySearch = Mapper.Map<AgencySearchViewModel, AgencySearchDTO>(model);
                List<AgencyDTO> agencies = memberService.GetAgencyByCityOrZipCode(agencySearch).ToList();
                model.Agencies = Mapper.Map<List<AgencyDTO>, List<AgencyViewModel>>(agencies);
            }
            return View(model);
        }
        #endregion
    }
}
