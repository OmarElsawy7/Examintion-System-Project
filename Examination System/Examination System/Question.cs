using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination_System
{
     abstract class Question
    {
        public string Text { get; set; }
        public int Mark { get; set; }
        public Question(string text , int mark) 
        { 
            Text = text;
            Mark =  Math.Max(0, mark); 
        }

        // Return awarded marks based on student's answer.
        public abstract int Grade(string studentAnswer);

        // For UI rendering
        public abstract void Print();
    }
}
