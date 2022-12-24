using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TinyMemFS
{
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
}