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
using static CareerCloud.gRPC.Protos.ApplicantSkill;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantSkillService : ApplicantSkillBase
    {
        private readonly ApplicantSkillLogic _logic;

        public ApplicantSkillService()
        {
            _logic = new ApplicantSkillLogic(new EFGenericRepository<ApplicantSkillPoco>());
        }

        public override Task<ApplicantSkillPayload> ReadApplicantSkill(ApplicantSkillRequest request, ServerCallContext context)
        {
            ApplicantSkillPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<ApplicantSkillPayload>(
                () => new ApplicantSkillPayload()
                {
                    Id = poco.Id.ToString(),
                    Applicant = poco.Applicant.ToString(),
                    Skill = poco.Skill,
                    SkillLevel = poco.SkillLevel,
                    StartMonth = poco.StartMonth,
                    StartYear = poco.StartYear,
                    EndMonth = poco.EndMonth,
                    EndYear = poco.EndYear
                });
        }

        public override Task<Empty> CreateApplicantSkill(ApplicantSkillPayload request, ServerCallContext context)
        {
            _logic.Add(GetArray(request));
            return new Task<Empty>(null);
        }

        public override Task<Empty> UpdateApplicantSkill(ApplicantSkillPayload request, ServerCallContext context)
        {
            _logic.Update(GetArray(request));
            return new Task<Empty>(null);
        }

        public override Task<Empty> DeleteApplicantSkill(ApplicantSkillPayload request, ServerCallContext context)
        {
            _logic.Delete(GetArray(request));
            return new Task<Empty>(null);
        }

        public ApplicantSkillPoco[] GetArray(ApplicantSkillPayload request)
        {
            ApplicantSkillPoco poco = new ApplicantSkillPoco()
            {
                Id = Guid.Parse(request.Id),
                Applicant = Guid.Parse(request.Applicant),
                Skill = request.Skill,
                SkillLevel = request.SkillLevel,
                StartMonth = (Byte)request.StartMonth,
                StartYear = request.StartYear,
                EndMonth = (Byte)request.EndMonth,
                EndYear = request.EndYear
            };

            ApplicantSkillPoco[] pocos = new ApplicantSkillPoco[1];
            pocos[0] = poco;
            return pocos;
        }

    }
}
