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
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using System.Net;
using ExCSS;

namespace SEOMacroscope
{

	public partial class MacroscopeDocument : Macroscope
	{

		/**************************************************************************/

		void ProcessCssPage ()
		{

			HttpWebRequest req = null;
			HttpWebResponse res = null;
			string sErrorCondition = null;

			DebugMsg( string.Format( "ProcessCssPage: {0}", "" ) );

			try
			{

				req = WebRequest.CreateHttp( this.Url );
				req.Method = "GET";
				req.Timeout = this.Timeout;
				req.KeepAlive = false;
				MacroscopePreferencesManager.EnableHttpProxy( req );

				res = ( HttpWebResponse )req.GetResponse();

			}
			catch( WebException ex )
			{

				DebugMsg( string.Format( "ProcessCssPage :: WebException: {0}", ex.Message ) );
				DebugMsg( string.Format( "ProcessCssPage :: WebException: {0}", ex.Status ) );
				DebugMsg( string.Format( "ProcessCssPage :: WebException: {0}", ( int )ex.Status ) );

				sErrorCondition = ex.Status.ToString();

			}

			if( res != null )
			{

				string sRawData = "";

				this.ProcessHttpHeaders( req, res );

				// Get Response Body
				try
				{

					DebugMsg( string.Format( "MIME TYPE: {0}", this.MimeType ) );
					Stream sStream = res.GetResponseStream();
					StreamReader srRead = new StreamReader ( sStream, Encoding.UTF8 ); // Assume UTF-8
					sRawData = srRead.ReadToEnd();
					this.ContentLength = sRawData.Length; // May need to find bytes length
					//DebugMsg( string.Format( "sRawData: {0}", sRawData ) );

				}
				catch( WebException ex )
				{

					DebugMsg( string.Format( "WebException", ex.Message ) );
					sRawData = "";
					this.ContentLength = 0;

				}
				catch( Exception ex )
				{

					DebugMsg( string.Format( "Exception", ex.Message ) );
					this.StatusCode = ( int )HttpStatusCode.BadRequest;
					this.ContentLength = 0;

				}

				if( sRawData.Length > 0 )
				{
					ExCSS.Parser ExCssParser = new  ExCSS.Parser ();
					ExCSS.StyleSheet ExCssStylesheet = ExCssParser.Parse( sRawData );

					this.ProcessCssHyperlinksOut( ExCssStylesheet );

				}

				{ // Title
					MatchCollection reMatches = Regex.Matches( this.Url, "/([^/]+)$" );
					string sTitle = null;
					foreach( Match match in reMatches )
					{
						if( match.Groups[ 1 ].Value.Length > 0 )
						{
							sTitle = match.Groups[ 1 ].Value.ToString();
							break;
						}
					}
					if( sTitle != null )
					{
						this.Title = sTitle;
						DebugMsg( string.Format( "TITLE: {0}", this.Title ) );
					}
					else
					{
						DebugMsg( string.Format( "TITLE: {0}", "MISSING" ) );
					}
				}

				res.Close();

			}

			if( sErrorCondition != null )
			{
				this.ProcessErrorCondition( sErrorCondition );
			}

		}

		/**************************************************************************/

		void ProcessCssHyperlinksOut ( ExCSS.StyleSheet ExCssStylesheet )
		{

			foreach( var rRule in ExCssStylesheet.StyleRules )
			{

				int iRule = ExCssStylesheet.StyleRules.IndexOf( rRule );

				foreach( Property pProp in ExCssStylesheet.StyleRules[ iRule ].Declarations.Properties )
				{

					if( pProp.Name.Equals( "background-image" ) )
					{

						string sBackgroundImageUrl = pProp.Term.ToString();
						string sLinkUrlAbs;

						DebugMsg( string.Format( "ProcessCssHyperlinksOut: {0}", sBackgroundImageUrl ) );

						sBackgroundImageUrl = MacroscopeUrlTools.CleanUrlCss( sBackgroundImageUrl );

						if( sBackgroundImageUrl != null )
						{

							sLinkUrlAbs = MacroscopeUrlTools.MakeUrlAbsolute( this.Url, sBackgroundImageUrl );

							DebugMsg( string.Format( "ProcessCssHyperlinksOut: {0}", sBackgroundImageUrl ) );
							DebugMsg( string.Format( "ProcessCssHyperlinksOut this.Url: {0}", this.Url ) );
							DebugMsg( string.Format( "ProcessCssHyperlinksOut sLinkUrlAbs: {0}", sLinkUrlAbs ) );

							// TODO: Verify that this actually works:

							this.HyperlinksOut.Add( this.Url, sLinkUrlAbs );

						}
						else
						{
							DebugMsg( string.Format( "ProcessCssHyperlinksOut: {0}", "NOT CSS URL" ) );
						}

					}

				}

			}

		}

		/**************************************************************************/

	}

}
