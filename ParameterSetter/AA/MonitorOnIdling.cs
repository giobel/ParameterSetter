using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;


namespace ParameterSetter.ViewModel
{
    class MonitorOnIdling: INotifyPropertyChanged
    {

        #region Fields
        public string WindowTitle { get; private set; }
        private int Progress { get; set; }
        private List<int> _lastSelIds;

        #endregion

        #region  Events

        public event EventHandler SelectionChanged;

        #endregion

        #region Properties

        public ObservableCollection<ElementId> SelectedElementIds { get; set; }

        private string _myTextContent;
        public string MyTextContent
        {
            get { return _myTextContent; }
            set { SetField(ref _myTextContent, value, "MyTextContent"); }
        }
        #endregion

        #region Methods (SC)         

        public MonitorOnIdling()
        {

            MyTextContent = "000";
            WindowTitle = "My Title here";

            //Task.Delay(100).ContinueWith(t =>
            //{
            //    while (Progress < 100)
            //    {
            //        MyTextContent = Progress.ToString();
            //        Progress += 5;
            //        Task.Delay(500).Wait();
            //    }
            //});
        }

        public void OnIdlingEvent(object sender, IdlingEventArgs e)
        {
            ICollection<ElementId> latestSelection = Command.UIApp.ActiveUIDocument.Selection.GetElementIds();

            if (latestSelection.Count == 0)
            {
                if (SelectedElementIds != null && SelectedElementIds.Count > 0)

                {
                    HandleSelectionChange(latestSelection);
                }
            }
            else
            {
                if (SelectedElementIds == null)
                {
                    HandleSelectionChange(latestSelection);
                }
                else
                {
                    if (SelectedElementIds.Count != latestSelection.Count)
                    {
                        HandleSelectionChange(latestSelection);
                    }
                    else
                    {
                        if (SelectionHasChanged(latestSelection))
                        {
                            HandleSelectionChange(latestSelection);
                        }
                    }
                }
            }

            Debug.WriteLine("idling");
            Debug.WriteLine(SelectedElementIds.Count.ToString());
        }


        private void HandleSelectionChange(IEnumerable<ElementId> elementIds)
        {
            SelectedElementIds = new ObservableCollection<ElementId>();
            _lastSelIds = new List<int>();

            foreach (var elementId in elementIds)
            {
                SelectedElementIds.Add(elementId);
                _lastSelIds.Add(elementId.IntegerValue);
            }
            MyTextContent = SelectedElementIds.Count.ToString();            
            InvokeSelectionChangedEvent();
        }


        private void InvokeSelectionChangedEvent()
        {
            SelectionChanged?.Invoke(this, new EventArgs());
        }


        private bool SelectionHasChanged(IEnumerable<ElementId> elementIds)
        {
            var i = 0;

            foreach (var elementId in elementIds)
            {
                if (_lastSelIds[i] != elementId.IntegerValue)
                {
                    return true;
                }

                ++i;
            }

            return false;
        }

        #endregion
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