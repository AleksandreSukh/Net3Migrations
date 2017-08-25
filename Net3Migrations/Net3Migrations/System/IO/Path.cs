using System;

namespace Net3Migrations.System.IO
{
    public class Path
    {
        public static string Combine(params string[] paths)
        {
            if (paths == null)
                throw new ArgumentNullException(nameof(paths));
            if (paths.Length == 0)
                throw new ArgumentException("Invalid number of arguments");
            var path = paths[0];
            for (var i = 1; i < paths.Length; i++)
            {
                path = global::System.IO.Path.Combine(path, paths[i]);
            }
            return path;
        }
    }
}
