﻿// ReSharper disable MemberCanBePrivate.Global - This is a library class, consumers may instantiate it

namespace Bender.Core
{
    using System;
    using System.IO;

    /// <summary>
    /// Pretty printer for Benders
    /// </summary>
    public class BenderPrinter
    {
        // '-' means left align
        private static readonly string DefaultRowFormat = "{0,-20} {1,-16}" + Environment.NewLine;
        private static readonly string DefaultHeader = string.Format(DefaultRowFormat, "Element", "Value");
        private static readonly string DefaultLineDelimiter = new string('=', 80);

        /// <summary>
        ///     Create a new Bender pretty printer
        /// </summary>
        public BenderPrinter()
        {
            Header = DefaultHeader;
            RowFormat = DefaultRowFormat;
            LineDelimiter = DefaultLineDelimiter;
        }

        /// <summary>
        /// Gets or Sets the formatted Element table header
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Gets or Sets the raw format. If you change this, consider
        /// also updating Header.
        /// </summary>
        public string RowFormat { get; set; }

        /// <summary>
        /// Long delimiter for splitting textual regions. Default
        /// is a 80 character string of '='
        /// </summary>
        public string LineDelimiter { get; set; }

        /// <summary>
        /// Writes bender to stream in a tabular format. When the write
        /// has completed, the stream position is set back to 0.
        /// </summary>
        /// <param name="bender">Data to write</param>
        /// <param name="stream">Where data is being written to</param>
        /// <exception cref="ArgumentException">Raised is bender or stream are null or if stream cannot be written</exception>
        public void WriteStream(Bender bender, StreamWriter writer)
        {
            if (bender == null)
            {
                throw new ArgumentException("{0} cannot be null", nameof(bender));
            }

            if (writer == null || !writer.BaseStream.CanWrite)
            {
                throw new ArgumentException("{0} cannot be written", nameof(writer));
            }

            void WriteNode(BNode node)
            {
                node?.Print(writer);
            }

            writer.Write(Header);
            writer.Write(LineDelimiter);
            writer.Write(Environment.NewLine);

            bender.Tree.Traverse(WriteNode);

            writer.BaseStream.Position = 0;
        }
    }
}