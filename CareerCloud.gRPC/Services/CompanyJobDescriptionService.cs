using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.CompanyJobDescription;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobDescriptionService : CompanyJobDescriptionBase
    {
        private readonly CompanyJobDescriptionLogic _logic;

        public CompanyJobDescriptionService()
        {
            _logic = new CompanyJobDescriptionLogic(new EFGenericRepository<CompanyJobDescriptionPoco>());
        }

        public override Task<CompanyJobDescriptionPayload> ReadCompanyJobDescription(CompanyJobDescriptionRequest request, ServerCallContext context)
        {
            CompanyJobDescriptionPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyJobDescriptionPayload>(
                () => new CompanyJobDescriptionPayload()
                {
                    Id = poco.Id.ToString(),
                    Job = poco.Job.ToString(),
                    JobName = poco.JobName,
                    JobDescriptions = poco.JobDescriptions
                });
        }

        public override Task<Empty> CreateCompanyJobDescription(CompanyJobDescriptionPayload request, ServerCallContext context)
        {
            _logic.Add(GetArray(request));
            return new Task<Empty>(null);
        }

        public override Task<Empty> UpdateCompanyJobDescription(CompanyJobDescriptionPayload request, ServerCallContext context)
        {
            _logic.Update(GetArray(request));
            return new Task<Empty>(null);
        }

        public override Task<Empty> DeleteCompanyJobDescription(CompanyJobDescriptionPayload request, ServerCallContext context)
        {
            _logic.Delete(GetArray(request));
            return new Task<Empty>(null);
        }

        public CompanyJobDescriptionPoco[] GetArray(CompanyJobDescriptionPayload request)
        {
            CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco()
            {
                Id = Guid.Parse(request.Id),
                Job = Guid.Parse(request.Job),
                JobName = request.JobName,
                JobDescriptions = request.JobDescriptions
            };

            CompanyJobDescriptionPoco[] pocos = new CompanyJobDescriptionPoco[1];
            pocos[0] = poco;
            return pocos;
        }
    }
}
