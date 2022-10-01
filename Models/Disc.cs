using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FakeDeleter.Models
{
    internal class Disc
    {
        public static IEnumerable<string> GetDirs(string path)
        {
            var dir = new DirectoryInfo(path);

            return dir.EnumerateDirectories().Select(d => d.FullName);
        }

        public static Task<IEnumerable<string>> GetDirsAsync(string path) => Task.Run(() => GetDirs(path));

        public static IEnumerable<string> GetFiles(string path)
        {
            var dir = new DirectoryInfo(path);

            return dir.EnumerateFiles().Select(f => f.Name);
        }

        public static Task<IEnumerable<string>> GetFilesAsync(string path) => Task.Run(() => GetFiles(path));

        public static int TotalFilesCount(string path)
        {
            var dir = new DirectoryInfo(path);

            return dir.EnumerateFiles("*", SearchOption.AllDirectories).Count();
        }

        public static Task<int> TotalFilesCountAsync(string path) => Task.Run(() => TotalFilesCount(path));
    }
}

//var videos = $"{Environment.ExpandEnvironmentVariables("%USERPROFILE%")}\\Videos";
