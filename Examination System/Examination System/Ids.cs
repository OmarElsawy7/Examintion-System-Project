using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination_System
{
    static class Ids
    {
        //fields[s_id , i_id , res_id]
        private static int Id_Student = 0;
        private static int Id_Instructor = 0;
        private static int Id_Result = 0;
        private static int Id_Course = 0;

        public static int Next_Id_Student()
        {
            return ++Id_Student;
        }

        public static int Next_Id_Instructor()
        {
            return ++Id_Instructor;
        }

        public static int Next_Id_Result()
        {
            return ++Id_Result;
        }
    }
}
