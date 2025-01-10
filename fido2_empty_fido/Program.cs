// See https://aka.ms/new-console-template for more information
using Yubico.YubiKey.Fido2;
using Yubico.YubiKey;
using Yubico.Core.Logging;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;

// Lets get rid of all the noise

Log.ConfigureLoggerFactory(builder => builder.ClearProviders());

// Then the super basic test

var yubiKey = YubiKeyDevice.FindAll().First();
using (var fido2Session = new Fido2Session(yubiKey))
{
    int? retries;
    bool? rebootRequired;
    bool verified = fido2Session.TryVerifyPin((new byte[] { 49,50,51,52,53,54 }).AsMemory(), null, null, out retries, out rebootRequired);
    Console.WriteLine($"Authenticated succeeded {verified}.");
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
