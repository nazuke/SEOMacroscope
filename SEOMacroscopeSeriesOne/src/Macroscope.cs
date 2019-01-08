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
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Runtime;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using System.Data.HashFunction;
using System.Data.HashFunction.Blake2;
using System.Data.HashFunction.Core;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of Macroscope.
  /// </summary>

  [Serializable()]
  public class Macroscope
  {

    /**************************************************************************/

    // Override SuppressDebugMsg to enable/disable debugging output statically.
    public static bool SuppressStaticDebugMsg;

    // Set SuppressDebugMsg to enable/disable debugging output in object.
    public bool SuppressDebugMsg;

    protected static Dictionary<string, string> Memoize = new Dictionary<string, string>( 1024 );

    protected static bool ThrowInsufficientMemoryException;

    protected static IBlake2B BlakeHasher;
    protected static HashAlgorithm ShaHasher;

    /**************************************************************************/

    static Macroscope ()
    {
      SuppressStaticDebugMsg = true;
      ThrowInsufficientMemoryException = false;
      BlakeHasher = Blake2BFactory.Instance.Create();
      ShaHasher = HashAlgorithm.Create( "SHA256" );
    }

    public Macroscope ()
    {
      this.SuppressDebugMsg = true;
    }

    /**************************************************************************/

    public static string GetVersion ()
    {
      string Location = Assembly.GetExecutingAssembly().Location;
      string Version = FileVersionInfo.GetVersionInfo( Location ).ProductVersion;
      return ( Version );
    }

    /** HTTP User Agent *******************************************************/

    public string UserAgentName ()
    {
#if DEBUG
      return ( "SEO-Macroscope-DEVELOPER-MODE" );
#else
      return ( "SEO-Macroscope" );
#endif
    }

    /** -------------------------------------------------------------------- **/

    public string UserAgentVersion ()
    {
#if DEBUG
      return ( "DEBUG" );
#else
      return( FileVersionInfo.GetVersionInfo( Assembly.GetExecutingAssembly().Location ).ProductVersion );
#endif
    }

    /** Global URL to Digest Routines *****************************************/

    public static ulong UrlToDigest ( string Url )
    {
      ulong hashed = StringToDigest( Text: Url );
      return ( hashed );
    }

    /** Text Numeeric Digest **************************************************/

    public static ulong StringToDigest ( string Text )
    {
      string Digested = BlakeHasher.ComputeHash( Text, 64 ).AsHexString();
      ulong HashedValue = ulong.Parse( Digested, System.Globalization.NumberStyles.HexNumber );
      return ( HashedValue );
    }

    /** Text Digest ***********************************************************/

    public static string GetStringDigest ( string Text )
    {

      string Digested = null;

      /*
      if( Macroscope.Memoize.ContainsKey( Text ) )
      {
        Digested = Macroscope.Memoize[Text];
      }
      else
      {
       */
        byte[] BytesIn = Encoding.UTF8.GetBytes( Text );
        byte[] Hashed = ShaHasher.ComputeHash( BytesIn );
        StringBuilder Buf = new StringBuilder();

        for( int i = 0 ; i < Hashed.Length ; i++ )
        {
          Buf.Append( Hashed[i].ToString( "X2" ) );
        }

        Digested = Buf.ToString();

        //Macroscope.Memoize[Text] = Digested;

      //}

      return ( Digested );
    }

    /** EXTERNAL BROWSER ******************************************************/

    public static void OpenUrlInBrowser ( string Url )
    {
      // The caller must catch any exceptions:
      Uri OpenUrl = null;
      OpenUrl = new Uri( Url );
      if( OpenUrl != null )
      {
        // FIXME: bug here when opening URL with query string
        Process.Start( OpenUrl.ToString() );
      }
    }

    /**************************************************************************/

    [Conditional( "DEVMODE" )]
    public static void DebugMsgStatic ( string Msg )
    {
      if( !SuppressStaticDebugMsg )
      {
        Debug.WriteLine( Msg );
      }
    }

    [Conditional( "DEVMODE" )]
    public void DebugMsg ( string Msg )
    {
      if( !SuppressDebugMsg )
      {
        Debug.WriteLine(
          string.Format(
            "TID:{0} :: {1} :: {2}",
            Thread.CurrentThread.ManagedThreadId,
            this.GetType(),
            Msg
          )
        );
      }
    }

    [Conditional( "DEVMODE" )]
    public void DebugMsgForced ( string Msg )
    {
      Debug.WriteLine(
        string.Format(
          "TID:{0} :: {1} :: {2}",
          Thread.CurrentThread.ManagedThreadId,
          this.GetType(),
          Msg
        )
      );
    }

    /**************************************************************************/

  }

}
