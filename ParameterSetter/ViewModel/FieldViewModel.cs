using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterSetter.ViewModel
{
    class FieldViewModel: ViewModelBase
    {
        string _caption = "";
        public string Caption
        {
            get { return _caption; }
            set
            {                
                SetField(ref _caption, value, "Caption");
            }
        }

        string _inputText = "";
        public string InputText
        {
            get { return _inputText; }
            set
            {
                SetField(ref _inputText, value, "InputText");                
                
            }
        }
    }
}
