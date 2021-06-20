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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

using Rock;
using Rock.Attribute;
using Rock.Data;
using Rock.Model;
using Rock.Security;
using Rock.Web.Cache;
using Rock.Web.UI;
using Rock.Web.UI.Controls;

namespace RockWeb.Blocks.DeveloperTest
{
    [DisplayName( "Custom People List" )]
    [Category( "Developer Test" )]
    [Description( "Lists people based on customized criteria." )]

    [BooleanField( "Include Deceased People", "Check this box to include records of deceased people", false, "", 1, "IncludeDeceased" )]
    [BooleanField("Include Underaged People", "Check this box to include records of Underaged (age less then 18) people", false, "", 1, "IncludeUnderage")]
    [LinkedPage( "Person Profile Page", "Page used for viewing a person's profile. If set a view profile button will show for each group member.", false, "", "", 2, "PersonProfilePage" )]
    public partial class CustomPeopleList : RockBlock
    {
        #region Private Variables

        #endregion

        #region Properties

        #endregion

        #region Control Methods

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnInit( EventArgs e )
        {
            base.OnInit( e );
            rFilter.ApplyFilterClick += rFilter_ApplyFilterClick;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load" /> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );

            if ( !Page.IsPostBack )
            {
                SetFilter();
                BindPeopleGrid();
            }
        }

        #endregion

        #region People Grid

        /// <summary>
        /// Handles the RowDataBound event of the gPeople control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gPeople_RowDataBound( object sender, System.Web.UI.WebControls.GridViewRowEventArgs e )
        {
            if ( e.Row.RowType == DataControlRowType.DataRow )
            {
                dynamic person = e.Row.DataItem;

                if ( person != null )
                {
                    if ( person.Gender == Gender.Unknown )
                    {
                        e.Row.AddCssClass( "is-unknown-gender" );
                    }

                    if ( person.IsDeceased )
                    {
                        e.Row.AddCssClass( "is-deceased" );
                    }

                    if (person.Age < 18)
                    {
                        e.Row.AddCssClass("is-underage");
                    }

                }
            }
        }

        /// <summary>
        /// Handles the ApplyFilterClick event of the rFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void rFilter_ApplyFilterClick( object sender, EventArgs e )
        {
            rFilter.SaveUserPreference( "CustomPeopleList-FirstName", "First Name", tbFirstName.Text );
            rFilter.SaveUserPreference( "CustomPeopleList-LastName", "Last Name", tbLastName.Text );
            rFilter.SaveUserPreference("CustomPeopleList-FunnyNickName", "Funny Nick Name", tbFunnyNickName.Text);
            rFilter.SaveUserPreference( "CustomPeopleList-Gender", "Gender", cblGenderFilter.SelectedValues.AsDelimited( ";" ) );

            BindPeopleGrid();
        }

        /// <summary>
        /// Handles the GridRebind event of the gPeople control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridRebindEventArgs" /> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected void gPeople_GridRebind( object sender, GridRebindEventArgs e )
        {
            BindPeopleGrid( e.IsExporting );
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Binds the filter.
        /// </summary>
        private void SetFilter()
        {
            // Add Link to Profile Page Column
            if ( !string.IsNullOrEmpty( GetAttributeValue( "PersonProfilePage" ) ) )
            {
                AddPersonProfileLinkColumn();
            }

            tbFirstName.Text = rFilter.GetUserPreference( "CustomPeopleList-FirstName" );
            tbLastName.Text = rFilter.GetUserPreference( "CustomPeopleList-LastName" );

            string genderValue = rFilter.GetUserPreference( "CustomPeopleList-Gender" );
            if ( !string.IsNullOrWhiteSpace( genderValue ) )
            {
                cblGenderFilter.SetValues( genderValue.Split( ';' ).ToList() );
            }
        }

        /// <summary>
        /// Adds the column with a link to profile page.
        /// </summary>
        private void AddPersonProfileLinkColumn()
        {
            HyperLinkField hlPersonProfileLink = new HyperLinkField();
            hlPersonProfileLink.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            hlPersonProfileLink.HeaderStyle.CssClass = "grid-columncommand";
            hlPersonProfileLink.ItemStyle.CssClass = "grid-columncommand";
            hlPersonProfileLink.DataNavigateUrlFields = new string[1] { "Id" };
            hlPersonProfileLink.DataNavigateUrlFormatString = LinkedPageUrl( "PersonProfilePage", new Dictionary<string, string> { { "PersonId", "###" } } ).Replace( "###", "{0}" );
            hlPersonProfileLink.DataTextFormatString = "<div class='btn btn-default btn-sm'><i class='fa fa-user'></i></div>";
            hlPersonProfileLink.DataTextField = "Id";
            gPeople.Columns.Add( hlPersonProfileLink );
        }

        /// <summary>
        /// Binds the people grid.
        /// </summary>
        protected void BindPeopleGrid( bool isExporting = false )
        {
            RockContext rockContext = new RockContext();
            PersonService personService = new PersonService( rockContext );
            Person per = new Person();
            var qry = personService.Queryable();

            //// Include or exclude deceased records.
            if (!GetAttributeValue("IncludeDeceased").AsBoolean())
            {
                qry = qry.Where(p => p.IsDeceased == false);
            }

            //// Include or exclude underaged records.
            if (!GetAttributeValue("IncludeUnderage").AsBoolean())
            {
                //Update max age if people start living that long. Should unknown age be included?
                qry = qry.WhereAgeRange(18, 256, true);
             }

            // Filter by First Name
            if ( !string.IsNullOrWhiteSpace( tbFirstName.Text ) )
            {
                string firstName = tbFirstName.Text;
                qry = qry.Where( p =>
                    p.FirstName.StartsWith( firstName ) ||
                    p.NickName.StartsWith( firstName ) );
            }

            // Filter by Last Name
            string lastName = tbLastName.Text;
            if ( !string.IsNullOrWhiteSpace( lastName ) )
            {
                qry = qry.Where( p => p.LastName.StartsWith( lastName ) );
            }

            // Get Funny NickName filter -but filter later
            string funnyNickName = tbFunnyNickName.Text;
            

            // Filter by Gender
            var genders = new List<Gender>();
            foreach ( var item in cblGenderFilter.SelectedValues )
            {
                var gender = item.ConvertToEnum<Gender>();
                genders.Add( gender );
            }
            if ( genders.Any() )
            {
                qry = qry.Where( p => genders.Contains( p.Gender ) );
            }

            var dataSource = qry.ToList().Select( p => new
            {
                p.Id,
                p.FirstName,
                p.NickName,
                FunnyNickName = p.NickName.GetFunnyNickName(),
                p.LastName,
                p.Age,
                p.Gender,
                p.IsDeceased
            } ).ToList();

            //filter on funny nick name here, since we need the funny nick name generated before we can filter on it           
            if (!string.IsNullOrWhiteSpace(funnyNickName))
            {
                var dataSourceFiltered = dataSource.AsQueryable().Where(p => p.FunnyNickName.StartsWith(funnyNickName)).ToList();
                gPeople.DataSource = dataSourceFiltered;
                gPeople.DataBind();
                return;
            }

            gPeople.DataSource = dataSource;
            gPeople.DataBind();

        }

        #endregion

    }

    public static class NickNameExtensions_Complete
    {
        public static string GetFunnyNickName( this string s )
        {
            int firstVowel = -1;
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
            foreach ( char c in vowels )
            {
                if ( s.ToLower().StartsWith( c.ToString() ) )
                {
                    return FixCapitalization(ReverseString(s));
                    //return s + "yay";
                }
                else
                {
                    if ( ( firstVowel == -1) || ( ( s.IndexOf( c ) > -1 ) && s.IndexOf( c ) < firstVowel ) )
                    {
                        firstVowel = s.IndexOf( c );
                    }
                }
            }

            if ( firstVowel > -1 )
            {
                return FixCapitalization( s.Substring( firstVowel ) + s.Substring( 0, firstVowel ) + "ay");
            }
            if (s.IndexOf("y") > -1)
            {
                return FixCapitalization(ReverseString(s));
            }
            else
            {
                return s + "ay";
            }
        }
        private static string FixCapitalization( string s )
        {
            if ( string.IsNullOrEmpty( s ) )
            {
                return "";
            }
            return s.ToUpper().Substring( 0, 1 ) + s.ToLower().Substring( 1 );
        }

        private static string ReverseString( string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

    }
}