using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterSetter.ViewModel
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
            using (var t = new Transaction(doc,"Change parameters"))
            {
                t.Start();
                foreach (ElementId eid in PanelEvent.SelectedElementsIds)
                {
                    Parameter p = doc.GetElement(eid).LookupParameter(myform.paramName.Text);
                    p.Set(myform.paramValue.Text);
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
