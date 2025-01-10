```pwsh
PS C:\examples> C:\GIT-VS\YubiKeyTests\fido2_empty_fido\bin\Debug\net8.0\fido2_empty_fido.exe
Authenticated succeeded True.
FIDO2 version '328707'.
An exception was thrown: The FIDO2 info returned is invalid.
An exception of type: Yubico.YubiKey.Fido2.Ctap2DataException
```

After addin a single entry.

```pwsh
PS C:\examples> C:\GIT-VS\YubiKeyTests\fido2_empty_fido\bin\Debug\net8.0\fido2_empty_fido.exe
Authenticated succeeded True.
FIDO2 version '328707'.
Found 1 Relying Parties.
```

Firmware version
```pwsh
PS C:\examples> get-YubiKey

PrettyName               : Security Key C by Yubico
AvailableUsbCapabilities : FidoU2f, Fido2
EnabledUsbCapabilities   : FidoU2f, Fido2
AvailableNfcCapabilities : FidoU2f, Ccid, Fido2
EnabledNfcCapabilities   : FidoU2f, Ccid, Fido2
FipsApproved             : None
FipsCapable              : None
ResetBlocked             : None
IsNfcRestricted          : False
PartNumber               :
IsPinComplexityEnabled   : False
SerialNumber             :
IsFipsSeries             : False
IsSkySeries              : True
FormFactor               : UsbCKeychain
FirmwareVersion          : 5.4.3
TemplateStorageVersion   :
ImageProcessorVersion    :
AutoEjectTimeout         : 0
ChallengeResponseTimeout : 15
DeviceFlags              : None
ConfigurationLocked      : False
AvailableTransports      : HidFido

PS C:\examples>
```