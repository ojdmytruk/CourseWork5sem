﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB.Models.EditModels
{
    public class EditStudentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdGroup { get; set; }
        public string GroupName { get; set; }
        public double StudentAvg { get; set; }
    }
}