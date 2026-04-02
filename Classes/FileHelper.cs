using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace game
{
    public static class FileHelper
    {
        public static string GetPath(string FileName)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileName);
        }
    }
}
