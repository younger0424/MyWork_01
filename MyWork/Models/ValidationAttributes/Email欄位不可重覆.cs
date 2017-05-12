using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWork.Models.ValidationAttributes
{
    public class Email欄位不可重覆Attribute : DataTypeAttribute
    {
        public Email欄位不可重覆Attribute() :base (DataType.Text) { }
        private 客戶資料Entities db = new 客戶資料Entities();

    }
}