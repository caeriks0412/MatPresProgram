using MVPMatpres.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVPMatpres.Views
{
    public interface IMaterialView
    {
        //References the fields in materialmodel
        string MaterialName { get; set;  }
        List<ConsumableModel> Consumables { get; set; }

        double TotalMass { get; set; }
        double ExternalMass { get; set; }

        //Extra fields that will be used in the view
        string SearchField { get; set; }
        bool IsEditing { get; set; }
        bool IsComplete { get; set; }
        string Message { get; set; }

        //Events that will be used with the form.
        event EventHandler SearchEvent;
        event EventHandler AddEvent;
        event EventHandler RemoveEvent;
        event EventHandler EditEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;
        event EventHandler PDFEvent;

        //Methods that will be used to set values in form etc.
        void SetMaterialSource(BindingSource materials);

        string getEPD();
        string getAllocationMatrix();
        void SetMaterialSource2(DataTable materials);
        void Show(); //Might not be needed
    }
}
