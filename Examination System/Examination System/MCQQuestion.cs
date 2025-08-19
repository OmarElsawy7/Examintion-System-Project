using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination_System
{
     class MCQQuestion : Question
    {

        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }


        public string Correct { get; set; }
        public MCQQuestion(string text ,int mark ,string correct,string A,string B ,string C, string D)
            :base(text,mark)
        {
            OptionA = A;
            OptionB=B;
            OptionC = C;
            OptionD = D;
            Correct =(correct ?? "").Trim().ToUpperInvariant();
        }
        public override int Grade(string studentAnswer)
        {
            string ans = (studentAnswer ?? "").Trim().ToUpperInvariant();
            return ans == Correct ? Mark : 0;
        }

        public override void Print()
        {
            Console.WriteLine($"[MCQ] ({Mark}) {Text}");
            Console.WriteLine($"A) {OptionA}");
            Console.WriteLine($"B) {OptionB}");
            Console.WriteLine($"C) {OptionC}");
            Console.WriteLine($"D) {OptionD}");
            Console.Write("Your answer (A/B/C/D): ");
        }
    }
}
