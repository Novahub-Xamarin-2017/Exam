using Exam1.Models;
using Exam1.Models.Base;
using Exam1.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Menus
{
    class Menu
    {
        static DataListModels dataListModels;

        static DataMenus dataMenus;

        public Menu()
        {
            Init();
        }

        public Menu(string folder)
        {
            Init(folder);
        }

        void Init()
        {
            dataListModels = new DataListModels();

            dataMenus = new DataMenus();
        }

        void Init(string folder)
        {
            dataListModels = new DataListModels(folder);

            dataMenus = new DataMenus(folder);
        }

        public void Save()
        {
            dataListModels.Save();
        }

        public void MenuStart()
        {
            dataMenus.MenuMain.Menu((pick, count) =>
            {
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
                    case "5":
                        break;
                    default:
                        MessageError(count);
                        break;
                }
            });
        }

        static void Input()
        {
            dataMenus.MenuInput.Menu((pick, count) =>
            {
                switch (pick)
                {
                    case "1":
                        var subject = new Subject();
                        subject.Id = dataListModels.subjects.Count + 1;
                        subject.Input();
                        dataListModels.subjects.Add(subject);
                        break;
                    case "2":
                        var teacher = new Teacher();
                        teacher.Id = dataListModels.teachers.Count + 1;
                        teacher.Input();
                        teacher.SubjectId = dataListModels.subjects.GetId(true);
                        dataListModels.teachers.Add(teacher);
                        break;
                    case "3":
                        var @class = new Class();
                        @class.Id = dataListModels.classes.Count + 1;
                        @class.Input();
                        @class.TeacherId = dataListModels.teachers.GetId(true);
                        dataListModels.classes.Add(@class);
                        break;
                    case "4":
                        var student = new Student();
                        student.Id = dataListModels.students.Count + 1;
                        student.Input();
                        student.ClassId = dataListModels.classes.GetId(true);
                        dataListModels.students.Add(student);
                        break;
                    case "5":
                        var course = new Course();
                        course.Id = dataListModels.courses.Count + 1;
                        course.Input();
                        course.ClassId = dataListModels.classes.GetId(true);
                        course.SubjectId = dataListModels.subjects.GetId(true);
                        course.TeacherId = dataListModels.teachers.GetId(true);
                        dataListModels.courses.Add(course);
                        break;
                    case "6":
                        var score = new Score();
                        score.Id = dataListModels.scores.Count + 1;
                        score.CourseId = dataListModels.courses.GetId(true);
                        score.StudentId = GetStudentIdFollowCouseId(score.CourseId);
                        score.Input();
                        dataListModels.scores.Add(score);
                        break;
                    case "7":
                        break;
                    default:
                        MessageError(count);
                        break;
                }
            });
        }

        static void Show()
        {
            dataMenus.MenuShow.Menu((pick, count) =>
            {
                switch (pick)
                {
                    case "1":
                        dataListModels.subjects.ShowConsoleTable();
                        break;
                    case "2":
                        var classesHasSumStudent = dataListModels.students
                            .GroupBy(x => x.ClassId)
                            .Select(x => new
                            {
                                ClassId = x.Key,
                                SumStudent = x.Count()
                            });

                        dataListModels.teachers.Select(x => new
                        {
                            @Teacher = x,

                            ClassManager = dataListModels.classes
                                .Where(y => y.TeacherId == x.Id)
                                .Select(y => new
                                {
                                    y.Name
                                })
                                .SingleOrDefault(),

                            Subjects = dataListModels.subjects.Where(y => y.Id == x.SubjectId)
                                .Select(y => new
                                {
                                    y.Name
                                })
                                .SingleOrDefault(),

                            SumStudent = dataListModels.courses.Where(y => y.TeacherId == x.Id)
                                .Join(
                                    classesHasSumStudent,
                                    y => y.ClassId,
                                    z => z.ClassId,
                                    (y, z) => new
                                    {
                                        @Course = y,
                                        SumStudent = z.SumStudent
                                    }
                                )
                                .GroupBy(y => y.Course.SubjectId)
                                .Select(y => new
                                {
                                    SumStudent = y.Sum(z => z.SumStudent)
                                }).SingleOrDefault()
                        }).ShowConsoleTable();

                        var teacherId = dataListModels.teachers.GetId(false);
                        dataListModels.courses
                            .Where(x => x.TeacherId == teacherId)
                            .ShowConsoleTable();

                        var courseId = dataListModels.courses.GetId(false);
                        dataListModels.scores
                            .Where(x => x.CourseId == courseId)
                            .ShowConsoleTable();

                        break;
                    case "3":
                        dataListModels.classes.Join(
                            dataListModels.teachers,
                            x => x.TeacherId,
                            y => y.Id,
                            (x, y) => new
                            {
                                x = x,
                                NameTeacher = y.Name
                            }).GroupJoin(
                                dataListModels.students,
                                x => x.x.Id,
                                y => y.ClassId,
                                (x, group) => new
                                {
                                    Id = x.x.Id,
                                    NameClass = x.x.Name,
                                    NameTeacher = x.NameTeacher,
                                    SumStudent = group.Count()
                                }).ShowConsoleTable();

                        var classId = dataListModels.classes.GetId(false);
                        dataListModels.courses
                            .Where(x => x.ClassId == classId)
                            .ShowConsoleTable();

                        var courseId3 = dataListModels.courses.GetId(false);
                        dataListModels.scores
                            .Where(x => x.CourseId == courseId3)
                            .ShowConsoleTable();

                        break;
                    case "4":
                        dataListModels.classes.GroupJoin(
                            dataListModels.students,
                            x => x.Id,
                            y => y.ClassId,
                            (x, group) => new
                            {
                                @class = x,
                                students = group
                            }).ToList()
                            .ForEach(x =>
                            {
                                Console.WriteLine(x.@class.Name);
                                x.students.ShowConsoleTable();
                            });

                        var studentId = dataListModels.students.GetId(false);
                        dataListModels.scores
                            .Where(x => x.StudentId == studentId)
                            .ShowConsoleTable();

                        break;
                    case "5":
                        break;
                    default:
                        MessageError(count);
                        break;
                }
            });
        }

        static void Search()
        {
            dataMenus.MenuSearch.Menu((pick, count) =>
            {
                switch (pick)
                {
                    case "1":
                        var searchString = GetSearchString("ten hoc sinh");
                        Console.WriteLine(searchString);
                        dataListModels.students
                            .Where(x => x.Name.Contains(searchString))
                            .ShowConsoleTable();

                        var studentId = dataListModels.students.GetId(false);
                        dataListModels.scores
                            .Where(x => x.StudentId == studentId)
                            .ShowConsoleTable();

                        break;
                    case "2":
                        searchString = GetSearchString("ten lop hoc");
                        dataListModels.classes
                            .Where(x => x.Name.Contains(searchString))
                            .ShowConsoleTable();

                        var classId = dataListModels.classes.GetId(false);
                        dataListModels.courses
                            .Where(x => x.ClassId == classId)
                            .ShowConsoleTable();

                        var courseId = dataListModels.courses.GetId(false);
                        dataListModels.scores
                            .Where(x => x.CourseId == courseId)
                            .ShowConsoleTable();

                        break;
                    case "3":
                        break;
                    default:
                        MessageError(count);
                        break;
                }
            });
        }

        static void Report()
        {
            var scoresHasWeightScore = dataListModels.scores.Join(
                            dataListModels.courses,
                            x => x.CourseId,
                            y => y.Id,
                            (x, y) => new
                            {
                                Id = x.Id,
                                StudentId = x.StudentId,
                                Score = x.AverageScore,
                                SubjectId = y.SubjectId
                            }).Join(
                                dataListModels.subjects,
                                n => n.SubjectId,
                                m => m.Id,
                                (n, m) => new
                                {
                                    Id = n.Id,
                                    StudentId = n.StudentId,
                                    Score = n.Score,
                                    WeightScore = m.WeightScore
                                });

            var studentsHasAverageScore = dataListModels.students.GroupJoin(
                scoresHasWeightScore,
                x => x.Id,
                y => y.StudentId,
                (x, group) => new
                {
                    Student = x,
                    AverageScore = group.Sum(z => z.Score * z.WeightScore) / group.Sum(z => z.WeightScore)
                });

            dataMenus.MenuReport.Menu((pick, count) =>
            {
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
                        var classId = dataListModels.classes.GetId(true);
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
                            .GroupBy(x => x.Student.ClassId)
                            .Select(x => new
                            {
                                @Class = x.Key,
                                SumStudent = x.Count()
                            })
                            .OrderByDescending(x => x.SumStudent)
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
                        dataListModels.courses
                            .GroupBy(x => x.TeacherId)
                            .Select(x => new
                            {
                                SumCourse = x.Count(),
                                TeacherId = x.Key
                            })
                            .Join(
                                dataListModels.teachers,
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

                        dataListModels.courses.GroupJoin(
                            dataListModels.students,
                            x => x.ClassId,
                            y => y.ClassId,
                            (x, group) => new
                            {
                                TeacherId = x.TeacherId,
                                SumStudent = group.Count()
                            })
                            .GroupBy(x => x.TeacherId)
                            .Select(x => new
                            {
                                SumStudent = x.Sum(y => y.SumStudent),
                                TeacherId = x.Key
                            })
                            .Join(
                                dataListModels.teachers,
                                x => x.TeacherId,
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

                        dataListModels.scores
                            .GroupBy(x => x.CourseId)
                            .Join(
                                dataListModels.courses,
                                x => x.Key,
                                y => y.Id,
                                (x, y) => new
                                {
                                    AverageScore = x.Average(z => z.AverageScore),
                                    TeacherId = y.TeacherId
                                }).Join(
                                    dataListModels.teachers,
                                    x => x.TeacherId,
                                    y => y.Id,
                                    (x, y) => new
                                    {
                                        Teacher = y,
                                        AverageScore = x.AverageScore
                                    })
                                    .OrderByDescending(x => x.AverageScore)
                                    .Take(3)
                                    .ShowConsoleTable();

                        break;
                    case "7":
                        break;
                    default:
                        MessageError(count);
                        break;
                }
            });
        }

        static int GetStudentIdFollowCouseId(int courseId)
        {
            dataListModels.courses
                .Where(x => x.Id == courseId)
                .GroupJoin(
                    dataListModels.students,
                    x => x.ClassId,
                    y => y.ClassId,
                    (x, group) => new {
                        student = group
                    }).ToList()
                    .ForEach(x => {
                        x.student.ShowConsoleTable();
                    });

            return dataListModels.students.GetId(false);
        }

        static string GetSearchString(string str)
        {
            Console.Write($"Nhap {str}: ");

            return Console.ReadLine();
        }

        static void MessageError(int length)
        {
            Console.WriteLine($"Yeu cau cua ban khong the thuc hien duoc. Ban xin long chon tu 1 -> {length} de thuc hien cac chuc nang o ben phai");
        }
    }
}