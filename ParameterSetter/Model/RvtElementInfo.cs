using Autodesk.Revit.DB;

namespace ParameterSetter.Model
{
    public class RvtElementInfo : ViewModel.ViewModelBase
    {
        public RvtElementInfo()
        {

        }

        public RvtElementInfo(ElementId EleId, string category, int Count)
        {        
            this.myElementId = EleId;
            this.Info = Count;
            this.Category = category;
        }
        private ElementId _elementId;

        public ElementId myElementId
        {
            get
            {
                return _elementId;
            }
            set
            {
                _elementId = value; OnPropertyChanged("myElementId");
            }
        }

        private string _category;
        public string Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value; OnPropertyChanged("Category");
            }
        }

        private int _fileInfo;

        public int Info
        {
            get { return _fileInfo; }
            set
            {
                _fileInfo = value;
                OnPropertyChanged("FilePath");
            }
        }

    }
}
