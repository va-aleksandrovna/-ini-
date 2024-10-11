using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Обработка_ini_файлов
{
    interface IIniParser
    {
        public List<Section> Sections { get; set; }

        public void Show();
        public void Read();
        public void ShowВata();
        public void Receiving(int tip, int section_id, int parameter_id);
    }
}
