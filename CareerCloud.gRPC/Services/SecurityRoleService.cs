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
using static CareerCloud.gRPC.Protos.SecurityRole;

namespace CareerCloud.gRPC.Services
{
    public class SecurityRoleService : SecurityRoleBase
    {
        private readonly SecurityRoleLogic _logic;

        public SecurityRoleService()
        {
            _logic = new SecurityRoleLogic(new EFGenericRepository<SecurityRolePoco>());
        }

        public override Task<SecurityRolePayload> ReadSecurityRole(SecurityRoleRequest request, ServerCallContext context)
        {
            SecurityRolePoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<SecurityRolePayload>(
                () => new SecurityRolePayload()
                {
                    Id = poco.Id.ToString(),
                    Role = poco.Role,
                    IsInactive = poco.IsInactive
                });
        }

        public override Task<Empty> CreateSecurityRole(SecurityRolePayload request, ServerCallContext context)
        {
            _logic.Add(GetArray(request));
            return new Task<Empty>(null);
        }

        public override Task<Empty> UpdateSecurityRole(SecurityRolePayload request, ServerCallContext context)
        {
            _logic.Update(GetArray(request));
            return new Task<Empty>(null);
        }

        public override Task<Empty> DeleteSecurityRole(SecurityRolePayload request, ServerCallContext context)
        {
            _logic.Delete(GetArray(request));
            return new Task<Empty>(null);
        }

        public SecurityRolePoco[] GetArray(SecurityRolePayload request)
        {
            SecurityRolePoco poco = new SecurityRolePoco()
            {
                Id = Guid.Parse(request.Id),
                Role = request.Role,
                IsInactive = request.IsInactive
            };

            SecurityRolePoco[] pocos = new SecurityRolePoco[1];
            pocos[0] = poco;
            return pocos;
        }
    }
}
