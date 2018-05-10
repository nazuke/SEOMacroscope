/*

	This file is part of SEOMacroscope.

	Copyright 2018 Jason Holland.

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
using System.Threading.Tasks;

namespace SEOMacroscope
{

  public class MacroscopeCheckForUpdate : Macroscope
  {

    /**************************************************************************/

    public MacroscopeCheckForUpdate ()
    {
    }

    /**************************************************************************/

    public async Task<bool> PhoneHome ()
    {

      bool NewVersionAvailable = true;
      MacroscopeHttpUrlLoader UrlLoader = new MacroscopeHttpUrlLoader();
      MacroscopeHttpTwoClient Client = new MacroscopeHttpTwoClient();
      Uri TargetUri = new Uri( MacroscopeConstants.CheckForUpdateUrl );
      byte[] Data = await UrlLoader.LoadImmediateDataFromUrl( Client: Client, TargetUri: TargetUri );
      string PublishedVersion = System.Text.Encoding.UTF8.GetString( Data );
      string CurrentVersion = Macroscope.GetVersion();

      if( PublishedVersion == CurrentVersion )
      {
        NewVersionAvailable = false;
      }
      else
      {
        NewVersionAvailable = true;
      }

      return ( NewVersionAvailable );

    }

    /**************************************************************************/

  }

}
