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
using System.Drawing;

namespace SEOMacroscope
{

  /// <summary>
  /// Calculate the width of a title string in pixels.
  /// </summary>

  public class MacroscopeAnalyzePageTitles : Macroscope
  {

    /**************************************************************************/

    private string TitleFontName;
		
    private FontFamily TitleFontFamily;
		
    private int TitleFontSizeInPixels;
		
    private Graphics GraphicsHandle;

    /**************************************************************************/

    public MacroscopeAnalyzePageTitles ()
    {
      this.SuppressDebugMsg = true;
      this.TitleFontName = "Arial";
      this.TitleFontSizeInPixels = 18;
      this.TitleFontFamily = new FontFamily ( this.TitleFontName );
      Image iImage = new Bitmap ( 1, 1 );
      this.GraphicsHandle = Graphics.FromImage( iImage );
    }

    public MacroscopeAnalyzePageTitles ( string sFontName )
    {
      this.SuppressDebugMsg = true;
      this.TitleFontName = sFontName;
      this.TitleFontSizeInPixels = 18;
      this.TitleFontFamily = new FontFamily ( this.TitleFontName );
      Image iImage = new Bitmap ( 1, 1 );
      this.GraphicsHandle = Graphics.FromImage( iImage );
    }

    public MacroscopeAnalyzePageTitles ( string sFontName, int iSize )
    {
      this.SuppressDebugMsg = true;
      this.TitleFontName = sFontName;
      this.TitleFontSizeInPixels = iSize;
      this.TitleFontFamily = new FontFamily ( this.TitleFontName );
      Image iImage = new Bitmap ( 1, 1 );
      this.GraphicsHandle = Graphics.FromImage( iImage );
    }

    /**************************************************************************/

    ~MacroscopeAnalyzePageTitles ()
    {
    }

    /**************************************************************************/

    public int CalcTitleWidth ( string sText )
    {
      Font fFont = new Font ( this.TitleFontFamily, this.TitleFontSizeInPixels, FontStyle.Regular, GraphicsUnit.Pixel );
      SizeF fTextSize = this.GraphicsHandle.MeasureString( sText, fFont );
      int iWidth = ( int )fTextSize.Width;
      DebugMsg( string.Format( "CalcTitleWidth: {0}", iWidth ) );
      return( iWidth );
    }

    /**************************************************************************/

  }

}
