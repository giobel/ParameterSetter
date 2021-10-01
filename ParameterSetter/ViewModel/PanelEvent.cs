using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace ParameterSetter.ViewModel
{
    class PanelEvent: ViewModelBase
    {
        
        public string WindowTitle { get; private set; }
        public static List<ElementId> SelectedElementsIds;
        
        private Tuple<string, int, List<Model.RvtElementInfo>> _selectedRvtCategory;
        public Tuple<string, int, List<Model.RvtElementInfo>> SelectedRvtCategory
        {
            get { return _selectedRvtCategory; }
            set { SetField(ref _selectedRvtCategory, value, "Subscribed"); }
        }

        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set { SetField(ref _isChecked, value, "IsChecked"); }
        }

        public ICommand ButtonCommand { get; set; }
        public ICommand ApplyCommand { get; set; }
        public ICommand UpdateSelectionCommand { get; set; }

        private bool _suscribed;
        public bool Subscribed
        {
            get { return _suscribed; }
            set { SetField(ref _suscribed, value, "Subscribed"); }
        }
        public static ExternalEvent ExEvent { get; set; }
        public static ExternalEvent ApplyEvent { get; set; }
        public static ExternalEvent UpdateSelection { get; set; }

        ObservableCollection<FieldViewModel> _fields;
        public ObservableCollection<FieldViewModel> Fields
        {
            get { return _fields; }
            set { SetField(ref _fields, value, "Fields"); }            
        }
        public PanelEvent()
        {
            _fields = new ObservableCollection<FieldViewModel>();
            _fields.Add(new FieldViewModel(){Caption = "Mark",InputText = ""});
            _fields.Add(new FieldViewModel() { Caption = "Comments", InputText = "" });
            _fields.Add(new FieldViewModel() { Caption = "Level", InputText = "" });

            SelectedElementsIds = new List<ElementId>();
            _suscribed = false; 
            if (ExEvent != null)
            {
                //ExEvent.Raise();
                //_suscribed = true;
                //Debug.Print("*************Not Subscribed****************");
            }

            else
                MessageBox.Show("External event handler is null");

            ButtonCommand = new RelayCommand(o => StartStopCommandClick("MainButton"));
            ApplyCommand = new RelayCommand(o => ApplyCommandClick("ApplyCommand"));
            UpdateSelectionCommand = new RelayCommand(o => UpdateSelectionCommandClick("UpdateSelection"));

            WindowTitle = "Panel Title here";

            //Task.Delay(100).ContinueWith(t =>
            //{
            //    while (Progress < 100)
            //    {
            //        SelectedIds = Progress.ToString();                    
            //        Progress += 5;
            //        Task.Delay(500).Wait();
            //    }
            //});

        }

        private void UpdateSelectionCommandClick(string v)
        {
            Model.UpdateSelection.SelectedRvtCategory = SelectedRvtCategory;
            UpdateSelection.Raise();
        }

        private void ApplyCommandClick(string v)
        {
            ApplyEvent.Raise();
            Debug.Print($"selected panel event category: {SelectedRvtCategory}");
            Debug.Print($"selected panel is checked: {IsChecked}");
        }

        private void StartStopCommandClick(object sender)
        {
            ExEvent.Raise();
            if (_suscribed == true)
            {
                Debug.Print("*************Unsuscribe****************");
                Subscribed = false;
            }
            else
            {
                Debug.Print("*************Subscribe****************");
                Subscribed = true;
            }
            
        }

        // boiler-plate for INOtifyPropertyChnged
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChangedEventHandler handler = PropertyChanged;
        //    if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        //}
        //protected bool SetField<T>(ref T field, T value, string propertyName)
        //{
        //    if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        //    field = value;
        //    OnPropertyChanged(propertyName);
        //    return true;
        //}
    }
}
