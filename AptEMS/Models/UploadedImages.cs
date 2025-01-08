using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptEMS.Models
{
    public class UploadedImages
    {


        public int ID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }

        public bool IsActive { get; set; } // New property

    }
}