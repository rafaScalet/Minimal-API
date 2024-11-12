using MinimalAPI.Domain.DTOs;
using MinimalAPI.Domain.Interfaces;
using MinimalAPI.Domain.ModelViews;

namespace MinimalAPI.Domain.Services;

public class ValidationServices : IValidationServices
{
	public ValidationErrors Validation(VehicleDTO vehicleDTO)
	{
		var validation = new ValidationErrors{ Messages = new List<string>() };

		if (string.IsNullOrEmpty(vehicleDTO.name)) validation.Messages.Add("name can't be blank");
		if (string.IsNullOrEmpty(vehicleDTO.mark)) validation.Messages.Add("mark can't be blank");
		if (vehicleDTO.year < 1950) validation.Messages.Add("vehicle year is to old");

		return validation;
	}

	public ValidationErrors Validation(AdminDTO adminDTO)
	{
		var validation = new ValidationErrors{ Messages = new List<string>() };

		if (string.IsNullOrEmpty(adminDTO.Email)) validation.Messages.Add("email can't be blank");
		if (string.IsNullOrEmpty(adminDTO.PWD)) validation.Messages.Add("password can't be blank");
		if (adminDTO.Profile == null) validation.Messages.Add("profile can't be blank");

		return validation;
	}
}