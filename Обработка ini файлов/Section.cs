using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Обработка_ini_файлов
{
    class Section
    {
        public string Name { get; set; }
        public List<Parameter> Parameters { get; set; }

        public Section(string name)
        {
            Name = name;
            Parameters = new List<Parameter>();
        }
    }
}
