using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination_System
{
     class Student : Person
    {
        //ID
        //Name
        //Email
        //Can enroll in multiple courses
        public string Email { get; set; }
        // array courses :
        private Course[] courses = new Course[Limites.MaxEnrollmentsPerStudent];
        private int _enrolledCount = 0;// counter enroll
        public Student ( string name,string email)
        {
            ID = Ids.Next_Id_Student();
            Name = name;
            Email = email;
        }

        public bool Enroll(Course c)
        {
            if (c == null) return false;
            if (_enrolledCount >= Limites.MaxEnrollmentsPerStudent) return false;
            // avoid duplicates
            for (int i = 0; i < _enrolledCount; i++)
                if (courses[i] == c) return false;

            courses[_enrolledCount++] = c;
            return true;
        }

        public bool IsEnrolledIn(Course c)
        {
            for (int i = 0; i < _enrolledCount; i++)
                if (courses[i] == c) return true;
            return false;
        }
        public override string ToString()
        {
            return $"[{ID}] {Name} | {Email}";

        }

    }
}