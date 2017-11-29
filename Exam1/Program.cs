using Exam1.Models;
using Exam1.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        static void MenuStart()
        {
            var menu = new List<string>();

            menu.Add("1 Nhap du lieu");
            menu.Add("2 Hien thi du lieu");
            menu.Add("3 Tim kiem du lieu");
            menu.Add("4 Bao cao");
            menu.Add("5 Thoat");

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
            var menuInput = new List<string>();

            menuInput.Add("1 Nhap danh sach mon hoc");
            menuInput.Add("2 Nhap danh sach giao vien");
            menuInput.Add("3 Nhap danh sach lop hoc");
            menuInput.Add("4 Nhap danh sach sinh vien");
            menuInput.Add("5 Nhap danh sach khoa hoc");
            menuInput.Add("6 Nhap danh sach diem");
            menuInput.Add("7 Thoat");

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
                        //Console.WriteLine(subject.ToString());
                        break;
                    case "2":
                        var teacher = new Teacher();
                        teacher.Id = teachers.Count + 1;
                        teacher.Input();
                        teacher.SubjectId = GetSubjectId();
                        teachers.Add(teacher);
                        //Console.WriteLine(teacher.ToString());
                        break;
                    case "3":
                        var @class = new Class();
                        @class.Id = classes.Count + 1;
                        @class.Input();
                        @class.TeacherId = GetTeacherId();
                        classes.Add(@class);
                        //Console.WriteLine(@class.ToString());
                        break;
                    case "4":
                        var student = new Student();
                        student.Id = students.Count + 1;
                        student.Input();
                        student.ClassId = GetClassId();
                        students.Add(student);
                        //Console.WriteLine(student.ToString());
                        break;
                    case "5":
                        var course = new Course();
                        course.Id = courses.Count + 1;
                        course.Input();
                        course.ClassId = GetClassId();
                        course.SubjectId = GetSubjectId();
                        course.TeacherId = GetTeacherId();
                        courses.Add(course);
                        //Console.WriteLine(course.ToString());
                        break;
                    case "6":
                        var score = new Score();
                        score.Id = scores.Count + 1;
                        score.Input();
                        score.CourseId = GetCourseId();
                        score.StudentId = GetStudentId(score.CourseId);
                        scores.Add(score);
                        //Console.WriteLine(score.ToString());
                        break;
                }
            }
            while (!pick.Equals("7"));
        }

        static void Show()
        {
            var menuInput = new List<string>();

            menuInput.Add("1 Hien thi danh sach mon hoc");
            menuInput.Add("2 Hien thi danh sach giao vien");
            menuInput.Add("3 Hien thi danh sach lop hoc");
            menuInput.Add("4 Hien thi danh sach sinh vien");
            menuInput.Add("5 Thoat");

            var pick = "";

            do
            {
                menuInput.ForEach(Console.WriteLine);

                pick = Console.ReadLine();
                switch (pick)
                {
                    case "1":
                        subjects.ForEach(Console.WriteLine);
                        break;
                    case "2":
                        var teachersHasSubject = teachers
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
                                    }).ToList();

                        teachersHasSubject.ForEach(x =>
                        {
                            Console.WriteLine(x.x);
                            x.group.ToList().ForEach(y=> 
                            {
                                Console.Write("Subject: ");
                                subjects.Where(z => z.Id == y.SubjectId).ToList().ForEach(Console.Write);
                                Console.WriteLine($" SumStudent: {students.Where(z => z.Id == y.ClassId).Count()}");
                            });
                        });

                        var teacherId = int.Parse(Console.ReadLine());
                        courses
                            .Where(x => x.TeacherId == teacherId)
                            .ToList()
                            .ForEach(Console.WriteLine);

                        var courseId = int.Parse(Console.ReadLine());
                        scores
                            .Where(x => x.CourseId == courseId)
                            .ToList()
                            .ForEach(Console.WriteLine);

                        break;
                    case "3":
                        var classHasNumberStudent = classes.Join(
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
                                })
                                .ToList();

                        classHasNumberStudent.ForEach(Console.WriteLine);

                        var classId = int.Parse(Console.ReadLine());
                        var coursesFollowClass = courses
                            .Where(x => x.ClassId == classId)
                            .ToList();

                        coursesFollowClass.ForEach(Console.WriteLine);

                        courseId = int.Parse(Console.ReadLine());
                        var scoresFollowCourse = scores
                            .Where(x => x.CourseId == courseId)
                            .ToList();

                        scoresFollowCourse.ForEach(Console.WriteLine);

                        break;
                    case "4":
                        var studentsFollowClass = classes.GroupJoin(
                            students,
                            x => x.Id,
                            y => y.ClassId,
                            (x,group) => new
                            {
                                @class = x,
                                students = group
                            }).ToList();

                        studentsFollowClass.ForEach(x =>
                        {
                            Console.WriteLine(x.@class.Name);
                            x.students.ToList().ForEach(Console.WriteLine);
                        });

                        var studentId = GetStudentId(0);
                        var scoreOfStudent = scores.Where(x => x.Id == studentId).ToList();

                        scoreOfStudent.ForEach(Console.WriteLine);

                        break;
                }
            }
            while (!pick.Equals("5"));
        }

        static void Search()
        {
            var menuSearch = new List<string>();

            menuSearch.Add("1 Tim ten hoc sinh");
            menuSearch.Add("2 Tim ten lop");
            menuSearch.Add("3 Thoat");

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
                            .ToList()
                            .ForEach(Console.WriteLine);

                        var studentId = int.Parse(Console.ReadLine());
                        scores
                            .Where(x => x.StudentId == studentId)
                            .ToList()
                            .ForEach(Console.WriteLine);

                        break;
                    case "2":
                        searchString = Console.ReadLine();
                        classes
                            .Where(x => x.Name.Contains(searchString))
                            .ToList()
                            .ForEach(Console.WriteLine);

                        var classId = int.Parse(Console.ReadLine());
                        courses
                            .Where(x => x.ClassId == classId)
                            .ToList()
                            .ForEach(Console.WriteLine);

                        var courseId = int.Parse(Console.ReadLine());
                        scores
                            .Where(x => x.CourseId == courseId)
                            .ToList()
                            .ForEach(Console.WriteLine);

                        break;
                }
            }
            while (!pick.Equals("3"));
        }

        static void Report()
        {
            var menuSearch = new List<string>();

            menuSearch.Add("1 Danh sach hoc sinh gioi");
            menuSearch.Add("2 Diem trung binh theo lop");
            menuSearch.Add("3 Top 100 hoc sinh theo diem lop ten");
            menuSearch.Add("4 Top 3 lop co so luong hoc sinh gioi nhieu nhat");
            menuSearch.Add("5 Top 3 lop co diem trung binh cao nhat");
            menuSearch.Add("6 Top 3 giao vien");
            menuSearch.Add("7 Thoat");

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
                                }
                            ).ToList();

            var studentsHasAverageScore = students.GroupJoin(
                scoresHasWeightScore,
                x => x.Id,
                y => y.StudentId,
                (x, group) => new
                {
                    Student = x,
                    AverageScore = group.Sum(z => z.Score * z.WeightScore) / group.Sum(z => z.WeightScore)
                }
                );

            do
            {
                menuSearch.ForEach(Console.WriteLine);

                pick = Console.ReadLine();
                switch (pick)
                {
                    case "1":
                        studentsHasAverageScore
                            .Where(x => x.AverageScore >= 8)
                            .OrderBy(x=>x.Student.ClassId)
                            .ThenBy(x=>x.Student.Name)
                            .ThenBy(x=>x.AverageScore)
                            .ToList()
                            .ForEach(Console.WriteLine);

                        break;
                    case "2":
                        var classId = GetClassId();
                        studentsHasAverageScore
                            .Where(x => x.Student.ClassId == classId)
                            .ToList()
                            .ForEach(Console.WriteLine);

                        break;
                    case "3":
                        studentsHasAverageScore
                            .OrderByDescending(x => x.AverageScore)
                            .Take(100)
                            .ToList()
                            .ForEach(Console.WriteLine);

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
                            .ToList()
                            .ForEach(Console.WriteLine);

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
                            .ToList()
                            .ForEach(Console.WriteLine);

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
                                .ForEach(Console.WriteLine);

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
                                .ToList()
                                .ForEach(Console.WriteLine);

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
                                    .ToList()
                                    .ForEach(Console.WriteLine);

                        break;
                }
            }
            while (!pick.Equals("7"));
        }

        static int GetTeacherId()
        {
            teachers.ForEach(Console.WriteLine);

            var id = int.Parse(Console.ReadLine());

            return id;
        }

        static int GetClassId()
        {
            classes.ForEach(Console.WriteLine);

            var id = int.Parse(Console.ReadLine());

            return id;
        }

        static int GetSubjectId()
        {
            subjects.ForEach(Console.WriteLine);

            var id = int.Parse(Console.ReadLine());

            return id;
        }

        static int GetCourseId()
        {
            courses.ForEach(Console.WriteLine);

            var id = int.Parse(Console.ReadLine());

            return id;
        }

        static int GetStudentId(int courseId)
        {
            if (courseId==0)
            {
                return int.Parse(Console.ReadLine());
            }

            var studentsFollowCourseId = courses
                .Where(x => x.Id == courseId)
                .GroupJoin(
                    students,
                    x => x.ClassId,
                    y => y.Id,
                    (x, group) => new {
                        student = group
                    })
                .ToList();

            studentsFollowCourseId.ForEach(x => {
                x.student.ToList().ForEach(Console.WriteLine);
            });
            //courses.ForEach(Console.WriteLine);

            var id = int.Parse(Console.ReadLine());

            return id;
        }
    }
}