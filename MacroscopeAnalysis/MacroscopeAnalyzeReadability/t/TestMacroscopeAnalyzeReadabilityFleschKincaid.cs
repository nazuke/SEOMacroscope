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
using System.Collections.Generic;
using NUnit.Framework;

namespace SEOMacroscope
{
  [TestFixture]
  public class TestMacroscopeAnalyzeReadabilityFleschKincaid
  {

    /**************************************************************************/
    
    [Test]
    public void Test01CountWords ()
    {

      Macroscope ms = new Macroscope ();
      MacroscopeAnalyzeReadabilityFleschKincaid AnalyzeReadability = new MacroscopeAnalyzeReadabilityFleschKincaid ();

      List<string> SampleTexts = new List<string> ( 10 );

      SampleTexts.Add( "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce euismod orci eget orci interdum, vitae ultrices mauris tempor. Sed lobortis placerat varius. Nunc sagittis rutrum risus et dictum. Suspendisse augue metus, malesuada et justo nec, rutrum facilisis est. Aliquam id orci sit amet lectus fringilla efficitur. Phasellus malesuada mauris ac dapibus dapibus. Maecenas a lectus lacinia, fermentum nibh in, venenatis eros. Nullam consequat magna nunc. Nunc id leo sed nisi posuere laoreet. In ac dolor turpis. Curabitur ac blandit nunc.  Donec magna purus, pellentesque sit amet semper quis, tincidunt condimentum massa. In placerat hendrerit ipsum. Morbi luctus nunc diam, quis cursus nulla faucibus nec. Etiam laoreet lacinia ligula, nec dictum sem dictum ut. Donec ac sem nibh. Donec sit amet lacus semper, vestibulum ex nec, congue justo. Fusce at mi quis purus interdum congue sed quis elit. Vestibulum tempor mollis velit. Donec interdum in elit in vulputate. Integer tempor sit amet lectus id congue. Donec vel rutrum tortor, ut varius nisl.  Ut et sapien vel arcu gravida tempus. Nullam quis velit et dui fermentum elementum. Quisque aliquet a quam eget laoreet. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ac mauris porttitor, tempor lorem ac, tempus diam. Curabitur ut elit at nibh efficitur interdum ut vel est. Quisque laoreet, felis eget vulputate pretium, purus dolor mollis velit, interdum dignissim justo ex a lectus. Vestibulum luctus bibendum vehicula. Phasellus nisl leo, suscipit a sagittis nec, condimentum a ante. Nam sit amet lectus lectus. Nunc felis velit, placerat vitae diam in, sagittis interdum eros.  Ut diam arcu, porttitor quis ultrices ut, laoreet nec felis. Maecenas ut diam sit amet ante consectetur pretium ut cursus nisi. Cras sit amet mattis massa. Phasellus in est elementum, ultricies odio dignissim, dictum orci. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Mauris tincidunt, dolor eget hendrerit fringilla, turpis mauris finibus nunc, ac posuere nibh sem ut lorem. Vivamus id lorem consequat, elementum enim vel, placerat diam. Morbi id est commodo, tempus augue a, ornare ligula. Mauris pellentesque erat risus, sit amet porta tortor viverra eget. Ut ac risus eu nisi euismod hendrerit eget eu magna. Vestibulum elementum nibh sit amet velit tempus sodales.  Sed non metus nibh. Pellentesque elementum bibendum dui eget rhoncus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Suspendisse potenti. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; In odio elit, pulvinar ac sapien eget, ultricies eleifend ligula. Curabitur sit amet lacus sapien. Mauris mattis sodales venenatis. Quisque in lorem lacinia, lobortis lectus sit amet, pulvinar nisl. Aenean vulputate enim ligula, condimentum fringilla enim sollicitudin sed." );
      SampleTexts.Add( "The domestic cat[1][5] (Latin: Felis catus) is a small, typically furry, carnivorous mammal. They are often called house cats when kept as indoor pets or simply cats when there is no need to distinguish them from other felids and felines.[6] Cats are often valued by humans for companionship and for their ability to hunt vermin. There are more than 70 cat breeds, though different associations proclaim different numbers according to their standards.  Cats are similar in anatomy to the other felids, with a strong flexible body, quick reflexes, sharp retractable claws, and teeth adapted to killing small prey. Cat senses fit a crepuscular and predatory ecological niche. Cats can hear sounds too faint or too high in frequency for human ears, such as those made by mice and other small animals. They can see in near darkness. Like most other mammals, cats have poorer color vision and a better sense of smell than humans. Cats, despite being solitary hunters, are a social species and cat communication includes the use of a variety of vocalizations (mewing, purring, trilling, hissing, growling, and grunting), as well as cat pheromones and types of cat-specific body language.[7]  Cats have a high breeding rate.[8] Under controlled breeding, they can be bred and shown as registered pedigree pets, a hobby known as cat fancy. Failure to control the breeding of pet cats by neutering, as well as the abandonment of former household pets, has resulted in large numbers of feral cats worldwide, requiring population control.[9] In certain areas outside cats' native range, this has contributed, along with habitat destruction and other factors, to the extinction of many bird species. Cats have been known to extirpate a bird species within specific regions and may have contributed to the extinction of isolated island populations.[10] Cats are thought to be primarily responsible for the extinction of 33 species of birds, and the presence of feral and free-ranging cats makes some otherwise suitable locations unsuitable for attempted species reintroduction.[11]  Since cats were venerated in ancient Egypt, they were commonly believed to have been domesticated there,[12] but there may have been instances of domestication as early as the Neolithic from around 9,500 years ago (7,500 BC).[13] A genetic study in 2007[14] concluded that domestic cats are descended from Near Eastern wildcats, having diverged around 8,000 BC in the Middle East.[12][15] A 2016 study found that leopard cats were undergoing domestication independently in China around 5,500 BC, though this line of partially domesticated cats leaves no trace in the domesticated populations of today.[16][17] A 2017 study confirmed that all domestic cats are descendants of those first domesticated by farmers in the Near East around 9,000 years ago.[18][19]  As of a 2007 study, cats are the second most popular pet in the US by number of pets owned, behind freshwater fish.[20] In a 2010 study they were ranked the third most popular pet in the UK, after fish and dogs, with around 8 million being owned.[21]" );
      SampleTexts.Add( "Once upon a time, there lived a strange bird with two heads; one facing the left and the other facing the right. The two heads used to fight and argue with each other, even for very simple reasons. Though they shared the same body, the two heads behaved like rivals!  The strange bird lived in a big banyan tree, along the bank of a river.  One day, while flying over the river, the left head of the bird saw a beautiful tree that had a bright red fruit. The left head of the bird wanted to eat the fruit and the bird flew down to pick the fruit from the tree.  The bird plucked the sweet smelling fruit, and sat by the banks of the river and started eating it. The fruit was eaten by the left head. While it was eating, the right head asked, \"Can you give me a piece to taste?\"  The left head said, \"See, we have only one stomach. So even if I eat in my mouth, it will go only into our stomach.\"  \"But I want to taste the fruit, so you should give me.\"  The left head replied in anger, \"I saw the fruit and hence, I have the right to eat it without sharing with anyone.\"  The right head felt sad and became silent.  A few days later, while the bird was flying over the river again, the right head saw a beautiful pink fruit in a tree. The bird flew down near the tree and tried to pick the fruit and eat it.  The other birds living in the tree said, \"Don't eat it. It is a poisonous fruit. It will kill you.\"  The left head shouted, \"Don't eat it. Don't eat it.\"  However the right head did not listen to the left head. The right head said, \"I will eat it, because I saw it. You have no right to stop me.\"  The left head shouted, \"Please don't eat it. We will all die.\"  The right head said, \"Since I saw it, I have the right to eat it.\" Clearly, the right head was trying to take revenge on the left head for not sharing the red fruit with it earlier.  Finally, the pink fruit was eaten by the right head, and in a few minutes, the strange bird with the two heads dropped down dead!" );

      foreach( string SampleText in SampleTexts )
      {

        double WordCount;

        WordCount = AnalyzeReadability.CountWords( SampleText: SampleText );

        ms.DebugMsg( string.Format( "WordCount: {0}", WordCount ) );

        Assert.GreaterOrEqual( WordCount, 1, "Unexpected WordCount" );
     
      }

    }

    /**************************************************************************/

    [Test]
    public void Test02CountSyllables ()
    {

      Macroscope ms = new Macroscope ();
      MacroscopeAnalyzeReadabilityFleschKincaid AnalyzeReadability = new MacroscopeAnalyzeReadabilityFleschKincaid ();

      List<string> SampleTexts = new List<string> ( 10 );

      SampleTexts.Add( "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce euismod orci eget orci interdum, vitae ultrices mauris tempor. Sed lobortis placerat varius. Nunc sagittis rutrum risus et dictum. Suspendisse augue metus, malesuada et justo nec, rutrum facilisis est. Aliquam id orci sit amet lectus fringilla efficitur. Phasellus malesuada mauris ac dapibus dapibus. Maecenas a lectus lacinia, fermentum nibh in, venenatis eros. Nullam consequat magna nunc. Nunc id leo sed nisi posuere laoreet. In ac dolor turpis. Curabitur ac blandit nunc.  Donec magna purus, pellentesque sit amet semper quis, tincidunt condimentum massa. In placerat hendrerit ipsum. Morbi luctus nunc diam, quis cursus nulla faucibus nec. Etiam laoreet lacinia ligula, nec dictum sem dictum ut. Donec ac sem nibh. Donec sit amet lacus semper, vestibulum ex nec, congue justo. Fusce at mi quis purus interdum congue sed quis elit. Vestibulum tempor mollis velit. Donec interdum in elit in vulputate. Integer tempor sit amet lectus id congue. Donec vel rutrum tortor, ut varius nisl.  Ut et sapien vel arcu gravida tempus. Nullam quis velit et dui fermentum elementum. Quisque aliquet a quam eget laoreet. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ac mauris porttitor, tempor lorem ac, tempus diam. Curabitur ut elit at nibh efficitur interdum ut vel est. Quisque laoreet, felis eget vulputate pretium, purus dolor mollis velit, interdum dignissim justo ex a lectus. Vestibulum luctus bibendum vehicula. Phasellus nisl leo, suscipit a sagittis nec, condimentum a ante. Nam sit amet lectus lectus. Nunc felis velit, placerat vitae diam in, sagittis interdum eros.  Ut diam arcu, porttitor quis ultrices ut, laoreet nec felis. Maecenas ut diam sit amet ante consectetur pretium ut cursus nisi. Cras sit amet mattis massa. Phasellus in est elementum, ultricies odio dignissim, dictum orci. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Mauris tincidunt, dolor eget hendrerit fringilla, turpis mauris finibus nunc, ac posuere nibh sem ut lorem. Vivamus id lorem consequat, elementum enim vel, placerat diam. Morbi id est commodo, tempus augue a, ornare ligula. Mauris pellentesque erat risus, sit amet porta tortor viverra eget. Ut ac risus eu nisi euismod hendrerit eget eu magna. Vestibulum elementum nibh sit amet velit tempus sodales.  Sed non metus nibh. Pellentesque elementum bibendum dui eget rhoncus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Suspendisse potenti. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; In odio elit, pulvinar ac sapien eget, ultricies eleifend ligula. Curabitur sit amet lacus sapien. Mauris mattis sodales venenatis. Quisque in lorem lacinia, lobortis lectus sit amet, pulvinar nisl. Aenean vulputate enim ligula, condimentum fringilla enim sollicitudin sed." );
      SampleTexts.Add( "The domestic cat[1][5] (Latin: Felis catus) is a small, typically furry, carnivorous mammal. They are often called house cats when kept as indoor pets or simply cats when there is no need to distinguish them from other felids and felines.[6] Cats are often valued by humans for companionship and for their ability to hunt vermin. There are more than 70 cat breeds, though different associations proclaim different numbers according to their standards.  Cats are similar in anatomy to the other felids, with a strong flexible body, quick reflexes, sharp retractable claws, and teeth adapted to killing small prey. Cat senses fit a crepuscular and predatory ecological niche. Cats can hear sounds too faint or too high in frequency for human ears, such as those made by mice and other small animals. They can see in near darkness. Like most other mammals, cats have poorer color vision and a better sense of smell than humans. Cats, despite being solitary hunters, are a social species and cat communication includes the use of a variety of vocalizations (mewing, purring, trilling, hissing, growling, and grunting), as well as cat pheromones and types of cat-specific body language.[7]  Cats have a high breeding rate.[8] Under controlled breeding, they can be bred and shown as registered pedigree pets, a hobby known as cat fancy. Failure to control the breeding of pet cats by neutering, as well as the abandonment of former household pets, has resulted in large numbers of feral cats worldwide, requiring population control.[9] In certain areas outside cats' native range, this has contributed, along with habitat destruction and other factors, to the extinction of many bird species. Cats have been known to extirpate a bird species within specific regions and may have contributed to the extinction of isolated island populations.[10] Cats are thought to be primarily responsible for the extinction of 33 species of birds, and the presence of feral and free-ranging cats makes some otherwise suitable locations unsuitable for attempted species reintroduction.[11]  Since cats were venerated in ancient Egypt, they were commonly believed to have been domesticated there,[12] but there may have been instances of domestication as early as the Neolithic from around 9,500 years ago (7,500 BC).[13] A genetic study in 2007[14] concluded that domestic cats are descended from Near Eastern wildcats, having diverged around 8,000 BC in the Middle East.[12][15] A 2016 study found that leopard cats were undergoing domestication independently in China around 5,500 BC, though this line of partially domesticated cats leaves no trace in the domesticated populations of today.[16][17] A 2017 study confirmed that all domestic cats are descendants of those first domesticated by farmers in the Near East around 9,000 years ago.[18][19]  As of a 2007 study, cats are the second most popular pet in the US by number of pets owned, behind freshwater fish.[20] In a 2010 study they were ranked the third most popular pet in the UK, after fish and dogs, with around 8 million being owned.[21]" );
      SampleTexts.Add( "Once upon a time, there lived a strange bird with two heads; one facing the left and the other facing the right. The two heads used to fight and argue with each other, even for very simple reasons. Though they shared the same body, the two heads behaved like rivals!  The strange bird lived in a big banyan tree, along the bank of a river.  One day, while flying over the river, the left head of the bird saw a beautiful tree that had a bright red fruit. The left head of the bird wanted to eat the fruit and the bird flew down to pick the fruit from the tree.  The bird plucked the sweet smelling fruit, and sat by the banks of the river and started eating it. The fruit was eaten by the left head. While it was eating, the right head asked, \"Can you give me a piece to taste?\"  The left head said, \"See, we have only one stomach. So even if I eat in my mouth, it will go only into our stomach.\"  \"But I want to taste the fruit, so you should give me.\"  The left head replied in anger, \"I saw the fruit and hence, I have the right to eat it without sharing with anyone.\"  The right head felt sad and became silent.  A few days later, while the bird was flying over the river again, the right head saw a beautiful pink fruit in a tree. The bird flew down near the tree and tried to pick the fruit and eat it.  The other birds living in the tree said, \"Don't eat it. It is a poisonous fruit. It will kill you.\"  The left head shouted, \"Don't eat it. Don't eat it.\"  However the right head did not listen to the left head. The right head said, \"I will eat it, because I saw it. You have no right to stop me.\"  The left head shouted, \"Please don't eat it. We will all die.\"  The right head said, \"Since I saw it, I have the right to eat it.\" Clearly, the right head was trying to take revenge on the left head for not sharing the red fruit with it earlier.  Finally, the pink fruit was eaten by the right head, and in a few minutes, the strange bird with the two heads dropped down dead!" );

      foreach( string SampleText in SampleTexts )
      {

        double SyllableCount;

        SyllableCount = AnalyzeReadability.CountSyllables( SampleText: SampleText );

        ms.DebugMsg( string.Format( "SyllableCount: {0}", SyllableCount ) );
      
        Assert.GreaterOrEqual( SyllableCount, 1, "Unexpected SyllableCount" );
      
      }

    }

    /**************************************************************************/

    [Test]
    public void Test03CountSentences ()
    {

      Macroscope ms = new Macroscope ();
      MacroscopeAnalyzeReadabilityFleschKincaid AnalyzeReadability = new MacroscopeAnalyzeReadabilityFleschKincaid ();

      List<string> SampleTexts = new List<string> ( 10 );

      SampleTexts.Add( "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce euismod orci eget orci interdum, vitae ultrices mauris tempor. Sed lobortis placerat varius. Nunc sagittis rutrum risus et dictum. Suspendisse augue metus, malesuada et justo nec, rutrum facilisis est. Aliquam id orci sit amet lectus fringilla efficitur. Phasellus malesuada mauris ac dapibus dapibus. Maecenas a lectus lacinia, fermentum nibh in, venenatis eros. Nullam consequat magna nunc. Nunc id leo sed nisi posuere laoreet. In ac dolor turpis. Curabitur ac blandit nunc.  Donec magna purus, pellentesque sit amet semper quis, tincidunt condimentum massa. In placerat hendrerit ipsum. Morbi luctus nunc diam, quis cursus nulla faucibus nec. Etiam laoreet lacinia ligula, nec dictum sem dictum ut. Donec ac sem nibh. Donec sit amet lacus semper, vestibulum ex nec, congue justo. Fusce at mi quis purus interdum congue sed quis elit. Vestibulum tempor mollis velit. Donec interdum in elit in vulputate. Integer tempor sit amet lectus id congue. Donec vel rutrum tortor, ut varius nisl.  Ut et sapien vel arcu gravida tempus. Nullam quis velit et dui fermentum elementum. Quisque aliquet a quam eget laoreet. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ac mauris porttitor, tempor lorem ac, tempus diam. Curabitur ut elit at nibh efficitur interdum ut vel est. Quisque laoreet, felis eget vulputate pretium, purus dolor mollis velit, interdum dignissim justo ex a lectus. Vestibulum luctus bibendum vehicula. Phasellus nisl leo, suscipit a sagittis nec, condimentum a ante. Nam sit amet lectus lectus. Nunc felis velit, placerat vitae diam in, sagittis interdum eros.  Ut diam arcu, porttitor quis ultrices ut, laoreet nec felis. Maecenas ut diam sit amet ante consectetur pretium ut cursus nisi. Cras sit amet mattis massa. Phasellus in est elementum, ultricies odio dignissim, dictum orci. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Mauris tincidunt, dolor eget hendrerit fringilla, turpis mauris finibus nunc, ac posuere nibh sem ut lorem. Vivamus id lorem consequat, elementum enim vel, placerat diam. Morbi id est commodo, tempus augue a, ornare ligula. Mauris pellentesque erat risus, sit amet porta tortor viverra eget. Ut ac risus eu nisi euismod hendrerit eget eu magna. Vestibulum elementum nibh sit amet velit tempus sodales.  Sed non metus nibh. Pellentesque elementum bibendum dui eget rhoncus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Suspendisse potenti. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; In odio elit, pulvinar ac sapien eget, ultricies eleifend ligula. Curabitur sit amet lacus sapien. Mauris mattis sodales venenatis. Quisque in lorem lacinia, lobortis lectus sit amet, pulvinar nisl. Aenean vulputate enim ligula, condimentum fringilla enim sollicitudin sed." );
      SampleTexts.Add( "The domestic cat[1][5] (Latin: Felis catus) is a small, typically furry, carnivorous mammal. They are often called house cats when kept as indoor pets or simply cats when there is no need to distinguish them from other felids and felines.[6] Cats are often valued by humans for companionship and for their ability to hunt vermin. There are more than 70 cat breeds, though different associations proclaim different numbers according to their standards.  Cats are similar in anatomy to the other felids, with a strong flexible body, quick reflexes, sharp retractable claws, and teeth adapted to killing small prey. Cat senses fit a crepuscular and predatory ecological niche. Cats can hear sounds too faint or too high in frequency for human ears, such as those made by mice and other small animals. They can see in near darkness. Like most other mammals, cats have poorer color vision and a better sense of smell than humans. Cats, despite being solitary hunters, are a social species and cat communication includes the use of a variety of vocalizations (mewing, purring, trilling, hissing, growling, and grunting), as well as cat pheromones and types of cat-specific body language.[7]  Cats have a high breeding rate.[8] Under controlled breeding, they can be bred and shown as registered pedigree pets, a hobby known as cat fancy. Failure to control the breeding of pet cats by neutering, as well as the abandonment of former household pets, has resulted in large numbers of feral cats worldwide, requiring population control.[9] In certain areas outside cats' native range, this has contributed, along with habitat destruction and other factors, to the extinction of many bird species. Cats have been known to extirpate a bird species within specific regions and may have contributed to the extinction of isolated island populations.[10] Cats are thought to be primarily responsible for the extinction of 33 species of birds, and the presence of feral and free-ranging cats makes some otherwise suitable locations unsuitable for attempted species reintroduction.[11]  Since cats were venerated in ancient Egypt, they were commonly believed to have been domesticated there,[12] but there may have been instances of domestication as early as the Neolithic from around 9,500 years ago (7,500 BC).[13] A genetic study in 2007[14] concluded that domestic cats are descended from Near Eastern wildcats, having diverged around 8,000 BC in the Middle East.[12][15] A 2016 study found that leopard cats were undergoing domestication independently in China around 5,500 BC, though this line of partially domesticated cats leaves no trace in the domesticated populations of today.[16][17] A 2017 study confirmed that all domestic cats are descendants of those first domesticated by farmers in the Near East around 9,000 years ago.[18][19]  As of a 2007 study, cats are the second most popular pet in the US by number of pets owned, behind freshwater fish.[20] In a 2010 study they were ranked the third most popular pet in the UK, after fish and dogs, with around 8 million being owned.[21]" );
      SampleTexts.Add( "Once upon a time, there lived a strange bird with two heads; one facing the left and the other facing the right. The two heads used to fight and argue with each other, even for very simple reasons. Though they shared the same body, the two heads behaved like rivals!  The strange bird lived in a big banyan tree, along the bank of a river.  One day, while flying over the river, the left head of the bird saw a beautiful tree that had a bright red fruit. The left head of the bird wanted to eat the fruit and the bird flew down to pick the fruit from the tree.  The bird plucked the sweet smelling fruit, and sat by the banks of the river and started eating it. The fruit was eaten by the left head. While it was eating, the right head asked, \"Can you give me a piece to taste?\"  The left head said, \"See, we have only one stomach. So even if I eat in my mouth, it will go only into our stomach.\"  \"But I want to taste the fruit, so you should give me.\"  The left head replied in anger, \"I saw the fruit and hence, I have the right to eat it without sharing with anyone.\"  The right head felt sad and became silent.  A few days later, while the bird was flying over the river again, the right head saw a beautiful pink fruit in a tree. The bird flew down near the tree and tried to pick the fruit and eat it.  The other birds living in the tree said, \"Don't eat it. It is a poisonous fruit. It will kill you.\"  The left head shouted, \"Don't eat it. Don't eat it.\"  However the right head did not listen to the left head. The right head said, \"I will eat it, because I saw it. You have no right to stop me.\"  The left head shouted, \"Please don't eat it. We will all die.\"  The right head said, \"Since I saw it, I have the right to eat it.\" Clearly, the right head was trying to take revenge on the left head for not sharing the red fruit with it earlier.  Finally, the pink fruit was eaten by the right head, and in a few minutes, the strange bird with the two heads dropped down dead!" );

      foreach( string SampleText in SampleTexts )
      {

        double SentenceCount;

        SentenceCount = AnalyzeReadability.CountSentences( SampleText: SampleText );

        ms.DebugMsg( string.Format( "SentenceCount: {0}", SentenceCount ) );

        Assert.GreaterOrEqual( SentenceCount, 1, "Unexpected SentenceCount" );

      }

    }

    /**************************************************************************/

    [Test]
    public void Test04SubtractSilentVowel ()
    {

      Macroscope ms = new Macroscope ();
      MacroscopeAnalyzeReadabilityFleschKincaid AnalyzeReadability = new MacroscopeAnalyzeReadabilityFleschKincaid ();

      Dictionary<string,string> SampleTexts = new Dictionary<string, string> ();

      SampleTexts.Add( "Ride", "rid" );
      SampleTexts.Add( "Abide", "abid" );
      SampleTexts.Add( "Hello", "hello" );
      SampleTexts.Add( "Release", "releas" );
      SampleTexts.Add( "Gonzo", "gonzo" );

      foreach( String Word in SampleTexts.Keys )
      {

        string Subtracted = AnalyzeReadability.SubtractSilentVowel( WordText: Word.ToLower() );

        ms.DebugMsg( string.Format( "Subtracted: {0} => {1}", Word, Subtracted ) );

        Assert.AreEqual( SampleTexts[ Word ], Subtracted, string.Format( "Unexpected subtracted vowel: {0}", Subtracted ) );

      }

    }

    /**************************************************************************/

    [Test]
    public void Test05FlattenDipthongs ()
    {

      Macroscope ms = new Macroscope ();
      MacroscopeAnalyzeReadabilityFleschKincaid AnalyzeReadability = new MacroscopeAnalyzeReadabilityFleschKincaid ();

      Dictionary<string,string> SampleTexts = new Dictionary<string, string> ();

      SampleTexts.Add( "Aaron", "aron" );
      SampleTexts.Add( "Aardvark", "ardvark" );
      SampleTexts.Add( "Oolite", "olite" );
      SampleTexts.Add( "Aaniipuubeedoo", "anipubedo" );
      SampleTexts.Add( "Zaaniipuubeedooxx", "zanipubedoxx" );
      SampleTexts.Add( "Quick", "quck" );

      foreach( String Word in SampleTexts.Keys )
      {

        string Flattened = AnalyzeReadability.FlattenDipthongs( WordText: Word.ToLower() );

        ms.DebugMsg( string.Format( "Flattened: {0} => {1}", Word, Flattened ) );

        Assert.AreEqual( SampleTexts[ Word ], Flattened, "Unexpected flattened dipthong" );

      }

    }

    /**************************************************************************/

    [Test]
    public void Test06CountVowels ()
    {

      Macroscope ms = new Macroscope ();
      MacroscopeAnalyzeReadabilityFleschKincaid AnalyzeReadability = new MacroscopeAnalyzeReadabilityFleschKincaid ();

      Dictionary<string,int> SampleTexts = new Dictionary<string, int> ();

      SampleTexts.Add( "Aaron", 2 );
      SampleTexts.Add( "Aardvark", 2 );
      SampleTexts.Add( "Oolite", 2 );
      SampleTexts.Add( "Aaniipuubeedoo", 5 );
      SampleTexts.Add( "Zaaniipuubeedooxx", 5 );
      SampleTexts.Add( "Yahoo", 2 );
      SampleTexts.Add( "Gym", 1 );
      SampleTexts.Add( "Yesterday", 3 );

      foreach( String Word in SampleTexts.Keys )
      {

        string Flattened = AnalyzeReadability.FlattenDipthongs( WordText: Word.ToLower() );
        ms.DebugMsg( string.Format( "Flattened: {0}", Flattened ) );
        
        string Subtracted = AnalyzeReadability.SubtractSilentVowel( WordText: Word.ToLower() );
        ms.DebugMsg( string.Format( "Subtracted: {0}", Subtracted ) );
                        
        double VowelCount = AnalyzeReadability.CountVowels( WordText: Subtracted );

        ms.DebugMsg( string.Format( "VowelCount: {0} => {1}", Word, VowelCount ) );

        Assert.AreEqual( SampleTexts[ Word ], VowelCount, string.Format( "Unexpected vowel count: {0}", Word ) );

      }

    }

    /**************************************************************************/

    [Test]
    public void Test07CountSyllables ()
    {

      Macroscope ms = new Macroscope ();
      MacroscopeAnalyzeReadabilityFleschKincaid AnalyzeReadability = new MacroscopeAnalyzeReadabilityFleschKincaid ();

      string SampleText = "The quick brown fox jumps over the lazy dog.";

      double Syllables = AnalyzeReadability.CountSyllables( SampleText: SampleText );

      ms.DebugMsg( string.Format( "Syllables: {0}", Syllables ) );

      Assert.AreEqual( 11, Syllables, string.Format( "Unexpected syllable count: \"{0}\"", SampleText ) );

    }

    /**************************************************************************/

    [Test]
    public void Test10AnalyzeReadability ()
    {

      Macroscope ms = new Macroscope ();
      MacroscopeAnalyzeReadabilityFleschKincaid AnalyzeReadability = new MacroscopeAnalyzeReadabilityFleschKincaid ();

      List<string> SampleTexts = new List<string> ( 10 );

      SampleTexts.Add( "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce euismod orci eget orci interdum, vitae ultrices mauris tempor. Sed lobortis placerat varius. Nunc sagittis rutrum risus et dictum. Suspendisse augue metus, malesuada et justo nec, rutrum facilisis est. Aliquam id orci sit amet lectus fringilla efficitur. Phasellus malesuada mauris ac dapibus dapibus. Maecenas a lectus lacinia, fermentum nibh in, venenatis eros. Nullam consequat magna nunc. Nunc id leo sed nisi posuere laoreet. In ac dolor turpis. Curabitur ac blandit nunc.  Donec magna purus, pellentesque sit amet semper quis, tincidunt condimentum massa. In placerat hendrerit ipsum. Morbi luctus nunc diam, quis cursus nulla faucibus nec. Etiam laoreet lacinia ligula, nec dictum sem dictum ut. Donec ac sem nibh. Donec sit amet lacus semper, vestibulum ex nec, congue justo. Fusce at mi quis purus interdum congue sed quis elit. Vestibulum tempor mollis velit. Donec interdum in elit in vulputate. Integer tempor sit amet lectus id congue. Donec vel rutrum tortor, ut varius nisl.  Ut et sapien vel arcu gravida tempus. Nullam quis velit et dui fermentum elementum. Quisque aliquet a quam eget laoreet. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ac mauris porttitor, tempor lorem ac, tempus diam. Curabitur ut elit at nibh efficitur interdum ut vel est. Quisque laoreet, felis eget vulputate pretium, purus dolor mollis velit, interdum dignissim justo ex a lectus. Vestibulum luctus bibendum vehicula. Phasellus nisl leo, suscipit a sagittis nec, condimentum a ante. Nam sit amet lectus lectus. Nunc felis velit, placerat vitae diam in, sagittis interdum eros.  Ut diam arcu, porttitor quis ultrices ut, laoreet nec felis. Maecenas ut diam sit amet ante consectetur pretium ut cursus nisi. Cras sit amet mattis massa. Phasellus in est elementum, ultricies odio dignissim, dictum orci. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Mauris tincidunt, dolor eget hendrerit fringilla, turpis mauris finibus nunc, ac posuere nibh sem ut lorem. Vivamus id lorem consequat, elementum enim vel, placerat diam. Morbi id est commodo, tempus augue a, ornare ligula. Mauris pellentesque erat risus, sit amet porta tortor viverra eget. Ut ac risus eu nisi euismod hendrerit eget eu magna. Vestibulum elementum nibh sit amet velit tempus sodales.  Sed non metus nibh. Pellentesque elementum bibendum dui eget rhoncus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Suspendisse potenti. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; In odio elit, pulvinar ac sapien eget, ultricies eleifend ligula. Curabitur sit amet lacus sapien. Mauris mattis sodales venenatis. Quisque in lorem lacinia, lobortis lectus sit amet, pulvinar nisl. Aenean vulputate enim ligula, condimentum fringilla enim sollicitudin sed." );
      SampleTexts.Add( "The domestic cat[1][5] (Latin: Felis catus) is a small, typically furry, carnivorous mammal. They are often called house cats when kept as indoor pets or simply cats when there is no need to distinguish them from other felids and felines.[6] Cats are often valued by humans for companionship and for their ability to hunt vermin. There are more than 70 cat breeds, though different associations proclaim different numbers according to their standards.  Cats are similar in anatomy to the other felids, with a strong flexible body, quick reflexes, sharp retractable claws, and teeth adapted to killing small prey. Cat senses fit a crepuscular and predatory ecological niche. Cats can hear sounds too faint or too high in frequency for human ears, such as those made by mice and other small animals. They can see in near darkness. Like most other mammals, cats have poorer color vision and a better sense of smell than humans. Cats, despite being solitary hunters, are a social species and cat communication includes the use of a variety of vocalizations (mewing, purring, trilling, hissing, growling, and grunting), as well as cat pheromones and types of cat-specific body language.[7]  Cats have a high breeding rate.[8] Under controlled breeding, they can be bred and shown as registered pedigree pets, a hobby known as cat fancy. Failure to control the breeding of pet cats by neutering, as well as the abandonment of former household pets, has resulted in large numbers of feral cats worldwide, requiring population control.[9] In certain areas outside cats' native range, this has contributed, along with habitat destruction and other factors, to the extinction of many bird species. Cats have been known to extirpate a bird species within specific regions and may have contributed to the extinction of isolated island populations.[10] Cats are thought to be primarily responsible for the extinction of 33 species of birds, and the presence of feral and free-ranging cats makes some otherwise suitable locations unsuitable for attempted species reintroduction.[11]  Since cats were venerated in ancient Egypt, they were commonly believed to have been domesticated there,[12] but there may have been instances of domestication as early as the Neolithic from around 9,500 years ago (7,500 BC).[13] A genetic study in 2007[14] concluded that domestic cats are descended from Near Eastern wildcats, having diverged around 8,000 BC in the Middle East.[12][15] A 2016 study found that leopard cats were undergoing domestication independently in China around 5,500 BC, though this line of partially domesticated cats leaves no trace in the domesticated populations of today.[16][17] A 2017 study confirmed that all domestic cats are descendants of those first domesticated by farmers in the Near East around 9,000 years ago.[18][19]  As of a 2007 study, cats are the second most popular pet in the US by number of pets owned, behind freshwater fish.[20] In a 2010 study they were ranked the third most popular pet in the UK, after fish and dogs, with around 8 million being owned.[21]" );
      SampleTexts.Add( "Once upon a time, there lived a strange bird with two heads; one facing the left and the other facing the right. The two heads used to fight and argue with each other, even for very simple reasons. Though they shared the same body, the two heads behaved like rivals!  The strange bird lived in a big banyan tree, along the bank of a river.  One day, while flying over the river, the left head of the bird saw a beautiful tree that had a bright red fruit. The left head of the bird wanted to eat the fruit and the bird flew down to pick the fruit from the tree.  The bird plucked the sweet smelling fruit, and sat by the banks of the river and started eating it. The fruit was eaten by the left head. While it was eating, the right head asked, \"Can you give me a piece to taste?\"  The left head said, \"See, we have only one stomach. So even if I eat in my mouth, it will go only into our stomach.\"  \"But I want to taste the fruit, so you should give me.\"  The left head replied in anger, \"I saw the fruit and hence, I have the right to eat it without sharing with anyone.\"  The right head felt sad and became silent.  A few days later, while the bird was flying over the river again, the right head saw a beautiful pink fruit in a tree. The bird flew down near the tree and tried to pick the fruit and eat it.  The other birds living in the tree said, \"Don't eat it. It is a poisonous fruit. It will kill you.\"  The left head shouted, \"Don't eat it. Don't eat it.\"  However the right head did not listen to the left head. The right head said, \"I will eat it, because I saw it. You have no right to stop me.\"  The left head shouted, \"Please don't eat it. We will all die.\"  The right head said, \"Since I saw it, I have the right to eat it.\" Clearly, the right head was trying to take revenge on the left head for not sharing the red fruit with it earlier.  Finally, the pink fruit was eaten by the right head, and in a few minutes, the strange bird with the two heads dropped down dead!" );

      foreach( string SampleText in SampleTexts )
      {

        double Grade;

        Grade = AnalyzeReadability.AnalyzeReadability( SampleText: SampleText );

        ms.DebugMsg( string.Format( "Grade: {0}", Grade ) );

      }

    }

    /**************************************************************************/

    [Test]
    public void Test11GradeToString ()
    {

      Macroscope ms = new Macroscope ();
      MacroscopeAnalyzeReadabilityFleschKincaid AnalyzeReadability = new MacroscopeAnalyzeReadabilityFleschKincaid ();

      List<string> SampleTexts = new List<string> ( 10 );

      SampleTexts.Add( "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce euismod orci eget orci interdum, vitae ultrices mauris tempor. Sed lobortis placerat varius. Nunc sagittis rutrum risus et dictum. Suspendisse augue metus, malesuada et justo nec, rutrum facilisis est. Aliquam id orci sit amet lectus fringilla efficitur. Phasellus malesuada mauris ac dapibus dapibus. Maecenas a lectus lacinia, fermentum nibh in, venenatis eros. Nullam consequat magna nunc. Nunc id leo sed nisi posuere laoreet. In ac dolor turpis. Curabitur ac blandit nunc.  Donec magna purus, pellentesque sit amet semper quis, tincidunt condimentum massa. In placerat hendrerit ipsum. Morbi luctus nunc diam, quis cursus nulla faucibus nec. Etiam laoreet lacinia ligula, nec dictum sem dictum ut. Donec ac sem nibh. Donec sit amet lacus semper, vestibulum ex nec, congue justo. Fusce at mi quis purus interdum congue sed quis elit. Vestibulum tempor mollis velit. Donec interdum in elit in vulputate. Integer tempor sit amet lectus id congue. Donec vel rutrum tortor, ut varius nisl.  Ut et sapien vel arcu gravida tempus. Nullam quis velit et dui fermentum elementum. Quisque aliquet a quam eget laoreet. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ac mauris porttitor, tempor lorem ac, tempus diam. Curabitur ut elit at nibh efficitur interdum ut vel est. Quisque laoreet, felis eget vulputate pretium, purus dolor mollis velit, interdum dignissim justo ex a lectus. Vestibulum luctus bibendum vehicula. Phasellus nisl leo, suscipit a sagittis nec, condimentum a ante. Nam sit amet lectus lectus. Nunc felis velit, placerat vitae diam in, sagittis interdum eros.  Ut diam arcu, porttitor quis ultrices ut, laoreet nec felis. Maecenas ut diam sit amet ante consectetur pretium ut cursus nisi. Cras sit amet mattis massa. Phasellus in est elementum, ultricies odio dignissim, dictum orci. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Mauris tincidunt, dolor eget hendrerit fringilla, turpis mauris finibus nunc, ac posuere nibh sem ut lorem. Vivamus id lorem consequat, elementum enim vel, placerat diam. Morbi id est commodo, tempus augue a, ornare ligula. Mauris pellentesque erat risus, sit amet porta tortor viverra eget. Ut ac risus eu nisi euismod hendrerit eget eu magna. Vestibulum elementum nibh sit amet velit tempus sodales.  Sed non metus nibh. Pellentesque elementum bibendum dui eget rhoncus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Suspendisse potenti. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; In odio elit, pulvinar ac sapien eget, ultricies eleifend ligula. Curabitur sit amet lacus sapien. Mauris mattis sodales venenatis. Quisque in lorem lacinia, lobortis lectus sit amet, pulvinar nisl. Aenean vulputate enim ligula, condimentum fringilla enim sollicitudin sed." );
      SampleTexts.Add( "The domestic cat[1][5] (Latin: Felis catus) is a small, typically furry, carnivorous mammal. They are often called house cats when kept as indoor pets or simply cats when there is no need to distinguish them from other felids and felines.[6] Cats are often valued by humans for companionship and for their ability to hunt vermin. There are more than 70 cat breeds, though different associations proclaim different numbers according to their standards.  Cats are similar in anatomy to the other felids, with a strong flexible body, quick reflexes, sharp retractable claws, and teeth adapted to killing small prey. Cat senses fit a crepuscular and predatory ecological niche. Cats can hear sounds too faint or too high in frequency for human ears, such as those made by mice and other small animals. They can see in near darkness. Like most other mammals, cats have poorer color vision and a better sense of smell than humans. Cats, despite being solitary hunters, are a social species and cat communication includes the use of a variety of vocalizations (mewing, purring, trilling, hissing, growling, and grunting), as well as cat pheromones and types of cat-specific body language.[7]  Cats have a high breeding rate.[8] Under controlled breeding, they can be bred and shown as registered pedigree pets, a hobby known as cat fancy. Failure to control the breeding of pet cats by neutering, as well as the abandonment of former household pets, has resulted in large numbers of feral cats worldwide, requiring population control.[9] In certain areas outside cats' native range, this has contributed, along with habitat destruction and other factors, to the extinction of many bird species. Cats have been known to extirpate a bird species within specific regions and may have contributed to the extinction of isolated island populations.[10] Cats are thought to be primarily responsible for the extinction of 33 species of birds, and the presence of feral and free-ranging cats makes some otherwise suitable locations unsuitable for attempted species reintroduction.[11]  Since cats were venerated in ancient Egypt, they were commonly believed to have been domesticated there,[12] but there may have been instances of domestication as early as the Neolithic from around 9,500 years ago (7,500 BC).[13] A genetic study in 2007[14] concluded that domestic cats are descended from Near Eastern wildcats, having diverged around 8,000 BC in the Middle East.[12][15] A 2016 study found that leopard cats were undergoing domestication independently in China around 5,500 BC, though this line of partially domesticated cats leaves no trace in the domesticated populations of today.[16][17] A 2017 study confirmed that all domestic cats are descendants of those first domesticated by farmers in the Near East around 9,000 years ago.[18][19]  As of a 2007 study, cats are the second most popular pet in the US by number of pets owned, behind freshwater fish.[20] In a 2010 study they were ranked the third most popular pet in the UK, after fish and dogs, with around 8 million being owned.[21]" );
      SampleTexts.Add( "Once upon a time, there lived a strange bird with two heads; one facing the left and the other facing the right. The two heads used to fight and argue with each other, even for very simple reasons. Though they shared the same body, the two heads behaved like rivals!  The strange bird lived in a big banyan tree, along the bank of a river.  One day, while flying over the river, the left head of the bird saw a beautiful tree that had a bright red fruit. The left head of the bird wanted to eat the fruit and the bird flew down to pick the fruit from the tree.  The bird plucked the sweet smelling fruit, and sat by the banks of the river and started eating it. The fruit was eaten by the left head. While it was eating, the right head asked, \"Can you give me a piece to taste?\"  The left head said, \"See, we have only one stomach. So even if I eat in my mouth, it will go only into our stomach.\"  \"But I want to taste the fruit, so you should give me.\"  The left head replied in anger, \"I saw the fruit and hence, I have the right to eat it without sharing with anyone.\"  The right head felt sad and became silent.  A few days later, while the bird was flying over the river again, the right head saw a beautiful pink fruit in a tree. The bird flew down near the tree and tried to pick the fruit and eat it.  The other birds living in the tree said, \"Don't eat it. It is a poisonous fruit. It will kill you.\"  The left head shouted, \"Don't eat it. Don't eat it.\"  However the right head did not listen to the left head. The right head said, \"I will eat it, because I saw it. You have no right to stop me.\"  The left head shouted, \"Please don't eat it. We will all die.\"  The right head said, \"Since I saw it, I have the right to eat it.\" Clearly, the right head was trying to take revenge on the left head for not sharing the red fruit with it earlier.  Finally, the pink fruit was eaten by the right head, and in a few minutes, the strange bird with the two heads dropped down dead!" );

      foreach( string SampleText in SampleTexts )
      {

        double Grade;
        string GradeString;

        Grade = AnalyzeReadability.AnalyzeReadability( SampleText: SampleText );
        GradeString = AnalyzeReadability.GradeToString();

        ms.DebugMsg( string.Format( "Graded: {0} => \"{1}\"", Grade, GradeString ) );
     
      }

    }

    /**************************************************************************/

  }

}
