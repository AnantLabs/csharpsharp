using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace CSharpSharp.Web.Tests
{
    /// <summary>
    /// Validates the RepeaterSharp properties, methods and extension methods.
    /// </summary>
    public class RepeaterSharpFixture
    {
        /// <summary>
        /// Validates that trying to find a control in an empty repeater returns null.
        /// </summary>
        [Test]
        public void FindControlInNewRepeaterReturnsNull()
        {
            Repeater repeater = new Repeater();

            Assert.IsNull(repeater.FindControlInHeader(""));
            Assert.IsNull(repeater.FindControlInFooter(""));
        }

        /// <summary>
        /// Validates that finding a control in the
        /// header or footer of a repeater works.
        /// </summary>
        [Test]
        public void FindControlInRepeaterTest()
        {
            Repeater repeater = new Repeater();

            repeater.HeaderTemplate = new DummyTemplate(ListItemType.Header, "ControlInHeader");
            repeater.FooterTemplate = new DummyTemplate(ListItemType.Footer, "ControlInFooter");

            // Assign a dummy list to ensure the header and footer controls are initialized
            List<int> dummyList = new List<int>();
            repeater.DataSource = dummyList;
            repeater.DataBind();

            Assert.IsNotNull(repeater.FindControlInHeader("ControlInHeader"));
            Assert.IsNotNull(repeater.FindControlInFooter("ControlInFooter"));
        }

        /// <summary>
        /// A dummy template to test the creation of controls in a repeater.
        /// </summary>
        private class DummyTemplate : ITemplate
        {
            ListItemType templateType;
            string idOfControl;

            /// <summary>
            /// Initializes a new instance of the <see cref="DummyTemplate"/> class.
            /// </summary>
            /// <param name="type">The type.</param>
            /// <param name="idOfControl">The id of control.</param>
            public DummyTemplate(ListItemType type, string idOfControl)
            {
                templateType = type;
                this.idOfControl = idOfControl;
            }

            /// <summary>
            /// When implemented by a class, defines the <see cref="T:System.Web.UI.Control"/> object that child controls and templates belong to. These child controls are in turn defined within an inline template.
            /// </summary>
            /// <param name="container">The <see cref="T:System.Web.UI.Control"/> object to contain the instances of controls from the inline template.</param>
            public void InstantiateIn(System.Web.UI.Control container)
            {
                Literal dummyControl = new Literal();
                dummyControl.ID = idOfControl;

                container.Controls.Add(dummyControl);

                Literal lc = new Literal();
            }
        }
    }
}
