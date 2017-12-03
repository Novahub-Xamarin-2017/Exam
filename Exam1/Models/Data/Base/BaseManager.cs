using Exam1.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Models.Data.Base
{
    public class BaseManager<T> : List<T>, IManager where T : EasyModel
    {
        public List<T> List;

        public string folder = "../../Data";

        public void Save()
        {
            List.SaveJson(folder);
        }

        public void Load()
        {
            List = List.GetObject(folder);
        }
    }
}
