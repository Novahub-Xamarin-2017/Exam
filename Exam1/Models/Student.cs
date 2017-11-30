using Exam1.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Models
{
    class Student : EasyModel
    {
        [IgnoreInput]
        public int Id { set; get; }

        [PrompDisplay("Ten hoc sinh")]
        public string Name { get; set; }

        [IgnoreInput]
        public int ClassId { set; get; }

        [PrompDisplay("Que quan")]
        public string NativeLand { get; set; }

        [PrompDisplay("Ngay sinh")]
        public DateTime Birthday { get; set; }
    }
}
