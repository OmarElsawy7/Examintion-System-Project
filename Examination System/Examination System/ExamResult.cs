using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination_System
{
     class ExamResult
    {
        public int ResultId { get; private set; }
        public Student Student { get; private set; }
        public Exam Exam { get; private set; }
        public DateTime DateTaken { get; private set; }

        // store answers and awarded marks per question
        private string[] _answers;
        private int[] _awarded;
        private int _qCount;

        public ExamResult(Student s, Exam e)
        {
            ResultId = Ids.Next_Id_Result();
            Student = s;
            Exam = e;
            DateTaken = DateTime.Now;
            _qCount = e.QuestionCount;
            _answers = new string[_qCount];
            _awarded = new int[_qCount];
        }

        public void SetAnswer(int qIndex, string answer, int awarded)
        {
            if (qIndex < 0 || qIndex >= _qCount) return;
            _answers[qIndex] = answer;
            _awarded[qIndex] = Math.Max(0, awarded);
        }

        public bool GradeEssayManually(int qIndex, int mark)
        {
            if (qIndex < 0 || qIndex >= _qCount) return false;
            Question q = Exam.GetQuestion(qIndex);
            if (q is EssayQuestion)
            {
                _awarded[qIndex] = Math.Max(0, Math.Min(mark, q.Mark));
                return true;
            }
            return false;
        }

        public int TotalScore()
        {
            int sum = 0;
            for (int i = 0; i < _qCount; i++) sum += _awarded[i];
            return sum;
        }

        public void PrintReport()
        {
            int score = TotalScore();
            int max = Exam.TotalMarks();
            bool pass = score >= (max * 0.5);

            Console.WriteLine("\n--- Exam Result ---");
            Console.WriteLine($"Exam: {Exam.Title}");
            Console.WriteLine($"Student: {Student.Name}");
            Console.WriteLine($"Course: {Exam.Course.Title}");
            Console.WriteLine($"Score: {score} / {max}");
            Console.WriteLine($"Status: {(pass ? "PASS" : "FAIL")}");
            Console.WriteLine($"Date: {DateTaken:yyyy-MM-dd HH:mm}");
        }

        public int CompareTo(ExamResult other)
        {
            if (other == null) return 1;
            return TotalScore().CompareTo(other.TotalScore());
        }

        public override string ToString()
        {
            return $"Result #{ResultId} | {Student.Name} | {Exam.Title} | {TotalScore()}/{Exam.TotalMarks()}";
        }
    }
}

