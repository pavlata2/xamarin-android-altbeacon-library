using System;
using System.Collections.Generic;
using AltBeaconOrg.BoundBeacon;
using Android.OS;
using Java.Util;
using NUnit.Framework;
using static Android.Webkit.WebStorage;

namespace AndroidAltBeaconLibrary.UnitTests
{
    [TestFixture]
    public class AltBeaconTest : TestBase
    {
        [Test]
        public void TestRecognizeBeacon()
        {
            var bytes = HexStringToByteArray("02011a1bff1801beac2f234454cf6d4a0fadf2f4911ba9ffa600010002c509");
            var parser = new AltBeaconParser();
            var beacon = parser.FromScanData(bytes, -55, null, DateTime.UtcNow.Ticks);
            Assert.AreEqual(9, ((AltBeacon)beacon).MfgReserved, "manData should be parsed");
        }

        [Test]
        public void TestCanSerializeParcelable()
        {
            var original = new AltBeacon.Builder()
                .SetMfgReserved(2)
                .SetBluetoothAddress("aa:bb:cc:dd:ee:ff")
                .SetBluetoothName("Any Bluetooth")
                .SetBeaconTypeCode(1)
                .SetExtraDataFields(new List<Java.Lang.Long>())
                .SetId1("6")
                .SetId2("7")
                .SetId3("8")
                .SetManufacturer(10)
                .SetMultiFrameBeacon(true)
                .SetParserIdentifier("Any Parser ID")
                .SetRssi(-11)
                .SetRunningAverageRssi(-12.3)
                .SetServiceUuid(13)
                .SetTxPower(14)
                .Build();

            original.PacketCount = 15;
            original.SetRssiMeasurementCount(16);

            var parcel = Parcel.Obtain();
            original.WriteToParcel(parcel, 0);
            parcel.SetDataPosition(0);

            var parceled = new AltBeacon(parcel);

            Assert.AreEqual("aa:bb:cc:dd:ee:ff", parceled.BluetoothAddress);
            Assert.AreEqual("Any Bluetooth", parceled.BluetoothName);
            Assert.AreEqual(1, parceled.BeaconTypeCode);
            Assert.AreEqual(6, parceled.Id1.ToInt());
            Assert.AreEqual(7, parceled.Id2.ToInt());
            Assert.AreEqual(8, parceled.Id3.ToInt());
            Assert.AreEqual(10, parceled.Manufacturer);
            Assert.AreEqual(true, parceled.MultiFrameBeacon);
            Assert.AreEqual("Any Parser ID", parceled.ParserIdentifier);
            Assert.AreEqual(-11, parceled.Rssi);
            Assert.AreEqual(-12.3, parceled.RunningAverageRssi);
            Assert.AreEqual(13, parceled.ServiceUuid);
            Assert.AreEqual(14, parceled.TxPower); ;
            Assert.AreEqual(2, parceled.MfgReserved);
            Assert.AreEqual(16, parceled.MeasurementCount);
            Assert.AreEqual(15, parceled.PacketCount);
        }
    }
}