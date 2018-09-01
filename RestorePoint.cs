using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Optimisation_Windows_Capet.Base.UserControl
{
    class RestorePoint
    {
        public static bool CreateRestorePoint(string description)
        {
            try
            {
                var restorePointName = "Optimisation Windows - " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
                var managementPath = new ManagementPath(@"\\.\ROOT\DEFAULT:SystemRestore");
                var systemRestoreClass = new ManagementClass(managementPath);
                var methodParameters = systemRestoreClass.Methods["CreateRestorePoint"].InParameters;

                methodParameters.Properties["Description"].Value = "Optimisation Windows - " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
                methodParameters.Properties["EventType"].Value = 100u;
                methodParameters.Properties["Description"].Value = "Optimisation Windows - " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();

                var outParameters = systemRestoreClass.InvokeMethod("CreateRestorePoint", methodParameters, null);
                var hresult = unchecked((int)(uint)outParameters["ReturnValue"]);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
