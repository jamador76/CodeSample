using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TCRC.Domain.DTOs;
using TCRC.Models.Admin;
using TCRC.Models.FileClaim;
using TCRC.Models.Member;
using TCRC.Helpers;
using TCRC.BusinessLogic.Services;
using AutoMapper;
using WebMatrix.WebData;

namespace TCRC.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class AdminController : BaseController
    {
        #region Members
        private readonly IAdminService adminService;
        private readonly IClaimService claimService;
        private readonly IMemberService memberService;
        #endregion

        #region Members
        /// <summary>
        /// Admin controller constructor
        /// </summary>
        /// <param name="adminService">The admin service</param>
        /// <param name="claimService">The claim service</param>
        /// <param name="memberService">The member service</param>
        public AdminController(IAdminService adminService, IClaimService claimService, IMemberService memberService)
        {
            this.adminService = adminService;
            this.claimService = claimService;
            this.memberService = memberService;
        }

        /// <summary>
        /// View Sot import
        /// </summary>
        public ActionResult ImportSot()
        {
            return View();
        }

        /// <summary>
        /// Import sellers of travel
        /// </summary>
        /// <param name="file">The file to import</param>
        /// <returns>Returns the view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportSot(HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    //todo: validate file format is csv
                    StringBuilder fileName = new StringBuilder(DateTime.Now.Ticks.ToString()).Append("_")
                        .Append(Path.GetFileNameWithoutExtension(file.FileName)).Append("_")
                        .Append(DateTime.Now.ToString("yyyyMMdd"))
                        .Append(Path.GetExtension(file.FileName));

                    Directory.CreateDirectory(Server.MapPath("~/App_Data/Uploads/SotImport"));

                    var filePath = Path.Combine(Server.MapPath("~/App_Data/Uploads/SotImport"), fileName.ToString());
                    file.SaveAs(filePath);
                    adminService.ImportSots(filePath);
                }
                //todo: return message
            }
            catch (Exception e)
            {
                //todo: handle exception
            }
            return View();
        }

        /// <summary>
        /// View reports
        /// </summary>
        public ActionResult Reports()
        {
            return View();
        }

        /// <summary>
        /// View users
        /// </summary>
        public ActionResult Users()
        {
            //var users = adminService.GetUsers();
            UsersViewModel model = new UsersViewModel();
            List<UserProfileDTO> userProfiles = adminService.GetUsers().ToList();
            model.Users = Mapper.Map<List<UserProfileDTO>, List<UserProfileViewModel>>(userProfiles);

            return View(model);
        }

        /// <summary>
        /// View member search
        /// </summary>
        /// <returns></returns>
        public ActionResult SearchMembers()
        {
            return View();
        }

        /// <summary>
        /// Search members
        /// </summary>
        /// <param name="model">The member search view model</param>
        /// <returns>Returns the member search view model</returns>
        [HttpPost]
        public ActionResult SearchMembers(MemberSearchViewModel model)
        {
            MemberSearchDTO memberSearch = Mapper.Map<MemberSearchViewModel, MemberSearchDTO>(model);
            List<MemberDTO> members = adminService.SearchMembers(memberSearch).ToList();
            
            model.Members = Mapper.Map<List<MemberDTO>, List<MemberViewModel>>(members);
            return View(model);
        }

        /// <summary>
        /// View claim search
        /// </summary>
        public ActionResult SearchClaim()
        {
            return View();
        }

        /// <summary>
        /// Search claims
        /// </summary>
        /// <param name="model">The search claim view model</param>
        /// <returns>Returns the search claim view model</returns>
        [HttpPost]
        public ActionResult SearchClaim(SearchClaimViewModel model)
        {
            SearchClaimDTO searchClaim = Mapper.Map<SearchClaimViewModel, SearchClaimDTO>(model);
            var claims = claimService.SearchClaims(searchClaim);
            model.Claims = claims;
            
            return View(model);
        }

        /// <summary>
        /// View claim details
        /// </summary>
        /// <param name="claimID">The claim id</param>
        /// <returns>Returns the process claim view model</returns>
        public ActionResult Claim(int claimID)
        {
            ClaimFormDTO processClaim = claimService.GetClaimByID(claimID);
            ProcessClaimViewModel model = Mapper.Map<ClaimFormDTO, ProcessClaimViewModel>(processClaim);

            return View(model);
        }

        /// <summary>
        /// Gets the claim status type
        /// </summary>
        /// <param name="claimId">The claim id</param>
        /// <param name="claimStatusTypeId">The claims status type id</param>
        /// <param name="claimant">The claimant</param>
        /// <param name="address">The address</param>
        /// <param name="city">The city</param>
        /// <param name="state">The state</param>
        /// <param name="zip">The zip code</param>
        /// <returns>Returns claim status type json</returns>
        public JsonResult GetClaimStatusType(int claimId, int claimStatusTypeId, string claimant, string address, string city, string state, string zip)
        {
            var claimStatusType = claimService.GetClaimStatusType(claimId, claimStatusTypeId, claimant, address, city, state, zip);

            return new JsonResult() { Data = claimStatusType, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// View process claim
        /// </summary>
        public ActionResult ProcessClaim()
        {
            return View();
        }

        /// <summary>
        /// Update a claim
        /// </summary>
        /// <param name="model">The claim view model</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultipleButton(Name = "action", Argument = "update")]
        public ActionResult UpdateClaim(ProcessClaimViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ClaimFormDTO claimForm = Mapper.Map<ClaimFormViewModel, ClaimFormDTO>(model);
                    claimService.UpdateClaim(claimForm, WebSecurity.CurrentUserId);
                }
                catch (Exception e)
                {
                    //todo: review the model error message
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if problem persists contact the system administrator.");
                    return RedirectToAction("Index", "Error");
                }

                return RedirectToAction("Claim", new { claimID = model.ClaimID });
            }
            else
            {
                return View(model);
            }
        }

        /// <summary>
        /// Process a claim
        /// </summary>
        /// <param name="model">The process claim view model</param>
        /// <returns>Return the process claim view model</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultipleButton(Name = "action", Argument = "process")]
        public ActionResult ProcessClaim(ProcessClaimViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProcessClaimDTO claimForm = Mapper.Map<ProcessClaimViewModel, ProcessClaimDTO>(model);
                    claimService.ProcessClaim(claimForm, WebSecurity.CurrentUserId);
                }
                catch (Exception e)
                {
                    //todo: review the model error message
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if problem persists contact the system administrator.");
                    return RedirectToAction("Index", "Error");
                }

                return RedirectToAction("Claim", new { claimID = model.ClaimID });
            }
            else
            {
                return View(model);
            }
        }

        /// <summary>
        /// View a member
        /// </summary>
        /// <param name="tcrcID">The tcrc id</param>
        /// <returns>Returns a member view model</returns>
        public ActionResult Member(string tcrcID)
        {
            MemberRegisterDTO member = memberService.GetMemberByID(tcrcID);
            ProcessMemberViewModel model = Mapper.Map<MemberRegisterDTO, ProcessMemberViewModel>(member);

            List<MemberCheckHistoryDTO> checkHistories = memberService.GetMemberCheckHistories(int.Parse(tcrcID)).ToList();
            model.MemberCheckHistories = Mapper.Map<List<MemberCheckHistoryDTO>, List<MemberCheckHistory>>(checkHistories);

            return View(model);
        }
        #endregion
    }
}