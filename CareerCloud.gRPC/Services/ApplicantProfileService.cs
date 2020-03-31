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
using static CareerCloud.gRPC.Protos.ApplicantProfile;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantProfileService : ApplicantProfileBase
    {
        private readonly ApplicantProfileLogic _logic;

        public ApplicantProfileService()
        {
            _logic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>());
        }

        public override Task<ApplicantProfilePayload> ReadApplicantProfile(ApplicantProfileRequest request, ServerCallContext context)
        {
            ApplicantProfilePoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<ApplicantProfilePayload>(
                () => new ApplicantProfilePayload()
                {
                    Id = poco.Id.ToString(),
                    Login = poco.Login.ToString(),
                    CurrentSalary = poco.CurrentSalary is null ?
                        0.0 :
                        (double)poco.CurrentSalary,
                    CurrentRate = poco.CurrentRate is null ?
                    0.0 :
                    (double)poco.CurrentRate,
                    Currency = poco.Currency,
                    Country = poco.Country,
                    Province = poco.Province,
                    Street = poco.Street,
                    City = poco.City,
                    PostalCode = poco.PostalCode
                });
        }

        public override Task<Empty> CreateApplicantProfile(ApplicantProfilePayload request, ServerCallContext context)
        {
            _logic.Add(GetArray(request));
            return new Task<Empty>(null);
        }

        public override Task<Empty> UpdateApplicantProfile(ApplicantProfilePayload request, ServerCallContext context)
        {
            _logic.Update(GetArray(request));
            return new Task<Empty>(null);
        }

        public override Task<Empty> DeleteApplicantProfile(ApplicantProfilePayload request, ServerCallContext context)
        {
            _logic.Delete(GetArray(request));
            return new Task<Empty>(null);
        }

        public ApplicantProfilePoco[] GetArray(ApplicantProfilePayload request)
        {
            ApplicantProfilePoco poco = new ApplicantProfilePoco()
            {
                Id = Guid.Parse(request.Id),
                Login = Guid.Parse(request.Login),
                CurrentSalary = (Decimal)request.CurrentSalary,
                CurrentRate = (Decimal)request.CurrentRate,
                Currency = request.Currency,
                Country = request.Country,
                Province = request.Province,
                Street = request.Street,
                City = request.City,
                PostalCode = request.PostalCode
            };

            ApplicantProfilePoco[] pocos = new ApplicantProfilePoco[1];
            pocos[0] = poco;
            return pocos;
        }
    }
}
