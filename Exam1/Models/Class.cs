using Exam1.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Models
{
    class Class : EasyModel
    {
        [IgnoreInput]
        public int Id { set; get; }

        [PrompDisplay("Ten lop")]
        public string Name { get; set; }

        [IgnoreInput]
        public int TeacherId { get; set; }
    }
}
