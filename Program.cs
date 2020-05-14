using System;
using System.IO;
using System.Linq;
using System.Text;

namespace FilePathValidator
{
    class Program
    {
        public static string ReadFile(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (!File.Exists(filePath))
            {
                throw new IOException("El archivo " + fileInfo.Name + " no existe");
            }

            return File.ReadAllText(filePath);
        }

        public static void ToFile(string filePath, string text, Encoding encoding = null)
        {
            File.WriteAllText(filePath, text, encoding != null ? encoding : Encoding.ASCII);
        }

        static void Main(string[] args)
        {
            StringBuilder str = new StringBuilder();

            string filesPath = AppDomain.CurrentDomain.BaseDirectory + "\\" + "archivos.txt";
            string directoriesPath = AppDomain.CurrentDomain.BaseDirectory + "\\" + "directorios.txt";

            string[] files = ReadFile(filesPath).Split('\n');
            if (files.Length > 0)
            {
                ToFile(AppDomain.CurrentDomain.BaseDirectory + "\\" + "archivos_perdidos.txt", string.Join("\n", files.Where(o => !(new FileInfo(o.Trim('\r').Trim('\n').Trim('\t'))).Exists)));
            }

            string[] directories = ReadFile(directoriesPath).Split('\n');
            if (directories.Length > 0)
            {
                ToFile(AppDomain.CurrentDomain.BaseDirectory + "\\" + "directorios_perdidos.txt", string.Join("\n", directories.Where(o => !(new DirectoryInfo(o.Trim('\r').Trim('\n').Trim('\t'))).Exists)));
            }
        }
    }

}