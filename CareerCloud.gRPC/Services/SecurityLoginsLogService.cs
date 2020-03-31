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
using static CareerCloud.gRPC.Protos.SecurityLoginsLog;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginsLogService : SecurityLoginsLogBase
    {
        private readonly SecurityLoginsLogLogic _logic;

        public SecurityLoginsLogService()
        {
            _logic = new SecurityLoginsLogLogic(new EFGenericRepository<SecurityLoginsLogPoco>());
        }

        public override Task<SecurityLoginsLogPayload> ReadSecurityLoginsLog(SecurityLoginsLogRequest request, ServerCallContext context)
        {
            SecurityLoginsLogPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<SecurityLoginsLogPayload>(
                () => new SecurityLoginsLogPayload()
                {
                    Id = poco.Id.ToString(),
                    Login = poco.Login.ToString(),
                    SourceIP = poco.SourceIP,
                    LogonDate = Timestamp.FromDateTime((DateTime)poco.LogonDate),
                    IsSuccesful = poco.IsSuccesful
                });
        }
        public override Task<Empty> CreateSecurityLoginsLog(SecurityLoginsLogPayload request, ServerCallContext context)
        {
            _logic.Add(GetArray(request));
            return new Task<Empty>(null);
        }

        public override Task<Empty> UpdateSecurityLoginsLog(SecurityLoginsLogPayload request, ServerCallContext context)
        {
            _logic.Update(GetArray(request));
            return new Task<Empty>(null);
        }

        public override Task<Empty> DeleteSecurityLoginsLog(SecurityLoginsLogPayload request, ServerCallContext context)
        {
            _logic.Delete(GetArray(request));
            return new Task<Empty>(null);
        }

        public SecurityLoginsLogPoco[] GetArray(SecurityLoginsLogPayload request)
        {
            SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco()
            {
                Id = Guid.Parse(request.Id),
                Login = Guid.Parse(request.Login),
                SourceIP = request.SourceIP,
                LogonDate = request.LogonDate.ToDateTime(),
                IsSuccesful = request.IsSuccesful
            };

            SecurityLoginsLogPoco[] pocos = new SecurityLoginsLogPoco[1];
            pocos[0] = poco;
            return pocos;
        }
    }
}
