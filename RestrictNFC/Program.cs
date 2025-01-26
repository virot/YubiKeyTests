using Yubico.YubiKey;
using Yubico.Core.Logging;
using Microsoft.Extensions.Logging;

// Lets get rid of all the noise

Log.ConfigureLoggerFactory(builder => builder.ClearProviders());

// Then the super basic test

var yubiKey = YubiKeyDevice.FindAll().First();

Console.WriteLine($"Found YubiKey with serial number '{yubiKey.SerialNumber}'.");

Console.WriteLine($"Currentstate of RestrictedNFC: {yubiKey.IsNfcRestricted}");

Console.WriteLine("I will now restrict NFC");

yubiKey.SetIsNfcRestricted(true);

Console.WriteLine($"Currentstate of RestrictedNFC, same yubikey object: {yubiKey.IsNfcRestricted}");

var yubiKey2 = YubiKeyDevice.FindAll().First();
Console.WriteLine($"Currentstate of RestrictedNFC, new yubiKey object: {yubiKey2.IsNfcRestricted}");
