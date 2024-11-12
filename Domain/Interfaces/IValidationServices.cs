using MinimalAPI.Domain.DTOs;
using MinimalAPI.Domain.ModelViews;

namespace MinimalAPI.Domain.Interfaces;

public interface IValidationServices
{
	ValidationErrors Validation (VehicleDTO vehicleDTO);
	ValidationErrors Validation (AdminDTO adminDTO);
}