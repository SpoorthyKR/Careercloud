using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CareerCloud.Pocos
{
    [Table("Company_Descriptions")]
    public class CompanyDescriptionPoco : IPoco
    {
      [Key]
        public Guid Id { get; set; }
        public Guid Company { get; set; }
        [Column("LanguageID")]
        public String LanguageId { get; set; }
        [Column("Company_Name")]
        public String CompanyName { get; set; }
        [Column("Company_Description")]
        public String CompanyDescription { get; set; }
        [Column("Time_Stamp")]
        [NotMapped]
        public Byte[] TimeStamp { get; set; }



        [ForeignKey("Company")]
        public virtual  CompanyProfilePoco CompanyProfile { get; set; }

        [ForeignKey("LanguageId")]
        public virtual  SystemLanguageCodePoco SystemLanguageCode { get; set; }


    }
}
