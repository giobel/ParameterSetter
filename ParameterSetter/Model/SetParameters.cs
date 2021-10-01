using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ParameterSetter.Model
{
    class SetParameters : IExternalEventHandler
    {
        private readonly ExternalEvent _setParameters;
        public SetParameters()
        {
            _setParameters = ExternalEvent.Create(this);
        }

        public void Execute(UIApplication app)
        {
            Document doc = app.ActiveUIDocument.Document;
            UserControl1 myform = Command.form;

            var inputsFields = myform.myItems.Items.SourceCollection;

            foreach (ViewModel.FieldViewModel item in inputsFields)
            {
                Debug.Print($"{item.Caption}:{item.InputText}");
            }

            using (var t = new Transaction(doc,"Change parameters"))
            {
                t.Start();
                foreach (ElementId eid in ViewModel.PanelEvent.SelectedElementsIds)
                {
                    //Parameter p = doc.GetElement(eid).LookupParameter();
                    //p.Set(myform.paramValue.Text);
                }
                t.Commit();
                //TaskDialog.Show("R", Command.SelectedElements.Count.ToString());
                
            }
        }

        public string GetName()
        {
            return "SetParameters";
        }
    }
}
