using Exam1.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Models
{
    class Course : EasyModel
    {
        [IgnoreInput]
        public int Id { set; get; }

        [IgnoreInput]
        public int ClassId { set; get; }

        [IgnoreInput]
        public int SubjectId { set; get; }

        [IgnoreInput]
        public int TeacherId { set; get; }
    }
}
