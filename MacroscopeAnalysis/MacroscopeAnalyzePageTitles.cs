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
	/// Description of MacroscopeAnalyzePageTitles.
	/// </summary>

	public class MacroscopeAnalyzePageTitles : Macroscope
	{

		/**************************************************************************/

		string TitleFontName;
		FontFamily TitleFontFamily;
		int TitleFontSizeInPixels;
		Graphics GraphicsHandle;

		/**************************************************************************/

		public MacroscopeAnalyzePageTitles ()
		{
			TitleFontName = "Arial";
			TitleFontSizeInPixels = 18;
			TitleFontFamily = new FontFamily ( TitleFontName );
			Image iImage = new Bitmap ( 1, 1 );
			GraphicsHandle = Graphics.FromImage( iImage );
		}

		public MacroscopeAnalyzePageTitles ( string sFontName )
		{
			TitleFontName = sFontName;
			TitleFontSizeInPixels = 18;
			TitleFontFamily = new FontFamily ( TitleFontName );
			Image iImage = new Bitmap ( 1, 1 );
			GraphicsHandle = Graphics.FromImage( iImage );
		}
		
		public MacroscopeAnalyzePageTitles ( string sFontName, int iSize )
		{
			TitleFontName = sFontName;
			TitleFontSizeInPixels = iSize;
			TitleFontFamily = new FontFamily ( TitleFontName );
			Image iImage = new Bitmap ( 1, 1 );
			GraphicsHandle = Graphics.FromImage( iImage );
		}

		~MacroscopeAnalyzePageTitles ()
		{
		}
		
		/**************************************************************************/

		public int CalcTitleWidth ( string sText )
		{
			Font fFont = new Font ( TitleFontFamily, TitleFontSizeInPixels, FontStyle.Regular, GraphicsUnit.Pixel );
			SizeF fTextSize = GraphicsHandle.MeasureString( sText, fFont );
			int iWidth = ( int )fTextSize.Width;
			DebugMsg( string.Format( "CalcTitleWidth: {0}", iWidth ) );
			return( iWidth );
		}

		/**************************************************************************/

	}

}
