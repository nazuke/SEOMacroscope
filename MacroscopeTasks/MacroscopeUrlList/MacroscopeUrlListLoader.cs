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
using System.IO;
using System.Text.RegularExpressions;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeUrlListLoader.
  /// </summary>

  public class MacroscopeUrlListLoader : Macroscope
  {

    /**************************************************************************/

    private MacroscopeJobMaster JobMaster;
    private string Path;
    private string [] UrlListText;
    private List<string> UrlList;

    /**************************************************************************/

    public MacroscopeUrlListLoader ( MacroscopeJobMaster JobMaster, string Path )
    {
      this.JobMaster = JobMaster;
      this.Path = Path;
      this.UrlListText = new string[0];
      this.UrlList = new List<string> ();
    }

    public MacroscopeUrlListLoader ( MacroscopeJobMaster JobMaster, string [] UrlListText )
    {
      this.JobMaster = JobMaster;
      this.Path = null;
      this.UrlListText = UrlListText;
      this.UrlList = new List<string> ();
    }

    /**************************************************************************/

    public Boolean Execute ()
    {
      
      Boolean Success = false;
      MacroscopeAllowedHosts AllowedHosts = this.JobMaster.GetAllowedHosts();

      this.CleanseList();

      if( this.UrlList.Count > 0 )
      {

        this.JobMaster.SetRunTimeMode(
          JobRunTimeMode: MacroscopeConstants.RunTimeMode.LISTFILE
        );

        for( int i = 0 ; i < this.UrlList.Count ; i++ )
        {
          string Url = this.UrlList[ i ];
          AllowedHosts.AddFromUrl( Url );
          this.JobMaster.AddUrlQueueItem( Url );
        }

        Success = true;
      }

      return( Success );
      
    }

    /**************************************************************************/

    private Boolean CleanseList ()
    {

      Boolean Success = false;
      string [] Urls = null;

      if( this.Path != null )
      {

        try
        {
          Urls = File.ReadAllLines( this.Path );
        }
        catch( FileLoadException ex )
        {
          DebugMsg( string.Format( "FileLoadException: {0}", ex.Message ) );
        }

      }
      else
      {
        Urls = this.UrlListText;
      }

      if( ( Urls != null ) && ( Urls.Length > 0 ) )
      {
        for( int i = 0 ; i < Urls.Length ; i++ )
        {
          string Url = Urls[ i ];
          Url = Regex.Replace( Url, "^\\s+", "" );
          Url = Regex.Replace( Url, "\\s+$", "" );
          if( Url.Length > 0 )
          {
            if( Uri.IsWellFormedUriString( Url, UriKind.Absolute ) )
            {
              DebugMsg( string.Format( "CleanseList Adding: {0}", Url ) );
              this.UrlList.Add( Url );
            }
          }
        }
        Success = true;
      }

      return( Success );
    }

    /**************************************************************************/

    public string GetUrlListItem ( int Item )
    {
      string Url = null;
      try
      {
        if( this.UrlList[ Item ].Length > 0 )
        {
          Url = this.UrlList[ Item ];
        }
      }
      catch( Exception ex )
      {
        DebugMsg( string.Format( "GetUrlListItem: {0}", ex.Message ) );
      }
      return( Url );
    }

    /**************************************************************************/

  }

}
