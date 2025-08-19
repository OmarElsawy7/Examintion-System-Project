using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Examination_System
{
     class EssayQuestion : Question
    {

        public EssayQuestion(string text, int mark) : base(text, mark)
        { 
            

        }

        public override int Grade(string studentAnswer)
        {
            // Essay is not auto-graded; returns 0 initially.
            return 0;
        }

        public override void Print()
        {
            Console.WriteLine($"[Essay] ({Mark}) {Text}");
            Console.Write("Your answer (free text): ");
        }
    }
}
