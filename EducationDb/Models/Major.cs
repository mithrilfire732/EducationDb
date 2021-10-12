﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationDb.Models
{
    public class Major
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int MinSat { get; set; }

        public virtual IEnumerable<Student> Students { get; set; }
        public Major() { }
    }
}
