using System;
using System.Text.RegularExpressions;

namespace SEOMacroscope
{

	public class MacroscopeURLTools : Macroscope
	{

		/**************************************************************************/

		static Macroscope ms = new Macroscope();
		
		/**************************************************************************/

		public MacroscopeURLTools ()
		{
		}

		/**************************************************************************/

		public static string make_url_absolute( string sBaseURL, string sURL )
		{

			string sURLFixed;
			Uri uBase = new Uri (sBaseURL, UriKind.Absolute);
			Uri uNew = null;

			Regex reSlash = new Regex ("^/");
			Regex reQuery = new Regex ("^\\?");
			Regex reHash = new Regex ("^#");
			Regex reHTTP = new Regex ("^http");

			if (reSlash.IsMatch( sURL )) {

				try {
					uNew = new Uri (
						string.Format(
							"{0}://{1}{2}",
							uBase.Scheme,
							uBase.Host,
							sURL
						),
						UriKind.Absolute
					);
			   	
				} catch (InvalidOperationException ex) {
					ms.debug_msg( ex.Message );
				} catch (UriFormatException ex) {
					ms.debug_msg( ex.Message );
				}
				
			} else if (reQuery.IsMatch( sURL )) {

				try {
					uNew = new Uri (
						string.Format(
							"{0}://{1}{2}",
							uBase.Scheme,
							uBase.Host,
							sURL
						),
						UriKind.Absolute
					);
			   	
				} catch (InvalidOperationException ex) {
					ms.debug_msg( ex.Message );
				} catch (UriFormatException ex) {
					ms.debug_msg( ex.Message );
				}

			} else if (reHash.IsMatch( sURL )) {

				string sNewURL = sURL.ToString();
				Regex reHashRemove = new Regex ( "#.*$", RegexOptions.Singleline );
				sNewURL = reHashRemove.Replace( sNewURL, "" );

				try {
					uNew = new Uri (
						string.Format(
							"{0}://{1}{2}",
							uBase.Scheme,
							uBase.Host,
							sNewURL
						),
						UriKind.Absolute
					);
			   	
				} catch (InvalidOperationException ex) {
					ms.debug_msg( ex.Message );
				} catch (UriFormatException ex) {
					ms.debug_msg( ex.Message );
				}

			} else if (reHTTP.IsMatch( sURL )) {

				try {
					uNew = new Uri (sURL, UriKind.Absolute);
				} catch (InvalidOperationException ex) {
					ms.debug_msg( ex.Message );
				} catch (UriFormatException ex) {
					ms.debug_msg( ex.Message );
				}

			} else {
				; // NO-OP, for now.
			}

			if (uNew != null) {
				sURLFixed = uNew.ToString();
			} else {
				sURLFixed = sURL;
			}

			return( sURLFixed );

		}

		/**************************************************************************/

		public static Boolean verify_same_host( string sBaseURL, string sURL )
		{
			Boolean bSuccess = false;
			Uri uBase = null;
			Uri uNew = null;

			try {
				uBase = new Uri (sBaseURL, UriKind.Absolute);
			} catch (InvalidOperationException ex) {
				ms.debug_msg( ex.Message );
				ms.debug_msg( string.Format( "FAILED TO VERIFY: {0}", sBaseURL ) );
			} catch (UriFormatException ex) {
				ms.debug_msg( ex.Message );
				ms.debug_msg( string.Format( "FAILED TO VERIFY: {0}", sBaseURL ) );
			} catch (Exception ex) {
				ms.debug_msg( ex.Message );
				ms.debug_msg( string.Format( "FAILED TO VERIFY: {0}", sBaseURL ) );
			}

			try {
				uNew = new Uri (sURL, UriKind.Absolute);
			} catch (InvalidOperationException ex) {
				ms.debug_msg( ex.Message );
				ms.debug_msg( string.Format( "FAILED TO VERIFY: {0}", sURL ) );
			} catch (UriFormatException ex) {
				ms.debug_msg( ex.Message );
				ms.debug_msg( string.Format( "FAILED TO VERIFY: {0}", sURL ) );
			} catch (Exception ex) {
				ms.debug_msg( ex.Message );
				ms.debug_msg( string.Format( "FAILED TO VERIFY: {0}", sURL ) );
			}

			try {
				if (( uBase != null ) && ( uNew != null ) && ( uBase.Host.ToString() == uNew.Host.ToString() )) {
					bSuccess = true;
				}
			} catch (UriFormatException ex) {
				ms.debug_msg( ex.Message );
				ms.debug_msg( string.Format( "FAILED TO VERIFY: {0}", sURL ) );
			}
			
			return( bSuccess );
		}

		/**************************************************************************/

		public static int find_url_depth( string sURL )
		{
			
			int iDepth = 0;
			Uri uURI = null;

			try {
				uURI = new Uri (sURL, UriKind.Absolute);
			} catch (InvalidOperationException ex) {
				ms.debug_msg( ex.Message );
			} catch (UriFormatException ex) {
				ms.debug_msg( ex.Message );
			}

			if (uURI != null) {
				string sPath = uURI.AbsolutePath;
				iDepth = sPath.Split( '/' ).Length - 1;
			}

			return( iDepth );
			
		}

		/**************************************************************************/

	}
	
}
