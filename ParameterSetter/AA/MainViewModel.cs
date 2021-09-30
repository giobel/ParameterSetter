using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ParameterSetter.ViewModel
{
    class MainViewModel: INotifyPropertyChanged
    {
        public string WindowTitle { get; private set; }
        
        private string _myTextContent;
        public string MyTextContent
        {
            get { return _myTextContent; }
            set { SetField(ref _myTextContent, value, "MyTextContent"); }
        }
        private int Progress { get; set; }
        public MainViewModel()
        {

            MyTextContent = "000";
            WindowTitle = "My Title here";

            Task.Delay(100).ContinueWith(t =>
            {
                while (Progress < 100)
                {
                    MyTextContent = Progress.ToString();                    
                    Progress += 5;
                    Task.Delay(500).Wait();
                }
            });
        }

        public void PanelEvent(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Debug.Assert(sender is Autodesk.Windows.RibbonTab,
              "expected sender to be a ribbon tab");

            if (e.PropertyName == "Title")
            {
                ICollection<ElementId> ids = Command.UIApp.ActiveUIDocument.Selection.GetElementIds();

                int n = ids.Count;

                string s = (0 == n)
                  ? "<nil>"
                  : string.Join(", ",
                    ids.Select<ElementId, string>(
                      id => id.IntegerValue.ToString()));

                Debug.Print(
                  "CmdSelectionChanged: selection changed: "
                  + s);
            }
        }


        // boiler-plate
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
