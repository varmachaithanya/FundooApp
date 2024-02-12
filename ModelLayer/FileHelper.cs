using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class FileHelper
    {
        private static readonly string BaseDirectory = @"F:\NoteImages";

        public static string GetFilePath(string filename)
        {
            string directorypath = BaseDirectory;

            if (!Directory.Exists(directorypath))
            {
                Directory.CreateDirectory(directorypath);
            }

            return Path.Combine(directorypath, filename);
        }
    }
}
