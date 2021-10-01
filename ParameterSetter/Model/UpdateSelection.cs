using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterSetter.Model
{
    class UpdateSelection : IExternalEventHandler
    {
        private readonly ExternalEvent _updateSelection;
        public static Tuple<string, int, List<Model.RvtElementInfo>> SelectedRvtCategory { get; set; }
        public UpdateSelection()
        {
            _updateSelection = ExternalEvent.Create(this);
        }

        public void Execute(UIApplication app)
        {
            List<ElementId> eids = new List<ElementId>();

            foreach (var item in SelectedRvtCategory.Item3)
            {
                eids.Add(item.myElementId);
            }
            app.ActiveUIDocument.Selection.SetElementIds(eids);
            //TaskDialog.Show("R", SelectedRvtCategory.Item1);
        }

        public string GetName()
        {
            return "UpdateSelection";
        }
    }
}
