namespace AltBeaconOrg.BoundBeacon
{
    // Metadata.xml XPath class reference: path="/api/package[@name='org.altbeacon.beacon']/class[@name='Beacon']"
    public partial class Beacon
    {
        public int DescribeContents()
        {
            return 0;
        }

        public void WriteToParcel(Android.OS.Parcel p, Android.OS.ParcelableWriteFlags f)
        {
            p.WriteInt(Identifiers.Count);
            foreach (Identifier identifier in Identifiers)
            {
                p.WriteString(identifier == null ? null : identifier.ToString());
            }
            p.WriteDouble(Distance);
            p.WriteInt(Rssi);
            p.WriteInt(TxPower);
            p.WriteString(BluetoothAddress);
            p.WriteInt(BeaconTypeCode);
            p.WriteInt(ServiceUuid);
            p.WriteByte((sbyte)(GetServiceUuid128Bit().Length != 0 ? 1 : 0));
            if (GetServiceUuid128Bit().Length != 0)
            {
                for (int i = 0; i < 16; i++)
                {
                    p.WriteByte((sbyte)GetServiceUuid128Bit()[i]);
                }
            }

            p.WriteInt(DataFields.Count);
            foreach (long dataField in DataFields)
            {
                p.WriteLong(dataField);
            }
            p.WriteInt(ExtraDataFields.Count);
            foreach (long dataField in ExtraDataFields)
            {
                p.WriteLong(dataField);
            }
            p.WriteInt(Manufacturer);
            p.WriteString(BluetoothName);
            p.WriteString(ParserIdentifier);
            p.WriteByte((sbyte)(MultiFrameBeacon ? 1 : 0));
            p.WriteValue(RunningAverageRssi);
            p.WriteInt(MeasurementCount);
            p.WriteInt(PacketCount);
            p.WriteLong(FirstCycleDetectionTimestamp);
            p.WriteLong(LastCycleDetectionTimestamp);
        }
    }
}