using Yubico.Core.Logging;
using Yubico.YubiKey.Otp;
using Microsoft.Extensions.Logging;
using Yubico.YubiKey;
using Yubico.YubiKey.Otp.Operations;

// See https://aka.ms/new-console-template for more information

Slot slot = Slot.ShortPress;

// We dont need the noise
Log.ConfigureLoggerFactory(builder => builder.ClearProviders());

var yubiKey = YubiKeyDevice.FindAll().First();
using (var otpSession = new OtpSession(yubiKey))
{
    if (slot == Slot.ShortPress) { Console.WriteLine($"Configuration state of {slot}: {otpSession.IsShortPressConfigured}"); }
    else { Console.WriteLine($"Configuration state of {slot}: {otpSession.IsLongPressConfigured}"); }
}
using (var otpSession = new OtpSession(yubiKey))
{
    Console.WriteLine($"Setting the content of slot {slot}");
    ConfigureStaticPassword staticpassword = otpSession.ConfigureStaticPassword(slot);
    staticpassword.AppendCarriageReturn();
    staticpassword.WithKeyboard(Yubico.Core.Devices.Hid.KeyboardLayout.sv_SE);
    staticpassword.SetPassword("password1234".AsMemory());
    staticpassword.Execute();
}
using (var otpSession = new OtpSession(yubiKey))
{

    if (slot == Slot.ShortPress) { Console.WriteLine($"Configuration state of {slot}: {otpSession.IsShortPressConfigured}"); }
    else { Console.WriteLine($"Configuration state of {slot}: {otpSession.IsLongPressConfigured}"); }
    Console.WriteLine($"Time to remove the configuration from {slot}");
    // Since we didnt set any password DeleteSlot should work
    try
    {
        otpSession.DeleteSlot(slot);
    }
    catch (Exception e)
    {
        Console.WriteLine($"An exception was thrown: {e.Message}");
    }
}
using (var otpSession = new OtpSession(yubiKey))
{
    if (slot == Slot.ShortPress) { Console.WriteLine($"Configuration state of {slot}: {otpSession.IsShortPressConfigured}"); }
    else { Console.WriteLine($"Configuration state of {slot}: {otpSession.IsLongPressConfigured}"); }
}

