using System.ComponentModel.DataAnnotations;

namespace MyWork.Models
{
    public class 客戶聯絡人批次更新ViewModel
    {
        public int Id { get; set; }
        public string 職稱 { get; set; }
        [Required]
        public string 手機 { get; set; }
        [Required]
        public string 電話 { get; set; }
    }
}