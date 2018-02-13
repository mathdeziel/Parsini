using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Parsini.Collections;

namespace Parsini.Models
{
    public class Ini
    {
        #region Constants
        private const char COMMENT_DELEMITER = ';';
        private const char SECTION_START_DELIMITER = '[';
        private const char SECTION_END_DELIMITER = ']';
        private const char KEY_VALUE_DELIMITER = '=';
        private const string DEFAULT_SECTION_NAME = "DEFAULT";
        #endregion

        #region Members
        private readonly List<string> lines;
        #endregion

        #region Properties
        public Collection<Section> Sections { get; private set; }
        #endregion

        #region Constructors
        internal Ini(string filePath)
        {
            this.Sections = new Collection<Section>();
            this.lines = File.ReadAllLines(filePath).ToList();

            if (this.lines == null)
                throw new Exception($"An error occured while reading the file {filePath}.");

            ProcessLines();
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            List<string> strings = new List<string>();

            foreach (Section section in this.Sections)
            {
                strings.Add($"[{section.Name}]");
                foreach (Key key in section.Keys)
                    strings.Add($"{key.Name}{KEY_VALUE_DELIMITER}{key.Value}");
            }

            return string.Join("\n", strings);
        }
        #endregion

        #region Private Methods
        private void ProcessLines()
        {
            foreach (string line in this.lines)
            {
                string l = line.Trim();

                // Pass any empty lines and comments
                if (l.Length == 0 || (l.Length > 0 && l[0] == COMMENT_DELEMITER)) continue;

                if (IsSection(line))
                {
                    ProcessSection(line);
                    continue;
                }
                else
                {
                    // Create a default section if there are no sections
                    if (this.Sections.Count == 0) CreateDefaultSection();

                    // Add a key to the last section added
                    this.Sections.Last().Keys.Add(new Key(l.Split(KEY_VALUE_DELIMITER)[0].Trim(), l.Split(KEY_VALUE_DELIMITER)[1].Trim()));
                }
            }
        }

        private bool IsSection(string line)
        {
            bool appearsToBeSection = line[0] == SECTION_START_DELIMITER;
            bool isClean = (line[0] == SECTION_START_DELIMITER && line[line.Length - 1] == SECTION_END_DELIMITER) && !line.Substring(1, line.Length - 2).Contains(SECTION_END_DELIMITER);

            if (appearsToBeSection && !isClean)
                throw new Exception($"The section {line} is not properly formatted.");

            return appearsToBeSection && isClean;
        }

        private void ProcessSection(string line) => this.Sections.Add(new Section(GetSectionName(line)));
        private void CreateDefaultSection() => this.Sections.Add(new Section(DEFAULT_SECTION_NAME));
        private string GetSectionName(string line) => line.Substring(1, line.Length - 2);
        #endregion

    }
}