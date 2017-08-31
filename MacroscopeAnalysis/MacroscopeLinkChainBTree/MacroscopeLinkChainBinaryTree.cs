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
  /// Description of MacroscopeLinkChainBinaryTree.
  /// </summary>

  public class MacroscopeLinkChainBinaryTree<T> where T : IComparable<T>
  {

    /**************************************************************************/

    private MacroscopeLinkChainBinaryTreeNode<T> Tree;

    /**************************************************************************/

    public MacroscopeLinkChainBinaryTree ()
    {

      this.Tree = null;

    }

    /**************************************************************************/

    public MacroscopeLinkChainBinaryTreeNode<T> SetRootNode (
      string Name,
      T Value
    )
    {

      this.Tree = new MacroscopeLinkChainBinaryTreeNode<T> (
        Name: Name,
        Value: Value
      );

      return( this.Tree );
      
    }

    /**************************************************************************/

    public MacroscopeLinkChainBinaryTreeNode<T> CreateNode (
      string Name,
      T Value
    )
    {

      MacroscopeLinkChainBinaryTreeNode<T> NewNode;
      
      NewNode = new MacroscopeLinkChainBinaryTreeNode<T> (
        Name: Name,
        Value: Value
      );

      this.InsertNode( Node: this.Tree, NewNode: NewNode );

      return( NewNode );
      
    }

    /**************************************************************************/
    
    private MacroscopeLinkChainBinaryTreeNode<T> InsertNode (
      MacroscopeLinkChainBinaryTreeNode<T> Node,
      MacroscopeLinkChainBinaryTreeNode<T> NewNode
    )
    {

      // TODO: this is broken
      
      this.DebugMsg( "InsertNode" );
      this.DebugMsg( string.Format( "NewNode: {0}", NewNode.GetNodeName() ) );

      MacroscopeLinkChainBinaryTreeNode<T> ChildNode;
      
      T NodeValue = Node.GetNodeValue();
      T NewNodeValue = NewNode.GetNodeValue();

      this.DebugMsg( string.Format( "NodeValue/NodeValue: {0} / {1}", NodeValue, NewNodeValue ) );

      MacroscopeLinkChainBinaryTreeNode<T>.NodeOrientation Orientation;
      
      if( NewNodeValue.CompareTo( NodeValue ) <= 0 )
      {
        Orientation = MacroscopeLinkChainBinaryTreeNode<T>.NodeOrientation.LEFT;
      }
      else
      {
        Orientation = MacroscopeLinkChainBinaryTreeNode<T>.NodeOrientation.RIGHT;
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

    public MacroscopeLinkChainBinaryTreeNode<T> SetNodeName (
      MacroscopeLinkChainBinaryTreeNode<T> Node,
      string Name
    )
    {

      return( Node.SetNodeName( Name: Name ) );
      
    }

    /**************************************************************************/

    public MacroscopeLinkChainBinaryTreeNode<T> SetNodeValue (
      MacroscopeLinkChainBinaryTreeNode<T> Node,
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
