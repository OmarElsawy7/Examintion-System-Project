using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination_System
{
    class ExamApp
    {
        private Course[] _courses = new Course[Limites.MaxCourses];
        private int _courseCount = 0;

        private Student[] _students = new Student[Limites.MaxStudents];
        private int _studentCount = 0;

        private Instructor[] _instructors = new Instructor[Limites.MaxInstructors];
        private int _instructorCount = 0;

        private Exam[] _exams = new Exam[Limites.MaxExams];
        private int _examCount = 0;

        private ExamResult[] _results = new ExamResult[Limites.MaxResults];
        private int _resultCount = 0;

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\n=== Examination System ===");
                Console.WriteLine("1) Add Course");
                Console.WriteLine("2) Add Student");
                Console.WriteLine("3) Add Instructor");
                Console.WriteLine("4) Enroll Student in Course");
                Console.WriteLine("5) Assign Course to Instructor");
                Console.WriteLine("6) Create Exam for Course");
                Console.WriteLine("7) Add/Edit/Remove Questions in Exam");
                Console.WriteLine("8) Start (Lock) Exam");
                Console.WriteLine("9) Duplicate Exam to Another Course");
                Console.WriteLine("10) Student Takes Exam");
                Console.WriteLine("11) Grade Essay (Manual)");
                Console.WriteLine("12) Show Report for a Student's Exam");
                Console.WriteLine("13) Compare Two Students in an Exam");
                Console.WriteLine("14) List Courses/Students/Instructors/Exams");
                Console.WriteLine("0) Exit");
                Console.Write("Choose: ");
                string ch = Console.ReadLine();

                try
                {
                    switch (ch)
                    {
                        case "1": MenuAddCourse(); break;
                        case "2": MenuAddStudent(); break;
                        case "3": MenuAddInstructor(); break;
                        case "4": MenuEnrollStudent(); break;
                        case "5": MenuAssignCourseToInstructor(); break;
                        case "6": MenuCreateExam(); break;
                        case "7": MenuManageQuestions(); break;
                        case "8": MenuStartExam(); break;
                        case "9": MenuDuplicateExam(); break;
                        case "10": MenuStudentTakesExam(); break;
                        case "11": MenuGradeEssay(); break;
                        case "12": MenuShowReport(); break;
                        case "13": MenuCompareStudents(); break;
                        case "14": MenuListAll(); break;
                        case "0": return;
                        default: Console.WriteLine("Invalid choice."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        // -------- Lookups ----------
        private Course FindCourseByTitle(string title)
        {
            for (int i = 0; i < _courseCount; i++)
                if (_courses[i].Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                    return _courses[i];
            return null;
        }

        private Student FindStudentById(int id)
        {
            for (int i = 0; i < _studentCount; i++)
                if (_students[i].ID == id) return _students[i];
            return null;
        }

        private Instructor FindInstructorById(int id)
        {
            for (int i = 0; i < _instructorCount; i++)
                if (_instructors[i].ID == id) return _instructors[i];
            return null;
        }

        private Exam FindExamByTitle(string title)
        {
            for (int i = 0; i < _examCount; i++)
                if (_exams[i].Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                    return _exams[i];
            return null;
        }

        private ExamResult FindResult(int resultId)
        {
            for (int i = 0; i < _resultCount; i++)
                if (_results[i].ResultId == resultId) return _results[i];
            return null;
        }

        // -------- Menus ----------
        private void MenuAddCourse()
        {
            Console.Write("Course Title: ");
            string t = Console.ReadLine();
            Console.Write("Description: ");
            string d = Console.ReadLine();
            int max = ReadInt("Maximum Degree: ");

            if (_courseCount >= Limites.MaxCourses) { Console.WriteLine("Capacity full."); return; }
            if (FindCourseByTitle(t) != null) { Console.WriteLine("Course already exists."); return; }

            _courses[_courseCount++] = new Course(t, d, max);
            Console.WriteLine("Course added.");
        }

        private void MenuAddStudent()
        {
            Console.Write("Student Name: "); string n = Console.ReadLine();
            Console.Write("Email: "); string e = Console.ReadLine();

            if (_studentCount >= Limites.MaxStudents) { Console.WriteLine("Capacity full."); return; }
            _students[_studentCount++] = new Student(n, e);
            Console.WriteLine($"Student added. ID = {_students[_studentCount - 1].ID}");
        }

        private void MenuAddInstructor()
        {
            Console.Write("Instructor Name: "); string n = Console.ReadLine();
            Console.Write("Specialization: "); string s = Console.ReadLine();

            if (_instructorCount >= Limites.MaxInstructors) { Console.WriteLine("Capacity full."); return; }
            _instructors[_instructorCount++] = new Instructor(n, s);
            Console.WriteLine($"Instructor added. ID = {_instructors[_instructorCount - 1].ID}");
        }

        private void MenuEnrollStudent()
        {
            int sid = ReadInt("Student ID: ");
            Console.Write("Course Title: "); 
            string title = Console.ReadLine();

            Student st = FindStudentById(sid);
            Course c = FindCourseByTitle(title);
            if (st == null || c == null) { 
                Console.WriteLine("Student/Course not found."); 
                return;
            }

            Console.WriteLine(st.Enroll(c) ? "Enrolled." : "Enroll failed.");
        }

        private void MenuAssignCourseToInstructor()
        {
            int iid = ReadInt("Instructor ID: ");
            Console.Write("Course Title: "); 
            string title = Console.ReadLine();

            var ins = FindInstructorById(iid);
            var c = FindCourseByTitle(title);
            if (ins == null || c == null) {
                Console.WriteLine("Instructor/Course not found.");
                return; 
            }

            Console.WriteLine(ins.AssignCourse(c) ? "Assigned." : "Assign failed.");
        }

        private void MenuCreateExam()
        {
            Console.Write("Exam Title: "); string et = Console.ReadLine();
            Console.Write("Course Title: "); string ct = Console.ReadLine();

            if (_examCount >= Limites.MaxExams) { Console.WriteLine("Capacity full."); return; }
            if (FindExamByTitle(et) != null) { Console.WriteLine("Exam title already exists."); return; }

            var c = FindCourseByTitle(ct);
            if (c == null) { Console.WriteLine("Course not found."); return; }

            _exams[_examCount++] = new Exam(et, c);
            Console.WriteLine("Exam created.");
        }

        private void MenuManageQuestions()
        {
            Console.Write("Exam Title: "); string et = Console.ReadLine();
            Exam ex = FindExamByTitle(et);
            if (ex == null) { 
                Console.WriteLine("Exam not found.");
                return; 
            }
            if (ex.IsStarted) { 
                Console.WriteLine("Exam locked. Cannot modify.");
                return; 
            }

            while (true)
            {
                Console.WriteLine($"\n--- Manage Questions for '{ex.Title}' --- Total: {ex.TotalMarks()}/{ex.Course.MaximumDegree} (Q: {ex.QuestionCount})");
                Console.WriteLine("1) Add MCQ");
                Console.WriteLine("2) Add True/False");
                Console.WriteLine("3) Add Essay");
                Console.WriteLine("4) Edit Question Mark");
                Console.WriteLine("5) Remove Question");
                Console.WriteLine("0) Back");
                Console.Write("Choose: ");
                string ch = Console.ReadLine();
                if (ch == "0") break;

                switch (ch)
                {
                    case "1":
                        Console.Write("Text: "); string t = Console.ReadLine();
                        int m1 = ReadInt("Mark: ");
                        Console.Write("Option A: "); string a = Console.ReadLine();
                        Console.Write("Option B: "); string b = Console.ReadLine();
                        Console.Write("Option C: "); string c = Console.ReadLine();
                        Console.Write("Option D: "); string d = Console.ReadLine();
                        Console.Write("Correct (A/B/C/D): "); string corr = Console.ReadLine();
                        Console.WriteLine(ex.AddQuestion(new MCQQuestion(t, m1, corr ,a, b, c, d)) ? "Added." : "Add failed.");
                        break;

                    case "2":
                        Console.Write("Text: "); 
                        string t2 = Console.ReadLine();
                        int m2 = ReadInt("Mark: ");
                        bool correct = ReadBool("Correct answer? (true/false): ");
                        Console.WriteLine(ex.AddQuestion(new TrueFalseQuestion(t2, m2, correct)) ? "Added." : "Add failed.");
                        break;

                    case "3":
                        Console.Write("Text: "); 
                        string t3 = Console.ReadLine();
                        int m3 = ReadInt("Mark: ");
                        Console.WriteLine(ex.AddQuestion(new EssayQuestion(t3, m3)) ? "Added." : "Add failed.");
                        break;

                    case "4":
                        int idx = ReadInt("Question index (0-based): ");
                        int newMark = ReadInt("New Mark: ");
                        Console.WriteLine(ex.EditQuestionMark(idx, newMark) ? "Updated." : "Update failed.");
                        break;

                    case "5":
                        int r = ReadInt("Question index (0-based): ");
                        Console.WriteLine(ex.RemoveQuestion(r) ? "Removed." : "Remove failed.");
                        break;

                    default:
                        Console.WriteLine("Invalid.");
                        break;
                }
            }
        }

        private void MenuStartExam()
        {
            Console.Write("Exam Title: "); 
            string et = Console.ReadLine();
            Exam ex = FindExamByTitle(et);
            if (ex == null) { 
                Console.WriteLine("Exam not found.");
                return; 
            }
            ex.Start();
        }

        private void MenuDuplicateExam()
        {
            Console.Write("Existing Exam Title: ");
            string oldT = Console.ReadLine();
            Exam ex = FindExamByTitle(oldT);
            if (ex == null) { 
                Console.WriteLine("Exam not found."); 
                return;
            }

            Console.Write("New Exam Title: "); 
            string newT = Console.ReadLine();
            Console.Write("Target Course Title: ");
            string ct = Console.ReadLine();
            Course c = FindCourseByTitle(ct);
            if (c == null) {
                Console.WriteLine("Course not found."); 
                return; 
            }

            if (_examCount >= Limites.MaxExams) { 
                Console.WriteLine("Capacity full.");
                return; 
            }
            if (FindExamByTitle(newT) != null) { 
                Console.WriteLine("Exam title already exists."); 
                return; 
            }

            Exam copy = ex.DuplicateForCourse(c, newT);
            _exams[_examCount++] = copy;
            Console.WriteLine("Exam duplicated.");
        }

        private void MenuStudentTakesExam()
        {
            int sid = ReadInt("Student ID: ");
            Console.Write("Exam Title: "); 
            string et = Console.ReadLine();

            Student st = FindStudentById(sid);
            Exam ex = FindExamByTitle(et);
            if (st == null || ex == null) {
                Console.WriteLine("Student/Exam not found."); 
                return;
            }

            // Enrollment check
            if (!st.IsEnrolledIn(ex.Course))
            {
                Console.WriteLine("Student is not enrolled in this course.");
                return;
            }

            if (!ex.IsStarted)
            {
                Console.WriteLine("Exam has not been started/locked yet.");
                return;
            }

            ExamResult result = new ExamResult(st, ex);

            Console.WriteLine($"\n--- Taking Exam: {ex.Title} | Course: {ex.Course.Title} ---");
            for (int i = 0; i < ex.QuestionCount; i++)
            {
                Question q = ex.GetQuestion(i);
                q.Print();
                string ans = Console.ReadLine();

                int awarded = 0;
                if (q is EssayQuestion)
                {
                    // No auto grade
                    awarded = 0;
                }
                else
                {
                    awarded = q.Grade(ans);
                }
                result.SetAnswer(i, ans, awarded);
            }

            if (_resultCount >= Limites.MaxResults) { 
                Console.WriteLine("Result capacity full.");
                return; 
            }
            _results[_resultCount++] = result;

            Console.WriteLine("\nExam submitted!");

            Console.WriteLine("\nResult ID :" + result.ResultId);
            result.PrintReport();
        }

        private void MenuGradeEssay()
        {
            int rid = ReadInt("Result ID: ");
            ExamResult res = FindResult(rid);
            if (res == null) { 
                Console.WriteLine("Result not found."); 
                return;
            }

            Console.Write("Exam Title: ");
            string et = Console.ReadLine();
            if (!res.Exam.Title.Equals(et, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Result does not match exam."); 
                return;
            }

            int qIndex = ReadInt("Essay Question index (0-based): ");
            int mark = ReadInt("Awarded Mark: ");

            Console.WriteLine(res.GradeEssayManually(qIndex, mark) ? "Essay graded." : "Grade failed (index/type?).");
        }

        private void MenuShowReport()
        {
            int rid = ReadInt("Result ID: ");
            ExamResult res = FindResult(rid);
            if (res == null) { 
                Console.WriteLine("Result not found."); 
                return; 
            }
            res.PrintReport();
        }

        private void MenuCompareStudents()
        {
            Console.Write("Exam Title: ");
            string et = Console.ReadLine();
            Exam ex = FindExamByTitle(et);
            if (ex == null) { 
                Console.WriteLine("Exam not found."); 
                return;
            }

            int rid1 = ReadInt("First Result ID: ");
            int rid2 = ReadInt("Second Result ID: ");

            ExamResult r1 = FindResult(rid1);
            ExamResult r2 = FindResult(rid2);

            if (r1 == null || r2 == null) { Console.WriteLine("One or both results not found."); return; }
            if (r1.Exam != ex || r2.Exam != ex) { Console.WriteLine("Both results must be for the same exam."); return; }

            Console.WriteLine($"\nCompare on Exam: {ex.Title}");
            Console.WriteLine($"{r1.Student.Name}: {r1.TotalScore()}/{ex.TotalMarks()}");
            Console.WriteLine($"{r2.Student.Name}: {r2.TotalScore()}/{ex.TotalMarks()}");

            int cmp = r1.CompareTo(r2);
            if (cmp > 0) Console.WriteLine($"Winner: {r1.Student.Name}");
            else if (cmp < 0) Console.WriteLine($"Winner: {r2.Student.Name}");
            else Console.WriteLine("Tie!");
        }

        private void MenuListAll()
        {
            Console.WriteLine("\n-- Courses --");
            for (int i = 0; i < _courseCount; i++) Console.WriteLine($"- {_courses[i]}");

            Console.WriteLine("\n-- Students --");
            for (int i = 0; i < _studentCount; i++) Console.WriteLine($"- {_students[i]}");

            Console.WriteLine("\n-- Instructors --");
            for (int i = 0; i < _instructorCount; i++) Console.WriteLine($"- {_instructors[i]}");

            Console.WriteLine("\n-- Exams --");
            for (int i = 0; i < _examCount; i++) Console.WriteLine($"- {_exams[i]}");
        }

        // -------- Helpers ----------
        private static int ReadInt(string prompt)
        {
            Console.Write(prompt);
            return int.Parse(Console.ReadLine() ?? "0");
        }

        private static bool ReadBool(string prompt)
        {
            Console.Write(prompt);
            string s = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();
            return s == "true" || s == "t" || s == "1" || s == "yes" || s == "y";
        }
    }
}
