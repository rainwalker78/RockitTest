// <copyright>
// Copyright by the Spark Development Network
//
// Licensed under the Rock Community License (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.rockrms.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//
using System;
using System.ComponentModel;
using System.Web.UI;
using Rock.Attribute;
using Rock.Model;
using System.Linq;
using System.Web.UI.WebControls;
using Rock.Web.UI.Controls;
using Rock.Data;
using Rock;

/// <summary>

/// </summary>
namespace RockWeb.Blocks.Utility
{
    /// <summary>
    /// Template block for developers to use to start a new block.
    /// </summary>
    [DisplayName( "Developer Test Get Groups" )]
    [Category( "Utility > Get Groups Dev Test" )]
    [Description( "Block to fetch Group list data from Rock." )]

    #region Block Attributes

    [BooleanField(
        "Show Email Address",
        Key = AttributeKey.ShowEmailAddress,
        Description = "Should the email address be shown?",
        DefaultBooleanValue = true,
        Order = 1 )]

    [EmailField(
        "Email",
        Key = AttributeKey.Email,
        Description = "The Email address to show.",
        DefaultValue = "ted@rocksolidchurchdemo.com",
        Order = 2 )]

    [CustomRadioListField("Gender Filter", "Select in order to list only records for that gender",
     "1^Male,2^Female", required: false)]
  

    #endregion Block Attributes
    public partial class GetGroups : Rock.Web.UI.RockBlock
    {

        #region Attribute Keys

        private static class AttributeKey
        {
            public const string ShowEmailAddress = "ShowEmailAddress";
            public const string Email = "Email";
        }

        #endregion Attribute Keys

        #region PageParameterKeys

        private static class PageParameterKey
        {
            public const string StarkId = "StarkId";
        }

        #endregion PageParameterKeys

        #region Fields

        // Used for private variables.

        #endregion

        #region Properties

        // Used for public / protected properties.

        #endregion

        #region Base Control Methods

        // Overrides of the base RockBlock methods (i.e. OnInit, OnLoad)

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnInit( EventArgs e )
        {
            base.OnInit( e );

            // This event gets fired after block settings are updated. It's nice to repaint the screen if these settings would alter it.
            this.BlockUpdated += Block_BlockUpdated;
            this.AddConfigurationUpdateTrigger( upnlContent );
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load" /> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            if (!Page.IsPostBack)
            {
                //var genderValue = GetAttributeValue("GenderFilter");

                //var query = new PersonService(new RockContext()).Queryable();

                //if (!string.IsNullOrEmpty(genderValue))
                //{
                //    Gender gender = genderValue.ConvertToEnum<Gender>();
                //    query = query.Where(p => p.Gender == gender);
                //}

                //gPeople.DataSource = query.ToList();
                //gPeople.DataBind();

                var items = new GroupService(new RockContext()).Queryable().ToList();
                gGroups.DataSource = items;
                gGroups.DataBind();      

               
            }
        }

        #endregion

        #region Events

        // Handlers called by the controls on your block.

        /// <summary>
        /// Handles the BlockUpdated event of the control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Block_BlockUpdated( object sender, EventArgs e )
        {

        }

        protected void gPeople_RowSelected(object sender, RowEventArgs e)
        {
            int personId = (int)e.RowKeyValues["Id"];
            Response.Redirect(string.Format("~/Person/{0}", personId), false);

            // prevents .NET from quietly throwing ThreadAbortException
            Context.ApplicationInstance.CompleteRequest();
            return;
        }

        protected void gGroups_RowSelected(object sender, RowEventArgs e)
        {
            //int personId = (int)e.RowKeyValues["Id"];
            //Response.Redirect(string.Format("~/Person/{0}", personId), false);

            //// prevents .NET from quietly throwing ThreadAbortException
            //Context.ApplicationInstance.CompleteRequest();
            return;
        }
        #endregion

        #region Methods

        // helper functional methods (like BindGrid(), etc.)

        #endregion
    }
}



