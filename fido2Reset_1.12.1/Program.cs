using Yubico.YubiKey.Fido2;
using Yubico.YubiKey;
using Yubico.Core.Logging;
using Microsoft.Extensions.Logging;
using Yubico.YubiKey.Fido2.Commands;

// Lets get rid of all the noise

Log.ConfigureLoggerFactory(builder => builder.ClearProviders());

// Then the super basic test

bool _yubiKeyRemoved = false;
bool _yubiKeyArrived = false;

var yubiKeyDeviceListener = YubiKeyDeviceListener.Instance;
yubiKeyDeviceListener.Removed += YubiKeyRemoved;
yubiKeyDeviceListener.Arrived += YubiKeyArrived;
Console.WriteLine("Remove YubiKey");
do { System.Threading.Thread.Sleep(100); } while (!_yubiKeyRemoved);
Console.WriteLine("Insert YubiKey");
do { System.Threading.Thread.Sleep(100); } while (!_yubiKeyArrived);
Console.WriteLine("Touch the YubiKey...");



var yubiKey = YubiKeyDevice.FindAll().First();
using (var fido2Session = new Fido2Session(yubiKey))
{
    ResetCommand resetCommand = new ResetCommand();
    ResetResponse reply = fido2Session.Connection.SendCommand(resetCommand);
}

void YubiKeyRemoved(object? sender, YubiKeyDeviceEventArgs e)
{   
    _yubiKeyRemoved = true;
 }

void YubiKeyArrived(object? sender, YubiKeyDeviceEventArgs e)
{
    _yubiKeyArrived = true;
}