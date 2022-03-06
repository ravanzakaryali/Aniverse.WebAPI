using System.IO;
using System.Linq;

namespace Aniverse.Business.Helpers
{
    public class Utilities
    {
        public void FileDelete(string root, string fileName, params string[] folders)
        {
            string rootInPath = folders.Aggregate((result, folder) => Path.Combine(result, folder));
            string path = Path.Combine(root, rootInPath, fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
