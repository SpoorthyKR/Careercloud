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
using static CareerCloud.gRPC.Protos.CompanyJobSkill;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobSkillService : CompanyJobSkillBase
    {
        private readonly CompanyJobSkillLogic _logic;

        public CompanyJobSkillService()
        {
            _logic = new CompanyJobSkillLogic(new EFGenericRepository<CompanyJobSkillPoco>());
        }

        public override Task<CompanyJobSkillPayload> ReadCompanyJobSkill(CompanyJobSkillRequest request, ServerCallContext context)
        {
            CompanyJobSkillPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyJobSkillPayload>(
                () => new CompanyJobSkillPayload()
                {
                    Id = poco.Id.ToString(),
                    Job = poco.Job.ToString(),
                    Skill = poco.Skill,
                    SkillLevel = poco.SkillLevel,
                    Importance = poco.Importance
                });
        }

        public override Task<Empty> CreateCompanyJobSkill(CompanyJobSkillPayload request, ServerCallContext context)
        {
            _logic.Add(GetArray(request));
            return new Task<Empty>(null);
        }

        public override Task<Empty> UpdateCompanyJobSkill(CompanyJobSkillPayload request, ServerCallContext context)
        {
            _logic.Update(GetArray(request));
            return new Task<Empty>(null);
        }

        public override Task<Empty> DeleteCompanyJobSkill(CompanyJobSkillPayload request, ServerCallContext context)
        {
            _logic.Delete(GetArray(request));
            return new Task<Empty>(null);
        }

        public CompanyJobSkillPoco[] GetArray(CompanyJobSkillPayload request)
        {
            CompanyJobSkillPoco poco = new CompanyJobSkillPoco()
            {
                Id = Guid.Parse(request.Id),
                Job = Guid.Parse(request.Job),
                Skill = request.Skill,
                SkillLevel = request.SkillLevel,
                Importance = request.Importance
            };

            CompanyJobSkillPoco[] pocos = new CompanyJobSkillPoco[1];
            pocos[0] = poco;
            return pocos;
        }
    }
}
