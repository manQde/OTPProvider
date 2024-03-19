using OTPProvider.Structures;
using OutSystems.ExternalLibraries.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTPProvider
{
    [OSInterface]
    public interface IOTPProvider
    {
        string GenerateSecret(int Length = 16);
        TimeRemainingStructure TOTP_TimeRemaining(string Secret);
        SecretStructure TOTP_GetCurrentOTP(string Secret);
        UriStructure GenerateOTPUri(string Secret, string UserName, string CompanyName, string OTPType, int Counter);
        TOTPValidate TOTP_Validate(string Secret, string OTP);
        HOTPValidate HOTP_Validate(string Secret, string OTP, long Counter);
        SecretStructure HOTP_GetCurrentOTP(string Secret, long Counter);
    }
}
