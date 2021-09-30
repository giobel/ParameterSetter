#region Namespaces
using System;
using System.Collections.Generic;
using System.Reflection;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
#endregion

namespace ParameterSetter
{
    class App : IExternalApplication
    {


        public Result OnStartup(UIControlledApplication a)
        {
            //var ribbonTab = new Ribbon("My Selection Monitor", "Monitor");
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }

        //private static UIApplication GetUiApplication()
        //{
        //    var versionNumber = UIContApp.ControlledApplication.VersionNumber;

        //    var fieldName = "m_uiapplication";

        //    var fieldInfo = UIContApp.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

        //    var uiApplication = (UIApplication)fieldInfo?.GetValue(UIContApp);

        //    return uiApplication;
        //}
    }
}
