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
using System.Drawing;
using System.IO;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeQrCodeAnalysis.
  /// </summary>

  [Serializable()]
  public class MacroscopeQrCodeAnalysis : MacroscopeAnalysis
  {

    /**************************************************************************/

    public MacroscopeQrCodeAnalysis ()
    {
      this.SuppressDebugMsg = true;
    }

    /**************************************************************************/

    public string Decode ( string ImageFilename )
    {

      QRCodeReader QrReader = new QRCodeReader();
      Bitmap QrCodeImage = null;
      Result QrResult = null;
      string ResultText = null;

      try
      {
        QrCodeImage = (Bitmap) Bitmap.FromFile( ImageFilename );
      }
      catch ( Exception )
      {
        throw new FileNotFoundException( string.Format( "QRCode image file not found: {0}", ImageFilename ) );
      }

      try
      {
        if ( QrCodeImage != null )
        {
          using ( QrCodeImage )
          {
            LuminanceSource ImageSource;
            ImageSource = new BitmapLuminanceSource( QrCodeImage );
            BinaryBitmap QrCodeBitmap = new BinaryBitmap( new HybridBinarizer( ImageSource ) );
            QrResult = QrReader.decode( QrCodeBitmap );
            if ( QrResult != null )
            {
              ResultText = QrResult.Text;
            }
          }
        }
      }
      catch ( Exception )
      {
        throw new Exception( "Failed to decode QRCode" );
      }

      return ( ResultText );

    }

    /**************************************************************************/

  }

}
