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
using static CareerCloud.gRPC.Protos.SystemLanguageCode;

namespace CareerCloud.gRPC.Services
{
    public class SystemLanguageCodeService : SystemLanguageCodeBase
    {
        private readonly SystemLanguageCodeLogic _logic;

        public SystemLanguageCodeService()
        {
            _logic = new SystemLanguageCodeLogic(new EFGenericRepository<SystemLanguageCodePoco>());
        }

        public override Task<SystemLanguageCodePayload> ReadSystemLanguageCode(SystemLanguageCodeRequest request, ServerCallContext context)
        {
            SystemLanguageCodePoco poco = _logic.Get(request.LanguageID);
            return new Task<SystemLanguageCodePayload>(
                () => new SystemLanguageCodePayload()
                {
                    LanguageID = poco.LanguageID,
                    Name = poco.Name,
                    NativeName = poco.NativeName
                });
        }

        public override Task<Empty> CreateSystemLanguageCode(SystemLanguageCodePayload request, ServerCallContext context)
        {
            _logic.Add(GetArray(request));
            return new Task<Empty>(null);
        }

        public override Task<Empty> UpdateSystemLanguageCode(SystemLanguageCodePayload request, ServerCallContext context)
        {
            _logic.Update(GetArray(request));
            return new Task<Empty>(null);

        }

        public override Task<Empty> DeleteSystemLanguageCode(SystemLanguageCodePayload request, ServerCallContext context)
        {
            _logic.Delete(GetArray(request));
            return new Task<Empty>(null);
        }

        public SystemLanguageCodePoco[] GetArray(SystemLanguageCodePayload request)
        {
            SystemLanguageCodePoco poco = new SystemLanguageCodePoco()
            {
                LanguageID = request.LanguageID,
                Name = request.Name,
                NativeName = request.NativeName
            };

            SystemLanguageCodePoco[] pocos = new SystemLanguageCodePoco[1];
            pocos[0] = poco;
            return pocos;
        }
    }
}
