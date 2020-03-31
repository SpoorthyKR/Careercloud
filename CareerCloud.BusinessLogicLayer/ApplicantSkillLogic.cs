using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantSkillLogic : BaseLogic<ApplicantSkillPoco>
    {
        public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> repository) : base(repository)
        {
        }
        public override void Add(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(ApplicantSkillPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (ApplicantSkillPoco poco in pocos)
            {
                //StartMonth	Cannot be greater than 12	101
                if (poco.StartMonth > 12)
                {
                    exceptions.Add(new ValidationException(101,
                        "StartMonth Cannot be greater than 12"));
                }
                //EndMonth Cannot be greater than 12   102
                if (poco.EndMonth > 12)
                {
                    exceptions.Add(new ValidationException(102,
                        "EndMonth Cannot be greater than 12"));
                }
                //StartYear Cannot be less then 1900    103
                if (poco.StartYear < 1900)
                {
                    exceptions.Add(new ValidationException(103,
                        "StartYear Cannot be less then 1900"));
                }
                //EndYear	Cannot be less then StartYear	104
                if (poco.EndYear < poco.StartYear)
                {
                    exceptions.Add(new ValidationException(104,
                        "EndYear Cannot be less then StartYear"));
                }

            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
