using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVPMatpres.Models;
using Npgsql;

namespace MVPMatpres.Repositories
{
    /// <summary>
    /// This class is meant to hold materials. A material repository is just what it says it is, a 
    /// repository for the materials. It handles all the adding, removing and edit functions we should have.
    /// </summary>
    internal class MaterialRepository : BaseRepository, IMaterialRepository
    {
        //constructor, we need a connectionstring to get all the materials from the database. 
        public MaterialRepository(string connectionString) 
        {
            this.connectionString = connectionString;
        }

        //We add a material to the database. This could be made different by implementing a trigger
        //in the database that also inserts it into each sub consumable table.
        //We use batchcommands because it is safer when we have multiple commands. If an insert were to fail,
        //no inserts is made and thus reverted.
        public void Add(MaterialModel material)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using var batch = new NpgsqlBatch(connection)
                {
                    BatchCommands =
                    {
                        new("INSERT INTO InputMaterials VALUES('" + material.Name + "')")
                    }
                };
                foreach(ConsumableModel con in material.Consumables)
                {
                    NpgsqlBatchCommand newBat = new NpgsqlBatchCommand();
                    newBat.CommandText = "INSERT INTO " + con.Name + "Consumption VALUES('" + material.Name + "'," + con.Value + ",'" + con.Unit + "')";
                    batch.BatchCommands.Add(newBat);
                }
                //Adding for totalmass and externalmass
                NpgsqlBatchCommand totalMass = new NpgsqlBatchCommand();
                totalMass.CommandText = "INSERT INTO TotalYearlyProduction VALUES('" + material.Name + "'," + material.TotalMass + ")";
                batch.BatchCommands.Add(totalMass);
                NpgsqlBatchCommand totalExternalMass = new NpgsqlBatchCommand();
                totalExternalMass.CommandText = "INSERT INTO TotalExternalMaterial VALUES('" + material.Name + "'," + material.ExternalMass + ")";
                batch.BatchCommands.Add(totalExternalMass);
                //end, should work in theory
                batch.ExecuteReader();
                batch.Dispose();
            }
            
        }

        //Deletion is easier, since we specified in the database that all sub table entries should be deleted
        //if the material is deleted from the inputmaterials table.
        public void Delete(MaterialModel material)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using var batch = new NpgsqlBatch(connection) { };
                batch.BatchCommands.Add(new NpgsqlBatchCommand("DELETE FROM InputMaterials WHERE name = '" + material.Name + "'"));
                batch.ExecuteReader();
                batch.Dispose(); //Prob not needed
            }
        }
        
        //Method for editing a material, it is a bit bugged atm
        public void Edit(MaterialModel material)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using var batch = new NpgsqlBatch(connection) { };
                foreach (ConsumableModel con in material.Consumables)
                {
                    NpgsqlBatchCommand newBat = new NpgsqlBatchCommand();
                    MessageBox.Show("wurk");
                    newBat.CommandText = "UPDATE " + con.Name + "Consumption SET " + con.Name + "=" + con.Value + ", unit='" +con.Unit + "' WHERE name='" + material.Name+"'";
                    batch.BatchCommands.Add(newBat);
                }
                batch.ExecuteReader();
                batch.Dispose(); //Prob not needed
            }

        }

        //Gets all the materials from the database, and create materialmodels for each material with
        //its corresponding consumables.
        public IEnumerable<MaterialModel> FindAll2()
        {
            List<MaterialModel> materials = new List<MaterialModel>();

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd  = conn.CreateCommand())
            {
                conn.Open();
                //Allinformationconsumables is a view with all needed information for a material
                cmd.CommandText = "SELECT * FROM AllInformationConsumables";
                cmd.Connection = conn;
                using (var reader = cmd.ExecuteReader()) 
                {
                    while(reader.Read()) 
                    {
                        MaterialModel material = new MaterialModel();
                        material.Consumables = new List<ConsumableModel>();
                        material.Name = reader.GetString(0);
                        
                        for(int i = 1; i <= reader.FieldCount-3; i++)
                        {
                            ConsumableModel con = new ConsumableModel();
                            con.Name = reader.GetName(i); 
                            string[] valueUnit = reader.GetString(i).Split(" ");
                            con.Value = Convert.ToDouble(valueUnit[0]);
                            con.Unit = valueUnit[1];                            material.AddConsumable(con);
                        }
                        material.TotalMass = reader.GetDouble(reader.FieldCount - 2);
                        material.ExternalMass = reader.GetDouble(reader.FieldCount - 1);
                        //Make it so that it selects only the total mass and external mass here.
                        //Select where name = material.name??
                        materials.Add(material);
                    }
                }

            }
            return materials;
        }
        //This was the original implementation, it is not used anywhere for the moment.
        //The reason it was changed was because it did not get the consumables in the materialmodels
        //The current one used is FindAll2()
        public IEnumerable<MaterialModel> FindAll()
        {
            List<MaterialModel> materials = new List<MaterialModel>();

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand())
            {
                conn.Open();
                cmd.Connection = conn; 
                cmd.CommandText = "SELECT * FROM InputMaterials";
                using (var reader =  cmd.ExecuteReader()) 
                { 
                    while (reader.Read())
                    {
                        MaterialModel mat = new MaterialModel();
                        mat.Name = reader.GetString(0);
                        mat.Consumables = new List<ConsumableModel>();
                        materials.Add(mat);
                    }
                }
            }

            return materials;
        }

        //This needs to be updated to the same implementation as above so that it gets the consumablemodels aswell
        //Works the same way except we add a WHERE name LIKE to only get names like the ones searched for.
        public IEnumerable<MaterialModel> FindByValue(string name)
        {
            List<MaterialModel> materials = new List<MaterialModel>();

            using (var conn = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM InputMaterials WHERE LOWER(name) LIKE '" + name.ToLower() + "%'";
                //After connection is created and command set, we read what the command has selected
                //The command works by selecting the right material, and provides search functionality 
                //where case does not matter.
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MaterialModel mat = new MaterialModel();
                        mat.Name = reader.GetString(0);
                        mat.Consumables = new List<ConsumableModel>();
                        materials.Add(mat);
                    }
                }
            }

            return materials;
        }
    }
}
