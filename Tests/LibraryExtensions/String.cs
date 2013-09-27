using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;
using DSE.Tests.Symptomatic;

using DSE.Extensions;

namespace DSE.Tests.LibraryExtensions
{
    public class _String : Symptomatic.SimpleTestCase
    {
        [Test]
        public void DoTestTitleCase()
        {
            Assert.AreEqual((string)null, ((string)null).ToTitleCase());
            Assert.AreEqual(String.Empty, String.Empty.ToTitleCase());
            Assert.AreEqual("Hello", "hello".ToTitleCase());
            Assert.AreEqual("Hello", "HELLO".ToTitleCase());
            Assert.AreEqual("Hello Dear", "HELLO DEAR".ToTitleCase());
        }

        [Test]
        public void DoTestToFirstLetterUpperCase()
        {
            Assert.AreEqual((string)null, ((string)null).ToFirstLetterUpperCase());
            Assert.AreEqual(String.Empty, String.Empty.ToFirstLetterUpperCase());
            Assert.AreEqual("Hello", "hello".ToFirstLetterUpperCase());
            Assert.AreEqual("HELLO", "HELLO".ToFirstLetterUpperCase());
            Assert.AreEqual("HELLO DEAR", "HELLO DEAR".ToFirstLetterUpperCase());
        }

        [Test]
        public void DoTestGetFirstLetter()
        {
            Assert.AreEqual((string)null, ((string)null).GetFirstLetter());
            Assert.AreEqual(String.Empty, String.Empty.GetFirstLetter());
            Assert.AreEqual("h", "hello".GetFirstLetter());
            Assert.AreEqual("H", "HELLO".GetFirstLetter());
        }

        [Test]
        public void DoTestGetFirstLetterAndAppendIfNotEmpty()
        {
            Assert.AreEqual((string)null, ((string)null).GetFirstLetterSuffixedIfNotEmpty("."));
            Assert.AreEqual(String.Empty, String.Empty.GetFirstLetterSuffixedIfNotEmpty("."));
            Assert.AreEqual("h.", "hello".GetFirstLetterSuffixedIfNotEmpty("."));
            Assert.AreEqual("H.", "HELLO".GetFirstLetterSuffixedIfNotEmpty("."));
            Assert.AreEqual("H.123", "HELLO".GetFirstLetterSuffixedIfNotEmpty(".123"));
        }

        [Test]
        public void DoTestStripHTMLTags()
        {
            Assert.AreEqual((string)null, ((string)null).StripHTMLTags());
            Assert.AreEqual(String.Empty, String.Empty.StripHTMLTags());
            Assert.AreEqual("hello", "hello".StripHTMLTags());
            Assert.AreEqual("hello", "<p>hello</p>".StripHTMLTags());
            Assert.AreEqual("hellokitty", "hello<p>kitty</p>".StripHTMLTags());
        }

        [Test]
        public void DoTestJoin()
        {
            Assert.Catch<NullReferenceException>(() =>
            {
                ((String[])null).Join("");
            });

            Assert.AreEqual("12", new String[] { "1", "2" }.Join(null));
            Assert.AreEqual("12", new String[] { "1", "2" }.Join(""));
            Assert.AreEqual("1_.,2", new String[] { "1", "2" }.Join("_.,"));
            Assert.AreEqual("1_.,_.,3", new String[] { "1", null, "3" }.Join("_.,"));
        }

        [Test]
        public void DoTestJoinIf()
        {
            Assert.Catch<NullReferenceException>(() =>
            {
                ((String[])null).JoinIf(_ => true, "");
            });

            Assert.AreEqual("12", new String[] { "1", "2" }.JoinIf(_ => true, null));
            Assert.AreEqual("12", new String[] { "1", "2" }.JoinIf(_ => true, ""));
            Assert.AreEqual("1_.,2", new String[] { "1", "2" }.JoinIf(_=>true, "_.,"));
            Assert.AreEqual("", new String[] { "1", "2" }.JoinIf(_ => false, "_.,"));
            Assert.AreEqual("1_.,3", new String[] { "1", null, "3" }.JoinIf(_=>!String.IsNullOrEmpty(_), "_.,"));
        }

        [Test]
        public void DoTestJoinNotNullOrEmpty()
        {
            Assert.Catch<NullReferenceException>(() =>
            {
                ((String[])null).JoinNotNullOrEmpty("");
            });

            Assert.AreEqual("12", new String[] { "1", "2" }.JoinNotNullOrEmpty(null));
            Assert.AreEqual("12", new String[] { "1", "2" }.JoinNotNullOrEmpty(""));
            Assert.AreEqual("", new String[] { null, null }.JoinNotNullOrEmpty("_.,"));
            Assert.AreEqual("1_.,2", new String[] { "1", "2" }.JoinNotNullOrEmpty("_.,"));
            Assert.AreEqual("1_.,3", new String[] { "1", null, "3" }.JoinNotNullOrEmpty("_.,"));
        }
    }
}