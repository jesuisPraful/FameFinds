using FameFindsDAL;
using FameFindsDAL.Models;
using FameFindsWebServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FameFindsWebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : Controller
    {
        private readonly FameFindsRepository _repository;
        public VendorController(FameFindsRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult GetVendorDetails()
        {
            List<FameFindsWebServices.Models.Vendor> vendors = new List<FameFindsWebServices.Models.Vendor>();
            try
            {
                var vendorList = _repository.GetVendorDetails();
                if (vendorList != null)
                {
                    foreach (var vendor in vendorList)
                    {
                        FameFindsWebServices.Models.Vendor v = new FameFindsWebServices.Models.Vendor();
                        v.VendorId = vendor.VendorId;
                        v.VendorName = vendor.VendorName;
                        v.Email = vendor.Email;
                        v.PasswordHash = vendor.PasswordHash;
                        v.PhoneNumber = vendor.PhoneNumber;
                        vendors.Add(v);
                    }
                }

            }
            catch (Exception ex)
            {
                vendors = null;
                return BadRequest("Failed to fetch vendordetails");
            }
            return Ok(vendors);
        }

        [HttpPost]
        public JsonResult AddVendor(Models.Vendor vendor)
        {
            bool result = false;
            try
            {
                FameFindsDAL.Models.Vendor v = new FameFindsDAL.Models.Vendor();
                //v.VendorId = vendor.VendorId;
                v.VendorName = vendor.VendorName;
                v.Email = vendor.Email;
                v.PasswordHash = vendor.PasswordHash;
                v.PhoneNumber = vendor.PhoneNumber;
                result = _repository.AddVendor(v);
            }
            catch (Exception ex)
            {
                result = true;
            }
            return Json(result);
        }
        [HttpPut]
        public IActionResult UpdateVendor(Models.Vendor vendor) 
        {
            bool status = false;
            try
            {
                if (ModelState.IsValid)
                {
                    FameFindsDAL.Models.Vendor v = new FameFindsDAL.Models.Vendor();
                    v.VendorId = vendor.VendorId;
                    v.Email = vendor.Email;
                    v.PhoneNumber = vendor.PhoneNumber;
                    v.PasswordHash = vendor.PasswordHash;
                    v.VendorName = vendor.VendorName;
                    status = _repository.UpdateVendor(v);
                    if (status)
                    {
                        return Ok("Vendor updated successfully");
                    }
                    else
                    {
                        return BadRequest("Failed to update Vendor");
                    }
                }
                else
                {
                    return BadRequest("Model state is not valid");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete]
        public IActionResult DeleteVendor(int vendorId)
        {
            bool status = false;
            try
            {
                status = _repository.RemoveVendorDetails(vendorId);
                if (status)
                {
                    return Ok("Vendor deleted successfully");
                }
                else
                {
                    return BadRequest("Failed to delete vendor");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }

        }

    }
}
