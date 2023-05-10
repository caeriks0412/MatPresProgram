using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPMatpres.Models
{
    /// <summary>
    /// A class that writes a material to a JSON file so that it can be processed by plantsmith. 
    /// </summary>
    public class JSONWriter
    {

        //We always write a material, hence it is needed.
        MaterialModel material;
        //Constructor where we specify material.
        public JSONWriter(MaterialModel material) 
        {
            this.material = material;
        }

        //A method to write the material to a JSON file.
        public void WriteToFile()
        {
            string toWrite = CreateJSONInput();
            File.WriteAllText(".\\plantsmith data.json", toWrite);
        }

        //This creates the string that gets written. It is very messy, but is needed for plantsmith.
        //Some stuff is still to be implemented.
        public string CreateJSONInput()
        {
            //Adding the ground fundamentals to the json
            string res = "{" +
                 "\"input_data\": {" +
                    "\"user_data\":  { " +
                        "\"consumables\": [ " + Environment.NewLine;
            //Each json needs to have the consumables of a material.
            foreach (ConsumableModel consumable in material.Consumables)
            {
                res += JSONConsumable(consumable);
            }
            //We remove the last , since it is not needed
            res = res.Remove(res.Length-1);

            res += "],";
            res += "\"allocationMatrix\": " + /*ALLOCATION MATRIX */ ",";
            res += "\"companyName\": " + /*COMPANY NAME*/"\"\"" +  ",";
            res += "\"companyDescription\": \"\",";
            res += "\"systemLocation\": \"SE\",";
            res += "\"systemDescription\": \"\",";
            res += "\"totalMass\": " + material.TotalMass + ",";
            res += "\"externalMass\": " + material.ExternalMass + ",";
            res += "\"certification\": \"\",";
            res += "\"verifier\": \"\",";
            res += "\"productDescription\": \"\",";
            res += "\"productApplication\": \"\",";
            res += "\"productStandards\": \"\",";
            res += "\"productProperties\": \"\",";
            res += "\"allocationDescription\": \"\",";
            res += "\"year\": \"" + 2023 + "\","; //This is subject to change technically
            //These are subject to change aswell. Wait for the plant to be up and running
            //This should probably be done manually actually. Not to be done by user.
            //TODO
            res += "\"groups\": [ { \"products\": [], \"name\": \"A1\" },";
            res += "{ \"products\": [], \"name\": \"A2\" },";
            res += "{ \"products\": [], \"name\": \"A3\" },";

            //Add products???
            //Line 278 in nicegui

            res += "],";
            res += "\"usesimulationdata\": true, \"contactPerson\": \"\", \"contactEmail\": \"\", \"selectedProducts\": [], " +
                "\"versionNumber\": 1.1, \"curGroup\": 3},";
            res += "\"IVL_data\": [] }, \"output_data\": {}}";


            return res;
        }

        public string JSONConsumable(ConsumableModel consumable)
        {
            string res = "{";
            res += "\"value\": " + consumable.Value + "," +
                "\"selectedUnit\": \"" + consumable.Unit + "\"," +
                "\"displayValue\": " + consumable.Value + "," +
                "\"customEPDData\": " + "false" + "," +
                "\"consumablesLabel\": \"" + consumable.Name + "\"";
            res += "},";

            return res;
        }

    }
}
