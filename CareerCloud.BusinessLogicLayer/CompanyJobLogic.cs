using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobLogic : BaseLogic<CompanyJobPoco>
    {
        public CompanyJobLogic(IDataRepository<CompanyJobPoco> repository) : base(repository)
        {
        }
        public override void Add(CompanyJobPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyJobPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(CompanyJobPoco[] pocos)
        {
            //List<ValidationException> exceptions = new List<ValidationException>();
            //foreach (CompanyJobPoco poco in pocos) {


            //}

            //if (exceptions.Count > 0)
            //{
            //    throw new AggregateException(exceptions);
            //}
        }
    }
}