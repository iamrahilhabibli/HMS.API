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

            RuleFor(hotel => hotel.CheckInTime)
                .NotEmpty()
                .Must(BeValidTime)
                .WithMessage("Check-In time must be valid time between 00:00 and 23:59");
        }

        private bool BeValidTime(string checkInTime)
        {
            if (TimeSpan.TryParse(checkInTime, out var time))
            {
                return time >= TimeSpan.Zero && time <= TimeSpan.FromHours(23) + TimeSpan.FromMinutes(59);
            }
            return false;
        }
    }
}
