using MVPMatpres.Models;
using MVPMatpres.Properties;
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVPMatpres.Views
{
    public partial class MaterialView : Form, IMaterialView
    {
        private string message;
        private bool isComplete;
        private bool isEditing;

        public MaterialView()
        {
            InitializeComponent();
            CreateEvents();
            GetVirginEPDs();
        }

        private void CreateEvents()
        {
            //If search button is pressed, search for materials
            buttonSearchMaterial.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            //If enter button is pressed, search for materials.
            textBoxSearchMaterial.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SearchEvent?.Invoke(this, EventArgs.Empty);
            };
            
            buttonAddMaterial.Click += delegate
            {
                AddEvent?.Invoke(this, EventArgs.Empty);
                //tabControl1.TabPages.Remove(tabPage1);
                //tabControl1.TabPages.Add(tabPage2);
                tabPage1.Text = "Add new material";
            };
            //This one is a bit bugged currently
            buttonEditMaterial.Click += delegate
            {
                EditEvent?.Invoke(this, EventArgs.Empty);
                //tabControl1.TabPages.Remove(tabPage1);
                //tabControl1.TabPages.Add(tabPage2);
                tabPage1.Text = "Edit current material";
            };
            buttonDeleteMaterial.Click += delegate
            {
                DialogResult delete = MessageBox.Show("You are about to delete a material, press yes if you intend to.", "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (delete == DialogResult.Yes)
                {
                    RemoveEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(message);
                }
            };
            buttonSaveMaterial.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (isComplete)
                {
                    //tabControl1.TabPages.Remove(tabPage2);
                    //tabControl1.TabPages.Add(tabPage1);
                }
                MessageBox.Show(message);
            };
            buttonCancelChanges.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                //tabControl1.TabPages.Remove(tabPage2);
                //tabControl1.TabPages.Add(tabPage1);
            };

            buttonCompareEPDs.Click += delegate
            {
                PDFEvent?.Invoke(this, EventArgs.Empty);
            };
        }

        //Variables for each material
        public string MaterialName
        {
            get { return textBoxMaterialName.Text; }
            set { textBoxMaterialName.Text = value; }
        }
        public List<ConsumableModel> Consumables
        {
            get { return GenerateConsumables(); }
            set
            {
                foreach (Control control in this.groupBoxConsumables.Controls)
                {
                    if (control.GetType() == typeof(TextBox) && control.Name != textBoxSearchMaterial.Name)
                    {
                        control.Text = "0";
                    }
                }
            }
        }
        public string SearchField
        {
            get { return textBoxSearchMaterial.Text; }
            set { textBoxSearchMaterial.Text = value; }
        }
        public bool IsEditing
        {
            get { return isEditing; }
            set { isEditing = value; }
        }
        public bool IsComplete
        {
            get { return isComplete; }
            set { isComplete = value; }
        }
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public double TotalMass
        {
            get { return Convert.ToDouble(textBoxTotalMass.Text); }
            set { textBoxTotalMass.Text = value + ""; }
        }
        public double ExternalMass
        {
            get { return Convert.ToDouble(textBoxExternalMass.Text); }
            set { textBoxExternalMass.Text = value + ""; }
        }

        //Events that will occur
        public event EventHandler SearchEvent;
        public event EventHandler AddEvent;
        public event EventHandler RemoveEvent;
        public event EventHandler EditEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;
        public event EventHandler PDFEvent;

        //Methods for defining properties and presenting to user
        public void SetMaterialSource(BindingSource materials)
        {
            dataGridViewMaterials.DataSource = materials;
        }

        public void SetMaterialSource2(DataTable materials)
        {
            dataGridViewMaterials.DataSource = materials;
        }

        public void GetVirginEPDs()
        {
            string[] EPDs = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.pdf");
            foreach (string EPD in EPDs)
            {
                MessageBox.Show(EPD);
            }
            comboBoxVirginEPDs.DataSource = EPDs;
        }

        public string getEPD()
        {
            string epd = comboBoxVirginEPDs.SelectedItem.ToString();
            return epd;
        }

        public List<ConsumableModel> GenerateConsumables()
        {
            List<ConsumableModel> consumables = new List<ConsumableModel>();

            for (int i = 0; i < groupBoxConsumables.Controls.Count - 1; i += 4)
            {
                string conName = "";
                int conValue = 0;
                string conUnit = "";
                for (int j = i; j < (i + 4); j++)
                {
                    //MessageBox.Show(j - i + "    " + i + "      " + j);
                    if (j - i == 0)
                        conName = this.groupBoxConsumables.Controls.OfType<Control>()
                                            .Where(c => c.TabIndex == j)
                                            .Select(c => c.Text)
                                            .First();
                    else if (j - i == 1)
                        conValue = Int32.Parse(this.groupBoxConsumables.Controls.OfType<Control>()
                                            .Where(c => c.TabIndex == j)
                                            .Select(c => c.Text)
                                            .First());
                    else if (j - i == 3)
                    {
                        conUnit = this.groupBoxConsumables.Controls.OfType<ComboBox>()
                                            .Where(c => c.TabIndex == j)
                                            .Select(c => c.Text)
                                            .First();
                    }

                }
                consumables.Add(new ConsumableModel
                {
                    Name = conName,
                    Value = conValue,
                    Unit = conUnit
                });
                //MessageBox.Show(conName);
                //MessageBox.Show(conValue+"");
                //MessageBox.Show(conUnit);
            }
            return consumables;
        }

        public string getAllocationMatrix()
        {
            throw new NotImplementedException();
        }
    }
}
