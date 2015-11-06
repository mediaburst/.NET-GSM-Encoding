using NUnit.Framework;


namespace Mediaburst.Text.Tests
{
    [TestFixture]
    public class GsmEncodingTests
    {
        /// <summary>
        /// FORM FEED unicode character
        /// </summary>
        private const char _FF = '\u000C';

        /// <summary>
        /// Contains all characters encodable as 1 byte
        /// </summary>
        private const string _AllMainTableChars =
            "\u0040\u00A3\u0024\u00A5\u00E8\u00E9\u00F9\u00EC\u00F2\u00C7\u000A\u00D8\u00F8\u000D\u00C5\u00E5\u0394\u005F\u03A6\u0393\u039B\u03A9\u03A0\u03A8\u03A3\u0398\u039E\u00C6\u00E6\u00DF\u00C9\u0020\u0021\u0022\u0023\u00A4\u0025\u0026\u0027\u0028\u0029\u002A\u002B\u002C\u002D\u002E\u002F\u0030\u0031\u0032\u0033\u0034\u0035\u0036\u0037\u0038\u0039\u003A\u003B\u003C\u003D\u003E\u003F\u00A1\u0041\u0042\u0043\u0044\u0045\u0046\u0047\u0048\u0049\u004A\u004B\u004C\u004D\u004E\u004F\u0050\u0051\u0052\u0053\u0054\u0055\u0056\u0057\u0058\u0059\u005A\u00C4\u00D6\u00D1\u00DC\u00A7\u00BF\u0061\u0062\u0063\u0064\u0065\u0066\u0067\u0068\u0069\u006A\u006B\u006C\u006D\u006E\u006F\u0070\u0071\u0072\u0073\u0074\u0075\u0076\u0077\u0078\u0079\u007A\u00E4\u00F6\u00F1\u00FC\u00E0";

        /// <summary>
        /// Contains all characters encodable using default extension table
        /// </summary>
        private const string _AllExtTableChars = "\u000C\u005E\u007B\u007D\u005C\u005B\u007E\u005D\u007C\u20AC";

        /// <summary>
        /// Encoding instance to be used in tests
        /// </summary>
        private readonly GSMEncoding m_GsmEncoding = new GSMEncoding();

        #region GetBytes

        /// <summary>
        /// Regular string encoding
        /// </summary>				
        [Test]
        public void GetBytes_NormalString_Success()
        {
            const string input = "abcd";
            var expectedResult = new byte[] { 97, 98, 99, 100 };

            var result = m_GsmEncoding.GetBytes(input);

            Assert.AreEqual(expectedResult, result);
        }

        #region NULL tests

        /// <summary>
        /// NULL character in the middle of string should be replaced with space (0x20)
        /// </summary>				
        [Test, Ignore] // Need to check expected behaviour on this
        public void GetBytes_NullInsideString_Success()
        {
            const string input = "a\0b";
            var expectedResult = new byte[] { 97, 32, 98 };

            var result = m_GsmEncoding.GetBytes(input);

            Assert.AreEqual(expectedResult, result);
        }

        /// <summary>
        /// NULL character in the middle of string should not be replaced with space (32) if followed by FORM FEED
        /// </summary>				
        [Test, Ignore] // Need to check expected behaviour on this
        public void GetBytes_NullFollowedByFormFeedInsideString_Success()
        {
            string input = "a\0" + _FF + "b";
            var expectedResult = new byte[] { 97, 0, 27, 10, 98 };

            var result = m_GsmEncoding.GetBytes(input);

            Assert.AreEqual(expectedResult, result);
        }

        /// <summary>
        /// Sequence of NULL characters in the middle of string should be replaced with spaces (32)
        /// </summary>				
        [Test, Ignore] // Need to check expected behaviour on this
        public void GetBytes_SequenceOfNullsInsideString_Success()
        {
            const string input = "a\0\0\0b";
            var expectedResult = new byte[] { 97, 32, 32, 32, 98 };

            var result = m_GsmEncoding.GetBytes(input);

            Assert.AreEqual(expectedResult, result);
        }

        /// <summary>
        /// Sequence of NULL characters followed by form feed in the middle of string should not be replaced with spaces (32)
        /// </summary>				
        [Test, Ignore] // Need to check expected behaviour on this
        public void GetBytes_SequenceOfNullsFollowedByFormFeedInsideString_Success()
        {
            var input = "a\0\0\0" + _FF + "b";
            var expectedResult = new byte[] { 97, 0, 0, 0, 27, 10, 98 };
            
            var result = m_GsmEncoding.GetBytes(input);

            Assert.AreEqual(expectedResult, result);
        }

        /// <summary>
        /// NULL character in the end of string should be encoded as NULL
        /// </summary>				
        [Test, Ignore] // Need to check expected behaviour on this
        public void GetBytes_NullInTheEndOfString_Success()
        {
            const string input = "ab\0";
            var expectedResult = new byte[] { 97, 98, 0 };

            var result = m_GsmEncoding.GetBytes(input);

            Assert.AreEqual(expectedResult, result);
        }

        #endregion NULL tests

        #region COMMERCIAL AT tests

        /// <summary>
        /// COMMERCIAL AT character in the middle of string and not followed by NULL
        /// </summary>				
        [Test]
        public void GetBytes_AtInTheMiddleOfString_Success()
        {
            const string input = "a@b";
            var expectedResult = new byte[] { 97, 0, 98 };

            var result = m_GsmEncoding.GetBytes(input);

            Assert.AreEqual(expectedResult, result);
        }

        /// <summary>
        /// COMMERCIAL AT character in the end of string. Encoder should append CARRIAGE RETURN
        /// </summary>				
        [Test, Ignore] // Think the behaviour is wrong here
        public void GetBytes_AtInTheEndOfString_Success()
        {
            const string input = "ab@";
            var expectedResult = new byte[] { 97, 98, 0, 13 };

            var result = m_GsmEncoding.GetBytes(input);

            Assert.AreEqual(expectedResult, result);
        }

        /// <summary>
        /// COMMERCIAL AT character in the middle of string followed by NULL. 
        /// Encoder should not append CARRIAGE RETURN because this NULL will be converted to space
        /// </summary>				
        [Test, Ignore] // Need to check expected behaviour on this
        public void GetBytes_AtFollowedByNullInTheMiddleOfString_Success()
        {
            const string input = "a@\0b";
            var expectedResult = new byte[] { 97, 0, 32, 98 };

            var result = m_GsmEncoding.GetBytes(input);

            Assert.AreEqual(expectedResult, result);
        }

        /// <summary>
        /// COMMERCIAL AT character in the end of string followed by NULL. 
        /// Encoder should append CARRIAGE RETURN because this NULL will not be converted to space
        /// </summary>				
        [Test, Ignore] // Need to check expected behaviour on this
        public void GetBytes_AtFollowedByNullInTheEndOfString_Success()
        {
            const string input = "ab@\0";
            var expectedResult = new byte[] { 97, 98, 0, 13, 0 };

            var result = m_GsmEncoding.GetBytes(input);

            Assert.AreEqual(expectedResult, result);
        }

        /// <summary>
        /// COMMERCIAL AT character in the middle of string followed by NULL and FORM FEED. 
        /// Encoder should append CARRIAGE RETURN and not replace NULL with space
        /// </summary>				
        [Test, Ignore] // Need to check expected behaviour on this
        public void GetBytes_AtFollowedByNullAndFormFeedInTheMiddleOfString_Success()
        {
            var input = "a@\0" + _FF + "b";
            var expectedResult = new byte[] { 
                97,     // a
                0, 13,  // @
                0,      // \0
                27, 10, // \u000C
                98      // b
            };

            var result = m_GsmEncoding.GetBytes(input);

            Assert.AreEqual(expectedResult, result);
        }

        #endregion COMMERCIAL AT tests

        #endregion GetBytes

        #region Encode and decode

        /// <summary>
        /// Chars from default extended table
        /// </summary>				
        [Test]
        public void GetBytesGetString_ExtendedTableChars_Success()
        {
            var encoded = m_GsmEncoding.GetBytes(_AllExtTableChars);
            var decoded = m_GsmEncoding.GetString(encoded);

            Assert.AreEqual(_AllExtTableChars, decoded);
        }

        /// <summary>
        /// Main table chars
        /// </summary>				
        [Test]
        public void GetBytesGetString_MainTableChars_Success()
        {
            var encoded = m_GsmEncoding.GetBytes(_AllMainTableChars);
            var decoded = m_GsmEncoding.GetString(encoded);

            Assert.AreEqual(_AllMainTableChars, decoded);
        }

        /// <summary>
        /// Currency symbols
        /// </summary>				
        [Test]
        public void GetBytesGetString_CurrencySymbols_Success()
        {
            const string input = "£$€¥¤";

            var encoded = m_GsmEncoding.GetBytes(input);
            var decoded = m_GsmEncoding.GetString(encoded);

            Assert.AreEqual(input, decoded);
        }


        #endregion Encode and decode
    }
}
