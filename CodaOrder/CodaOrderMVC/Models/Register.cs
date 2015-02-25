using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iOrder.Models
{
    public class Register
    {
        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string UserName { get; set; }
        [Required]
        [StringLength(5, MinimumLength = 3)]
        public string Password { get; set; }
        [Required]
        [NotMapped]
        [Compare("Password", ErrorMessage = "Password must be equal")]
        public string PasswordConfirm { get; set; }
    }

    //public class RegisterModelValidator : AbstractValidator<Register>
    //{
    //    private readonly IUserProfileRepository userProfileRepository;

    //    public RegisterModelValidator(IUserProfileRepository userProfileRepository)
    //    {
    //        this.userProfileRepository = userProfileRepository;

    //        RuleFor(x => x.UserName)
    //            .NotEmpty();
    //        RuleFor(x => x.Password)
    //            .NotEmpty()
    //            .Length(6, 100);
    //        RuleFor(x => x.ConfirmPassword)
    //            .Equal(x => x.Password);

    //        Custom(rm =>
    //        {
    //            UserProfile userProfile = userProfileRepository.GetUserProfileByUserName(rm.UserName);

    //            if (userProfile != null)
    //                return new ValidationFailure("UserName", "This user name is already registered");

    //            return null;
    //        });
    //    }
    //}
}