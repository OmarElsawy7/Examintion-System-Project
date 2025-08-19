using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination_System
{
     class Course
    {
         //Title
         //Description
         //Maximum degree

        public string Title { get; set; }
        public string Description { get; set; }
        public int MaximumDegree { get; set; }

        public Course(string title, string description, int maxDeg)
        {
            Title = title;
            Description = description;
            MaximumDegree = Math.Max(0, maxDeg);
        }

        public override string ToString()
        {
            return $"{Title} | Max Degree: {MaximumDegree} | {Description}";
        }
    }
}
