using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COD
{
   public class CreateArrayList
    {
        private string disName;
        private string factValue;

        public CreateArrayList(string name, string Value)
        {

            this.disName = name;
            this.factValue = Value;
        }

        public string strName
        {
            get
            {
                return disName;
            }
        }

        public string strValue
        {
            get
            {
                return factValue;
            }
        }

    }
}
