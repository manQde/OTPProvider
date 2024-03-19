using OtpNet;
using OTPProvider.Structures;
using OTPProvider.Helpers;

namespace OTPProvider
{
    public class OTPProvider : IOTPProvider
    {
        /// <summary>
        /// Generates a Secret that can be used in the TOTP/HOTP generator
        /// </summary>
        /// <param name="Length">The length of the secret.</param>
        /// <returns></returns>
        public string GenerateSecret(int Length = 16)
        {
            return Base32Encoding.ToString(KeyGeneration.GenerateRandomKey(Length));
        }


        /// <summary>
        /// Returns the amount of time remaining before a new OTP will be calculated.
        /// </summary>
        /// <param name="Secret">The secret of the current user.</param>
        /// <returns></returns>
        public TimeRemainingStructure TOTP_TimeRemaining(string Secret)
        {
            TimeRemainingStructure data = new TimeRemainingStructure();
            try
            {
                Totp totp = new Totp(Base32Encoding.ToBytes(Secret));
                data.TimeRemaining = totp.RemainingSeconds();
                return data;
            }
            catch (Exception e) { 
                data.IsError = true;
                data.ErrorMessage = e.Message;
                return data;
            }
        } 


        /// <summary>
        /// Returns the current OTP. This actions is specifically when using external authenticators like Google Authenticator or Microsoft Authenticator.
        /// </summary>
        /// <param name="Secret">The secret of the current user.</param>
        public SecretStructure TOTP_GetCurrentOTP(string Secret)
        {
            SecretStructure data = new SecretStructure();
            try
            {
                Totp totp = new Totp(Base32Encoding.ToBytes(Secret));
                data.Secret = totp.ComputeTotp();
                return data;
            }
            catch (Exception e) {
                data.IsError = true;
                data.ErrorMessage = e.Message;
                return data;
            }
        } 


        /// <summary>
        /// Creates an URI that can be used by QR generators
        /// </summary>
        /// <param name="Secret">The secret of the current user.</param>
        /// <param name="UserName">The name of the user.</param>
        /// <param name="CompanyName">The name of the Company.</param>
        /// <param name="OTPType">The type of OTP.
        /// 
        /// Possible values are &quot;TOTP&quot; and &quot;HOTP&quot;.
        /// 
        /// Default value is &quot;TOTP&quot;.</param>
        /// <param name="Counter">The current counter value.
        /// 
        /// Default value is 0</param>
        public UriStructure GenerateOTPUri(string Secret, string UserName, string CompanyName, string OTPType, int Counter)
        {
            UriStructure data = new UriStructure();

            // if we get an empty string we will set the value to null so that the OtpUri knows it has no value provided
            if (CompanyName == "")
            {
                CompanyName = null;
            }

            try
            {
                OtpType otpType = Helper.GetOtpType(OTPType);
                OtpUri otpUri = new OtpUri(otpType, Secret, UserName, CompanyName, counter: Counter);
                data.URI = otpUri.ToString();
                return data;
            }
            catch (Exception e) {
                data.IsError = true;
                data.ErrorMessage = e.Message;
                return data;
            }
        }

        /// <summary>
        /// Validates the TOTP code from the user. This actions is specifically when using external authenticators like Google Authenticator or Microsoft Authenticator.
        /// </summary>
        /// <param name="Secret">The secret of the current user.</param>
        /// <param name="OTP">The OTP of the user.</param>
        /// <param name="ssIsValid">Indicates if the OTP is valid.</param>
        public TOTPValidate TOTP_Validate(string Secret, string OTP)
        {
            TOTPValidate data = new TOTPValidate();

            try
            {
                Totp totp = new Totp(Base32Encoding.ToBytes(Secret));
                data.IsValid = totp.VerifyTotp(OTP, out long OTPSize, VerificationWindow.RfcSpecifiedNetworkDelay);
                data.TimeWindowUser = OTPSize;
                return data;
            }
            catch (Exception e) {
                data.IsError = true;
                data.ErrorMessage = e.Message;
                return data;
            }
        }

        /// <summary>
        /// Validates the HOTP code from the user. This actions is specifically when using external authenticators like Google Authenticator or Microsoft Authenticator.
        /// </summary>
        /// <param name="Secret">The secret of the current user.</param>
        /// <param name="OTP">The OTP of the user.</param>
        /// <param name="Counter"></param>
        public HOTPValidate HOTP_Validate(string Secret, string OTP, long Counter)
        {
            HOTPValidate data = new HOTPValidate();

            try
            {
                Hotp hotp = new Hotp(Base32Encoding.ToBytes(Secret));
                data.IsValid = hotp.VerifyHotp(OTP, Counter);
                return data;
            }
            catch (Exception e) {
                data.IsError = true;
                data.ErrorMessage = e.Message;
                return data;
            }
        }


        /// <summary>
        /// Returns the current OTP. This actions is specifically when using external authenticators like Google Authenticator or Microsoft Authenticator.
        /// </summary>
        /// <param name="Secret">The secret of the current user.</param>
        /// <param name="Counter">The counter of the HOTP verification. Both the client as the server needs to be in sync with the token. If they are not then the validation will fail.
        /// 
        /// Note: Keep track of the counter on the server and use only that value for validation. Do not use the counter value of the client.</param>
        /// <param name="OTP">The calculated OTP.</param>
        /// <param name="ssIsError">Indicates an error occured.</param>
        /// <param name="ssErrorMessage">The message of the error.</param>
        public SecretStructure HOTP_GetCurrentOTP(string Secret, long Counter)
        {
            SecretStructure data = new SecretStructure();

            try
            {
                Hotp hotp = new Hotp(Base32Encoding.ToBytes(Secret));
                data.Secret = hotp.ComputeHOTP(Counter);
                return data;
            }
            catch (Exception e) {
                data.IsError = true;
                data.ErrorMessage = e.Message;
                return data;
            }
        } 
    }
}
