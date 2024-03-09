using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using ZooAnimalManagement.API.Contracts.Requests;
using ZooAnimalManagement.API.Mapping;
using ZooAnimalManagement.API.Services;

namespace ZooAnimalManagement.API.Endpoints.EnclosuresEndpoints
{
    [HttpPost("enclosures/list"), AllowAnonymous]
    public class CreateEnclosuresListEndpoint : Endpoint<CreateEnclosuresListRequest, bool>
    {
        private readonly IEnclosureService _enclosureService;

        public CreateEnclosuresListEndpoint(IEnclosureService enclosureService)
        {
            _enclosureService = enclosureService;
        }

        public override async Task HandleAsync(CreateEnclosuresListRequest request, CancellationToken ct)
        {
            foreach (var enclosureDto in request.Enclosures)
            {
                var enclosure = enclosureDto.ToEnclosure();

                await _enclosureService.CreateAsync(enclosure);
            }

            await SendOkAsync(true, ct);
        }
    }
}
