using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileRename
{
    public enum RenameMode
    {
        Prefix = 0,
        Suffix = 1,
        FindReplace = 2,
        SequentialNumber = 3
    }

    public class RenamePattern
    {
        public RenameMode Mode { get; set; }
        public string Prefix { get; set; } = "";
        public string Suffix { get; set; } = "";
        public string FindText { get; set; } = "";
        public string ReplaceText { get; set; } = "";
        public int StartNumber { get; set; } = 1;
        public int Step { get; set; } = 1;
        public int NumberDigits { get; set; } = 3;
    }

    public class RenameEntry
    {
        public string FullPath { get; set; }
        public string OriginalName { get; set; }
        public string NewName { get; set; }
        public string NewFullPath { get; set; }
        public bool Conflict { get; set; }
    }

    public class RenameEngine
    {
        /// <summary>
        /// Generates preview rename entries without actually renaming files.
        /// </summary>
        public List<RenameEntry> PreviewRename(List<string> filePaths, RenamePattern pattern)
        {
            var results = new List<RenameEntry>();
            int counter = pattern.StartNumber;

            foreach (var path in filePaths)
            {
                var entry = new RenameEntry
                {
                    FullPath = path,
                    OriginalName = Path.GetFileName(path)
                };

                string nameWithoutExt = Path.GetFileNameWithoutExtension(path);
                string ext = Path.GetExtension(path);

                switch (pattern.Mode)
                {
                    case RenameMode.Prefix:
                        entry.NewName = pattern.Prefix + nameWithoutExt + ext;
                        break;

                    case RenameMode.Suffix:
                        entry.NewName = nameWithoutExt + pattern.Suffix + ext;
                        break;

                    case RenameMode.FindReplace:
                        if (!string.IsNullOrEmpty(pattern.FindText))
                            entry.NewName = nameWithoutExt.Replace(pattern.FindText, pattern.ReplaceText) + ext;
                        else
                            entry.NewName = entry.OriginalName;
                        break;

                    case RenameMode.SequentialNumber:
                        string numStr = counter.ToString().PadLeft(pattern.NumberDigits, '0');
                        entry.NewName = numStr + ext;
                        counter += pattern.Step;
                        break;
                }

                entry.NewFullPath = Path.Combine(Path.GetDirectoryName(path), entry.NewName);

                // Check for conflict: new name exists and is not the original file
                entry.Conflict = File.Exists(entry.NewFullPath) &&
                    !string.Equals(entry.FullPath, entry.NewFullPath, StringComparison.OrdinalIgnoreCase);

                results.Add(entry);
            }

            return results;
        }

        /// <summary>
        /// Applies the rename operations. Returns the list of entries that were successfully renamed (for undo).
        /// </summary>
        public List<RenameEntry> BatchRename(List<RenameEntry> previewEntries)
        {
            var undoList = new List<RenameEntry>();

            foreach (var entry in previewEntries)
            {
                if (entry.Conflict) continue;
                if (string.Equals(entry.FullPath, entry.NewFullPath, StringComparison.OrdinalIgnoreCase)) continue;

                try
                {
                    // For undo: remember the reverse mapping
                    var undoEntry = new RenameEntry
                    {
                        FullPath = entry.NewFullPath,
                        OriginalName = entry.NewName,
                        NewName = entry.OriginalName,
                        NewFullPath = entry.FullPath
                    };

                    File.Move(entry.FullPath, entry.NewFullPath);
                    undoList.Add(undoEntry);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Failed to rename {entry.FullPath}: {ex.Message}");
                }
            }

            return undoList;
        }

        /// <summary>
        /// Undoes a previous batch rename operation.
        /// </summary>
        public void UndoRename(List<RenameEntry> undoEntries)
        {
            foreach (var entry in undoEntries)
            {
                try
                {
                    if (File.Exists(entry.FullPath))
                        File.Move(entry.FullPath, entry.NewFullPath);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Failed to undo rename {entry.FullPath}: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Applies a pattern to generate a new filename (single file utility).
        /// </summary>
        public string ApplyPattern(string originalPath, RenamePattern pattern, int sequenceNumber)
        {
            string nameWithoutExt = Path.GetFileNameWithoutExtension(originalPath);
            string ext = Path.GetExtension(originalPath);

            switch (pattern.Mode)
            {
                case RenameMode.Prefix:
                    return pattern.Prefix + nameWithoutExt + ext;
                case RenameMode.Suffix:
                    return nameWithoutExt + pattern.Suffix + ext;
                case RenameMode.FindReplace:
                    if (!string.IsNullOrEmpty(pattern.FindText))
                        return nameWithoutExt.Replace(pattern.FindText, pattern.ReplaceText) + ext;
                    return Path.GetFileName(originalPath);
                case RenameMode.SequentialNumber:
                    string numStr = sequenceNumber.ToString().PadLeft(pattern.NumberDigits, '0');
                    return numStr + ext;
                default:
                    return Path.GetFileName(originalPath);
            }
        }
    }
}
