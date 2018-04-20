using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PLFApp.Server.Core
{
    public class BaseFullEntity<PrimaryKeyType> : BaseEntity<PrimaryKeyType>, ISoftDelete, ICreateDateTime
    {
        private bool _isdelete = false;       
        public bool IsDelete { get => _isdelete; set => _isdelete = value; }

        private DateTime _createdatetime = DateTime.Now;
        public DateTime CreateDateTime { get => _createdatetime; set => _createdatetime = value; }

        [NotMapped]
        public string CreateDateTimeFormat { get => _createdatetime.ToString(Constant.DATETIMEFORMAT); }
    }
}
