using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ParameterSetter.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;

namespace ParameterSetter.ViewModel
{
    public class GetCurrentSelection : IExternalEventHandler
    {
        public bool EventRegistered { get; set; }
        public Timer SelectionTimer;
        public string Result { get; set; }
        public string SelectedIds;
        public UserControl1 UIForm { get; set; }
        private readonly ExternalEvent _externalEvent;
        UIApplication _uiapp = null;

        public GetCurrentSelection()
        {
            _externalEvent = ExternalEvent.Create(this);
        }

        public void Execute(UIApplication app)
        {
            _uiapp = app;
            
            if (EventRegistered)
            {
                EventRegistered = false;
                //app.Application.DocumentChanged -= Application_DocumentChanged;                
                SelectionTimer.Elapsed -= SelectionTimer_Elapsed;
                PanelEvent.SelectedElementsIds.Clear();
                Debug.Print("**************EventRegisterHandler Unsuscribe************");
                UIForm.Dispatcher.Invoke(() =>
                {
                    UIForm.OutputLabel.Content = $"Addin disabled";
                });                
            }
            else
            {
                EventRegistered = true;                
                //app.Application.DocumentChanged += Application_DocumentChanged;
                SelectionTimer = new Timer(1400) { AutoReset = true, Enabled = true };
                SelectionTimer.Elapsed += SelectionTimer_Elapsed;
                Debug.Print("**************EventRegisterHandler Subscribed************");
            }
        }

        
        

        void SelectionTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            List<RvtElementInfo> fileinfoList = new List<RvtElementInfo>();
            PanelEvent.SelectedElementsIds.Clear();
            
            var selectedObjects = GetSelectedObjects();
            SelectedIds = $"{selectedObjects.Count.ToString()}";
            string selection = "";
            string categories = "";
            RvtElementInfo selectedCategory = new RvtElementInfo();

            foreach (ElementId item in selectedObjects)
            {
                Element ele = _uiapp.ActiveUIDocument.Document.GetElement(item);
                selection += $"{item.ToString()},";
                categories += $"{ele.Category.Name},";
                PanelEvent.SelectedElementsIds.Add(item);
                RvtElementInfo fi = new RvtElementInfo(item, ele.Category.Name, ele.Id.IntegerValue);
                fileinfoList.Add(fi);
            }

            var query = fileinfoList.GroupBy(item => item.Category);
            List < Tuple<string, int, List<RvtElementInfo>>> newR = new List<Tuple<string, int, List<RvtElementInfo>>>();
            
            foreach (var result in query)
            {
                newR.Add(new Tuple<string, int, List<RvtElementInfo>>(result.Key, result.Count(), result.ToList())) ;
                Debug.WriteLine("\nCategory: " + result.Key);
                Debug.WriteLine("Count: " + result.Count().ToString());                
            }
            Debug.WriteLine("**********");

            UIForm.Dispatcher.Invoke(() =>
            {
                if (UIForm.OutputLabel.Content.ToString() != categories)
                {
                    UIForm.OutputLabel.Content = $"{categories}";
                    UIForm.newLbox.ItemsSource = newR.OrderBy(x=>x.Item1);
                }
                if (UIForm.newLbox.SelectedItem != null) {

                }
                    selectedCategory = UIForm.newLbox.SelectedItem as RvtElementInfo;
            });

            //Debug.Print($"{SelectedIds} Element(s) selected. Element Id: { selection }" );
            if (selectedCategory != null)
            {
                Debug.Print($"selected category: {selectedCategory.Category}");
            }

            
        }
        private ICollection<ElementId> GetSelectedObjects()
        {
            Document CurrentDoc = Command.UIApp.ActiveUIDocument.Document;
            if (CurrentDoc == null)
            {
                return new List<ElementId>();
            }

            var selectedObjects = Command.UIApp.ActiveUIDocument.Selection.GetElementIds();
            return selectedObjects;
        }

        public string GetName()
        {
            return "EventRegisterHandler";
        }
    }
}
