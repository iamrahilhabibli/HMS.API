using FluentValidation;
using HMS.Application.DTOs.Hotel_DTOs;

namespace HMS.Application.Validators.HotelValidators
{
    public class HotelCreateDtoValidator : AbstractValidator<HotelCreateDto>
    {
        public HotelCreateDtoValidator()
        {
            RuleFor(hotel => hotel.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(hotel => hotel.Description)
                .NotEmpty()
                .MinimumLength(20)
                .MaximumLength(500);

            RuleFor(hotel => hotel.FloorNumber)
                .NotEmpty();

            RuleFor(hotel => hotel.RoomNumber)
                .NotEmpty();
        }
    }
}
