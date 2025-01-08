using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptEMS.Models
{
    public class Skill
    {
        public int SkillID { get; set; }
        public string SkillName { get; set; }
        public bool IsActive { get; set; }
    }
}