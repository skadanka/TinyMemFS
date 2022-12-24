using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace TinyMemFS
{
    // *** Main class to Implement all method, we Implemnted all methods *Excepts* compress, decompress *** //
    class TinyMemFS
    {
        Dictionary<string, FileObject> filesDict;
        public List<FileObject> filesList;
        private MyConCurrentQueue myConCurrentQueue;

        // Dictionary store <filename, FileObject> allowing fast accses for editing/searching for requested files in the system
        // filesData a secondary list holding the files, the dictionary and the list share the same references to the FileObjects in the system
        //          used for printing sorting etc...
        // myConCurrentQueue is fifo safe thread (hopefully) queue used to mange big operations such add/remove that hold the entire list in the operation
        public TinyMemFS()
        {
            // constructor
            filesDict = new Dictionary<string, FileObject>();
            filesList = new List<FileObject>();
            myConCurrentQueue = new MyConCurrentQueue();
        }

        /// <summary>
        ///
        /// </summary>
        /// add the files to the data structures after getting the turn in the Queue;
        /// <param name="fileName"></param> 
        /// <param name="fileToAdd"></param>
        /// <returns></returns>
        public bool add(String fileName, String fileToAdd)
        {
            // fileName - The name of the file to be added to the file system
            // fileToAdd - The file path on the computer that we add to the system
            // return false if operation failed for any reason
            // Example:
            // add("name1.pdf", "C:\\Users\\user\Desktop\\report.pdf")
            // note that fileToAdd isn't the same as the fileName
            if (File.Exists(fileToAdd))
            {
                myConCurrentQueue.WaitOne();
                FileObject file = new FileObject(fileName, fileToAdd);
                filesDict.Add(fileName, file);
                filesList.Add(file);
                myConCurrentQueue.Release();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removing the files from the DataStructures after getting the turn in line
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool remove(String fileName)
        {
            // fileName - remove fileName from the system
            // this operation releases all allocated memory for this file
            // return false if operation failed for any reason
            // Example:
            // remove("name1.pdf")
            FileObject file;
            if (filesDict.TryGetValue(fileName, out file))
            {
                myConCurrentQueue.WaitOne();
                filesDict.Remove(fileName);
                filesList.Remove(file);
                myConCurrentQueue.Release();
                return true;
            }
            return false;
        }

        // we slice and create temporary list for printing making it thread safe for the print function to continue
        public List<String> listFiles()
        {
            // The function returns a list of strings with the file information in the system
            // Each string holds details of one file as following: "fileName,size,creation time"
            // Example:{
            // "report.pdf,630KB,Friday, ‎May ‎13, ‎2022, ‏‎12:16:32 PM",
            // "table1.csv,220KB,Monday, ‎February ‎14, ‎2022, ‏‎8:38:24 PM" }
            // You can use any format for the creation time and date
            myConCurrentQueue.WaitOne();
            List<string> stringList = filesList.GetRange(0, filesList.Count).ConvertAll(obj => obj.toString());
            myConCurrentQueue.Release();
            return stringList;
        }

        // after checking we can save the file we request from Each File queue to enter and writing to the requested path all the bytes
        public bool save(String fileName, String fileToAdd)
        {
            // this function saves file from the TinyMemFS file system into a file in the physical disk
            // fileName - file name from TinyMemFS to save in the computer
            // fileToAdd - The file path to be saved on the computer
            // return false if operation failed for any reason
            // Example:
            // save("name1.pdf", "C:\\tmp\\fileName.pdf")
            bool flag = false;
            FileObject save_file;
            if (File.Exists(fileToAdd) || !filesDict.TryGetValue(fileName, out save_file))
                return false;

            save_file.myConCurrentQueue.WaitOne();
            try
            {
                File.WriteAllBytes(fileToAdd, save_file.getBytes());
                flag = true;
            }
            catch (Exception error)
            {
                File.Delete(fileToAdd);
                flag = false;
            }
            save_file.myConCurrentQueue.Release();

            return flag;
        }

        // Iterating over all files in the system and encrypting each one after enterting to the queue of each file
        // we give this operation higher priority to avoid bottle necks of holding all the files to wait all the previous files Queues
        // the file push in line and enter after the current task finished
        public bool encrypt(String key)
        {
            // key - Encryption key to encrypt the contents of all files in the system 
            // You can use an encryption algorithm of your choice
            // return false if operation failed for any reason
            // Example:
            // encrypt("myFSpassword")
            //huge bottle neck here, each file will wait for other file to enter the queue will cause problem
            const int priority = 1;
            foreach (FileObject file in filesList.GetRange(0, filesList.Count))
            {
                file.myConCurrentQueue.WaitOne(priority);
                file.encryptKey(key);
                file.myConCurrentQueue.Release(priority);
            }
            return true;
        }


        // Iterating over all files in the system and Dycrypting each one after enterting to the queue of each file
        // we give this operation higher priority to avoid bottle necks of holding all the files to wait all the previous files Queues
        // the file push in line and enter after the current task finished
        public bool decrypt(String key)
        {
            // fileName - Decryption key to decrypt the contents of all files in the system 
            // return false if operation failed for any reason
            // Example:
            // decrypt("myFSpassword")
            bool failedDecrypt = true;
            const int priority = 1;
            foreach (FileObject file in filesList.GetRange(0, filesList.Count))
            {
                file.myConCurrentQueue.WaitOne(priority);
                failedDecrypt = failedDecrypt && file.decryptKey(key);
                file.myConCurrentQueue.Release(priority);
            }
            return failedDecrypt;
        }

        // ************** NOT MANDATORY ********************************************
        // ********** Extended features of TinyMemFS ********************************
        public bool saveToDisk(String fileName)
        {
            /*
             * Save the FS to a single file in disk
             * return false if operation failed for any reason
             * You should store the entire FS (metadata and files) from memory to a single file.
             * You can decide how to save the FS in a single file (format, etc.) 
             * Example:
             * SaveToDisk("MYTINYFS.DAT")
             */
            // Saveing the files backup inside TinyMemFs\bin\Debug\Backup
            // using fromatter.Serialize to save the List<FileObject>, FileObject.Serialize Implenment Custom in class.
            // we try catch error if anything go wrong
            //fileName = "Backup\\{fileName}.myData";
            fileName = $@"Backup\{fileName}.myData";
            bool succses = true;
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            myConCurrentQueue.WaitOne();
            try
            {
                IFormatter formatter = new BinaryFormatter();
                FileStream s = new FileStream(fileName, FileMode.Create);
                formatter.Serialize(s, filesList);
                s.Close();
            }
            catch (Exception error)
            {
                Console.WriteLine($"Failed to save Data {fileName} {error.Message}");
                succses = false;
            }
            myConCurrentQueue.Release();
            return succses;
        }


        public bool loadFromDisk(String fileName)
        {
            /*
             * Load a saved FS from a file  
             * return false if operation failed for any reason
             * You should clear all the files in the current TinyMemFS if exist, before loading the filenName
             * Example:
             * LoadFromDisk("MYTINYFS.DAT")
             */
            // Loading the file requested if found in the TinyMemFs\bin\Debug\Backup
            // if does we clear our dictionary and update the filesList, filesDict with the loaded files
            bool succses = true;
            //fileName = "Backup\\{fileName}.myData";
            fileName = $@"Backup\{fileName}.myData";
            myConCurrentQueue.WaitOne();
            try
            {
                IFormatter formatter = new BinaryFormatter();
                FileStream s1 = new FileStream(fileName, FileMode.Open);
                filesList = (List<FileObject>)formatter.Deserialize(s1);
                s1.Close();
                filesDict.Clear();
                foreach (FileObject file in filesList)
                    filesDict.Add(file.fileName, file);
            }
            catch (Exception error)
            {
                Console.WriteLine($"Failed to Load {fileName} {error.Message}");
                succses = false;
            }
            myConCurrentQueue.Release();
            return succses;
        }

        public bool compressFile(String fileName)
        {
            /* Compress file fileName
             * return false if operation failed for any reason
             * You can use an compression/uncompression algorithm of your choice
             * Note that the file size might be changed due to this operation, update it accordingly
             * Example:
             * compressFile ("name1.pdf");
             */
            return false;
        }

        public bool uncompressFile(String fileName)
        {
            /* uncompress file fileName
             * return false if operation failed for any reason
             * You can use an compression/uncompression algorithm of your choice
             * Note that the file size might be changed due to this operation, update it accordingly
             * Example:
             * uncompressFile ("name1.pdf");
             */
            return false;
        }

        // trical field update
        public bool setHidden(String fileName, bool hidden)
        {
            /* set the hidden property of fileName
             * If file is hidden, it will not appear in the listFiles() results
             * return false if operation failed for any reason
             * Example:
             * setHidden ("name1.pdf", true);
             */
            FileObject file;
            if (filesDict.TryGetValue(fileName, out file))
            {
                file.setHidden(hidden);
            }
            return false;
        }

        public bool rename(String fileName, String newFileName)
        {
            /* Rename filename to newFileName
             * Return false if operation failed for any reason (E.g., newFileName already exists)
             * Example:
             * rename ("name1.pdf", "name2.pdf");
             */
            if (filesDict.ContainsKey(newFileName))
                return false;
            FileObject file;
            if (filesDict.TryGetValue(fileName, out file))
            {
                file.fileName = newFileName;
                filesDict.Remove(fileName);
                filesDict.Add(newFileName, file);
                return true;
            }
            return false;
        }

        // creating copy of each file reAdding to the dictionary
        public bool copy(String fileName1, String fileName2)
        {
            /*Rename filename1 to a new filename2
            * Return false if operation failed for any reason (E.g., fileName1 doesn't exist or filename2 already exists)
            * Example:
             *rename("name1.pdf", "name2.pdf");*/
            if (filesDict.ContainsKey(fileName2))
                return false;
            FileObject file;
            if (filesDict.TryGetValue(fileName1, out file))
            {
                file.myConCurrentQueue.WaitOne();
                FileObject fileCopy = file.copy(fileName2);
                filesDict.Add(fileName2, fileCopy);
                filesList.Add(fileCopy);
                file.myConCurrentQueue.Release();
                return true;
            }
            return false;

        }


        public void sortByName()
        {
            /* Sort the files in the FS by their names (alphabetical order)
             * This should affect the order the files appear in the listFiles 
             * if two names are equal you can sort them arbitrarily
             */
            myConCurrentQueue.WaitOne();
            filesList.Sort((f1, f2) => f1.getName().CompareTo(f2.getName()));
            myConCurrentQueue.Release();
            return;
        }

        public void sortByDate()
        {
            /* Sort the files in the FS by their date (new to old)
             * This should affect the order the files appear in the listFiles  
             * if two dates are equal you can sort them arbitrarily
             */
            myConCurrentQueue.WaitOne();
            filesList.Sort((f1, f2) => f1.getCreationDate().CompareTo(f2.getCreationDate()));
            myConCurrentQueue.Release();
            return;
        }

        public void sortBySize()
        {
            /* Sort the files in the FS by their sizes (large to small)
             * This should affect the order the files appear in the listFiles  
             * if two sizes are equal you can sort them arbitrarily
             */
            myConCurrentQueue.WaitOne();
            filesList.Sort((f1, f2) => f1.getSize().CompareTo(f2.getSize()));
            myConCurrentQueue.Release();
            return;
        }


        public bool compare(String fileName1, String fileName2)
        {
            /* compare fileName1 and fileName2
             * files considered equal if their content is equal 
             * Return false if the two files are not equal, or if operation failed for any reason (E.g., fileName1 or fileName2 not exist)
             * Example:
             * compare ("name1.pdf", "name2.pdf");
             */
            FileObject file1;
            FileObject file2;
            if (filesDict.TryGetValue(fileName1, out file1) && filesDict.TryGetValue(fileName2, out file2))
            {
                byte[] data1 = file1.getBytes();
                byte[] data2 = file2.getBytes();
                if (data1.Length != data2.Length)
                    return false;
                for (int i = 0; i < data1.Length; i++)
                    if (data1[i] != data2[i]) return false;
            }
            else
                return false;
            return true;
        }

        public Int64 getSize()
        {
            /* return the size of all files in the FS (sum of all sizes)
             */
            return filesList.Sum(file => file.getSize());
        }

    }

    // *** FileObject class, contain data keys and other traits each file have, encryption and decryption called from this class **** //
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
        }

        public void setHidden(bool hidden)
        {
            this.hidden = hidden;
        }

        // unuseable in this program //
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

	// *** TextEncryptor Class, Used for encryption and decryption Implemented with AES algorithem *** //
	public static class TextEncryptor
    {

            // we Encrypt the given data, if salt and IV given we use them for encryption else we create new and return to the user with the encrypted text and salt, IV //
            public static Tuple<byte[], Tuple<byte[], byte[]>> 
            Encrypt(byte[] plainText, string EncryptionKey, byte[] saltStringBytes = null, byte[] ivStringBytes = null)
        {
         
            Tuple<byte[], Tuple<byte[], byte[]>> tuple_data;
            using (Aes encryptor = Aes.Create())
            {
                if (saltStringBytes == null)
                {
                    saltStringBytes = new Byte[16];

                    //RNGCryptoServiceProvider is an implementation of a random number generator.
                    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                    rng.GetBytes(saltStringBytes); // The array is now filled with cryptographically strong random bytes.
                }
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, saltStringBytes);
                encryptor.Key = pdb.GetBytes(32);
                if(ivStringBytes == null)
                    encryptor.IV = pdb.GetBytes(16);
                else
                    encryptor.IV = ivStringBytes;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(plainText, 0, plainText.Length);
                        cs.Close();
                    }
                    plainText = ms.ToArray();
                    tuple_data = Tuple.Create(plainText, Tuple.Create(saltStringBytes, encryptor.IV));
                }
            }
            return tuple_data;
        }

        // Given Text and decrpytion key, salt, IV we try to decrypt with AES, if IV wasnt given by the user it will fail anyway, return the decrypted Text //
        public static byte[] Decrypt(byte[] plainText, string EncryptionKey, byte[] saltStringBytes = null, byte[] ivStringBytes = null)
        {

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, saltStringBytes);
                encryptor.Key = pdb.GetBytes(32);
                if (ivStringBytes == null)
                    encryptor.IV = pdb.GetBytes(16);
                else
                    encryptor.IV = ivStringBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(plainText, 0, plainText.Length);
                        cs.Close();
                    }
                    plainText = ms.ToArray();
                }
            }
            return plainText;
        }
    }

        // *** Thread Safe FIFO Queue Implemention, used to Mange all the differnt levels of request from TinyMemFS all files, to each file in its Own *** //
        public class MyConCurrentQueue
        {
            private ConcurrentQueue<int> concurrentQueuePrior0;
            private ConcurrentQueue<int> concurrentQueuePrior1;
            public long f_maintance = 0;
            private long taskRunning = 0;
            private volatile bool[] flag = new bool[] { false, false };
            private volatile int turn;

            public MyConCurrentQueue()
            {
                concurrentQueuePrior0 = new ConcurrentQueue<int>();
                concurrentQueuePrior1 = new ConcurrentQueue<int>();
            }

            public void WaitOne(int priority = 0)
            {
                if (priority == 0)
                    enterQueue();
                else if (priority == 1)
                    enterMaintance();
            }

            public void Release(int priority = 0)
            {
                if (priority == 0)
                    exitQueue();
                else if (priority == 1)
                    exitMaintance();
            }

            private void enterQueue()
            {
                concurrentQueuePrior0.Enqueue(Thread.CurrentThread.ManagedThreadId);
                int result = -1;
                while (result != Thread.CurrentThread.ManagedThreadId)
                    while (!concurrentQueuePrior0.TryPeek(out result))
                        Thread.Sleep(100);
                flag[0] = true;
                turn = 1;
                while (flag[1] == true && turn == 1) ;
                Console.WriteLine($"Thread Left Queue currectly Prior 0 {Thread.CurrentThread.ManagedThreadId}");

                Interlocked.Exchange(ref taskRunning, 1);
            }

            private void exitQueue()
            {
                int result = -1;
                while (!concurrentQueuePrior0.TryDequeue(out result)) ;

                if (result == Thread.CurrentThread.ManagedThreadId)
                    Console.WriteLine($"Thread Left Queue currectly Prior 0 {Thread.CurrentThread.ManagedThreadId}");
                else
                    Console.WriteLine($" Thread Left Queue, Error Prior 0 {result} != {Thread.CurrentThread.ManagedThreadId}");
                flag[0] = false;
                Interlocked.Exchange(ref taskRunning, 0);
            }


            private void enterMaintance()
            {
                concurrentQueuePrior1.Enqueue(Thread.CurrentThread.ManagedThreadId);
                int result = -1;
                while (result != Thread.CurrentThread.ManagedThreadId)
                    while (!concurrentQueuePrior1.TryPeek(out result))
                        Thread.Sleep(100);
                flag[1] = true;
                turn = 0;
                while (flag[0] == true && turn == 0) ;
                Console.WriteLine($"Thread Entered Queue currectly Prior 1 {Thread.CurrentThread.ManagedThreadId}");

                Interlocked.Exchange(ref taskRunning, 1);
            }

            private void exitMaintance()
            {
                int result = -1;
                while (!concurrentQueuePrior1.TryDequeue(out result)) ;

                if (result == Thread.CurrentThread.ManagedThreadId)
                    Console.WriteLine($"Thread Left Queue currectly Prior 1{Thread.CurrentThread.ManagedThreadId}");
                else
                    Console.WriteLine($" Thread Left Queue, Error Prior 1 {result} != {Thread.CurrentThread.ManagedThreadId}");

                flag[1] = false;
                Interlocked.Exchange(ref taskRunning, 0);
            }
        }
    }





