﻿/*

  This file is part of SEOMacroscope.

  Copyright 2019 Jason Holland.

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
using System.Runtime.Serialization;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeSaveCsvFileException.
  /// </summary>

  [Serializable]
  public class MacroscopeSaveCsvFileException : Exception, ISerializable
  {

    /**************************************************************************/

    public MacroscopeSaveCsvFileException ()
    {
    }

    /**************************************************************************/

    public MacroscopeSaveCsvFileException ( string message )
      : base( message )
    {
    }

    /**************************************************************************/

    public MacroscopeSaveCsvFileException ( string message, Exception innerException )
      : base( message, innerException )
    {
    }

    /**************************************************************************/

    protected MacroscopeSaveCsvFileException ( SerializationInfo info, StreamingContext context )
      : base( info, context )
    {
    }

    /**************************************************************************/

  }

}
