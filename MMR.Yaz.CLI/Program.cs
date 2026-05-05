using System;
using System.CommandLine;
using System.IO;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using MzxYaz = MMR.Randomizer.Utils.Mzxrules.Yaz;

namespace MMR.Yaz.CLI;

[SupportedOSPlatform("windows")]
public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        var root = new RootCommand
        {
            CreateDecodeCommand(),
            CreateEncodeCommand()
        };

        root.Description = "Command-line interface for MMR.Yaz decoder & encoder";

        var parseResult = root.Parse(args);
        return await parseResult.InvokeAsync();
    }

    private static Command CreateDecodeCommand()
    {
        var input = new Argument<string>("input")
        {
            Description = "Input file path."
        };

        var output = new Argument<string>("output")
        {
            Description = "Output file path."
        };

        var legacy = new Option<bool>("--legacy")
        {
            Description = "Use legacy implementation."
        };

        var cmd = new Command("decode")
        {
            Description = "Decode a file"
        };

        cmd.Arguments.Add(input);
        cmd.Arguments.Add(output);
        cmd.Options.Add(legacy);

        cmd.SetAction(parseResult =>
        {
            HandleDecode(
                parseResult.GetValue(input),
                parseResult.GetValue(output),
                parseResult.GetValue(legacy)
            );
            return 0;
        });

        return cmd;
    }

    static void HandleDecode(string input, string output, bool legacy)
    {
        var inputBytes = File.ReadAllBytes(input);
        byte[] outputBytes = null;

        // Perform decode.
        if (legacy)
        {
            // Legacy decode.
            using var memoryStream = new MemoryStream(inputBytes);
            outputBytes = MzxYaz.Decode(memoryStream, inputBytes.Length);
        }
        else
        {
            // New decode.
            outputBytes = Yaz.Decode(inputBytes);
        }

        File.WriteAllBytes(output, outputBytes);
    }

    private static Command CreateEncodeCommand()
    {
        var input = new Argument<string>("input")
        {
            Description = "Input file path."
        };

        var output = new Argument<string>("output")
        {
            Description = "Output file path."
        };

        var legacy = new Option<bool>("--legacy")
        {
            Description = "Use legacy implementation."
        };

        var cmd = new Command("encode")
        {
            Description = "Encode a file"
        };

        cmd.Arguments.Add(input);
        cmd.Arguments.Add(output);
        cmd.Options.Add(legacy);

        cmd.SetAction(parseResult =>
        {
            HandleEncode(
                parseResult.GetValue(input),
                parseResult.GetValue(output),
                parseResult.GetValue(legacy)
            );
            return 0;
        });

        return cmd;
    }

    static void HandleEncode(string input, string output, bool legacy)
    {
        var inputBytes = File.ReadAllBytes(input);
        byte[] outputBytes = null;

        if (legacy)
        {
            // Legacy encode.
            var amount = MzxYaz.Encode(inputBytes, inputBytes.Length, out var result);
            if (amount >= 0)
            {
                outputBytes = new byte[amount];
                Buffer.BlockCopy(result, 0, outputBytes, 0, outputBytes.Length);
            }
        }
        else
        {
            // New encode.
            var amount = Yaz.Encode(inputBytes, out var compressed);
            outputBytes = new byte[amount];
            compressed.AsSpan(0, amount).CopyTo(outputBytes);
        }

        File.WriteAllBytes(output, outputBytes);
    }
}
