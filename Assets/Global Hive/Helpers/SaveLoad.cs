using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace GlobalHive.SaveLoad
{
    public static class SaveLoad
    {
        public static void Save(string _FilePath, SaveData _Data)
        {
            BinaryFormatter _BF = new BinaryFormatter();
            FileStream _FS = new FileStream(_FilePath, FileMode.OpenOrCreate);

            try
            {
                _BF.Serialize(_FS, _Data);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                _FS.Close();
            }
        }

        public static SaveData Load(string _FilePath)
        {
            FileStream _FS = new FileStream(_FilePath, FileMode.Open);
            BinaryFormatter _BF = new BinaryFormatter();

            try
            {
                return (SaveData)_BF.Deserialize(_FS);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                _FS.Close();
            }
        }
    }

    [System.Serializable]
    public class SaveData
    {
        public object Data;
    }
}
