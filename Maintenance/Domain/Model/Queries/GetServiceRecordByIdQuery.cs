using Flota365.Platform.API.Shared.Domain.Model;
using Flota365.Platform.API.Maintenance.Interfaces.REST.Resources;
namespace Flota365.Platform.API.Maintenance.Domain.Model.Queries
{
public record GetServiceRecordByIdQuery(int ServiceRecordId) : IQuery<ServiceRecordResource?>;
}