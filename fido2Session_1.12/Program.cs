// See https://aka.ms/new-console-template for more information
using Yubico.YubiKey.Fido2;
using Yubico.YubiKey;
using Yubico.Core.Logging;
using Microsoft.Extensions.Logging;

// Lets get rid of all the noise

Log.ConfigureLoggerFactory(builder => builder.ClearProviders());

// Then the super basic test

Console.WriteLine("Prior to FIDO2 Session!");

var yubiKey = YubiKeyDevice.FindAll().First();
using (var fido2Session = new Fido2Session(yubiKey))
{
    Console.WriteLine("inside FIDO2 Session!");
}

Console.WriteLine("After FIDO2 Session!");
