using System;
using System.IO;
using Parsini.Models;

namespace Parsini
{
    public class IniParser
    {
        #region Properties
        public Ini Result { get; private set; }
        #endregion

        #region Constructors
        public IniParser(string filePath)
        {
            this.Result = new Ini(filePath);
        }
        #endregion
    }
}