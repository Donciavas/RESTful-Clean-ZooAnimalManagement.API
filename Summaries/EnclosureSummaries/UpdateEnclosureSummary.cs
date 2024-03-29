﻿using FastEndpoints;
using ZooAnimalManagement.API.Contracts.Responses;
using ZooAnimalManagement.API.Endpoints.EnclosuresEndpoints;

namespace ZooAnimalManagement.API.Summaries.EnclosureSummaries
{
    public class UpdateEnclosureSummary : Summary<UpdateEnclosureEndpoint>
    {
        public UpdateEnclosureSummary()
        {
            Summary = "Updates an existing enclosure in the system";
            Description = "Updates an existing enclosure in the system";
            Response<EnclosureResponse>(201, "Enclosure was successfully updated");
            Response<ValidationFailureResponse>(400, "The request did not pass validation checks");
        }
    }
}
