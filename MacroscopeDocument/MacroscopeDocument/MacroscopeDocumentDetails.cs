/*
	
	This file is part of SEOMacroscope.
	
	Copyright 2017 Jason Holland.
	
	The GitHub repository may be found at:
	
		https://github.com/nazuke/SEOMacroscope
	
	Foobar is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.
	
	Foobar is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.
	
	You should have received a copy of the GNU General Public License
	along with Foobar.  If not, see <http://www.gnu.org/licenses/>.

*/

using System;
using System.Collections.Generic;

namespace SEOMacroscope
{

	/// <summary>
	/// Description of MacroscopeDocument.
	/// </summary>

	public partial class MacroscopeDocument : Macroscope
	{

		/**************************************************************************/
		
		public List<KeyValuePair<string,string>> DetailDocumentDetails ()
		{

			List<KeyValuePair<string,string>> slDetails = new List<KeyValuePair<string,string>> ();

			slDetails.Add( new KeyValuePair<string,string> ( "URL", this.GetUrl() ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Status Code", this.GetStatusCode().ToString() ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Error Condition", this.GetErrorCondition() ) );
						
			slDetails.Add( new KeyValuePair<string,string> ( "Duration (seconds)", this.GetDurationInSecondsFormatted() ) );

			slDetails.Add( new KeyValuePair<string,string> ( "HTST Policy Enabled", this.HypertextStrictTransportPolicy.ToString() ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Content Type", this.GetMimeType() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Content Length", this.ContentLength.ToString() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Encoding", this.ContentEncoding ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Compressed", this.GetIsCompressed().ToString() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Compression Method", this.GetCompressionMethod() ) );
									
			slDetails.Add( new KeyValuePair<string,string> ( "Date", this.GetDateServer() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Date Modified", this.GetDateModified() ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Language", this.GetLang() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Character Set", this.GetCharacterSet() ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Canonical", this.GetCanonical() ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Redirect", this.GetIsRedirect().ToString() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Redirected From", this.UrlRedirectFrom ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Links In Count", this.CountHyperlinksIn().ToString() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Links Out Count", this.CountHyperlinksOut().ToString() ) );

			slDetails.Add( new KeyValuePair<string,string> ( "HrefLang Count", this.GetHrefLangs().Count.ToString() ) );
				
			slDetails.Add( new KeyValuePair<string,string> ( "Title", this.GetTitle() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Title Length", this.GetTitleLength().ToString() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Title Pixel Width", this.GetTitlePixelWidth().ToString() ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Description", this.GetDescription() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Description Length", this.GetDescriptionLength().ToString() ) );

			slDetails.Add( new KeyValuePair<string,string> ( "Keywords", this.GetKeywords() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Keywords Length", this.GetKeywordsLength().ToString() ) );
			slDetails.Add( new KeyValuePair<string,string> ( "Keywords Count", this.GetKeywordsCount().ToString() ) );

			for( ushort iLevel = 1 ; iLevel <= 6 ; iLevel++ )
			{
				string sHeading;
				if( this.GetHeadings( iLevel ).Count > 0 )
				{
					sHeading = this.GetHeadings( iLevel )[ 0 ].ToString();
				}
				else
				{
					sHeading = null;
				}
				if( sHeading != null )
				{
					slDetails.Add( new KeyValuePair<string,string> ( string.Format( "H{0}", iLevel ), sHeading ) );
					slDetails.Add( new KeyValuePair<string,string> ( string.Format( "H{0} Length", iLevel ), sHeading.Length.ToString() ) );
				}
			}

			slDetails.Add( new KeyValuePair<string,string> ( "Page Depth", this.Depth.ToString() ) );

			return( slDetails );

		}

		/**************************************************************************/

	}

}
