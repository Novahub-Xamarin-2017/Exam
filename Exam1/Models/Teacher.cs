using Exam1.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Models
{
    class Teacher : EasyModel
    {
        [IgnoreInput]
        public int Id { set; get; }

        [PrompDisplay("Ten giao vien")]
        public string Name { get; set; }

        [PrompDisplay("Que quan")]
        public string NativeLand { get; set; }

        [PrompDisplay("Ngay sinh")]
        public DateTime Birthday { get; set; }

        [IgnoreInput]
        public int SubjectId { set; get; }
    }
}
