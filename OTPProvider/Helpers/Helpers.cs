using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTPProvider.Helpers
{
    internal static class Helper
    {
        internal static DateTime getOutSystemsNullDateTime()
        {
            return new DateTime(1900, 1, 1, 00, 00, 00);
        }


        // Determines the OTP Type
        internal static OtpType GetOtpType(string otpType)
        {
            otpType = otpType.ToUpper();
            switch (otpType)
            {
                case "TOTP": return OtpType.Totp;
                case "HOTP": return OtpType.Hotp;
            }

            throw new Exception("Unknown OTP Type");
        }


        // Gets the TimeCorrection object
        internal static TimeCorrection GetTimeCorrect(DateTime currentTime)
        {
            TimeCorrection correction;
            DateTime OutSystemsDefaultDateTime = getOutSystemsNullDateTime();
            if (OutSystemsDefaultDateTime == currentTime || currentTime == DateTime.MinValue)
                correction = new TimeCorrection(DateTime.UtcNow);
            else
                correction = new TimeCorrection(DateTime.UtcNow, currentTime);
            return correction;
        }


        // Gets the correct VerificationWindow value
        internal static VerificationWindow GetVerificationWindow(bool AllowPreviousCodeToBeValid, bool AllowFutureCodeToBeValid)
        {
            int previous = AllowPreviousCodeToBeValid ? 1 : 0;  // If we allow the previous code to be allowed we set "previous" to the value of 1 to include the first previous code
            int future = AllowFutureCodeToBeValid ? 1 : 0; // If we allow the future code to be allowed we set "future" to the value of 1 to include the first next code
            VerificationWindow result = new VerificationWindow(previous, future);
            return result;
        }


        // Determines the Hash Method
        internal static OtpHashMode GetHashMethod(string HashMethod)
        {
            HashMethod = HashMethod.ToLower();
            OtpHashMode output;
            switch (HashMethod)
            {
                case "sha1":
                    output = OtpHashMode.Sha1;
                    break;
                case "sha256":
                    output = OtpHashMode.Sha256;
                    break;
                case "sha512":
                    output = OtpHashMode.Sha512;
                    break;
                default:
                    throw new Exception("Unknown Hash Method.");
            }
            return output;
        }


        // Generates the TOTP Object
        internal static Totp GenerateTotp(string Secret, OtpHashMode HashMode, int OTPTimeToLive, int OTPSize)
		{
			var totp = new Totp(Base32Encoding.ToBytes(Secret), OTPTimeToLive, HashMode, OTPSize);
			return totp;
		}
    }
}
