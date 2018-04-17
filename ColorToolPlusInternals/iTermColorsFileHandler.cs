using ConsolePlus;
using SuccincT.Options;
using System;
using System.Globalization;
using System.IO;
using System.Xml;

namespace ColorToolPlusInternals
{
    // ReSharper disable once InconsistentNaming - following file extension convention rather than C# conventions here
    internal class iTermColorsFileHandler
    {
        private const string Key = "key";
        private const string ColorValue = "real";
        private const string KeyValueCollection = "dict";
        private const string RedKey = "Red Component";
        private const string GreenKey = "Green Component";
        private const string BlueKey = "Blue Component";

        public (bool loadedCorrectly, ConsoleColorSettings colorSettings) LoadColorSetFromFile(Stream file,
                                                                                               IConsolePlus console)
        {
            
            var xmlDocument = new XmlDocument();

            xmlDocument.Load(file);
            var colorCollectionNode = xmlDocument.GetElementsByTagName(KeyValueCollection)[0];

            var (processSuccess, fileContents) = ProcessFileContents(console, colorCollectionNode);

            if (!processSuccess) return (false, default);

            return CreateConsoleColorSettingsFromFileContents(console, fileContents);
        }

        private static (bool processSuccess, iTermColorsFileContents fileContents) ProcessFileContents(
            IConsolePlus console,
            XmlNode colorCollectionNode)
        {
            var fileContents = new iTermColorsFileContents();

            foreach (XmlNode node in colorCollectionNode.ChildNodes)
            {
                if (node.Name != Key) continue;

                var colorName = node.InnerText;

                if (node.NextSibling == null)
                {
                    console.WriteErrorLine($"No \"{KeyValueCollection}\" tag follows \"{colorName}\" {Key} tag.");
                    return (false, default);
                }

                var (parseSuccess, rgb) = ParseRgbFromKeyValuePairs(colorName, node.NextSibling, console);

                if (!parseSuccess) return (false, default);

                var setColorSuccess = fileContents.TrySetColor(
                    colorName,
                    rgb,
                    () => console.WriteErrorLine("Unexpected color name, \"{colorName}\" found."));

                if (!setColorSuccess) return (false, default);
            }

            return (true, fileContents);
        }

        private (bool loadedCorrectly, ConsoleColorSettings colorSettings) CreateConsoleColorSettingsFromFileContents(
            IConsolePlus console,
            iTermColorsFileContents fileContents)
        {
            var colorTable = new Rgb[16];

            foreach (var color in Enums.GetValues<ConsoleColorIndexes>())
            {
                var rgb = fileContents[color];
                if (!rgb.ValidColor)
                {
                    console.WriteErrorLine($"Didn't find a color definition for color {(int)color}");
                    return (false, default);
                }

                colorTable[(int)color] = rgb;
            }

            var noColor = Option<ConsoleColorIndexes>.None();
            return (true, new ConsoleColorSettings(colorTable,
                                                   noColor,
                                                   noColor,
                                                   noColor,
                                                   noColor));
        }

        private static (bool parsed, Rgb rgb) ParseRgbFromKeyValuePairs(string colorDefinitionName,
                                                                        XmlNode keyValueSet,
                                                                        IConsolePlus console)
        {
            var (red, green, blue) = (-1.0, -1.0, -1.0);

            foreach (XmlNode node in keyValueSet.ChildNodes)
            {
                if (node.Name != Key) continue;

                if (node.NextSibling == null)
                {
                    console.WriteErrorLine($"No \"{ColorValue}\" tag follows \"{node.Name}\".");
                    return (false, default);
                }

                if (node.NextSibling.Name == ColorValue)
                {
                    var value = Convert.ToDouble(node.NextSibling.InnerText, CultureInfo.InvariantCulture);

                    switch (node.InnerText)
                    {
                        case RedKey:
                            red = value;
                            break;
                        case GreenKey:
                            green = value;
                            break;
                        case BlueKey:
                            blue = value;
                            break;
                        default:
                            console.WriteErrorLine($"Unexpected color key: {node.InnerText}.");
                            return (false, default);
                    }
                }
                else
                {
                    console.WriteErrorLine($"Expected \"{ColorValue}\" tag to follow \"{node.Name}\". " +
                                           $"Got \"{node.NextSibling.Name}\".");
                    return (false, default);
                }
            }

            if (red >= 0 && green >= 0 && blue >= 0) return (true, (red, green, blue).ToRgb());

            console.WriteErrorLine($"Failed to obtain one or more colors for ${colorDefinitionName}. " +
                                   $"Missing {MissingColors(red, green, blue)} values.");

            return (false, default);
        }

        private static string MissingColors(double red, double green, double blue) =>
            $"{(red < 0 ? "red " : "")}{(green < 0 ? "green " : "")}{(blue < 0 ? "blue " : "")}";
    }
}
