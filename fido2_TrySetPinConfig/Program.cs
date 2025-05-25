// See https://aka.ms/new-console-template for more information
using Yubico.YubiKey.Fido2;
using Yubico.YubiKey;
using Yubico.Core.Logging;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;
using Yubico.YubiKey.Fido2.Commands;
using System.Runtime.InteropServices;

namespace fido2_TrySetPinConfig
{
    internal static class Program
    {
        public static YKKeyCollector _KeyCollector = new YKKeyCollector();

        static void Main()
        {
            // Lets get rid of all the noise

            //Log.ConfigureLoggerFactory(builder => builder.ClearProviders());

            // Then the super basic test

            var yubiKey = YubiKeyDevice.FindAll().First();

            Console.WriteLine($"Found YubiKey with serial number '{yubiKey.SerialNumber}'.");
            Console.WriteLine("Please enter the PIN for the FIDO2 application of the YubiKey:");
            string? userInput = Console.ReadLine();

            if (userInput is null)
            {
                Console.WriteLine("No PIN entered.");
                return;
            }

            _KeyCollector.pin = userInput.Select(c => (byte)c).ToArray();

            using (var fido2Session = new Fido2Session(yubiKey))
            {
                fido2Session.KeyCollector = _KeyCollector.YKKeyCollectorDelegate; ;
                Console.WriteLine($"FIDO2 version '{fido2Session.AuthenticatorInfo.FirmwareVersion}'.");
                var minLength= fido2Session.AuthenticatorInfo.MinimumPinLength;
                Console.WriteLine($" '{minLength}'.");
                try
                {
                    _ = fido2Session.TrySetPinConfig(minLength + 1, null, null);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An exception was thrown: {e.Message}");
                    Console.WriteLine($"An exception of type: {e.GetType()}");
                }
            }
        }
    }

    public class YKKeyCollector
    {
        public byte[]? pin;
        public bool YKKeyCollectorDelegate(KeyEntryData keyEntryData)
        {
            if (keyEntryData is null)
            {
                return false;
            }

            if (keyEntryData.IsRetry)
            {
                switch (keyEntryData.Request)
                {
                    case KeyEntryRequest.ChangeFido2Pin:
                        throw new Exception("Failed to change FIDO2 PIN.");

                    case KeyEntryRequest.VerifyFido2Pin:
                        throw new Exception("Failed to auth FIDO2 PIN.");
                }
            }

            byte[] currentValue;

            switch (keyEntryData.Request)
            {
                default:
                    return false;

                case KeyEntryRequest.Release:
                    break;

                case KeyEntryRequest.VerifyFido2Pin:
                    if (pin is null)
                    {
                        return false;
                    }
                    currentValue = pin;
                    keyEntryData.SubmitValue(currentValue);
                    break;
            }
            return true;
        }
    }
}