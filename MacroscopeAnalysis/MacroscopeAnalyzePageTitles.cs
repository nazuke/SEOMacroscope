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
      Image ImageInstance = new Bitmap ( 1, 1 );
      this.GraphicsHandle = Graphics.FromImage( ImageInstance );
    }

    public MacroscopeAnalyzePageTitles ( string FontName )
    {
      this.SuppressDebugMsg = true;
      this.TitleFontName = FontName;
      this.TitleFontSizeInPixels = 18;
      this.TitleFontFamily = new FontFamily ( this.TitleFontName );
      Image ImageInstance = new Bitmap ( 1, 1 );
      this.GraphicsHandle = Graphics.FromImage( ImageInstance );
    }

    public MacroscopeAnalyzePageTitles ( string FontName, int FontSize )
    {
      this.SuppressDebugMsg = true;
      this.TitleFontName = FontName;
      this.TitleFontSizeInPixels = FontSize;
      this.TitleFontFamily = new FontFamily ( this.TitleFontName );
      Image ImageInstance = new Bitmap ( 1, 1 );
      this.GraphicsHandle = Graphics.FromImage( ImageInstance );
    }

    /**************************************************************************/

    ~MacroscopeAnalyzePageTitles ()
    {
    }

    /**************************************************************************/

    public int CalcTitleWidth ( string Text )
    {
      Font FontInstance = new Font ( this.TitleFontFamily, this.TitleFontSizeInPixels, FontStyle.Regular, GraphicsUnit.Pixel );
      SizeF FontTextSize = this.GraphicsHandle.MeasureString( Text, FontInstance );
      int FontWidth = ( int )FontTextSize.Width;
      DebugMsg( string.Format( "CalcTitleWidth: {0}", FontWidth ) );
      return( FontWidth );
    }

    /**************************************************************************/

  }

}
