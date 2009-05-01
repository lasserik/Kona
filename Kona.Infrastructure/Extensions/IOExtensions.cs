using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Kona.Infrastructure {
    public static class IOExtensions {

        public static string LocateFilePath(this string fileName, string directoryRoot) {
            string result = "";
            //make sure the directory exists
            if (!Directory.Exists(directoryRoot)) {
                throw new InvalidOperationException("There is no directory " + directoryRoot);
            }

            //see if there's a file in the root
            foreach (string file in Directory.GetFiles(directoryRoot)) {
                if (Path.GetFileNameWithoutExtension(file) == fileName || Path.GetFileName(file) == fileName) {
                    result = file;
                    break;
                }
            }

            //if not, walk the subs recursively
            if (string.IsNullOrEmpty(result)) {
                foreach (string directory in Directory.GetDirectories(directoryRoot)) {
                    string appDir= Path.Combine(directoryRoot, directory);
                    result=fileName.LocateFilePath(appDir);
                    if (!String.IsNullOrEmpty(result))
                        break;
                }
            }
            return result;
        }

        public static string GetFileText(this string filePath) {
            using (StreamReader sr = new StreamReader(filePath))
                return sr.ReadToEnd();
        }
        public static void PutFileText(this string fileText, string filePath) {
            using (StreamWriter sw = File.CreateText(filePath))
                sw.Write(fileText);
        }
    }



}
