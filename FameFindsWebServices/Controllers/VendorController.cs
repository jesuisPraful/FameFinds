using FameFindsDAL;
using FameFindsWebServices.Models;
using FameFindsWebServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResetModel = FameFindsWebServices.Models.ResetPasswordRequest;


namespace FameFindsWebServices.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class VendorController : Controller
    {
        private readonly IFameFindsDAL _repository;
        private readonly AuthenticationService _authService;
        private readonly EmailService _emailService;
        public VendorController(IFameFindsDAL repository, AuthenticationService authService, EmailService emailService)
        {
            _repository = repository;
            _authService = authService;
            _emailService = emailService;
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
                        Email = vendor.Email,
                        PasswordHash = vendor.PasswordHash,
                        PhoneNumber = vendor.PhoneNumber
                    };
                    result = _authService.AddVendor(vendorOne);
                    if (result)
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
                    var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                    return BadRequest(new { message = "Invalid Data", errors });
                }

            }
            catch (Exception ex)
            {
                result = true;
                return BadRequest("Registration Failed");
            }
        }
        [HttpGet("check-email")]
        public IActionResult CheckEmailExists([FromQuery] string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return BadRequest("Email must be provided.");

                bool exists = _repository.IsVendorEmailExists(email);
                return Ok(exists);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CheckEmailExists: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
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
        public IActionResult GetVendorByEmail(string Email)
        {
            try
            {
                var vendor = _repository.GetVendorByUsername(Email);
                if (vendor != null)
                {
                    return Ok(new { vendor.VendorId } );
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

        [HttpPut("update-password")]
        public IActionResult UpdatePassword([FromQuery] int userId, [FromQuery] string newPasswordHash)
        {
            var success = _authService.UpdatePassword(userId, newPasswordHash);
            return success ? Ok("Password updated.") : NotFound("User not found.");
        }

        //forget password
        //generating otp, saving in th db, sending to otp to that mail.

        [HttpPost("request-otp")]
        public IActionResult RequestOtp([FromBody] EmailRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest("Email is required.");
            }

            var vendor = _repository.GetVendorByUsername(request.Email);
            if (vendor == null)
            {
                return NotFound("Email not found");
            }
            int vendorId = vendor.VendorId;

            var otp = _emailService.GenerateOtp();
            _repository.SaveVendorOtp(vendorId, otp);

            _emailService.SendOtpEmail(request.Email, otp);

            return Ok("OTP has been sent to your email.");
        }

        [HttpPost("verify-otp")]
        public IActionResult VerifyOtp([FromBody] OtpVerificationRequest request)
        {
            var token = _repository.GetVendorOtpByEmail(request.Email, request.Otp);

            if (token == null)
            {
                return BadRequest("Invalid or expired OTP.");
            }

            _repository.MarkVendorOtpAsUsedByEmail(request.Email, request.Otp);

            return Ok("OTP verified. You may now reset your password.");
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] ResetModel request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                return BadRequest("Email is required.");

            if (string.IsNullOrWhiteSpace(request.NewPassword) || string.IsNullOrWhiteSpace(request.ConfirmPassword))
                return BadRequest("All fields are required.");

            if (request.NewPassword != request.ConfirmPassword)
                return BadRequest("Passwords do not match.");

            var token = _repository.GetVendorLatestVerifiedOtp(request.Email);
            if (token == null || token.Expiry < DateTime.Now)
                return BadRequest("OTP not verified or session expired. Please verify your OTP again.");

            var vendor = _repository.GetVendorByUsername(request.Email);
            if (vendor == null)
                return NotFound("Customer not found.");

            var result = _authService.UpdatePassword(vendor.VendorId, request.NewPassword);
            if (!result)
                return StatusCode(500, "Error updating password.");

            return Ok("Password reset successful.");
        }

        // for view-ratings

        [HttpGet("RatingsByVendor/{vendorId}")]
        public IActionResult GetRatingsByVendor(int vendorId)
        {
            var ratings = _repository.GetRatingsByVendor(vendorId);
            return Ok(ratings);
        }


    }
}
