/*

	This file is part of SEOMacroscope.

	Copyright 2020 Jason Holland.

	The GitHub repository may be found at:

		https://github.com/nazuke/SEOMacroscope

	SEOMacroscope is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	SEOMacroscope is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with SEOMacroscope.  If not, see <http://www.gnu.org/licenses/>.

*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeHttpUrlLoader.
  /// </summary>

  public class MacroscopeHttpUrlLoader : Macroscope
  {

    /**************************************************************************/

    public MacroscopeHttpUrlLoader ()
    {
    }

    /**************************************************************************/

    public MemoryStream LoadMemoryStreamFromUrl ( MacroscopeJobMaster JobMaster, Uri TargetUri, List<string> Expects )
    {

      // TODO: List of expected mime types

      MemoryStream StreamLoader = null;

      return ( StreamLoader );

    }

    /**************************************************************************/

    public async Task<MemoryStream> LoadMemoryStreamFromUrl ( MacroscopeJobMaster JobMaster, Uri TargetUri )
    {
      MemoryStream MemStream = null;
      byte[] ByteData = await this._LoadMemoryStreamFromUrl( JobMaster: JobMaster, TargetUri: TargetUri );
      MemStream = new MemoryStream( ByteData );
      return ( MemStream );
    }

    /** -------------------------------------------------------------------- **/

    private async Task<byte[]> _LoadMemoryStreamFromUrl ( MacroscopeJobMaster JobMaster, Uri TargetUri )
    {

      MacroscopeHttpTwoClient Client = JobMaster.GetHttpClient();
      MacroscopeHttpTwoClientResponse Response = null;
      byte[] ByteData = null;

      try
      {

        Response = await Client.Get(
          TargetUri,
          this.ConfigureHeadRequestHeadersCallback,
          this.PostProcessRequestHttpHeadersCallback
        );

      }
      catch( MacroscopeDocumentException ex )
      {
        this.DebugMsg( string.Format( "MacroscopeDocumentException: {0}", ex.Message ) );
        this.DebugMsg( string.Format( "MacroscopeDocumentException: {0}", TargetUri.ToString() ) );
      }
      catch( Exception ex )
      {
        this.DebugMsg( string.Format( "Exception: {0}", ex.Message ) );
        this.DebugMsg( string.Format( "Exception: {0}", TargetUri.ToString() ) );
      }

      if( Response != null )
      {

        try
        {
          ByteData = Response.GetContentAsBytes();
        }
        catch( Exception ex )
        {
          this.DebugMsg( string.Format( "Exception: {0}", ex.Message ) );
        }

      }
      else
      {
        this.DebugMsg( "NULL" );
      }

      return ( ByteData );

    }

    /** Load Data Immediately *************************************************/

    public async Task<byte[]> LoadImmediateDataFromUrl ( MacroscopeHttpTwoClient Client, Uri TargetUri )
    {

      MacroscopeHttpTwoClientResponse Response = null;
      byte[] ByteData = null;

      try
      {

        Response = await Client.Get(
          TargetUri,
          this.ConfigureHeadRequestHeadersCallback,
          this.PostProcessRequestHttpHeadersCallback
        );

      }
      catch( MacroscopeDocumentException ex )
      {
        this.DebugMsg( string.Format( "MacroscopeDocumentException: {0}", ex.Message ) );
        this.DebugMsg( string.Format( "MacroscopeDocumentException: {0}", TargetUri.ToString() ) );
      }
      catch( Exception ex )
      {
        this.DebugMsg( string.Format( "Exception: {0}", ex.Message ) );
        this.DebugMsg( string.Format( "Exception: {0}", TargetUri.ToString() ) );
      }

      if( Response != null )
      {

        try
        {
          ByteData = Response.GetContentAsBytes();
        }
        catch( Exception ex )
        {
          this.DebugMsg( string.Format( "Exception: {0}", ex.Message ) );
        }

      }
      else
      {
        this.DebugMsg( "NULL" );
      }

      return ( ByteData );

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
