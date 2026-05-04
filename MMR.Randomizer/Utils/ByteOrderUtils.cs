using System;
using System.IO;
using System.Linq;

namespace MMR.Randomizer.Utils;

public static class ByteOrderUtils
{
    /*
     * Nintendo 64 ROMs come in at least four different byte orders:
     * 1. Big Endian - 32-bit words store their MSB at the lowest possible memory address.
     * 2. Little Endian - 32-bit words store their LSB at the lowest possible memory address.
     * 3. Byteswapped (Big Endian) - Big endian, but 16-bit halfwords have their bytes swapped.
     * 4. Byteswapped (Little Endian) - Little endian, but 16-bit halfwords have their bytes swapped.
     * 
     * Thanks ROM dumpers for making things difficult.
     * 
     * The values below are the only valid values for a clean and unmodified Majora's Mask ROM.
     * If a user's ROM has different values, then it is invalid.
     */
    private static readonly byte[] BE_WORD = new byte[] { 0x80, 0x37, 0x12, 0x40 };
    private static readonly byte[] LE_WORD = new byte[] { 0x40, 0x12, 0x37, 0x80 };
    private static readonly byte[] BB_WORD = new byte[] { 0x37, 0x80, 0x40, 0x12 };
    private static readonly byte[] BL_WORD = new byte[] { 0x12, 0x40, 0x80, 0x37 };

    private static readonly byte[] BE_BYTE = new byte[] { 0x80 };
    private static readonly byte[] LE_BYTE = new byte[] { 0x40 };
    private static readonly byte[] BB_BYTE = new byte[] { 0x37 };
    private static readonly byte[] BL_BYTE = new byte[] { 0x12 };

    public enum ByteOrder
    {
        BigEndian = 0,
        LittleEndian = 1,
        ByteswappedBig = 2,
        ByteswappedLittle = 3,
    }

    public static ByteOrder DetectByteOrder(string input)
    {
        /*
         * The first 32-bit word of a Nintendo 64 ROM is 1 byte followed by
         * the PI BSD DOM1 configuration (3 bytes). If the user provides a
         * clean and unmodified ROM of Majora's Mask, then these four bytes
         * will be constant (all known commercial Nintendo 64 games use the
         * same 32-bit word value). Because of this, the ROM's byte order can
         * be determined by reading the word or just the first byte. The time
         * it takes to check 1 or 4 bytes is miniscule, so it doesn't matter
         * if it's a 4 byte or 1 byte check.
         */
        using BinaryReader rom = new (File.OpenRead(input));

        if (rom.BaseStream.Length < 4 || rom.BaseStream.Length % 4 != 0)
            throw new InvalidDataException(
                $"Invalid ROM length ({rom.BaseStream.Length} bytes). Must be >= 4 and a multiple of 4.");

        byte[] word = new byte[4];
        rom.Read(word, 0, 4);

        if (word.SequenceEqual(BE_WORD)) return ByteOrder.BigEndian;
        if (word.SequenceEqual(LE_WORD)) return ByteOrder.LittleEndian;
        if (word.SequenceEqual(BB_WORD)) return ByteOrder.ByteswappedBig;
        if (word.SequenceEqual(BL_WORD)) return ByteOrder.ByteswappedLittle;

        throw new InvalidDataException("Unknown byte order or modified ROM");
    }

    public static string ToBigEndian(string inputFile)
    {
        ByteOrder order = DetectByteOrder(inputFile);

        if (order == ByteOrder.BigEndian)
            return inputFile;

        string dir = Path.GetDirectoryName(inputFile)!;
        string name = Path.GetFileNameWithoutExtension(inputFile);
        string ext = Path.GetExtension(inputFile);

        string tempFile = Path.GetTempFileName();
        string outputFile = Path.Combine(dir, name + ".z64");
        string backupFile = Path.Combine(dir, name + ext + ".bak");

        using (BinaryReader reader = new(File.OpenRead(inputFile)))
        using (BinaryWriter writer = new(File.Create(tempFile)))
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                uint value = reader.ReadUInt32();

                value = order switch
                {
                    ByteOrder.LittleEndian => ReadWriteUtils.Byteswap32(value),
                    ByteOrder.ByteswappedBig => SwapHalfwordInWord(value),
                    ByteOrder.ByteswappedLittle => SwapHalfwordInWord(ReadWriteUtils.Byteswap32(value)),
                    _ => value
                };

                writer.Write(value);
            }
        }

        File.Move(inputFile, backupFile, overwrite: true);
        File.Move(tempFile, outputFile, overwrite: true);

        return outputFile;
    }

    private static uint SwapHalfwordInWord(uint v)
    {
        return (uint)(
            (ReadWriteUtils.Byteswap16((ushort)(v >> 16)) << 16)
            | ReadWriteUtils.Byteswap16((ushort)(v & 0xFFFF))
        );
    }
}
