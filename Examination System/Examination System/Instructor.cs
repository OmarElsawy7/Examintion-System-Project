using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination_System
{
     class Instructor : Person
    {
        //Name
        //Specialization
        //Can teach multiple courses
        public string Specialization { get; set; }

        private Course[] _courses = new Course[Limites.MaxCoursesPerInstructor];
        private int _courseCount = 0;

        public Instructor(string name , string Spec)
        {
            ID = Ids.Next_Id_Instructor();
            Name = name;
            Specialization= Spec;
        }

        //  AssignCourse

        public bool AssignCourse(Course c)
        {
            if (c == null)
            {
                return false;
            }

            if (_courseCount >= Limites.MaxCoursesPerInstructor)
            {
                return false;
            }

            for (int i = 0; i < _courseCount; i++)
            {
                if (_courses[i] == c)
                {
                    return false;
                }

                _courses[_courseCount++] = c;
            }

            return true;
        }


        public override string ToString()
        {
            return $"[{ID}] {Name} | {Specialization}";
        }
    }
}
