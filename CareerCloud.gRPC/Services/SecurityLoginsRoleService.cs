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
using static CareerCloud.gRPC.Protos.SecurityLoginsRole;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginsRoleService : SecurityLoginsRoleBase
    {
        private readonly SecurityLoginsRoleLogic _logic;

        public SecurityLoginsRoleService()
        {
            _logic = new SecurityLoginsRoleLogic(new EFGenericRepository<SecurityLoginsRolePoco>());
        }

        public override Task<SecurityLoginsRolePayload> ReadSecurityLoginsRole(SecurityLoginsRoleRequest request, ServerCallContext context)
        {
            SecurityLoginsRolePoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<SecurityLoginsRolePayload>(
                () => new SecurityLoginsRolePayload()
                {
                    Id = poco.Id.ToString(),
                    Login = poco.Login.ToString(),
                    Role = poco.Role.ToString()
                });
        }

        public override Task<Empty> CreateSecurityLoginsRole(SecurityLoginsRolePayload request, ServerCallContext context)
        {
            _logic.Add(GetArray(request));
            return new Task<Empty>(null);
        }

        public override Task<Empty> UpdateSecurityLoginsRole(SecurityLoginsRolePayload request, ServerCallContext context)
        {
            _logic.Update(GetArray(request));
            return new Task<Empty>(null);
        }

        public override Task<Empty> DeleteSecurityLoginsRole(SecurityLoginsRolePayload request, ServerCallContext context)
        {
            _logic.Delete(GetArray(request));
            return new Task<Empty>(null);
        }

        public SecurityLoginsRolePoco[] GetArray(SecurityLoginsRolePayload request)
        {
            SecurityLoginsRolePoco poco = new SecurityLoginsRolePoco()
            {
                Id = Guid.Parse(request.Id),
                Login = Guid.Parse(request.Login),
                Role = Guid.Parse(request.Role)
            };

            SecurityLoginsRolePoco[] pocos = new SecurityLoginsRolePoco[1];
            pocos[0] = poco;
            return pocos;
        }


    }
}
