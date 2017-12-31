using Exam1.Models;
using Exam1.Models.Base;
using Exam1.Models.Data;
using Exam1.Models.Data.Base;
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

        void Init()
        {
            dataListModels = new DataListModels();

            dataListModels.LoadAll();

            dataMenus = new DataMenus();
        }

        public void Save()
        {
            dataListModels.SaveAll();
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

                        subject.Id = dataListModels.ForType<Subject>().List.Count + 1;

                        subject.Input();

                        dataListModels.ForType<Subject>().List.Add(subject);

                        break;
                    case "2":
                        var teacher = new Teacher();

                        teacher.Id = dataListModels.ForType<Teacher>().List.Count + 1;

                        teacher.Input();

                        teacher.SubjectId = dataListModels.ForType<Subject>().List.GetId(true);

                        dataListModels.ForType<Teacher>().List.Add(teacher);

                        break;
                    case "3":
                        var @class = new Class();

                        @class.Id = dataListModels.ForType<Class>().List.Count + 1;

                        @class.Input();

                        @class.TeacherId = dataListModels.ForType<Teacher>().List.GetId(true);

                        dataListModels.ForType<Class>().List.Add(@class);

                        break;
                    case "4":
                        var student = new Student();

                        student.Id = dataListModels.ForType<Student>().List.Count + 1;

                        student.Input();

                        student.ClassId = dataListModels.ForType<Class>().List.GetId(true);

                        dataListModels.ForType<Student>().List.Add(student);

                        break;
                    case "5":
                        var course = new Course();

                        course.Id = dataListModels.ForType<Course>().List.Count + 1;

                        course.Input();

                        course.ClassId = dataListModels.ForType<Class>().List.GetId(true);

                        course.SubjectId = dataListModels.ForType<Subject>().List.GetId(true);

                        course.TeacherId = dataListModels.ForType<Teacher>().List.GetId(true);

                        dataListModels.ForType<Course>().List.Add(course);

                        break;
                    case "6":
                        var score = new Score();

                        score.Id = dataListModels.ForType<Score>().List.Count + 1;

                        score.CourseId = dataListModels.ForType<Course>().List.GetId(true);

                        score.StudentId = GetStudentIdFollowCouseId(score.CourseId);

                        score.Input();

                        dataListModels.ForType<Score>().List.Add(score);

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
                        dataListModels.ForType<Subject>().List
                            .ShowConsoleTable();

                        break;
                    case "2":
                        var classesHasSumStudent = dataListModels.ForType<Student>().List
                            .GroupBy(x => x.ClassId)
                            .Select(x => new
                            {
                                ClassId = x.Key,
                                SumStudent = x.Count()
                            });

                        dataListModels.ForType<Teacher>().List.Select(x => new
                        {
                            @Teacher = x,

                            ClassManager = dataListModels.ForType<Class>().List
                                .Where(y => y.TeacherId == x.Id)
                                .Select(y => new
                                {
                                    y.Name
                                })
                                .SingleOrDefault(),

                            Subjects = dataListModels.ForType<Subject>().List.Where(y => y.Id == x.SubjectId)
                                .Select(y => new
                                {
                                    y.Name
                                })
                                .SingleOrDefault(),

                            SumStudent = dataListModels.ForType<Course>().List.Where(y => y.TeacherId == x.Id)
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

                        var teacherId = dataListModels.ForType<Teacher>().List.GetId(false);
                        dataListModels.ForType<Course>().List
                            .Where(x => x.TeacherId == teacherId)
                            .ShowConsoleTable();

                        var courseId = dataListModels.ForType<Course>().List.GetId(false);
                        dataListModels.ForType<Score>().List
                            .Where(x => x.CourseId == courseId)
                            .ShowConsoleTable();

                        break;
                    case "3":
                        dataListModels.ForType<Class>().List.Join(
                            dataListModels.ForType<Teacher>().List,
                            x => x.TeacherId,
                            y => y.Id,
                            (x, y) => new
                            {
                                x = x,
                                NameTeacher = y.Name
                            }).GroupJoin(
                                dataListModels.ForType<Student>().List,
                                x => x.x.Id,
                                y => y.ClassId,
                                (x, group) => new
                                {
                                    Id = x.x.Id,
                                    NameClass = x.x.Name,
                                    NameTeacher = x.NameTeacher,
                                    SumStudent = group.Count()
                                }).ShowConsoleTable();

                        var classId = dataListModels.ForType<Class>().List.GetId(false);
                        dataListModels.ForType<Course>().List
                            .Where(x => x.ClassId == classId)
                            .ShowConsoleTable();

                        var courseId3 = dataListModels.ForType<Course>().List.GetId(false);
                        dataListModels.ForType<Score>().List
                            .Where(x => x.CourseId == courseId3)
                            .ShowConsoleTable();

                        break;
                    case "4":
                        dataListModels.ForType<Class>().List.GroupJoin(
                            dataListModels.ForType<Student>().List,
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

                        var studentId = dataListModels.ForType<Student>().List.GetId(false);
                        dataListModels.ForType<Score>().List
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
                        dataListModels.ForType<Student>().List
                            .Where(x => x.Name.Contains(searchString))
                            .ShowConsoleTable();

                        var studentId = dataListModels.ForType<Student>().List.GetId(false);
                        dataListModels.ForType<Score>().List
                            .Where(x => x.StudentId == studentId)
                            .ShowConsoleTable();

                        break;
                    case "2":
                        searchString = GetSearchString("ten lop hoc");
                        dataListModels.ForType<Class>().List
                            .Where(x => x.Name.Contains(searchString))
                            .ShowConsoleTable();

                        var classId = dataListModels.ForType<Class>().List.GetId(false);
                        dataListModels.ForType<Course>().List
                            .Where(x => x.ClassId == classId)
                            .ShowConsoleTable();

                        var courseId = dataListModels.ForType<Course>().List.GetId(false);
                        dataListModels.ForType<Score>().List
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
            var scoresHasWeightScore = dataListModels.ForType<Score>().List.Join(
                            dataListModels.ForType<Course>().List,
                            x => x.CourseId,
                            y => y.Id,
                            (x, y) => new
                            {
                                Id = x.Id,
                                StudentId = x.StudentId,
                                Score = x.AverageScore,
                                SubjectId = y.SubjectId
                            }).Join(
                                dataListModels.ForType<Subject>().List,
                                n => n.SubjectId,
                                m => m.Id,
                                (n, m) => new
                                {
                                    Id = n.Id,
                                    StudentId = n.StudentId,
                                    Score = n.Score,
                                    WeightScore = m.WeightScore
                                });

            var studentsHasAverageScore = dataListModels.ForType<Student>().List.GroupJoin(
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
                        var classId = dataListModels.ForType<Class>().List.GetId(true);
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
                        dataListModels.ForType<Course>().List
                            .GroupBy(x => x.TeacherId)
                            .Select(x => new
                            {
                                SumCourse = x.Count(),
                                TeacherId = x.Key
                            })
                            .Join(
                                dataListModels.ForType<Teacher>().List,
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

                        dataListModels.ForType<Course>().List.GroupJoin(
                            dataListModels.ForType<Student>().List,
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
                                dataListModels.ForType<Teacher>().List,
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

                        dataListModels.ForType<Score>().List
                            .GroupBy(x => x.CourseId)
                            .Join(
                                dataListModels.ForType<Course>().List,
                                x => x.Key,
                                y => y.Id,
                                (x, y) => new
                                {
                                    AverageScore = x.Average(z => z.AverageScore),
                                    TeacherId = y.TeacherId
                                }).Join(
                                    dataListModels.ForType<Teacher>().List,
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
            dataListModels.ForType<Course>().List
                .Where(x => x.Id == courseId)
                .GroupJoin(
                    dataListModels.ForType<Student>().List,
                    x => x.ClassId,
                    y => y.ClassId,
                    (x, group) => new {
                        student = group
                    }).ToList()
                    .ForEach(x => {
                        x.student.ShowConsoleTable();
                    });

            return dataListModels.ForType<Student>().List.GetId(false);
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