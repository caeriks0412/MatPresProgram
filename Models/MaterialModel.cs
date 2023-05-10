using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPMatpres.Models
{
    public class MaterialModel
    {
        //Instance Variables
        private string name;
        private List<ConsumableModel> consumables;
        private double totalMass;
        private double externalMass;


        //Auto generated properties to resp. variables
        public string Name 
        { 
            get => name; 
            set => name = value; 
        }
        public List<ConsumableModel> Consumables 
        { 
            get => consumables; 
            set => consumables = value; 
        }
        public double TotalMass { get => totalMass; set => totalMass = value; }
        public double ExternalMass { get => externalMass; set => externalMass = value; }


        public void AddConsumable(ConsumableModel consumable)
        {
            consumables.Add(consumable);
        }
    }
}
