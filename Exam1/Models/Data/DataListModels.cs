using Exam1.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Models.Data
{
    class DataListModels
    {
        public List<Subject> subjects { set; get; }

        public List<Teacher> teachers { set; get; }

        public List<Class> classes { set; get; }

        public List<Course> courses { set; get; }

        public List<Student> students { set; get; }

        public List<Score> scores { set; get; }

        public string folder { set; get; }

        public DataListModels()
        {
            folder = "../../Data";

            subjects = (subjects.GetObject<Subject>(folder));
            teachers = teachers.GetObject<Teacher>(folder);
            classes = classes.GetObject<Class>(folder);
            courses = courses.GetObject<Course>(folder);
            students = students.GetObject<Student>(folder);
            scores = scores.GetObject<Score>(folder);
        }

        public DataListModels(string folder)
        {
            this.folder = folder;

            subjects = (subjects.GetObject<Subject>(folder));
            teachers = teachers.GetObject<Teacher>(folder);
            classes = classes.GetObject<Class>(folder);
            courses = courses.GetObject<Course>(folder);
            students = students.GetObject<Student>(folder);
            scores = scores.GetObject<Score>(folder);
        }

        public void Save()
        {
            subjects.SaveJson(folder);
            teachers.SaveJson(folder);
            classes.SaveJson(folder);
            courses.SaveJson(folder);
            students.SaveJson(folder);
            scores.SaveJson(folder);
        }
    }
}
