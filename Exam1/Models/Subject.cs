using Exam1.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Models
{
    class Subject : EasyModel
    {
        [IgnoreInput]
        public int Id { set; get; }

        [PrompDisplay("Ten mon hoc")]
        public string Name { get; set; }

        [PrompDisplay("Trong so diem")]
        public double WeightScore { get; set; }
    }
}
