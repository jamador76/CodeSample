using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCRC.BusinessLogic.Services;
using TCRC.Domain.DTOs;
using TCRC.Models;
using TCRC.Models.FileClaim;
using TCRC.Models.Member;
using AutoMapper;

namespace TCRC.Controllers
{
    public class FileClaimController : BaseController
    {
        #region Members
        private readonly IClaimService service;
        #endregion

        #region Members
        /// <summary>
        /// File claim controller constructor
        /// </summary>
        /// <param name="service">The claim service</param>
        public FileClaimController(IClaimService service)
        {
            this.service = service;
        }

        /// <summary>
        /// View claim form
        /// </summary>
        public ActionResult ClaimForm()
        {
            return View(); 
        }

        /// <summary>
        /// View add new claim
        /// </summary>
        public ActionResult AddNewClaim()
        {
            return View();
        }

        /// <summary>
        /// Submit a claim
        /// </summary>
        /// <param name="files">The uploaded files</param>
        /// <param name="model">The claim model</param>
        /// <returns>Returns the claim view model</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit(IEnumerable<HttpPostedFileBase> files, ClaimFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                //server side validation of business address id
                if (model.BusinessAddressId != 0)
                {
                    try
                    {
                        ClaimFormDTO claimForm = Mapper.Map<ClaimFormViewModel, ClaimFormDTO>(model);
                        service.InsertClaim(claimForm);

                        return RedirectToAction("ClaimSubmitted");
                    }
                    catch (Exception e)
                    {
                        //todo: do something with this exception
                    }
                }
                else
                {
                    ModelState.AddModelError("BusinessAddressId", "Travel Seller is required");
                }
            }
            return View("ClaimForm", model);
        }

        /// <summary>
        /// View claim submitted
        /// </summary>
        public ActionResult ClaimSubmitted()
        {
            return View();
        }

        /// <summary>
        /// Gets business addresses for claim
        /// </summary>
        /// <param name="tcrcNum">The tcrc number</param>
        /// <param name="sotNum">The sot number</param>
        /// <param name="agencyName">The agency name</param>
        /// <param name="city">The city</param>
        /// <param name="claimDateStr">The claim date</param>
        /// <param name="scheduledReturnDateStr">The scheduled return date</param>
        /// <returns>Returns business addresses partial view</returns>
        public PartialViewResult GetBusinessAddressesForClaim(int? tcrcNum, int? sotNum, string agencyName, string city, string claimDateStr, string scheduledReturnDateStr)
        {
            List<BusinessAddressDTO> addresses = service.GetBusinessAddressesForClaim(tcrcNum, sotNum, 0, agencyName, city, claimDateStr, scheduledReturnDateStr);
            List<BusinessAddress> businessAddresses = Mapper.Map<List<BusinessAddressDTO>, List<Models.Member.BusinessAddress>>(addresses);

            return PartialView("_SellerOfTravel", businessAddresses);
        }

        /// <summary>
        /// Uploads files
        /// </summary>
        /// <param name="files">The files to upload</param>
        /// <param name="uploadKey">The upload key</param>
        /// <returns>Returns the upload key</returns>
        [HttpPost]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> files, Guid? uploadKey)
        {
            if (files != null)
            {
                if (uploadKey == null)
                {
                    uploadKey = Guid.NewGuid();
                }

                var uploadPath = Server.MapPath("~/Uploads/" + uploadKey);

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                foreach (var file in files)
                {
                    try
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var physicalPath = Path.Combine(uploadPath, fileName);
                        file.SaveAs(physicalPath);
                    }
                    catch (Exception ex)
                    {
                        //todo: do something with this exception
                    }
                }
            }

            return Json(new { data = uploadKey }, "text/plain");
        }

        /// <summary>
        /// Removes files from upload
        /// </summary>
        /// <param name="fileNames">The file names to remove</param>
        /// <param name="uploadKey">The upload key</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Remove(string[] fileNames, Guid uploadKey)
        {
            if (fileNames != null && uploadKey != null)
            {
                var uploadPath = Server.MapPath("~/Uploads/" + uploadKey);

                foreach (var fullName in fileNames)
                {
                    try
                    {
                        var fileName = Path.GetFileName(fullName);
                        var physicalPath = Path.Combine(uploadPath, fileName);

                        if (System.IO.File.Exists(physicalPath))
                        {
                            System.IO.File.Delete(physicalPath);
                        }

                        int numFiles = Directory.GetFiles(uploadPath).Count();

                        if (numFiles == 0)
                        {
                            Directory.Delete(uploadPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        //todo: do something with this exception
                    }
                }
            }

            return Content("");
        }
        #endregion
    }
}