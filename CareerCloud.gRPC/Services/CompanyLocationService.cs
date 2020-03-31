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
using static CareerCloud.gRPC.Protos.CompanyLocation;

namespace CareerCloud.gRPC.Services
{
    public class CompanyLocationService : CompanyLocationBase
    {
        private readonly CompanyLocationLogic _logic;
        public CompanyLocationService()
        {
            _logic = new CompanyLocationLogic(new EFGenericRepository<CompanyLocationPoco>());
        }

        public override Task<CompanyLocationPayload> ReadCompanyLocation(CompanyLocationRequest request, ServerCallContext context)
        {
            CompanyLocationPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyLocationPayload>(
                () => new CompanyLocationPayload()
                {
                    Id = poco.Id.ToString(),
                    Company = poco.Company.ToString(),
                    CountryCode = poco.CountryCode,
                    Province = poco.Province,
                    Street = poco.Street,
                    City = poco.City,
                    PostalCode = poco.PostalCode
                });
        }

        public override Task<Empty> CreateCompanyLocation(CompanyLocationPayload request, ServerCallContext context)
        {
            _logic.Add(GetArray(request));
            return new Task<Empty>(null);
        }

        public override Task<Empty> UpdateCompanyLocation(CompanyLocationPayload request, ServerCallContext context)
        {
            _logic.Update(GetArray(request));
            return new Task<Empty>(null);
        }

        public override Task<Empty> DeleteCompanyLocation(CompanyLocationPayload request, ServerCallContext context)
        {
            _logic.Delete(GetArray(request));
            return new Task<Empty>(null);
        }
        public CompanyLocationPoco[] GetArray(CompanyLocationPayload request)
        {
            CompanyLocationPoco poco = new CompanyLocationPoco()
            {
                Id = Guid.Parse(request.Id),
                Company = Guid.Parse(request.Company),
                CountryCode = request.CountryCode,
                Province = request.Province,
                Street = request.Street,
                City = request.City,
                PostalCode = request.PostalCode
            };

            CompanyLocationPoco[] pocos = new CompanyLocationPoco[1];
            pocos[0] = poco;
            return pocos;
        }

    }
}
