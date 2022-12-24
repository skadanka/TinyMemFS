using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TinyMemFS
{

    [Serializable()]
    public class FileObject : ISerializable
    {
        long size;
        public string fileName { get; set; }
        DateTime creationDate;
        bool compressed;
        bool hidden;
        byte[] byteData;
        private Stack<Tuple<string, Tuple<byte[], byte[]>>> Keys;
        public MyConCurrentQueue myConCurrentQueue;


        public byte[] getBytes()
        {
            return byteData;
        }

        public DateTime getCreationDate()
        {
            return creationDate;
        }
        public FileObject(string fileName)
        {
            this.fileName = fileName;
            compressed = false;
            hidden = false;
            myConCurrentQueue = new MyConCurrentQueue();
        }


        public FileObject(string fileName, string filePath)
        {
            myConCurrentQueue = new MyConCurrentQueue();
            Keys = new Stack<Tuple<string, Tuple<byte[], byte[]>>>();
            FileInfo fileInfo = new FileInfo(filePath);
            this.creationDate = fileInfo.CreationTime;
            this.size = fileInfo.Length;
            this.byteData = File.ReadAllBytes(filePath);
            this.fileName = fileName;
            compressed = false;
            hidden = false;
        }

        // Implement this method to serialize data. The method is called
        /*  long size;
          public string fileName { get; set; }
          DateTime creationDate;
          bool compressed;
          bool hidden;
          byte[] byteData;
          private Stack<Tuple<string, Tuple<byte[], byte[]>>> Keys;
          public MyConCurrentQueue myConCurrentQueue;*/
        // on serialization.
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Use the AddValue method to specify serialized values.
            info.AddValue("fileName", fileName, typeof(string));
            info.AddValue("size", size, typeof(long));
            info.AddValue("DateTime", creationDate, typeof(DateTime));
            info.AddValue("compressed", compressed, typeof(bool));
            info.AddValue("hidden", hidden, typeof(bool));
            info.AddValue("Data", byteData, typeof(byte[]));
            info.AddValue("Keys", Keys.ToList(), typeof(List<Tuple<string, Tuple<byte[], byte[]>>>));

        }

        // The special constructor is used to deserialize values.
        public FileObject(SerializationInfo info, StreamingContext context)
        {
            // Reset the property value using the GetValue method.
            fileName = (string)info.GetValue("fileName", typeof(string));
            size = (long)info.GetValue("size", typeof(long));
            creationDate = (DateTime)info.GetValue("DateTime", typeof(DateTime));
            compressed = (bool)info.GetValue("compressed", typeof(bool));
            hidden = (bool)info.GetValue("hidden", typeof(bool));
            byteData = (byte[])info.GetValue("Data", typeof(byte[]));
            Keys = new Stack<Tuple<string, Tuple<byte[], byte[]>>>(
                    new Stack<Tuple<string, Tuple<byte[], byte[]>>>
                        ((List<Tuple<string, Tuple<byte[], byte[]>>>)
                            info.GetValue("Keys", typeof(List<Tuple<string, Tuple<byte[], byte[]>>>))));
            myConCurrentQueue = new MyConCurrentQueue();
        }


        public void setHidden(bool hidden)
        {
            this.hidden = hidden;
        }

        private String getState()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"[{fileName},{size},{creationDate},{compressed},{hidden}]");
            sb.Append(",");
            List<Tuple<string, Tuple<byte[], byte[]>>> stack = Keys.ToList();
            sb.Append("[");
            foreach (Tuple<string, Tuple<byte[], byte[]>> tup in stack)
            {
                string pwd = tup.Item1;
                Tuple<byte[], byte[]> saltAndIV = tup.Item2;
                byte[] salt = saltAndIV.Item1;
                byte[] IV = saltAndIV.Item2;
                sb.Append($"[{pwd},{salt},{IV}]");
            }
            sb.Append("],");
            sb.Append($"[{Encoding.UTF8.GetString(byteData)}]]");
            return sb.ToString();
        }

     

        public long getSize()
        {
            return size;
        }

        public string getName()
        {
            return fileName;
        }

        public FileObject copy(string fileName)
        {
            FileObject filecopy = new FileObject(fileName);
            filecopy.Keys = new Stack<Tuple<string, Tuple<byte[], byte[]>>>
                (new Stack<Tuple<string, Tuple<byte[], byte[]>>>(this.Keys));
            filecopy.byteData = this.byteData.Clone() as byte[];
            filecopy.creationDate = this.creationDate;
            filecopy.size = this.size;
            return filecopy;
        }

        public String toString()
        {
            // "report.pdf,630KB,Friday, ‎May ‎13, ‎2022, ‏‎12:16:32 PM",
            // "table1.csv,220KB,Monday, ‎February ‎14, ‎2022, ‏‎8:38:24 PM" }
            return $"{fileName},{size}KB,{creationDate}";
        }

        /// <summary>
        /// On given key we call TextEncryptor to encrypt the key. we get in return 
        /// Tuple< the Key after encryption , Tuple< salt, IV> we push the password with salt and IV to the stack
        /// to Ensure its the last Encryption made
        /// We then ecrypt the file with Encrypted Password for extrea security</salt>
        /// </summary>
        /// <param name="key"></param>
        public void encryptKey(string key)
        {
            Tuple<byte[], Tuple<byte[], byte[]>> data = TextEncryptor.Encrypt(Encoding.UTF8.GetBytes(key), this.fileName);
            string pwd = Encoding.UTF8.GetString(data.Item1);
            Tuple<byte[], byte[]> saltAndIV = data.Item2;
            byte[] salt = saltAndIV.Item1;
            byte[] IV = saltAndIV.Item2;
            Keys.Push(Tuple.Create(pwd, Tuple.Create(salt, IV)));
            this.byteData = TextEncryptor.Encrypt(this.byteData, pwd, salt, IV).Item1;
            Console.WriteLine($"{fileName} {pwd} Encrypted");
            //Console.WriteLine($"{fileName} {key} {pwd}");
        }

        // We Peek on the first item in the stack, we get Password, salt, IV
        // on Key Given we send it with the salt And IV,
        // if key is the currect one we will get the same output from the TextEncryptor
        //        after verify we decrypt the file with password, and pop the key, return true;
        // else the key is not curret return false
        public bool decryptKey(string key)
        {
            if (Keys.Count > 0)
            {
                Tuple<string, Tuple<byte[], byte[]>> topKey = Keys.Peek();
                string pwd = topKey.Item1;
                Tuple<byte[], byte[]> saltAndIV = topKey.Item2;
                byte[] salt = saltAndIV.Item1;
                byte[] IV = saltAndIV.Item2;
                string Inputpwd = Encoding.UTF8.GetString(TextEncryptor.Encrypt(Encoding.UTF8.GetBytes(key), this.fileName, salt, IV).Item1);

                if (Inputpwd == pwd)
                {
                    Console.WriteLine($"{fileName} {key} {pwd} Decrypted");

                    this.byteData = TextEncryptor.Decrypt(this.byteData, pwd, salt, IV);
                    Keys.Pop();
                    return true;
                }
            }
            return false;
        }

    
    }

    // *** comparetors for files, wasn't needed in the end *** //
    public class NameCompare : IComparer<FileObject>
    {
        public int Compare(FileObject fo1, FileObject fo2)
        {
            return String.Compare(fo1.getName(), fo2.getName());
        }
    }

    public class SizeCompare : IComparer<FileObject>
    {
        public int Compare(FileObject fo1, FileObject fo2)
        {
            if (fo1.getSize() > fo2.getSize())
                return 1;
            else if (fo1.getSize() < fo2.getSize())
                return -1;
            return 0;
        }
    }

    public class DateCompare : IComparer<FileObject>
    {
        public int Compare(FileObject fo1, FileObject fo2)
        {
            return DateTime.Compare(fo1.getCreationDate(), fo2.getCreationDate());
        }
    }
}




