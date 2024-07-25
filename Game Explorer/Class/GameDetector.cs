using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Game_Explorer.Class
{
    public class GameInfo
    {
        public string Name { get; set; }
        public string LogoPath { get; set; }
        public string ExecutablePath { get; set; }
    }

    public abstract class GameDetector
    {
        private static readonly string[] GamesPaths =
        {
            @"C:\Program Files (x86)\Steam\steamapps\common",
            @"C:\Program Files (x86)\Ubisoft\Ubisoft Game Launcher\games",
            @"C:\XboxGames"
        };

        public static List<GameInfo> DetectInstalledGamesWithLogos()
        {
            var gamesList = new List<GameInfo>();

            foreach (var directory in GamesPaths)
            {
                gamesList.AddRange(DetectGamesInDirectory(directory));
            }

            return gamesList;
        }

        private static List<GameInfo> DetectGamesInDirectory(string directoryPath)
        {
            var gamesList = new List<GameInfo>();

            if (!Directory.Exists(directoryPath)) return gamesList;
            var gameDirectories = Directory.GetDirectories(directoryPath);
            gamesList.AddRange(from directory in gameDirectories
                let gameName = new DirectoryInfo(directory).Name
                let logoPath = GetGameLogoPath(directory, gameName)
                let executablePath = FindExecutable(directory, gameName)
                select new GameInfo { Name = gameName, LogoPath = logoPath, ExecutablePath = executablePath });

            return gamesList;
        }

        private static string FindExecutable(string gamePath, string gameName)
        {
            var exeFiles = Directory.GetFiles(gamePath, "*.exe", SearchOption.AllDirectories);
            var exactMatch = exeFiles.FirstOrDefault(file =>
                Path.GetFileNameWithoutExtension(file).Equals(gameName, StringComparison.OrdinalIgnoreCase));

            if (exactMatch != null && File.Exists(exactMatch))
            {
                return exactMatch;
            }

            var bestMatch = exeFiles.FirstOrDefault(file =>
                Path.GetFileNameWithoutExtension(file).ToLower().Contains(gameName.ToLower()));

            return bestMatch != null && File.Exists(bestMatch) ? bestMatch : null;
        }

        private static string GetGameLogoPath(string gamePath, string gameName)
        {
            var imageExtensions = new[] { ".png", ".jpg", ".jpeg", ".ico" };
            var logoFiles = Directory.GetFiles(gamePath, "*.*", SearchOption.AllDirectories)
                .Where(file => imageExtensions.Contains(Path.GetExtension(file).ToLower()));

            var enumerable = logoFiles as string[] ?? logoFiles.ToArray();
            var bestMatch = enumerable.FirstOrDefault(file =>
                Path.GetFileNameWithoutExtension(file).ToLower().Contains(gameName.ToLower()));

            return bestMatch ?? enumerable.FirstOrDefault();
        }
    }
}