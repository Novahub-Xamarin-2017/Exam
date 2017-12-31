using Exam1.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Models
{
    class Score : EasyModel
    {
        [IgnoreInput]
        public int Id { set; get; }

        [IgnoreInput]
        public int CourseId { set; get; }

        [IgnoreInput]
        public int StudentId { set; get; }

        [PrompDisplay("Diem")]
        public double AverageScore { set; get; }
    }
}
