using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyLocationLogic : BaseLogic<CompanyLocationPoco>
    {
        public CompanyLocationLogic(IDataRepository<CompanyLocationPoco> repository) : base(repository)
        {
        }
        public override void Add(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(CompanyLocationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (CompanyLocationPoco poco in pocos)
            {
                //CountryCode	CountryCode cannot be empty	500
                if (string.IsNullOrEmpty(poco.CountryCode))
                {
                    exceptions.Add(new ValidationException(500,
                        $"CountryCode cannot be empty : Id = ${poco.Id}"));
                }
                //Province Province cannot be empty    501
                if (string.IsNullOrEmpty(poco.Province))
                {
                    exceptions.Add(new ValidationException(501,
                        $"Province cannot be empty : Id = ${poco.Id}"));
                }
                //Street Street cannot be empty  502
                if (string.IsNullOrEmpty(poco.Street))
                {
                    exceptions.Add(new ValidationException(502,
                        $"Street cannot be empty : Id = ${poco.Id}"));
                }
                //City City cannot be empty    503
                if (string.IsNullOrEmpty(poco.City))
                {
                    exceptions.Add(new ValidationException(503,
                        $"City cannot be empty : Id = ${poco.Id}"));
                }
                //PostalCode PostalCode cannot be empty  504
                if (string.IsNullOrEmpty(poco.PostalCode))
                {
                    exceptions.Add(new ValidationException(504,
                        $"PostalCode cannot be empty : Id = ${poco.Id}"));
                }

            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }

        }
    }
}
