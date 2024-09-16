namespace Utility
{
    using System;

    public static class OtpGenerator
    {
        public static string GenerateOtp(int numberOfDigits)
        {
            if (numberOfDigits <= 0)
            {
                throw new ArgumentException("Number of digits must be greater than zero.");
            }

            // Create a random number generator
            Random random = new();

            // Generate the maximum and minimum values based on the number of digits
            int maxValue = (int)Math.Pow(10, numberOfDigits) - 1;
            int minValue = (int)Math.Pow(10, numberOfDigits - 1);

            // Generate a random OTP
            int otp = random.Next(minValue, maxValue + 1);

            // Return the OTP as a string, ensuring it has the correct number of digits
            return otp.ToString().PadLeft(numberOfDigits, '0');
        }
    }

}
