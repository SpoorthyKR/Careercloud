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
using static CareerCloud.gRPC.Protos.CompanyProfile;

namespace CareerCloud.gRPC.Services
{
    public class CompanyProfileService : CompanyProfileBase 
    {
        private readonly CompanyProfileLogic _logic;

        public CompanyProfileService()
        {
            _logic = new CompanyProfileLogic(new EFGenericRepository<CompanyProfilePoco>());
        }

        public override Task<CompanyProfilePayload> ReadCompanyProfile(CompanyProfileRequest request, ServerCallContext context)
        {
            CompanyProfilePoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyProfilePayload>(
                () => new CompanyProfilePayload()
                {
                    Id = poco.Id.ToString(),
                    RegistrationDate = Timestamp.FromDateTime((DateTime)poco.RegistrationDate),
                    CompanyWebsite = poco.CompanyWebsite,
                    ContactPhone = poco.ContactPhone,
                    ContactName = poco.ContactName,
                    CompanyLogo = Google.Protobuf.ByteString.CopyFrom(poco.CompanyLogo)
                });
        }

        public override Task<Empty> CreateCompanyProfile(CompanyProfilePayload request, ServerCallContext context)
        {
            _logic.Add(GetArray(request));
            return new Task<Empty>(null);
        }

        public override Task<Empty> UpdateCompanyProfile(CompanyProfilePayload request, ServerCallContext context)
        {
            _logic.Update(GetArray(request));
            return new Task<Empty>(null);
        }

        public override Task<Empty> DeleteCompanyProfile(CompanyProfilePayload request, ServerCallContext context)
        {
            _logic.Delete(GetArray(request));
            return new Task<Empty>(null);
        }

        public CompanyProfilePoco[] GetArray(CompanyProfilePayload request)
        {
            CompanyProfilePoco poco = new CompanyProfilePoco()
            {
                Id = Guid.Parse(request.Id),
                RegistrationDate = request.RegistrationDate.ToDateTime(),
                CompanyWebsite = request.CompanyWebsite,
                ContactPhone = request.ContactPhone,
                ContactName = request.ContactName,
                CompanyLogo = request.CompanyLogo.ToByteArray()
            };

            CompanyProfilePoco[] pocos = new CompanyProfilePoco[1];
            pocos[0] = poco;
            return pocos;
        }

    }
}
