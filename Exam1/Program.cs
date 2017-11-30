using Exam1.Models;
using Exam1.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleTables;
using System.Text;
using System.Threading.Tasks;

namespace Exam1
{
    class Program
    {
        public static List<Subject> subjects;

        public static List<Teacher> teachers;

        public static List<Class> classes;

        public static List<Course> courses;

        public static List<Student> students;

        public static List<Score> scores;

        public static string folder = "../../Data";

        public static List<string> menu;

        public static List<string> menuInput;

        public static List<string> menuShow;

        public static List<string> menuSearch;

        public static List<string> menuReport;

        static void Main(string[] args)
        {
            Init();
            MenuStart();
            Save();
        }

        static void Generator()
        {
            for (int i = 1; i <= 10; i++)
            {
                var subject = new Subject();
                subject.Id = i;
                subject.Name = $"Subject{i}";
                subject.WeightScore = i % 3 + 1;
                subjects.Add(subject);
            }

            for (int i = 1; i <= 10; i++)
            {
                var teacher = new Teacher();
                teacher.Id = i;
                teacher.Name = $"Teacher{i}";
                teacher.Birthday = new DateTime(1111, 11, 11);
                teacher.NativeLand = "Da Nang";
                teacher.SubjectId = i;
                teachers.Add(teacher);
            }

            for (int i = 1; i <= 5; i++)
            {
                var @class = new Class();
                @class.Id = i;
                @class.Name = $"Class{i}";
                @class.TeacherId = i;
                classes.Add(@class);
            }

            for (int i = 1; i <= 20; i++)
            {
                var student = new Student();
                student.Id = i;
                student.Name = $"Student{i}";
                student.Birthday = new DateTime(1111, 11, 11);
                student.NativeLand = "Quang Nam";
                student.ClassId = i % 5 + 1;
                students.Add(student);
            }

            for (int i = 1; i <= 20; i++)
            {
                var course = new Course();
                course.Id = i;
                course.ClassId = i % 5 + 1;
                course.SubjectId = i % 10 + 1;
                course.TeacherId = i % 10 + 1;
                courses.Add(course);
            }

            for (int i = 1; i <= 20; i++)
            {
                var score = new Score();
                score.Id = i;
                score.StudentId = i;
                score.CourseId = i;
                score.AverageScore = i % 3 + 8;
                scores.Add(score);
            }
        }

        static void Init()
        {
            subjects = (subjects.GetObject<Subject>(folder));
            teachers = teachers.GetObject<Teacher>(folder);
            classes = classes.GetObject<Class>(folder);
            courses = courses.GetObject<Course>(folder);
            students = students.GetObject<Student>(folder);
            scores = scores.GetObject<Score>(folder);

            menu = new List<string>();
            menu.Add("1 Nhap du lieu");
            menu.Add("2 Hien thi du lieu");
            menu.Add("3 Tim kiem du lieu");
            menu.Add("4 Bao cao");
            menu.Add("5 Thoat");

            menuInput = new List<string>();
            menuInput.Add("1 Nhap danh sach mon hoc");
            menuInput.Add("2 Nhap danh sach giao vien");
            menuInput.Add("3 Nhap danh sach lop hoc");
            menuInput.Add("4 Nhap danh sach sinh vien");
            menuInput.Add("5 Nhap danh sach khoa hoc");
            menuInput.Add("6 Nhap danh sach diem");
            menuInput.Add("7 Thoat");

            menuShow = new List<string>();
            menuShow.Add("1 Hien thi danh sach mon hoc");
            menuShow.Add("2 Hien thi danh sach giao vien");
            menuShow.Add("3 Hien thi danh sach lop hoc");
            menuShow.Add("4 Hien thi danh sach sinh vien");
            menuShow.Add("5 Thoat");

            menuSearch = new List<string>();
            menuSearch.Add("1 Tim ten hoc sinh");
            menuSearch.Add("2 Tim ten lop");
            menuSearch.Add("3 Thoat");

            menuReport = new List<string>();
            menuReport.Add("1 Danh sach hoc sinh gioi");
            menuReport.Add("2 Diem trung binh theo lop");
            menuReport.Add("3 Top 100 hoc sinh theo diem lop ten");
            menuReport.Add("4 Top 3 lop co so luong hoc sinh gioi nhieu nhat");
            menuReport.Add("5 Top 3 lop co diem trung binh cao nhat");
            menuReport.Add("6 Top 3 giao vien");
            menuReport.Add("7 Thoat");
        }

        static void MenuStart()
        {
            var pick = "";

            do
            {
                menu.ForEach(Console.WriteLine);

                pick = Console.ReadLine();
                switch (pick)
                {
                    case "1":
                        Input();
                        break;
                    case "2":
                        Show();
                        break;
                    case "3":
                        Search();
                        break;
                    case "4":
                        Report();
                        break;
                }
            }
            while (!pick.Equals("5"));
        }

        static void Save()
        {
            subjects.SaveJson(folder);
            teachers.SaveJson(folder);
            classes.SaveJson(folder);
            courses.SaveJson(folder);
            students.SaveJson(folder);
            scores.SaveJson(folder);
        }

        static void Input()
        {
            var pick = "";

            do
            {
                menuInput.ForEach(Console.WriteLine);

                pick = Console.ReadLine();
                switch (pick)
                {
                    case "1":
                        var subject = new Subject();
                        subject.Id = subjects.Count + 1;
                        subject.Input();
                        subjects.Add(subject);
                        break;
                    case "2":
                        var teacher = new Teacher();
                        teacher.Id = teachers.Count + 1;
                        teacher.Input();
                        teacher.SubjectId = subjects.GetId(true);
                        teachers.Add(teacher);
                        break;
                    case "3":
                        var @class = new Class();
                        @class.Id = classes.Count + 1;
                        @class.Input();
                        @class.TeacherId = teachers.GetId(true);
                        classes.Add(@class);
                        break;
                    case "4":
                        var student = new Student();
                        student.Id = students.Count + 1;
                        student.Input();
                        student.ClassId = classes.GetId(true);
                        students.Add(student);
                        break;
                    case "5":
                        var course = new Course();
                        course.Id = courses.Count + 1;
                        course.Input();
                        course.ClassId = classes.GetId(true);
                        course.SubjectId = subjects.GetId(true);
                        course.TeacherId = teachers.GetId(true);
                        courses.Add(course);
                        break;
                    case "6":
                        var score = new Score();
                        score.Id = scores.Count + 1;
                        score.CourseId = courses.GetId(true);
                        score.StudentId = GetStudentId(score.CourseId);
                        score.Input();
                        scores.Add(score);
                        break;
                }
            }
            while (!pick.Equals("7"));
        }

        static void Show()
        {
            var pick = "";

            do
            {
                menuShow.ForEach(Console.WriteLine);

                pick = Console.ReadLine();
                switch (pick)
                {
                    case "1":
                        subjects.ShowConsoleTable();
                        break;
                    case "2":
                        teachers
                            .Join(
                                classes,
                                x => x.Id,
                                y => y.TeacherId,
                                (x, y) => new
                                {
                                    x,
                                    ClassManager = y.Name
                                }).GroupJoin(
                                    courses,
                                    x => x.x.Id,
                                    y => y.TeacherId,
                                    (x, group) => new
                                    {
                                        x,
                                        group
                                    }).ToList()
                                    .ForEach(x =>
                                    {
                                        Console.WriteLine(x.x);
                                        x.group.ToList().ForEach(y=> 
                                        {
                                            Console.Write("Subject: ");
                                            subjects.Where(z => z.Id == y.SubjectId).ToList().ForEach(Console.Write);
                                            Console.WriteLine($" SumStudent: {students.Where(z => z.Id == y.ClassId).Count()}");
                                        });
                                    });

                        var teacherId = teachers.GetId(false);
                        courses
                            .Where(x => x.TeacherId == teacherId)
                            .ShowConsoleTable();

                        var courseId = courses.GetId(false);
                        scores
                            .Where(x => x.CourseId == courseId)
                            .ShowConsoleTable();

                        break;
                    case "3":
                        classes.Join(
                            teachers,
                            x => x.TeacherId,
                            y => y.Id,
                            (x, y) => new
                            {
                                x = x,
                                NameTeacher = y.Name
                            }).GroupJoin(
                                students,
                                x => x.x.Id,
                                y => y.ClassId,
                                (x, group) => new
                                {
                                    Id = x.x.Id,
                                    NameClass = x.x.Name,
                                    NameTeacher = x.NameTeacher,
                                    SumStudent = group.Count()
                                }).ShowConsoleTable();

                        var classId = classes.GetId(false);
                        courses
                            .Where(x => x.ClassId == classId)
                            .ShowConsoleTable();

                        courseId = courses.GetId(false);scores
                            .Where(x => x.CourseId == courseId)
                            .ShowConsoleTable();

                        break;
                    case "4":
                        classes.GroupJoin(
                            students,
                            x => x.Id,
                            y => y.ClassId,
                            (x,group) => new
                            {
                                @class = x,
                                students = group
                            }).ToList()
                            .ForEach(x =>
                            {
                                Console.WriteLine(x.@class.Name);
                                x.students.ShowConsoleTable();
                            });

                        var studentId = students.GetId(false);
                        scores
                            .Where(x => x.Id == studentId)
                            .ShowConsoleTable();

                        break;
                }
            }
            while (!pick.Equals("5"));
        }

        static void Search()
        {
            var pick = "";

            do
            {
                menuSearch.ForEach(Console.WriteLine);

                pick = Console.ReadLine();
                switch (pick)
                {
                    case "1":
                        var searchString = Console.ReadLine();
                        Console.WriteLine(searchString);
                        students
                            .Where(x => x.Name.Contains(searchString))
                            .ShowConsoleTable();

                        var studentId = students.GetId(false);
                        scores
                            .Where(x => x.StudentId == studentId)
                            .ShowConsoleTable();

                        break;
                    case "2":
                        searchString = Console.ReadLine();
                        classes
                            .Where(x => x.Name.Contains(searchString))
                            .ShowConsoleTable();

                        var classId = classes.GetId(false);
                        courses
                            .Where(x => x.ClassId == classId)
                            .ShowConsoleTable();

                        var courseId = courses.GetId(false);
                        scores
                            .Where(x => x.CourseId == courseId)
                            .ShowConsoleTable();

                        break;
                }
            }
            while (!pick.Equals("3"));
        }

        static void Report()
        {
            var pick = "";

            var scoresHasWeightScore = scores.Join(
                            courses,
                            x => x.CourseId,
                            y => y.Id,
                            (x, y) => new
                            {
                                Id = x.Id,
                                StudentId = x.StudentId,
                                Score = x.AverageScore,
                                SubjectId = y.SubjectId
                            }).Join(
                                subjects,
                                n => n.SubjectId,
                                m => m.Id,
                                (n, m) => new
                                {
                                    Id = n.Id,
                                    StudentId = n.StudentId,
                                    Score = n.Score,
                                    WeightScore = m.WeightScore
                                });

            var studentsHasAverageScore = students.GroupJoin(
                scoresHasWeightScore,
                x => x.Id,
                y => y.StudentId,
                (x, group) => new
                {
                    Student = x,
                    AverageScore = group.Sum(z => z.Score * z.WeightScore) / group.Sum(z => z.WeightScore)
                });

            do
            {
                menuReport.ForEach(Console.WriteLine);

                pick = Console.ReadLine();
                switch (pick)
                {
                    case "1":
                        studentsHasAverageScore
                            .Where(x => x.AverageScore >= 8)
                            .OrderBy(x => x.Student.ClassId)
                            .ThenBy(x => x.Student.Name)
                            .ThenBy(x => x.AverageScore)
                            .ShowConsoleTable();

                        break;
                    case "2":
                        var classId = classes.GetId(true);
                        studentsHasAverageScore
                            .Where(x => x.Student.ClassId == classId)
                            .ShowConsoleTable();

                        break;
                    case "3":
                        studentsHasAverageScore
                            .OrderByDescending(x => x.AverageScore)
                            .Take(100)
                            .ShowConsoleTable();

                        break;
                    case "4":
                        studentsHasAverageScore
                            .Where(x => x.AverageScore >= 8)
                            .GroupBy(x=> x.Student.ClassId)
                            .Select(x => new
                            {
                                @Class = x.Key,
                                SumStudent = x.Count()
                            })
                            .OrderByDescending(x=>x.SumStudent)
                            .Take(3)
                            .ShowConsoleTable();

                        break;
                    case "5":
                        studentsHasAverageScore
                            .GroupBy(x => x.Student.ClassId)
                            .Select(x => new
                            {
                                @Class = x.Key,
                                AverageClass = x.Average(y => y.AverageScore)
                            })
                            .OrderByDescending(x => x.AverageClass)
                            .Take(3)
                            .ShowConsoleTable();

                        break;
                    case "6":
                        courses
                            .GroupBy(x => x.TeacherId)
                            .Select(x => new
                            {
                                SumCourse = x.Count(),
                                TeacherId = x.Key
                            })
                            .Join(
                                teachers,
                                x => x.TeacherId,
                                y => y.Id,
                                (x, y) => new
                                {
                                    Id = y.Id,
                                    Name = y.Name,
                                    NativeLand = y.NativeLand,
                                    Birthday = y.Birthday,
                                    SumCourse = x.SumCourse
                                })
                                .OrderByDescending(x => x.SumCourse)
                                .Take(3)
                                .ToList()
                                .ShowConsoleTable();

                        courses.GroupJoin(
                            students,
                            x=>x.ClassId,
                            y=>y.ClassId,
                            (x,group) => new
                            {
                                @Course = x,
                                SumStudent = group.Count()
                            }
                            )
                            .Join(
                                teachers,
                                x => x.Course.TeacherId,
                                y => y.Id,
                                (x, y) => new
                                {
                                    Id = y.Id,
                                    Name = y.Name,
                                    NativeLand = y.NativeLand,
                                    Birthday = y.Birthday,
                                    SumStudent = x.SumStudent
                                })
                                .OrderByDescending(x => x.SumStudent)
                                .Take(3)
                                .ShowConsoleTable();

                        scores
                            .GroupBy(x => x.CourseId)
                            .Join(
                                courses,
                                x => x.Key,
                                y => y.Id,
                                (x, y) => new
                                {
                                    AverageScore = x.Average(z => z.AverageScore),
                                    TeacherId = y.TeacherId
                                }).Join(
                                    teachers,
                                    x => x.TeacherId,
                                    y => y.Id,
                                    (x, y) => new
                                    {
                                        Teacher = y,
                                        AverageScore = x.AverageScore
                                    })
                                    .OrderByDescending(x=>x.AverageScore)
                                    .Take(3)
                                    .ShowConsoleTable();

                        break;
                }
            }
            while (!pick.Equals("7"));
        }

        static int GetStudentId(int courseId)
        {
            courses
                .Where(x => x.Id == courseId)
                .GroupJoin(
                    students,
                    x => x.ClassId,
                    y => y.Id,
                    (x, group) => new {
                        student = group
                    }).ToList()
                    .ForEach(x => {
                        x.student.ShowConsoleTable();
                    });

            return students.GetId(false);
        }
    }
}