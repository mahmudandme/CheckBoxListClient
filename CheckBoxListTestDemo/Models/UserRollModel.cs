using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckBoxListTestDemo.Models
{
    public class UserRollModel
    {
        public int ID { get; set; }
        public string RollName { get; set; }
        public string RollValue { get; set; }
        public string Status { get; set; }
        public int AccessRollId { get; set; }
    }
}