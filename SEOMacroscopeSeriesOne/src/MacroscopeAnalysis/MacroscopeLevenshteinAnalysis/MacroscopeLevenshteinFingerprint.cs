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

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeLevenshteinFingerprint.
  /// </summary>

  [Serializable()]
  public class MacroscopeLevenshteinFingerprint : MacroscopeAnalysis
  {

    /**************************************************************************/

    private MacroscopeDocument Document;
    private string Fingerprint;
    private object FingerprintLocker;

    /**************************************************************************/

    public MacroscopeLevenshteinFingerprint ( MacroscopeDocument msDoc )
    {

      this.SuppressDebugMsg = true;

      this.Document = msDoc;
      this.Fingerprint = "";
      this.FingerprintLocker = new object();

    }

    /**************************************************************************/

    public void Analyze ()
    {

      string Text = Document.GetDocumentTextRaw();
      SortedDictionary<char, int> Tokens;
      char[] Characters;

      lock( this.FingerprintLocker )
      {

        this.Fingerprint = "";

        if( !string.IsNullOrEmpty( Text ) )
        {

          Characters = Text.ToLower().ToCharArray();
          Tokens = new SortedDictionary<char, int>();

          foreach( char Token in Characters )
          {

            if( Tokens.ContainsKey( Token ) )
            {
              Tokens[ Token ] = Tokens[ Token ] + 1;
            }
            else
            {
              Tokens[ Token ] = 1;
            }

          }

          foreach( char Token in Tokens.Keys )
          {
            this.Fingerprint = this.Fingerprint + string.Format(
              "{0}:{1}\n",
              Token,
              Tokens[ Token ]
            );
          }

        }

      }

      this.DebugMsg( this.Fingerprint );

      return;

    }

    /**************************************************************************/

    public string GetFingerprint ()
    {
      return ( this.Fingerprint );
    }

    /**************************************************************************/

  }

}
