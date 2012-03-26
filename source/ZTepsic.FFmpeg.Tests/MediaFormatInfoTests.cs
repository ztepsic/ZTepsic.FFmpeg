using System;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using NUnit.Framework;

namespace ZTepsic.FFmpeg.Tests {
	[TestFixture]
	public class MediaFormatInfoTests {

		private string xml;

		private string fileName = "filename.flv";
		private string format = "flv";
		private string formatLongName = "FLV format";
		private decimal bitRate = 3000;
		private decimal duration = 564.23m;
		private decimal startTime = 0.12m;
		private decimal fileSize = 20;

		[SetUp]
		public void SetUp() {
			XDocument xDoc = new XDocument();
			var rootElem = new XElement("ffprobe");
			var formatElem = new XElement(MediaFormatInfoFactory.FORMAT_NODE);
			formatElem.Add(new XAttribute(MediaFormatInfoFactory.FILE_NAME, fileName));
			formatElem.Add(new XAttribute(MediaFormatInfoFactory.FORMAT, format));
			formatElem.Add(new XAttribute(MediaFormatInfoFactory.FORMAT_LONG_NAME, formatLongName));
			formatElem.Add(new XAttribute(MediaFormatInfoFactory.BIT_RATE, bitRate.ToString(CultureInfo.InvariantCulture)));
			formatElem.Add(new XAttribute(MediaFormatInfoFactory.DURATION, duration.ToString(CultureInfo.InvariantCulture)));
			formatElem.Add(new XAttribute(MediaFormatInfoFactory.START_TIME, startTime.ToString(CultureInfo.InvariantCulture)));
			formatElem.Add(new XAttribute(MediaFormatInfoFactory.FILE_SIZE, fileSize.ToString(CultureInfo.InvariantCulture)));

			rootElem.Add(formatElem);
			xDoc.Add(rootElem);

			xml = xDoc.ToString();

		}

		[Test]
		public void Can_Create_MediaFormatInfo() {
			// Arrange
			string toString = String.Format("Filename: {0}; Format: {1}; Format long name: {2}; Bitrate: {3}; Duration: {4} sec; Start Time: {5} sec; File size: {6}",
				fileName,
				format,
				formatLongName,
				bitRate,
				duration,
				startTime,
				fileSize);

			// Act
			MediaFormatInfo mediaFormatInfo = MediaFormatInfoFactory.CreateFromXml(xml);

			// Assert
			Assert.IsNotNull(mediaFormatInfo);
			Assert.AreEqual(fileName, mediaFormatInfo.FileName);
			Assert.AreEqual(format, mediaFormatInfo.Format);
			Assert.AreEqual(toString, mediaFormatInfo.ToString());
		}

		[Test]
		public void Can_Read_Values_From_MediaFormatInfo_Object() {
			// Arrange
			string toString = String.Format("Filename: {0}; Format: {1}; Format long name: {2}; Bitrate: {3}; Duration: {4} sec; Start Time: {5} sec; File size: {6}", 
				fileName,
				format,
				formatLongName,
				bitRate,
				duration,
				startTime,
				fileSize);

			// Act
			MediaFormatInfo mediaFormatInfo = MediaFormatInfoFactory.CreateFromXml(xml);

			// Assert
			Assert.IsNotNull(mediaFormatInfo);
			Assert.AreEqual(fileName, mediaFormatInfo.FileName);
			Assert.AreEqual(format, mediaFormatInfo.Format);
			Assert.AreEqual(formatLongName, mediaFormatInfo.FormatLongName);
			Assert.AreEqual(bitRate, mediaFormatInfo.Bitrate);
			Assert.AreEqual(duration, mediaFormatInfo.Duration);
			Assert.AreEqual(startTime, mediaFormatInfo.StartTime);
			Assert.AreEqual(fileSize, mediaFormatInfo.FileSize);
			Assert.AreEqual(toString, mediaFormatInfo.ToString());
		}

		[Test]
		public void Can_Compare_Two_MediFormatInfo_Objects() {
			// Arrange
			MediaFormatInfo mediaFormatInfo01 = MediaFormatInfoFactory.CreateFromXml(xml);
			/*mediaFormatInfo01.SetPropertyTo<MediaFormatInfo>(x => x.FormatLongName, formatLongName);
			mediaFormatInfo01.SetPropertyTo<MediaFormatInfo>(x => x.Bitrate, bitRate);
			mediaFormatInfo01.SetPropertyTo<MediaFormatInfo>(x => x.Duration, duration);
			mediaFormatInfo01.SetPropertyTo<MediaFormatInfo>(x => x.StartTime, startTime);
			mediaFormatInfo01.SetPropertyTo<MediaFormatInfo>(x => x.FileSize, fileSize);*/

			MediaFormatInfo mediaFormatInfo02 = MediaFormatInfoFactory.CreateFromXml(xml);
			/*mediaFormatInfo02.SetPropertyTo<MediaFormatInfo>(x => x.FormatLongName, formatLongName);
			mediaFormatInfo02.SetPropertyTo<MediaFormatInfo>(x => x.Bitrate, bitRate);
			mediaFormatInfo02.SetPropertyTo<MediaFormatInfo>(x => x.Duration, duration);
			mediaFormatInfo02.SetPropertyTo<MediaFormatInfo>(x => x.StartTime, startTime);
			mediaFormatInfo02.SetPropertyTo<MediaFormatInfo>(x => x.FileSize, fileSize);*/

			// Act

			// Assert

			// reflexive: for any non-null reference value x, x.Equals(x) must return true
			Assert.IsTrue(mediaFormatInfo01.Equals(mediaFormatInfo01));

			// symetric: for any non-null reference values x and y, x.Equals(y) must return true if
			// and only if y.Equasls(x) returns true
			Assert.IsTrue(mediaFormatInfo01.Equals(mediaFormatInfo02));
			Assert.IsTrue(mediaFormatInfo02.Equals(mediaFormatInfo01));

			// transitive: for any non-null reference values x, y, z, if x.Equals(y) returns true and
			// y.Equals(z) returns true, then x.Equals(z) must return true

			// for any non-null reference value x, x.Equals(null) must return false
			Assert.IsFalse(mediaFormatInfo01.Equals(null));

			// two equals objects must return the same hash code
			Assert.AreEqual(mediaFormatInfo01.GetHashCode(), mediaFormatInfo02.GetHashCode());

		}

	}
}
