using System;
using System.Timers;
using System.Collections.Generic;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using System.Linq;
using System.Diagnostics;

namespace ParameterSetter
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public static UIApplication UIApp;
        public static UIControlledApplication UIContApp;
        public static UserControl1 form;

        
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApp = commandData.Application;

            
            //new ViewModel.PanelEvent();

            //https://adndevblog.typepad.com/aec/2015/09/revitapi-revit-2016-events-registerunregister-behavior-change.html
            ViewModel.GetCurrentSelection _exeventHander = new ViewModel.GetCurrentSelection();                        
            ViewModel.PanelEvent.ExEvent = ExternalEvent.Create(_exeventHander);

            Model.SetParameters _setParameters = new Model.SetParameters();
            ViewModel.PanelEvent.ApplyEvent = ExternalEvent.Create(_setParameters);

            Model.UpdateSelection _updateSelection = new Model.UpdateSelection();
            ViewModel.PanelEvent.UpdateSelection = ExternalEvent.Create(_updateSelection);
            

            form = new UserControl1();
            _exeventHander.UIForm = form;
            form.Show();
            
            return Result.Succeeded;
        }



    }


}