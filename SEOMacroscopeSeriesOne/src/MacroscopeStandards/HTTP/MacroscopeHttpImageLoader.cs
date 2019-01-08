/*

	This file is part of SEOMacroscope.

	Copyright 2019 Jason Holland.

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
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeHttpImageLoader.
  /// </summary>

  public class MacroscopeHttpImageLoader : Macroscope
  {

    /**************************************************************************/

    private static List<string> TemporaryFiles;

    /**************************************************************************/

    static MacroscopeHttpImageLoader ()
    {
      SuppressStaticDebugMsg = true;
      TemporaryFiles = new List<string>( 16 );
    }

    /** -------------------------------------------------------------------- **/

    ~MacroscopeHttpImageLoader ()
    {
      try
      {
        lock ( TemporaryFiles )
        {
          foreach ( string Filename in TemporaryFiles )
          {
            if ( File.Exists( Filename ) )
            {
              try
              {
                File.Delete( Filename );
              }
              catch ( Exception ex )
              {
                DebugMsg( ex.Message );
              }
            }
          }
        }
      }
      catch ( Exception ex )
      {
        DebugMsg( ex.Message );
      }
    }

    /** -------------------------------------------------------------------- **/

    public MacroscopeHttpImageLoader ()
    {
      this.SuppressDebugMsg = true;
    }

    /**************************************************************************/

    public async Task<Image> LoadImageFromUri ( MacroscopeJobMaster JobMaster, Uri TargetUri )
    {

      MacroscopeHttpTwoClient Client = JobMaster.GetHttpClient();
      MacroscopeHttpTwoClientResponse Response = null;
      Image LoadedImage = null;

      try
      {

        Response = await Client.Get(
          TargetUri,
          this.ConfigureHeadRequestHeadersCallback,
          this.PostProcessRequestHttpHeadersCallback
        );

      }
      catch ( MacroscopeDocumentException ex )
      {
        this.DebugMsg( string.Format( "MacroscopeDocumentException: {0}", ex.Message ) );
        this.DebugMsg( string.Format( "MacroscopeDocumentException: {0}", TargetUri.ToString() ) );
      }
      catch ( Exception ex )
      {
        this.DebugMsg( string.Format( "Exception: {0}", ex.Message ) );
        this.DebugMsg( string.Format( "Exception: {0}", TargetUri.ToString() ) );
      }

      if ( Response != null )
      {

        try
        {

          string ImageFilename = Path.GetTempFileName();
          byte[] ByteData = Response.GetContentAsBytes();

          using ( FileStream ImageStream = File.Create( ImageFilename ) )
          {
            foreach ( byte b in ByteData )
            {
              ImageStream.WriteByte( b );
            }
            ImageStream.Close();
          }

          if ( File.Exists( ImageFilename ) )
          {
            TemporaryFiles.Add( ImageFilename );
            LoadedImage = Image.FromFile( ImageFilename );
          }

        }
        catch ( Exception ex )
        {
          this.DebugMsg( string.Format( "Exception: {0}", ex.Message ) );
        }

      }

      return ( LoadedImage );

    }

    /**************************************************************************/

    public async Task<string> DownloadImageFromUriToFile ( MacroscopeJobMaster JobMaster, Uri TargetUri )
    {

      MacroscopeHttpTwoClient Client = JobMaster.GetHttpClient();
      MacroscopeHttpTwoClientResponse Response = null;
      string ImageFilename = null;

      try
      {

        Response = await Client.Get(
          TargetUri,
          this.ConfigureHeadRequestHeadersCallback,
          this.PostProcessRequestHttpHeadersCallback
        );

      }
      catch ( MacroscopeDocumentException ex )
      {
        this.DebugMsg( string.Format( "MacroscopeDocumentException: {0}", ex.Message ) );
        this.DebugMsg( string.Format( "MacroscopeDocumentException: {0}", TargetUri.ToString() ) );
      }
      catch ( Exception ex )
      {
        this.DebugMsg( string.Format( "Exception: {0}", ex.Message ) );
        this.DebugMsg( string.Format( "Exception: {0}", TargetUri.ToString() ) );
      }

      if ( Response != null )
      {

        try
        {

          ImageFilename = Path.GetTempFileName();
          byte[] ByteData = Response.GetContentAsBytes();

          using ( FileStream ImageStream = File.Create( ImageFilename ) )
          {
            foreach ( byte b in ByteData )
            {
              ImageStream.WriteByte( b );
            }
            ImageStream.Close();
          }

          if ( !File.Exists( ImageFilename ) )
          {
            ImageFilename = null;
          }

        }
        catch ( Exception ex )
        {
          this.DebugMsg( string.Format( "Exception: {0}", ex.Message ) );
        }

      }

      return ( ImageFilename );

    }

    /**************************************************************************/

    private void ConfigureHeadRequestHeadersCallback ( HttpRequestMessage Request )
    {
    }

    /** -------------------------------------------------------------------- **/

    private void PostProcessRequestHttpHeadersCallback ( HttpRequestMessage Request, HttpRequestHeaders DefaultRequestHeaders )
    {
    }

    /**************************************************************************/

  }

}
