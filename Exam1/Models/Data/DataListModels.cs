using Exam1.Models.Base;
using Exam1.Models.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Models.Data
{
    class DataListModels
    {
        public List<IManager> managers;

        public DataListModels()
        {
            managers = new List<IManager>()
            {
                new BaseManager<Student>(),
                new BaseManager<Subject>(),
                new BaseManager<Teacher>(),
                new BaseManager<Class>(),
                new BaseManager<Score>(),
                new BaseManager<Course>()
            };
        }

        public BaseManager<T> ForType<T>() where T : EasyModel
        {
            return managers.OfType<BaseManager<T>>().First();
        }

        public void LoadAll()
        {
            managers.ForEach(x =>
            {
                x.Load();
            });
        }

        public void SaveAll()
        {
            managers.ForEach(x =>
            {
                x.Save();
            });
        }
    }
}
