using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using TCRC.BusinessLogic.Services;
using TCRC.Domain.DTOs;
using TCRC.Helpers;
using TCRC.Models;
using TCRC.Models.Member;
using AutoMapper;

namespace TCRC.Controllers
{
    public class MemberController : BaseController
    {
        #region Members
        private readonly IMemberService memberService;
        #endregion

        #region Methods
        /// <summary>
        /// Member controller constructor
        /// </summary>
        /// <param name="service">The member service</param>
        public MemberController(IMemberService service)
        {
            this.memberService = service;
        }

        /// <summary>
        /// View register member
        /// </summary>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Register a member
        /// </summary>
        /// <param name="model">The member register view model</param>
        /// <returns>Returns the register view model</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                MemberRegisterDTO member = Mapper.Map<RegisterViewModel, MemberRegisterDTO>(model);
                memberService.CreateMember(member);

                //add user authentication thru SimpleMembership model
                WebSecurityInitializer.Instance.EnsureInitialize();
                WebSecurity.CreateUserAndAccount(model.UserName, model.Password, propertyValues: new
                {
                    FirstName = model.BusinessContacts[0].FirstName,
                    LastName = model.BusinessContacts[0].LastName,
                    Email = model.BusinessContacts[0].EmailAddress
                });

                return View("RegistrationSubmitted", model);
            }
            else
            {
                return View(model);
            }
        }

        /// <summary>
        /// Renew a membership
        /// </summary>
        /// <param name="model">The register view model</param>
        /// <returns>Returns the register view model</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RenewMembership(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                MemberRegisterDTO member = Mapper.Map<RegisterViewModel, MemberRegisterDTO>(model);
                memberService.RenewMembership(member);

                return View();
            }
            else
            {
                return View(model);
            }
        }

        /// <summary>
        /// Gets member details
        /// </summary>
        /// <returns>Returns member details view model</returns>
        [Authorize]
        public ActionResult Details()
        {
            string userName = User.Identity.Name;
            DetailsViewModel detailsViewModel = new DetailsViewModel();

            if (!String.IsNullOrEmpty(userName))
            {
                MemberDetailDTO memberDetail = memberService.GetMemberDetailsByUserName(userName);
                detailsViewModel = Mapper.Map<MemberDetailDTO, DetailsViewModel>(memberDetail);
            }

            return View(detailsViewModel);
        }

        /// <summary>
        /// View change password
        /// </summary>
        public ActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// Renew a membership
        /// </summary>
        /// <returns>Returns a register view model</returns>
        public ActionResult RenewMembership()
        {
            //todo: add redirect for admin or member without a history
            string userName = User.Identity.Name;
            MemberRegisterDTO member = memberService.GetMemberByUserName(userName);
            RegisterViewModel model = Mapper.Map<MemberRegisterDTO, RegisterViewModel>(member);
            return View(model);
        }
        #endregion
    }
}
