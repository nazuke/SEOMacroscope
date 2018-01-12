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
using NUnit.Framework;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeBinaryTreeGeneric
  {

    /**************************************************************************/

    [Test]
    public void TestCreateTree ()
    {
      
      MacroscopeBinaryTreeGeneric<int> Tree = new MacroscopeBinaryTreeGeneric<int> ();

      Assert.IsNotNull( Tree, "Tree is null" );

    }

    /**************************************************************************/

    [Test]
    public void TestInsertNode ()
    {
      
      MacroscopeBinaryTreeGeneric<int> Tree = new MacroscopeBinaryTreeGeneric<int> ();

      const string Name = "root";
      int Value = new Random ().Next( 1, 666 );
      
      Assert.IsNotNull( Tree, "Tree is null" );

      MacroscopeBinaryTreeGenericNode<int> Node = Tree.SetRootNode( Name, Value );
      
      Assert.IsNotNull( Node, "Root node is null" );

      Assert.AreEqual( Name, Node.GetNodeName(), "Node name does not match" );
      
      Assert.AreEqual( Value, Node.GetNodeValue(), "Node value does not match" );
      
    }
    
    /**************************************************************************/

    [Test]
    public void TestInsertManyNodes ()
    {
      
      MacroscopeBinaryTreeGeneric<int> Tree = new MacroscopeBinaryTreeGeneric<int> ();

      MacroscopeBinaryTreeGenericNode<int> RootNode;
      const string Name = "root";
      int Value = new Random ().Next( 1, 666 );
      
      RootNode = Tree.SetRootNode( Name, Value );

      for( int i = 1 ; i <= 100 ; i++ )
      {

        MacroscopeBinaryTreeGenericNode<int> ChildNode;
        string ChildName = i.ToString();
        int ChildValue = new Random ().Next( 1, 666 );

        ChildNode = Tree.CreateNode( Name: ChildName, Value: Value );

        Assert.IsNotNull( ChildNode, "ChildNode is null" );

      }

    }

    /**************************************************************************/

  }

}
