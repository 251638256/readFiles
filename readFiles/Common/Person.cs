using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace readFiles {
    public class Person {
        public string Name { get; set; }
        public int Age { get; set; }
        /// <summary>
        /// 1:男 0:女
        /// </summary>
        public int? Sex { get; set; }
        public Score score { get; set; }
    }

    public class Score {
        public int Csharp { get; set; }

        public int Database { get; set; }

        public int Java { get; set; }
    }
}
