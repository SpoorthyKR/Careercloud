using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantProfileLogic : BaseLogic<ApplicantProfilePoco>
    {
        public ApplicantProfileLogic(IDataRepository<ApplicantProfilePoco> repository) : base(repository)
        {
        }
        public override void Add(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(ApplicantProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (ApplicantProfilePoco poco in pocos)
            {
                //CurrentSalary CurrentSalary cannot be negative    111
                if (poco.CurrentSalary < 0)
                {
                    exceptions.Add(new ValidationException(111,
                        "CurrentSalary cannot be negative"));
                }
                //CurrentRate CurrentRate cannot be negative  112
                if (poco.CurrentRate < 0)
                {
                    exceptions.Add(new ValidationException(112,
                        "CurrentRate cannot be negative"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
