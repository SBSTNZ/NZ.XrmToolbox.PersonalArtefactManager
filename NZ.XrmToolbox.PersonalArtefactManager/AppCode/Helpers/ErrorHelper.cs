using System;
using System.ServiceModel;
using System.Windows.Forms;

namespace NZ.XrmToolbox.PersonalArtefactManager.AppCode.Helpers
{
    public class ErrorHelper
    {
        public static void ShowExceptionMessageDialog(Exception exc)
        {
            // Get reference to the dialog type.
            var dialogTypeName = "System.Windows.Forms.PropertyGridInternal.GridErrorDlg";
            var dialogType = typeof(System.Windows.Forms.Form).Assembly.GetType(dialogTypeName);

            // Create dialog instance.
            var dialog = (System.Windows.Forms.Form)Activator.CreateInstance(dialogType, new PropertyGrid());

            // Populate relevant properties on the dialog instance.
            dialog.Text = "Error occured";
            dialogType.GetProperty("Details").SetValue(dialog, GetErrorMessage(exc, true), null);
            dialogType.GetProperty("Message").SetValue(dialog, GetErrorMessage(exc, false), null);

            // Display dialog.
            var result = dialog.ShowDialog(System.Windows.Forms.Form.ActiveForm);
        }

        public static string GetErrorMessage(Exception exc, bool returnWithStackTrace)
        {
            if (exc.InnerException is FaultException)
            {
                if (returnWithStackTrace)
                {
                    return (exc.InnerException).ToString();
                }
                else
                {
                    return (exc.InnerException).Message;
                }
            }
            else if (exc.InnerException != null)
            {
                if (returnWithStackTrace)
                {
                    return exc.InnerException.ToString();
                }
                else
                {
                    return exc.InnerException.Message;
                }
            }
            else
            {
                if (returnWithStackTrace)
                {
                    return exc.ToString();
                }
                else
                {
                    return exc.Message;
                }
            }
        }
    }
}