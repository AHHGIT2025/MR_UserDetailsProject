using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
 

namespace User_Registration
{

    public static class FileStorage
    {
        // Robust: climb up until we hit the project folder name OR a .csproj
        public static string GetProjectRoot(string projectFolderName = null)
        {
            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            while (dir != null)
            {
                if (!string.IsNullOrWhiteSpace(projectFolderName) &&
                    dir.Name.Equals(projectFolderName, StringComparison.OrdinalIgnoreCase))
                {
                    return dir.FullName;
                }

                try
                {
                    if (dir.GetFiles("*.csproj").Any())
                        return dir.FullName;
                }
                catch
                {
                    // ignore and continue upward
                }

                dir = dir.Parent;
            }

            // Fallback: base directory (bin\Debug\...\)
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public static string GetRegistrationsFile(string projectFolderName)
        {
            string projectRoot = GetProjectRoot(projectFolderName);
            string folder = Path.Combine(projectRoot, "SavedData");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            return Path.Combine(folder, "registrations.txt");
        }
    }

}
