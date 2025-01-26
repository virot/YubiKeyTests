// See https://aka.ms/new-console-template for more information
using Yubico.YubiKey.Fido2;
using Yubico.YubiKey;
using Yubico.Core.Logging;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;
using Yubico.YubiKey.Fido2.Commands;

// Lets get rid of all the noise

Log.ConfigureLoggerFactory(builder => builder.ClearProviders());

// Then the super basic test

var yubiKey = YubiKeyDevice.FindAll().First();

Console.WriteLine($"Found YubiKey with serial number '{yubiKey.SerialNumber}'.");
Console.WriteLine("Please enter the PIN for the FIDO2 application of the YubiKey:");
string? userInput = Console.ReadLine();

if (userInput is null || userInput.Length != 6)
{
    Console.WriteLine("No PIN entered.");
    return;
}

byte[] pin = userInput.Select(c => (byte)c).ToArray();

using (var fido2Session = new Fido2Session(yubiKey))
{
    int? retries;
    bool? rebootRequired;
    if (fido2Session.AuthenticatorInfo.Options.ContainsKey("pinUvAuthToken"))
    {
        Console.WriteLine("PIN UV Auth Token is supported.");
        bool verified = fido2Session.TryVerifyPin(pin.AsMemory(), PinUvAuthTokenPermissions.CredentialManagement, null, retriesRemaining: out retries, rebootRequired: out rebootRequired);
        Console.WriteLine($"Authenticated succeeded {verified}.");
    }
    else
    {
        Console.WriteLine("PIN UV Auth Token is not supported.");
        bool verified = fido2Session.TryVerifyPin(pin.AsMemory(),null, null, retriesRemaining: out retries, rebootRequired: out rebootRequired);
        Console.WriteLine($"Authenticated succeeded {verified}.");
    }
    Console.WriteLine($"FIDO2 version '{fido2Session.AuthenticatorInfo.FirmwareVersion}'.");
    try
    {
        var RPs = fido2Session.EnumerateRelyingParties();
        Console.WriteLine($"Found {RPs.Count()} Relying Parties.");
    }
    catch (Exception e)
    {
        Console.WriteLine($"An exception was thrown: {e.Message}");
        Console.WriteLine($"An exception of type: {e.GetType()}");
    }
}
