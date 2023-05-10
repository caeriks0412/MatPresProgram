using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPMatpres.Models
{
    public class ConsumableModel
    {
        private string name;
        private double value;
        private string unit;

        public string Name 
        { 
            get => name; 
            set => name = value; 
        }
        public double Value 
        { 
            get => value; 
            set => this.value = value; 
        }
        public string Unit 
        { 
            get => unit; 
            set => unit = value; 
        }


        public string ToString()
        {
            return value + " " + unit;
        }
    }
}
