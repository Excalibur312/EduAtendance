
using EduAtendance.WebAPI.Dtos;
using FluentValidation;


namespace EduAtendance.WebAPI.Validators;

public class UpdateStudentDtoValidator : AbstractValidator<UpdateStudentDto>
{

    public UpdateStudentDtoValidator()
    {
        RuleFor(p => p.FirstName)

            .MinimumLength(3).WithMessage("3 harf ve daaha kısası olamaz");
        RuleFor(p => p.LastName)

             .MinimumLength(3).WithMessage("3 harf ve daaha kısası olamaz");
        RuleFor(p => p.PhoneNumber)

             .MinimumLength(10).WithMessage("3 harf ve daaha kısası olamaz")
            .MaximumLength(10).WithMessage("3 harf ve daaha uzun olamaz");
        RuleFor(p => p.Email)

            .EmailAddress().WithMessage("Geçerli bir mail giriniz");
        RuleFor(p => p.IdentityNumber)

            .MinimumLength(11).WithMessage("11 harf ve daaha kısası olamaz")
            .MaximumLength(11).WithMessage("11 harf ve daaha kısası olamaz")
            .IdentityNumber().WithMessage("Geçerli TC yaz");
        //*Check.IdentityNumber().WithMessage("Geçerli bir Tc Numarası gir")  
        //.Must(IsValidTc).WithMessage("Geçerli bir Tc Numarası gir");

    }


}
