using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questao2
{
    public class JsonRoot
    {

        public string Page {  get; set; }

        public string Per_page { get; set; }

        public string Total_pages { get; set; }

        public JsonData[] data { get; set; }
       
    }
}
