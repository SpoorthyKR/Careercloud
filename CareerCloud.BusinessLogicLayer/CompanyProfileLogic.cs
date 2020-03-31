using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
        {
        }
        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (CompanyProfilePoco poco in pocos)
            {
                //CompanyWebsite	Valid websites must end with the following extensions – ".ca", ".com", ".biz"	600
                List<String> _validExtensions = new List<String>() {
                    ".ca", ".com", ".biz"
                };
                if (!String.IsNullOrEmpty(poco.CompanyWebsite) && !_validExtensions.Any(x => poco.CompanyWebsite.EndsWith(x)))
                {
                    exceptions.Add(new ValidationException(600,
                        "Valid websites must end with the following extensions – \".ca\", \".com\", \".biz\" "));
                }
                //ContactPhone Must correspond to a valid phone number(e.g. 416 - 555 - 1234)	601
                if (string.IsNullOrEmpty(poco.ContactPhone) ||
                     !Regex.IsMatch(poco.ContactPhone, @"^\s*[0-9]{3}\s*-\s*[0-9]{3}\s*-\s*[0-9]{4}\s*$")
                    )
                {
                    exceptions.Add(new ValidationException(601,
                       "ContactPhone Must correspond to a valid phone number(e.g. 416 - 555 - 1234)"));
                }

            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
