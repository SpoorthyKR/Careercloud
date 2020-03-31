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
using static CareerCloud.gRPC.Protos.ApplicantWorkHistory;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantWorkHistoryService : ApplicantWorkHistoryBase
    {
        private readonly ApplicantWorkHistoryLogic _logic;
        public ApplicantWorkHistoryService()
        {
            _logic = new ApplicantWorkHistoryLogic(new EFGenericRepository<ApplicantWorkHistoryPoco>());
        }
        public override Task<ApplicantWorkHistoryPayload> ReadApplicantWorkHistory(ApplicantWorkHistoryRequest request, ServerCallContext context)
        {
            ApplicantWorkHistoryPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<ApplicantWorkHistoryPayload>(
                () => new ApplicantWorkHistoryPayload()
                {
                    Id = poco.Id.ToString(),
                    Applicant = poco.Applicant.ToString(),
                    CompanyName = poco.CompanyName,
                    CountryCode = poco.CountryCode,
                    Location = poco.Location,
                    JobTitle = poco.JobTitle,
                    JobDescription = poco.JobDescription,
                    StartMonth = poco.StartMonth,
                    StartYear = poco.StartYear,
                    EndMonth = poco.EndMonth,
                    EndYear = poco.EndYear
                });
        }
        public override Task<Empty> CreateApplicantWorkHistory(ApplicantWorkHistoryPayload request, ServerCallContext context)
        {
            _logic.Add(GetArray(request));
            return new Task<Empty>(null);
        }

        public override Task<Empty> UpdateApplicantWorkHistory(ApplicantWorkHistoryPayload request, ServerCallContext context)
        {
            _logic.Update(GetArray(request));
            return new Task<Empty>(null);
        }

        public override Task<Empty> DeleteApplicantWorkHistory(ApplicantWorkHistoryPayload request, ServerCallContext context)
        {
            _logic.Delete(GetArray(request));
            return new Task<Empty>(null);
        }
        public ApplicantWorkHistoryPoco[] GetArray(ApplicantWorkHistoryPayload request)
        {
            ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco()
            {
                Id = Guid.Parse(request.Id),
                Applicant = Guid.Parse(request.Applicant),
                CompanyName = request.CompanyName,
                CountryCode = request.CountryCode,
                Location = request.Location,
                JobTitle = request.JobTitle,
                JobDescription = request.JobDescription,
                StartMonth = (short)request.StartMonth,
                StartYear = request.StartYear,
                EndMonth = (short)request.EndMonth,
                EndYear = request.EndYear
            };

            ApplicantWorkHistoryPoco[] pocos = new ApplicantWorkHistoryPoco[1];
            pocos[0] = poco;
            return pocos;
        }
    }
}