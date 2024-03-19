using OtpNet;
using OTPProvider.Structures;
using OutSystems.ExternalLibraries.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTPProvider
{
    [OSInterface(Description = "Component that provides TOTP and HOTP for things like two-factor authentication codes.\r\n\r\nImplementation of Otp.NET (https://github.com/kspearrin/Otp.NET)\r\n\r\nIcon provided by FlatIcon", IconResourceName = "OTPProvider.resources.icon.png", Name = "OTPProvider")]
    public interface IOTPProvider
    {
        /// <summary>
        /// Generates a Secret that can be used in the TOTP/HOTP generator
        /// </summary>
        [OSAction(Description = "Generates a Secret that can be used in the TOTP/HOTP generator", IconResourceName = "OTPProvider.resources.icon.png", ReturnName = "Secret", ReturnType = OSDataType.Text)]
        string GenerateSecret(
            [OSParameter(DataType = OSDataType.Integer, Description = "The length of the secret (Default is 16)")]
            int Length = 16
            );

        /// <summary>
        /// Creates an URI that can be used by QR generators
        /// </summary>
        [OSAction(Description = "Creates an URI that can be used by QR generators.", IconResourceName = "OTPProvider.resources.icon.png", ReturnName = "Out")]
        UriStructure GenerateOTPUri(
            [OSParameter(DataType = OSDataType.Text, Description = "The secret of the current user.")]
            string Secret,
            [OSParameter(DataType = OSDataType.Text, Description = "The name of the user.")]
            string UserName,
            [OSParameter(DataType = OSDataType.Text, Description = "The name of the company.")]
            string CompanyName = "",
            [OSParameter(DataType = OSDataType.Text, Description = "The type of OTP. \r\n\r\nPossible values are \"TOTP\" and \"HOTP\".\r\n\r\nDefault value is \"TOTP\".")]
            string OTPType = "TOTP",
            [OSParameter(DataType = OSDataType.Integer, Description = "The current counter value.\r\n\r\nDefault value is 0")]
            int Counter = 0
            );

        /// <summary>
        /// Returns the current OTP. This actions is specifically when using external authenticators like Google Authenticator or Microsoft Authenticator.
        /// </summary>
        [OSAction(Description = "Returns the current OTP. Usefull when you want to create your own authenticator app or for debugging purposes.", IconResourceName = "OTPProvider.resources.icon.png", ReturnName = "Out")]
        SecretStructure TOTP_GetCurrentOTP(
            [OSParameter(DataType = OSDataType.Text, Description = "The secret of the current user.")]
            string Secret
            );

        /// <summary>
        /// Returns the amount of time remaining before a new OTP will be calculated.
        /// </summary>
        [OSAction(Description = "Returns the amount of time remaining before a new OTP will be calculated.", IconResourceName = "OTPProvider.resources.icon.png", ReturnName = "Out")]
        TimeRemainingStructure TOTP_TimeRemaining(
            [OSParameter(DataType = OSDataType.Text, Description = "The secret of the current user.")]
            string Secret
            );

        /// <summary>
        /// Validates the TOTP code from the user. This actions is specifically when using external authenticators like Google Authenticator or Microsoft Authenticator.
        /// </summary>
        [OSAction(Description = "Validates the TOTP code from the user.", IconResourceName = "OTPProvider.resources.icon.png", ReturnName = "Out")]
        TOTPValidate TOTP_Validate(
            [OSParameter(DataType = OSDataType.Text, Description = "The secret of the current user.")]
            string Secret,
            [OSParameter(DataType = OSDataType.Text, Description = "The provided OTP of the user.")]
            string OTP
            );

        /// <summary>
        /// Returns the current OTP. This actions is specifically when using external authenticators like Google Authenticator or Microsoft Authenticator.
        /// </summary>
        [OSAction(Description = "Returns the current OTP.", IconResourceName = "OTPProvider.resources.icon.png", ReturnName = "Out")]
        SecretStructure HOTP_GetCurrentOTP(
            [OSParameter(DataType = OSDataType.Text, Description = "The secret of the current user.")] 
            string Secret,
            [OSParameter(DataType = OSDataType.LongInteger, Description = "The current counter value.\r\n\r\nDefault value is 0")]
            long Counter = 0
            );

        /// <summary>
        /// Validates the HOTP code from the user. This actions is specifically when using external authenticators like Google Authenticator or Microsoft Authenticator.
        /// </summary>
        [OSAction(Description = "Validates the HOTP code from the user.", IconResourceName = "OTPProvider.resources.icon.png", ReturnName = "Out")]
        HOTPValidate HOTP_Validate(
            [OSParameter(DataType = OSDataType.Text, Description = "The secret of the current user.")] 
            string Secret,
            [OSParameter(DataType = OSDataType.Text, Description = "The provided OTP of the user.")]
            string OTP,
            [OSParameter(DataType = OSDataType.LongInteger, Description = "The current counter value.\r\n\r\nDefault value is 0")]
            long Counter = 0);
    }
}
