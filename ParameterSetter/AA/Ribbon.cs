using System.Reflection;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;

namespace ParameterSetter
{



    public class Ribbon
    {

        #region Constructors

        public Ribbon(string tabName, string panelName)
        {
            RibbonPanelName = panelName;
            RibbonTabName = tabName;
            Create();
        }

        #endregion

        #region Properties

        public static string RibbonPanelName
        {
            get;
            private set;
        }

        public static string RibbonTabName
        {
            get;
            private set;
        }

        private static string ImagePath { get { return "SelectionMonitorCore.UI.Images."; } }

        private static string Path { get { return Assembly.GetExecutingAssembly().Location; } }

        #endregion

        #region Methods (SC)

        private static void Create()
        {
            // Tab
            //App.UIContApp.CreateRibbonTab(RibbonTabName);


            // On Idling Selection Monitor
            //var panelOnIdlingMonitor = App.UIContApp.CreateRibbonPanel(RibbonTabName, "OnIdling");
            //panelOnIdlingMonitor.AddItem(OnIdlingCommand(RibbonPanelName + panelOnIdlingMonitor));

        }


        private static PushButtonData OnIdlingCommand(string ribbonAndPanelName)
        {
            var buttonName = "OnIdlingCommand";
            var buttonTitle = "Start/Stop";
            
            return new PushButtonData(ribbonAndPanelName + buttonName, buttonTitle, Path, "ParameterSetter.MonitorOnIdlingCommand")
            {
                LargeImage = GetBitmapFrame(ImagePath + "startstop_32.png"),
                Image = GetBitmapFrame(ImagePath + "startstop_16.png")
            };
        }

        #endregion
        public static BitmapSource GetBitmapFrame(string name)
        {
            try
            {
                var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);

                return BitmapFrame.Create(stream);
            }
            catch
            {
                return null;
            }
        }
    }

}