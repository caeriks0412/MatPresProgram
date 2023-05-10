using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPMatpres.Models
{
    public interface IMaterialRepository
    {
        
        void Add(MaterialModel material);
        void Edit(MaterialModel material);
        void Delete(MaterialModel material);
        IEnumerable<MaterialModel> FindAll();       //Supposed to find all models

        IEnumerable<MaterialModel> FindAll2();
        IEnumerable<MaterialModel> FindByValue(string name);   //Find when searching

    }
}
