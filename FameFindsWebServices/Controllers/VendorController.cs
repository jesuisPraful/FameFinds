using FameFindsDAL;
using FameFindsWebServices.Models;
using FameFindsWebServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FameFindsWebServices.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class VendorController : Controller
    {
        private readonly FameFindsRepository _repository;
        private readonly AuthenticationService _authService;
        public VendorController(FameFindsRepository repository, AuthenticationService authService)
        {
            _repository = repository;
            _authService = authService;
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
        [HttpPost("Register")]
        public IActionResult AddVendor([FromBody] Models.Vendor vendor)
        {
            bool result = false;
            try
            {
                if (ModelState.IsValid)
                {
                    var vendorOne = new Vendor
                    {
                        VendorName = vendor.VendorName,
                        Email=vendor.Email,
                        PasswordHash = vendor.PasswordHash,
                        PhoneNumber = vendor.PhoneNumber
                    };
                    result = _authService.AddVendor(vendorOne);
                    if(result)
                    {
                        return Ok("Vendor Registered Successfully");
                    }
                    else
                    {
                        return BadRequest("Registration Failed");
                    }
                }
                else
                {
                    var errors=ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                    return BadRequest(new {message="Invalid Data",errors});
                }
               
            }
            catch (Exception ex)
            {
                result = true;
                return BadRequest("Registration Failed");
            }
        }
        [HttpPost("login")]
        public IActionResult Login([FromQuery] string email, [FromQuery] string passwordHash)
        {
            try
            {
                var vendor = _authService.LoginVendor(new Models.Vendor
                {
                    Email = email,
                    PasswordHash = passwordHash
                });

                if (vendor != null)
                    return Ok(vendor);

                return Unauthorized("Invalid email or password.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
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
        [HttpGet]
        public IActionResult GetVendorByName(string VendorName)
        {
            try
            {
                var vendor = _repository.GetVendorByName(VendorName);
                if (vendor != null)
                {
                    return Ok(vendor);
                }
                else
                {
                    return NotFound("vendor not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
