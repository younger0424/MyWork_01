namespace MyWork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Security;


    [MetadataType(typeof(客戶資料MetaData))]
    public partial class 客戶資料
    {
        public void 對密碼進行雜湊運算() { this.密碼 = FormsAuthentication.HashPasswordForStoringInConfigFile(this.密碼, "SHA1"); }
　　　　　
    }
    
    public partial class 客戶資料MetaData
    {
        [Required]
        public int Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 客戶名稱 { get; set; }
        
        [StringLength(8, ErrorMessage="欄位長度不得大於 8 個字元")]
        [Required]
        public string 統一編號 { get; set; }

        [RegularExpression("[0-9]{2}-[0-9]{8}", ErrorMessage = "電話號碼不符合格式")]
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 電話 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 傳真 { get; set; }
        
        [StringLength(100, ErrorMessage="欄位長度不得大於 100 個字元")]
        public string 地址 { get; set; }

        [EmailAddress]
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        public string Email { get; set; }
        [UIHint("客戶分類")]
        public string 客戶分類 { get; set; }

        public string 帳號 { get; set; }
        [DataType(DataType.Password)]
        public string 密碼 { get; set; }

        public virtual ICollection<客戶銀行資訊> 客戶銀行資訊 { get; set; }
        public virtual ICollection<客戶聯絡人> 客戶聯絡人 { get; set; }

    }
}
