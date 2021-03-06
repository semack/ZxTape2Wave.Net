﻿using System;
using CommandLine;
using ZxTape2Wav.Settings;

namespace ZxTape2Wav
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var settings = new OutputSettings();
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(async o =>
                {
                    try
                    {
                        if (o.Amplify)
                            settings.AmplifySoundSignal = true;
                        if (o.Frequency != 0)
                            settings.Frequency = o.Frequency;
                        if (o.ValidateCheckSum)
                            settings.ValidateCheckSum = true;
                        
                        var tape = await TapeFile.CreateAsync(o.Input);
                        await tape.SaveToWavAsync(o.Output, settings);
                        
                        Console.WriteLine("The file has been converted successfully.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                    }
                });
        }
    }
}