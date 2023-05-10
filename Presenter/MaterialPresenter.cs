using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MVPMatpres.Models;
using MVPMatpres.Views;

namespace MVPMatpres.Presenter
{   
    /// <summary>
    /// The presenter of the MVP architecture. It acts upon the view and the model. It retrieves data from the
    /// repositories we have and formats it to be displayed in the view. It also handles the events from the view.
    /// </summary>
    public class MaterialPresenter
    {
        //Variables needed for a presenter
        private IMaterialView view;
        private IMaterialRepository repository;
        private BindingSource materialSource;
        private IEnumerable<MaterialModel> materials;
        private IDocumentOpener PDFOpener;
        private IDocumentOpener DocXOpener;
        private FileUploader uploader;

        //Constructor using needed fields, note that source and materials is not included.
        public MaterialPresenter(IMaterialView view, IMaterialRepository repository)
        {
            this.view = view;
            this.repository = repository;
            this.materialSource = new BindingSource();
            this.PDFOpener = new DocumentOpener();
            this.DocXOpener = new DocumentOpener();
            this.uploader = new FileUploader();
            //We want to change the events here.
            this.view.SearchEvent += searchMaterial; //Since searchevents uses a method, we send the whole method.
            this.view.AddEvent += addMaterial;
            this.view.RemoveEvent += removeMaterial;
            this.view.EditEvent += editMaterial;
            this.view.SaveEvent += saveMaterial;
            this.view.CancelEvent += cancelCurrent;
            this.view.PDFEvent += bringPDFForm;
            //Set the data source
            this.view.SetMaterialSource(materialSource);
            LoadMaterialsAll();

            this.view.Show();
        }
        /// <summary>
        /// Loads the materials from the repository and sets the source to the view.
        /// </summary>
        private void LoadMaterialsAll()
        {
            materials = repository.FindAll2();
            materialSource.DataSource = materials;
        }
        /// <summary>
        /// brings up the pdf and docx files the user can see.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void bringPDFForm(object? sender, EventArgs e)
        {

            //This part is for opening existing EPDs, virgin materials
            this.PDFOpener.SetFilePath(this.view.getEPD());
            this.PDFOpener.OpenFile();
            MaterialModel mat = (MaterialModel)materialSource.Current;
            JSONWriter writer = new JSONWriter(mat);
            writer.WriteToFile();
            uploader.UploadToBucket();

            await uploader.waitForFile("EPD_Results.docx");

            this.DocXOpener.SetFilePath(@".\EPD_Results.docx");
            this.DocXOpener.OpenFile();
        }
        /// <summary>
        /// Searches for a material in the repository.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchMaterial(object? sender, EventArgs e)
        {
            bool searchIsEmpty = string.IsNullOrWhiteSpace(this.view.SearchField);
            if(!searchIsEmpty)
            {
                materials = this.repository.FindByValue(this.view.SearchField);
            }
            else
            {
                materials = this.repository.FindAll();
            }
            materialSource.DataSource = materials;
        }
        /// <summary>
        /// Resets all the text boxes, basically setting them empty or 0
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelCurrent (object? sender, EventArgs e)
        {
            ResetAllTextBoxes();
        }

        /// <summary>
        /// Saves the material to the repository.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveMaterial(object? sender, EventArgs e)
        {
            MaterialModel material = new MaterialModel();
            material.Name = this.view.MaterialName;
            material.Consumables = this.view.Consumables; 
            material.TotalMass = this.view.TotalMass;
            material.ExternalMass = this.view.ExternalMass;
            try
            {
                if (material.Consumables == null)
                    throw new Exception("Consumables is null");
                else if (material.Name == null)
                    throw new Exception("name is null");
                else
                {
                    repository.Add(material);
                    view.Message = ("Added new material: " + material.Name);
                }
                view.IsComplete = true;
                LoadMaterialsAll();
                ResetAllTextBoxes();
            }
            catch (Exception ex)
            {
                view.IsComplete = false;
                view.Message = ex.ToString();
            }
        }
        /// <summary>
        /// Resets all the text boxes in the view.
        /// </summary>
        private void ResetAllTextBoxes()
        {
            view.MaterialName = "";
            view.TotalMass = 0;
            view.ExternalMass = 0;
            view.Consumables = null;
        }
        /// <summary>
        /// Removes a material from the repository
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeMaterial(object? sender, EventArgs e)
        {
            MaterialModel mat = (MaterialModel)materialSource.Current;
            view.Message = "Deleted material: " + mat.Name;
            repository.Delete(mat);
            LoadMaterialsAll();
        }
        /// <summary>
        /// Edits a material in the repository.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editMaterial(object? sender, EventArgs e)
        {
            MessageBox.Show("Hej");
            MaterialModel mat = (MaterialModel)materialSource.Current;
            view.MaterialName = mat.Name.ToString();
        }
        /// <summary>
        /// Sets the mode to add material. A bit buggy right now.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addMaterial(object? sender, EventArgs e)
        {
            view.IsEditing = false;
        }
    }
}
