
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
   public class SystemLanguageCodeLogic : SystemLanguageCodePoco
    {
        protected IDataRepository<SystemLanguageCodePoco> _repository;

        public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository)
        {
            _repository = repository;
        }

        public void Add(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Add(pocos);
        }

        public void Update(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Update(pocos);
        }

        public void Delete(SystemLanguageCodePoco[] pocos)
        {
            _repository.Remove(pocos);
        }

        public List<SystemLanguageCodePoco> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public SystemLanguageCodePoco Get(string id)
        {
            return _repository.GetSingle(c => c.LanguageID == id);
        }

        protected void Verify(SystemLanguageCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (SystemLanguageCodePoco poco in pocos)
            {
                //LanguageID	Cannot be empty	1000
                if (String.IsNullOrEmpty(poco.LanguageID))
                {
                    exceptions.Add(new ValidationException(1000,
                        "LanguageID Cannot be empty"));
                }
                //Name Cannot be empty 1001
                if (String.IsNullOrEmpty(poco.Name))
                //if (poco.Name=="")
                {
                    exceptions.Add(new ValidationException(1001,
                        "Name Cannot be empty 1001"));
                }
                //NativeName Cannot be empty 1002
                if (String.IsNullOrEmpty(poco.NativeName))
                {
                    exceptions.Add(new ValidationException(1002,
                        "NativeName Cannot be empty	901 "));
                }

            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }

        }

    }
}

