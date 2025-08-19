using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination_System
{
     class Exam
    {
        public string Title { get; private set; }
        public Course Course { get; private set; }
        public bool IsStarted { get; private set; } = false;

        private Question[] _questions = new Question[Limites.MaxQuestionsPerExam];
        private int _qCount = 0;

        public Exam(string title, Course course)
        {
            Title = title;
            Course = course ?? throw new ArgumentNullException(nameof(course));
        }

        public int QuestionCount => _qCount;

        public int TotalMarks()
        {
            int sum = 0;
            for (int i = 0; i < _qCount; i++) sum += _questions[i].Mark;
            return sum;
        }

        public bool AddQuestion(Question q)
        {
            if (IsStarted) { 
                Console.WriteLine("Cannot modify: exam already started.");
                return false; 
            }
            if (_qCount >= Limites.MaxQuestionsPerExam) return false;
            // Check course max degree
            if (TotalMarks() + q.Mark > Course.MaximumDegree)
            {
                Console.WriteLine("Cannot add: total marks would exceed course maximum.");
                return false;
            }
            _questions[_qCount++] = q;
            return true;
        }

        public bool EditQuestionMark(int index, int newMark)
        {
            if (IsStarted) { Console.WriteLine("Cannot modify: exam already started."); return false; }
            if (index < 0 || index >= _qCount) return false;
            int currentTotal = TotalMarks();
            int currentMark = _questions[index].Mark;
            int newTotal = currentTotal - currentMark + Math.Max(0, newMark);
            if (newTotal > Course.MaximumDegree)
            {
                Console.WriteLine("Cannot update: total marks would exceed course maximum.");
                return false;
            }
            _questions[index].Mark = Math.Max(0, newMark);
            return true;
        }

        public bool RemoveQuestion(int index)
        {
            if (IsStarted) {
                Console.WriteLine("Cannot modify: exam already started.");
                return false; 
            }
            if (index < 0 || index >= _qCount) return false;
            _questions[index] = _questions[_qCount - 1];
            _questions[_qCount - 1] = null;
            _qCount--;
            return true;
        }

        public void Start()
        {
            if (_qCount == 0) { 
                Console.WriteLine("Exam has no questions.");
                return; 
            }
            IsStarted = true;
            Console.WriteLine("Exam started and locked from further modifications.");
        }

        public Question GetQuestion(int i)
        {
            if (i < 0 || i >= _qCount) return null;
            return _questions[i];
        }

        public Exam DuplicateForCourse(Course newCourse, string newTitle = null)
        {
            if (newCourse == null) return null;
            Exam copy = new Exam(newTitle ?? (Title + " (Copy)"), newCourse);
            // Copy questions respecting new course maximum
            for (int i = 0; i < _qCount; i++)
            {
                Question q = _questions[i];
                Question cloned = null;
                if (q is MCQQuestion m)
                    cloned = new MCQQuestion( m.Text,m.Mark, m.Correct, m.OptionA, m.OptionB, m.OptionC, m.OptionD);
                else if (q is TrueFalseQuestion t)
                    cloned = new TrueFalseQuestion(t.Text, t.Mark, t.True_OR_False);
                else if (q is EssayQuestion e)
                    cloned = new EssayQuestion(e.Text, e.Mark);

                if (cloned != null)
                {
                    if (!copy.AddQuestion(cloned))
                    {
                        Console.WriteLine("Warning: some questions skipped due to new course max degree.");
                        break;
                    }
                }
            }
            return copy;
        }

        public override string ToString()
        {
            return $"Exam: {Title} | Course: {Course.Title} | Qs: {_qCount} | Total: {TotalMarks()}/{Course.MaximumDegree} | {(IsStarted ? "Started/Locked" : "Not started")}";
        }
    }
}

