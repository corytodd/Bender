﻿using Xunit;

namespace BenderLib.Tests
{
    public class ElementTests
    {
        [Fact]
        public void TestClone()
        {
            var src = new Element
            {
                Name = "Test",
                Width = 8,
                Format = ElementFormat.ASCII,
                Matrix = string.Empty,
                LittleEndian = true,
                IsSigned = true,
                Elide = false,
                IsReadOnly = true,
            };

            var clone = src.Clone();

            Assert.Equal(src.Name, clone.Name);
            Assert.Equal(src.Width, clone.Width);
            Assert.Equal(src.Format, clone.Format);
            Assert.Equal(src.Matrix, clone.Matrix);
            Assert.Equal(src.LittleEndian, clone.LittleEndian);
            Assert.Equal(src.IsSigned, clone.IsSigned);
            Assert.Equal(src.Elide, clone.Elide);
            Assert.Equal(src.IsReadOnly, clone.IsReadOnly);
        }

        [Fact]
        public void TestToString()
        {
            var src = new Element
            {
                Name = "Test",
                Width = 8,
                Format = ElementFormat.ASCII,
                Matrix = string.Empty,
                LittleEndian = true,
                IsSigned = true,
                Elide = false,
                IsReadOnly = true,
            };

            var actual = src.ToString();
            Assert.Contains(src.Name, actual);
            Assert.Contains(src.Width.ToString(), actual);
            Assert.Contains(src.Format.ToString(), actual);
            Assert.Contains(src.LittleEndian.ToString(), actual);
            Assert.Contains(src.IsSigned.ToString(), actual);
            Assert.Contains(src.Elide.ToString(), actual);
            Assert.Contains(src.IsReadOnly.ToString(), actual);
        }

        [Fact]
        public void TestEnumerateLayout()
        {
            var src = new Element
            {
                Name = "Test",
                Width = 8,
                Format = ElementFormat.ASCII,
                Matrix = string.Empty,
                LittleEndian = true,
                IsSigned = true,
                Elide = false,
                IsReadOnly = true,
            };

            var actual = string.Join(",", src.EnumerateLayout());
            Assert.Contains(src.Name, actual);
            Assert.Contains(src.Width.ToString(), actual);
            Assert.Contains(src.Format.ToString(), actual);
            Assert.Contains(src.LittleEndian.ToString(), actual);
            Assert.Contains(src.IsSigned.ToString(), actual);
            Assert.Contains(src.Elide.ToString(), actual);
            Assert.Contains(src.IsReadOnly.ToString(), actual);
        }
    }
}