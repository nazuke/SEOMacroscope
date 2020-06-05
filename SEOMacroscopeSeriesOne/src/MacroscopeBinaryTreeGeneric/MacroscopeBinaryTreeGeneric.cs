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
using System.Diagnostics;
using System.Threading;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeBinaryTreeGeneric.
  /// </summary>

  public class MacroscopeBinaryTreeGeneric<T> where T : IComparable<T>
  {

    // TODO: This implementation incomplete    
    
    /**************************************************************************/

    private MacroscopeBinaryTreeGenericNode<T> Tree;

    /**************************************************************************/

    public MacroscopeBinaryTreeGeneric ()
    {

      this.Tree = null;

    }

    /**************************************************************************/

    public MacroscopeBinaryTreeGenericNode<T> SetRootNode (
      string Name,
      T Value
    )
    {

      this.Tree = new MacroscopeBinaryTreeGenericNode<T> (
        Name: Name,
        Value: Value
      );

      return( this.Tree );
      
    }

    /**************************************************************************/

    public MacroscopeBinaryTreeGenericNode<T> CreateNode (
      string Name,
      T Value
    )
    {

      MacroscopeBinaryTreeGenericNode<T> NewNode;
      
      NewNode = new MacroscopeBinaryTreeGenericNode<T> (
        Name: Name,
        Value: Value
      );

      this.InsertNode( Node: this.Tree, NewNode: NewNode );

      return( NewNode );
      
    }

    /**************************************************************************/
    
    private MacroscopeBinaryTreeGenericNode<T> InsertNode (
      MacroscopeBinaryTreeGenericNode<T> Node,
      MacroscopeBinaryTreeGenericNode<T> NewNode
    )
    {

      // TODO: This is broken
      
      this.DebugMsg( "InsertNode" );
      this.DebugMsg( string.Format( "NewNode: {0}", NewNode.GetNodeName() ) );

      MacroscopeBinaryTreeGenericNode<T> ChildNode;
      
      T NodeValue = Node.GetNodeValue();
      T NewNodeValue = NewNode.GetNodeValue();

      this.DebugMsg( string.Format( "NodeValue/NodeValue: {0} / {1}", NodeValue, NewNodeValue ) );

      MacroscopeBinaryTreeGenericNode<T>.NodeOrientation Orientation;
      
      if( NewNodeValue.CompareTo( NodeValue ) <= 0 )
      {
        Orientation = MacroscopeBinaryTreeGenericNode<T>.NodeOrientation.LEFT;
      }
      else
      {
        Orientation = MacroscopeBinaryTreeGenericNode<T>.NodeOrientation.RIGHT;
      }

      ChildNode = Node.GetChildNode( Orientation: Orientation );

      this.DebugMsg( string.Format( "Orientation: {0}", Orientation ) );

      if( ChildNode == null )
      {
        Node.AddChildNode( Orientation: Orientation, ChildNode: NewNode );
      }
      else
      {
        this.InsertNode( Node: ChildNode, NewNode: NewNode );
      }

      return( NewNode );
      
    }

    /**************************************************************************/

    public MacroscopeBinaryTreeGenericNode<T> SetNodeName (
      MacroscopeBinaryTreeGenericNode<T> Node,
      string Name
    )
    {

      return( Node.SetNodeName( Name: Name ) );
      
    }

    /**************************************************************************/

    public MacroscopeBinaryTreeGenericNode<T> SetNodeValue (
      MacroscopeBinaryTreeGenericNode<T> Node,
      T Value
    )
    {

      return( Node.SetNodeValue( Value: Value ) );

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
