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
using System.Diagnostics;
using System.Threading;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeLinkChainBinaryTreeNode.
  /// </summary>

  public class MacroscopeLinkChainBinaryTreeNode<T> where T : IComparable<T>
  {

    /**************************************************************************/

    public enum NodeOrientation
    {
      LEFT = 0,
      RIGHT = 1
    }

    private string NodeName;

    private T NodeValue;

    private MacroscopeLinkChainBinaryTreeNode<T> ChildNodeLeft;

    private MacroscopeLinkChainBinaryTreeNode<T> ChildNodeRight;

    /**************************************************************************/

    public MacroscopeLinkChainBinaryTreeNode ( string Name, T Value )
    {

      this.NodeName = Name;

      this.NodeValue = Value;
      
      this.ChildNodeLeft = null;

      this.ChildNodeRight = null;

    }

    /**************************************************************************/

    public MacroscopeLinkChainBinaryTreeNode<T> SetNodeName (
      string Name
    )
    {

      this.NodeName = Name;

      return( this );

    }

    /** -------------------------------------------------------------------- **/

    public string GetNodeName ()
    {
      return( this.NodeName );
    }

    /**************************************************************************/

    public MacroscopeLinkChainBinaryTreeNode<T> SetNodeValue ( 
      T Value
    )
    {

      this.NodeValue = Value;

      return( this );
      
    }

    /** -------------------------------------------------------------------- **/

    public T GetNodeValue ()
    {
      return( this.NodeValue );
    }
    
    /**************************************************************************/

    public MacroscopeLinkChainBinaryTreeNode<T> AddChildNode (
      MacroscopeLinkChainBinaryTreeNode<T>.NodeOrientation Orientation,
      MacroscopeLinkChainBinaryTreeNode<T> ChildNode
    )
    {

      switch( Orientation )
      {
        case MacroscopeLinkChainBinaryTreeNode<T>.NodeOrientation.LEFT:
          this.ChildNodeLeft = ChildNode;
          break;
        case MacroscopeLinkChainBinaryTreeNode<T>.NodeOrientation.RIGHT:
          this.ChildNodeRight = ChildNode;
          break;
        default:
          throw new Exception ( "Invalid NodeOrientation" );
      }

      return( this );
      
    }
    
    /**************************************************************************/

    public MacroscopeLinkChainBinaryTreeNode<T> GetChildNode (
      MacroscopeLinkChainBinaryTreeNode<T>.NodeOrientation Orientation
    )
    {

      MacroscopeLinkChainBinaryTreeNode<T> ChildNode;
      
      switch( Orientation )
      {
        case MacroscopeLinkChainBinaryTreeNode<T>.NodeOrientation.LEFT:
          ChildNode = this.ChildNodeLeft;
          break;
        case MacroscopeLinkChainBinaryTreeNode<T>.NodeOrientation.RIGHT:
          ChildNode = this.ChildNodeRight;
          break;
        default:
          throw new Exception ( "Invalid NodeOrientation" );
      }

      return( ChildNode );
      
    }

    /**************************************************************************/
    
    [Conditional( "DEVMODE" )]
    private void DebugMsg ( string Msg )
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
