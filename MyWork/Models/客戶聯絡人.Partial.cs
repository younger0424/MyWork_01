namespace MyWork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var db = new 客戶資料Entities();
            //create
            if (this .Id  == 0)
            {
                if (db.客戶聯絡人.Where(p => p.客戶Id == this.客戶Id && p.Email == this.Email).Any())
                {
                    yield return new ValidationResult("Email 已存在" , new string[] { "Email"});
                }
            }else
            {
                //update
                if (db.客戶聯絡人.Where(p => p.Id != this.Id && p.客戶Id == this.客戶Id && p.Email == this.Email).Any())
                {
                    yield return new ValidationResult("Email 已存在", new string[] { "Email" });
                }

            }
            yield return ValidationResult.Success;
        }
    }

    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }

        [EmailAddress]
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        public string Email { get; set; }

        [RegularExpression("[0-9]{4}-[0-9]{6}", ErrorMessage = "手機號碼格式不符合")]
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 手機 { get; set; }

        [RegularExpression("[0-9]{2}-[0-9]{8}", ErrorMessage = "電話號碼不符合格式")]
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
