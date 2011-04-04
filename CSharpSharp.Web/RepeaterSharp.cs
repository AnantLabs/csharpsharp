using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSharpSharp.Web
{
    /// <summary>
    /// Provides additional methods for working with Repeater controls.
    /// </summary>
    public static class RepeaterSharp
    {
        /// <summary>
        /// Searches the header of the current repeater for a server control with the specified id parameter.
        /// </summary>
        /// <param name="repeater">The repeater.</param>
        /// <param name="id">The identifier for the control to be found.</param>
        /// <returns>The specified control, or null if the specified control does not exist.</returns>
        public static Control FindControlInHeader(this Repeater repeater, string id)
        {
            if (!repeater.HasControls())
            {
                return null;
            }

            return repeater.Controls[0].Controls[0].FindControl(id);
        }

        /// <summary>
        /// Searches the footer of the current repeater for a server control with the specified id parameter.
        /// </summary>
        /// <param name="repeater">The repeater.</param>
        /// <param name="id">The identifier for the control to be found.</param>
        /// <returns>The specified control, or null if the specified control does not exist.</returns>
        public static Control FindControlInFooter(this Repeater repeater, string id)
        {
            if (!repeater.HasControls())
            {
                return null;
            }

            return repeater.Controls[repeater.Controls.Count - 1].Controls[0].FindControl(id);
        }
    }
}
