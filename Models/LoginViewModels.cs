using System.ComponentModel.DataAnnotations;

namespace EwizProcureWeb.Models
{
    [Serializable]
    public class LoginDetailViewModels
    {
        public byte UserRoleId { get; set; }
        public int UserId { get; set; }
        public byte RoleId { get; set; }
        [Required]
        [Display(Name = "EmailId")]
        public string EmailId { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public Nullable<byte> LoginAttempts { get; set; }
        public Nullable<System.DateTime> LoginAttemptDateUtc { get; set; }
        public short CurrencyId { get; set; }
        public int LanguageId { get; set; }
        public string IPAddress { get; set; }
        public Nullable<System.DateTime> ResetTime { get; set; }
        public bool IsResetActive { get; set; }
        public bool IsActive { get; set; }
        public string RoleName { get; set; }
        public string CurrencySymbol { get; set; }
        public Nullable<System.Guid> PasswordLinkGUID { get; set; }
        public Nullable<System.DateTime> PasswordLinkExpiration { get; set; }
        public string PunchOutCompanyCode { get; set; }
        public int PunchOutcountryID { get; set; }
        public Guid PunchoutGUID { get; set; }
        public int DefaultCountryId { get; set; }
        public bool? IsCountryMismatchedMessageShown { get; set; }
        public bool? UpdatedDefaultCountry { get; set; }
        public Nullable<bool> Islogout { get; set; }

        public bool IsICCabinetUser { get; set; }
        public bool IsICCabinetView { get; set; }
        public bool IsCoupaUser { get; set; }

        //added on 24 dec 2018 by namrata
        public string DeliverTo { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string Street2 { get; set; }
        public string ISOCountryCode { get; set; }
        public string Misc { get; set; }

        //rasik - 15 March 2019
        public string PORG { get; set; }
        public string PORGCompCode { get; set; }
        public string IsBrandFlag { get; set; }
        public bool isCompanyCodeSelected { get; set; }

        //6 feb 2020 - UN-3395
        public string encoderUserID { get; set; }
        public string isEncoder { get; set; }
        public int? isEncoderUser { get; set; }

        public bool isRoleSelectedByUser { get; set; }
        public string NotificationEmailId { get; set; }
    }


    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }


    public class ResetPasswordViewModel
    {

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class CoupaDetails
    {
        public int UserId { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CompanyCode { get; set; }
        public string ContentGroups { get; set; }
        public string CompanyName { get; set; }
    }

    public class SiteMaintenance
    {
        public int Status { get; set; }
    }

    public class CoupaXML
    {
        public int UserId { get; set; }
        public string EmailId { get; set; }
        public string Name { get; set; }
        public string LastPunchoutRequestShell { get; set; }
    }

    public class UserRolesAll
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class SetUserRoleResult
    {
        public string Result { get; set; }
    }

    public class CountriesAndCC
    {
        public string CountryId { get; set; }
        public string CountryName { get; set; }
    }

    public class CCs
    {
        public string CompanyId { get; set; }
        public string AribaUniqueName { get; set; }
    }
}
