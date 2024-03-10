using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using ZooAnimalManagement.API.Contracts.Requests;
using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Mapping;
using ZooAnimalManagement.API.Services;

namespace ZooAnimalManagement.API.Endpoints.EnclosuresEndpoints
{
    [HttpPost("enclosures/list"), AllowAnonymous]
    public class CreateEnclosuresListEndpoint : Endpoint<CreateEnclosuresListRequest, List<EnclosureResponse>>
    {
        private readonly IEnclosureService _enclosureService;

        public CreateEnclosuresListEndpoint(IEnclosureService enclosureService)
        {
            _enclosureService = enclosureService;
        }

        public override async Task HandleAsync(CreateEnclosuresListRequest request, CancellationToken ct)
        {
            var enclosureResponses = new List<EnclosureResponse>();

            foreach (var enclosureReq in request.Enclosures)
            {
                var enclosure = enclosureReq.ToEnclosure();

                await _enclosureService.CreateAsync(enclosure);

                enclosureResponses.Add(enclosure.ToEnclosureResponse());
            }

            await SendOkAsync(enclosureResponses, ct);
        }
    }
}
