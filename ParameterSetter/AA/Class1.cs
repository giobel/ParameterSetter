using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace ParameterSetter.ViewModel
{
    class Class1 : INotifyPropertyChanged
    {
        public string WindowTitle { get; private set; }
        public bool EventRegistered { get; set; }
        public Timer SelectionTimer;
        public string SelectedIds;

        public ICommand ButtonCommand { get; set; }

        public Class1()
        {
            WindowTitle = "Class 1 View Model";
            if (EventRegistered)
            {
                EventRegistered = false;
                //app.Application.DocumentChanged -= Application_DocumentChanged;                
                SelectionTimer.Elapsed -= SelectionTimer_Elapsed;
                Debug.Print("**************EventRegisterHandler Unsuscribe************");
            }
            else
            {
                EventRegistered = true;
                //app.Application.DocumentChanged += Application_DocumentChanged;
                SelectionTimer = new Timer(1400) { AutoReset = true, Enabled = true };
                SelectionTimer.Elapsed += SelectionTimer_Elapsed;
                Debug.Print("**************EventRegisterHandler Subscribed************");
            }
            ButtonCommand = new RelayCommand(o => MainButtonClick("MainButton"));

        }

        private void MainButtonClick(string v)
        {
            throw new NotImplementedException();
        }

        void SelectionTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var selectedObjects = GetSelectedObjects();
            SelectedIds = $"{selectedObjects.Count.ToString()} selected";
            foreach (ElementId item in selectedObjects)
            {
                Debug.Print($"Element Id {item.ToString()}");
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
