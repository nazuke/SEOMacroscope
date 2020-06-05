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

namespace SEOMacroscope
{

  public static class MacroscopePreferencesPresets
  {

    /**************************************************************************/

    static MacroscopePreferencesPresets ()
    {
    }

    /**************************************************************************/

    public static void DefaultSettings ()
    {
      MacroscopePreferencesManager.SetDefaultValues();
      MacroscopePreferencesManager.SavePreferences();
    }

    /**************************************************************************/

    public static void HtmlOnly ()
    {

      MacroscopePreferencesManager.SetDefaultValues();

      MacroscopePreferencesManager.SetCheckExternalLinks( false );
      MacroscopePreferencesManager.SetCheckHreflangs( false );
      MacroscopePreferencesManager.SetEnableTextIndexing( false );
      MacroscopePreferencesManager.SetDetectQrCodeInImage( false );
      MacroscopePreferencesManager.SetEnableLevenshteinDeduplication( false );
      MacroscopePreferencesManager.SetProbeHumansText( false );
      MacroscopePreferencesManager.SetProbeHead404sWithGet( true );
      MacroscopePreferencesManager.SetCheckRedirects( true );
      MacroscopePreferencesManager.SetFollowRedirects( false );
      MacroscopePreferencesManager.SetFollowNoFollow( true );
      MacroscopePreferencesManager.SetFollowCanonicalLinks( true );
      MacroscopePreferencesManager.SetFollowAlternateLinks( false );
      MacroscopePreferencesManager.SetFollowHrefLangLinks( false );

      MacroscopePreferencesManager.SetFetchStylesheets( false );
      MacroscopePreferencesManager.SetFetchJavascripts( false );
      MacroscopePreferencesManager.SetFetchImages( false );
      MacroscopePreferencesManager.SetFetchAudio( false );
      MacroscopePreferencesManager.SetFetchVideo( false );
      MacroscopePreferencesManager.SetFetchXml( true );
      MacroscopePreferencesManager.SetFetchBinaries( false );

      MacroscopePreferencesManager.SetProcessAudio( false );
      MacroscopePreferencesManager.SetProcessBinaries( false );
      MacroscopePreferencesManager.SetProcessImages( false );
      MacroscopePreferencesManager.SetProcessJavascripts( false );
      MacroscopePreferencesManager.SetProcessPdfs( false );
      MacroscopePreferencesManager.SetProcessStylesheets( false );
      MacroscopePreferencesManager.SetProcessVideo( false );
      MacroscopePreferencesManager.SetProcessXml( false );

      MacroscopePreferencesManager.SetAnalyzeTextReadability( false );

      MacroscopePreferencesManager.SavePreferences();

    }

    /**************************************************************************/

    public static void HtmlAndPdfs ()
    {

      MacroscopePreferencesManager.SetDefaultValues();

      HtmlOnly();

      MacroscopePreferencesManager.SetProcessPdfs( true );

      MacroscopePreferencesManager.SavePreferences();

    }

    /**************************************************************************/

    public static void HtmlAndLinkedAssets ()
    {

      MacroscopePreferencesManager.SetDefaultValues();

      HtmlOnly();

      MacroscopePreferencesManager.SetFetchStylesheets( true );
      MacroscopePreferencesManager.SetFetchJavascripts( true );
      MacroscopePreferencesManager.SetFetchImages( true );
      MacroscopePreferencesManager.SetFetchAudio( true );
      MacroscopePreferencesManager.SetFetchVideo( true );
      MacroscopePreferencesManager.SetFetchXml( true );
      MacroscopePreferencesManager.SetFetchBinaries( false );

      MacroscopePreferencesManager.SetProcessAudio( true );
      MacroscopePreferencesManager.SetProcessBinaries( false );
      MacroscopePreferencesManager.SetProcessImages( true );
      MacroscopePreferencesManager.SetProcessJavascripts( true );
      MacroscopePreferencesManager.SetProcessPdfs( false );
      MacroscopePreferencesManager.SetProcessStylesheets( true );
      MacroscopePreferencesManager.SetProcessVideo( false );
      MacroscopePreferencesManager.SetProcessXml( false );

      MacroscopePreferencesManager.SavePreferences();

    }

    /**************************************************************************/

    public static void HrefLangMatrix ()
    {

      MacroscopePreferencesManager.SetDefaultValues();

      HtmlOnly();

      MacroscopePreferencesManager.SetFollowCanonicalLinks( true );
      MacroscopePreferencesManager.SetFollowAlternateLinks( true );
      MacroscopePreferencesManager.SetFollowHrefLangLinks( true );

      MacroscopePreferencesManager.SavePreferences();

    }

    /**************************************************************************/

  }

}
