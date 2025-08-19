using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination_System
{
     class TrueFalseQuestion : Question
    {
        public bool True_OR_False { get; set; }

        public TrueFalseQuestion(string text, int mark, bool true_OR_False)
            : base(text, mark)
        {
            True_OR_False = true_OR_False;
        }

        public override int Grade(string studentAnswer)
        {
            string ans = (studentAnswer ?? "").Trim().ToLowerInvariant();
            bool parsed = ans == "true" || ans == "t" || ans == "1" || ans == "yes" || ans == "y";
            return parsed == True_OR_False ? Mark : 0;
        }

        public override void Print()
        {
            Console.WriteLine($"[True/False] ({Mark}) {Text}");
            Console.Write("Your answer (True/False): ");
        }
    }
}
