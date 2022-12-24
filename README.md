# TinyMemFS

## Tiny Memory File System
Supproting many operation to mange files on memory, for compact and fast systems, Encryption and Decryption.
Can be shared among users. ( Yet to support it, but its there) by developing the system architechture around multithreded
Operations, avoiding conflicts and race conditions.

![image](https://user-images.githubusercontent.com/62882347/209450436-55c8e8e2-ff03-42ef-a4ec-2cc240197e6a.png)

In our TinyMemFS we choose to handle all the files in dictionary to allow fast access to each file with searching by name. 
In addition, we have a list so we can Sort by Name, Date, Size and return Files as String List.
To Store all the Files and their data we implemented class name FileObject, hold the different traits and data of each file added to our system. 
The encryption and decryption passed to fileObject to handle. In this way, we encapsulate all operation done data and add another layer of privacy to our system.

## The MultiThreaded
We Implemented myConCurrentQueue which hold built-in ConCurrentQueue given by C-sharp, each thread doing operation of file or TinyMemFS will join to Queue of its own. 
After added to queue, the thread will try in while Loop to peek at the first Int (ManagedThreaID) till he is first in Line, and will be released to continue the process. Some Operation were heavy like encryption of all files so to avoid the Bottleneck of each file potentially waiting all the previous Queues to over till we release him to encrypt, to solve this we added another Queue with higher priority, and the Queues are managed with Peterson’s algorithms. Here we avoid starvation and allowing encrypt/decrypt to push to the head of line. 
Each FileObject Implement the Queue so each atomic operation Independent to each file.

## Encryption and Decryption
We Choose AES encryption algorithm because this the algorithm that was recommended by peers. 
all the encryption keys kept on each file on stack with the salt,IV, so we can decrypt only on reverse Order of encryption keys.

![image](https://user-images.githubusercontent.com/62882347/209450722-bdb31ea3-f42b-49b9-ad15-8fcdb174e9ef.png)

Encrypt
Each encryption request entering to the system following the following steps: For the example, we encrypt helloworld.txt:
• Search fileName in dictionary and get the FileObject related.
• We call FileObject.encrypt(“ByeWorld”).
• We encrypt the Key and keep the encrypted key, salt, IV returned with tuple.
• Add the encrypted key to FileObject.Keys (stack).
• Then Ecrypt the FileData saved with bytes

![image](https://user-images.githubusercontent.com/62882347/209450732-db62c226-1c91-4762-9e52-dbaea442e1a8.png)

Decrypt
• Want to Decrypt helloworld.txt, given Key “ByeWorld”.
• Get FileObject related to helloworld.txt by searching in FilesDict.
• Call FileObject.Decrypt(“ByeWorld”).
• Peek in the stack for the first object, saving encrypted Key, Salt, IV on local.
• User Input = Encrypt the “ByeWorld” given the Salt, IV.
• If User Input Equal to encrypted Key
• We Decrypt the FileObject.Data and Pop the Key.

![image](https://user-images.githubusercontent.com/62882347/209450737-ace7e210-d87f-4975-b3bf-44bc9f319dd1.png)

Load\Save To Disk
To support this opertaion we made FileObject Serializable and implement
ISerializable interface, required methods:
* FileObject(SerializationInfo info, StreamingContext context)
GetObjectData(SerializationInfo info, StreamingContext context) *
And calling in each method to serialize\deserialize the object public\private fields. We contain all the files in the TinyMemFS in List of FileObjects.
List is serializeable, therefore call to serialize the List, each FileObject is serialized with the methods implemented by us saved to FileName.myData.
On Load we call deserialize on FileName.myData and catch the returned List of FileObjects, clearing the dictionary and adding all the FileObjects returned.

In the not mandatory functions we chose to implement: saveToDisk, loadFromDisk, NOTE:setHidden, reaname, copy, sortByName, sortByDate, sortBySize, compare, getSize. (All the functions except compressFile and uncompressFile). saving the file system in a file in a directory The functions saveToDisk, loadFromDisk NOTE:inside the project TinyMemFS\bin\Dubug\Backup in the GUI application project
Now we will demonstrate all the operations we implement in a GUI.

## TinyMemFS GUI application
Our main screen of the GUI looks like that:
![image](https://user-images.githubusercontent.com/62882347/209450571-4f568d90-4c37-497f-ad00-53328c66e864.png)

The user can see in the main screen all the operations he can perform on the TinyMemFS. For example, add/remove. 
In order to add, the user need to give file name and a full path. For demonstration we will add three files to the system.
Now the systems look like that:
![image](https://user-images.githubusercontent.com/62882347/209450583-4fb8bfbf-0e10-42af-962d-0b8540a072f5.png)
Notice that the TextBox of total files size updated with changes om the TinyMemFS.

In order to delete a file, the user needs to mark a row and press the "Remove file" button. For example, to delete file name 'dog':

![image](https://user-images.githubusercontent.com/62882347/209450592-def89f3c-d090-4cf1-b4bc-64633583c24f.png)

Our GUI also support operations: save to disk, encrypt and decrypt. 
To perform these operations the user needs to insert the relevant arguments and click the match button. 
For these operations, we give the user information about the status of the operations – succeed/failed.

For example, if the user tries to Decrypt using not valid key:
![image](https://user-images.githubusercontent.com/62882347/209450611-e3bdcca3-7f0a-40af-b64f-7523667f9279.png)

Or whether the user tries to save file in not valid path or file name that doesn’t exist in the system
![image](https://user-images.githubusercontent.com/62882347/209450628-dfb1bbc7-4fcc-4ab0-aa13-846f0a292e34.png)

The user can also rename a file, for example:
![image](https://user-images.githubusercontent.com/62882347/209450632-22ccb8e5-7933-4d39-b4bb-0b09092faeed.png)

Now the table has changed:
![image](https://user-images.githubusercontent.com/62882347/209450634-8dab50d5-ba3a-41d0-9641-08da7ca21638.png)

The user can also create a copy of file he wants, and give the copy a name, for example:
![image](https://user-images.githubusercontent.com/62882347/209450637-74f56c66-6cba-4b30-a281-fb8498cb4bae.png)

After clicking "create copy" the table looks like that:
![image](https://user-images.githubusercontent.com/62882347/209450640-05ef82ac-dd96-411e-9e4f-ab62e122f0b8.png)

The user can also save the file system to the disk and load file system from disk. Continue with our example, if we press File tab:
![image](https://user-images.githubusercontent.com/62882347/209450642-313954a1-91c5-4904-acab-7f805b3bfe79.png)

Now we save our system to the disk. The next window will pop and we will save the file system to file name "dugma"
![image](https://user-images.githubusercontent.com/62882347/209450648-ab8bc4c3-1779-4723-af6e-c483b873cb0a.png)

We can see the file in the path we defined for save\load functions in the directory of the project.
![image](https://user-images.githubusercontent.com/62882347/209450653-726d2c96-40c9-4252-bdbb-4481b98143b4.png)

Now, our file system is saved on the disk. 
If we close the current running of the program and run again, the file system file be empty.
When we click Load system from disk:
![image](https://user-images.githubusercontent.com/62882347/209450658-edd63924-20ba-4284-8c33-8c021816df05.png)

After pressing load button, the system will be exactly the same system we saved before.
![image](https://user-images.githubusercontent.com/62882347/209450669-2695fb6c-53b1-458b-b284-19268100c9b0.png)

The user can also sort the data of the file system using the sort button by name, size or date using the tab 'File':
![image](https://user-images.githubusercontent.com/62882347/209450676-8e7cf8ec-540a-4e42-b940-539efa2dd8ba.png)

By name:
![image](https://user-images.githubusercontent.com/62882347/209450684-b83c4c3c-7f5b-49af-b36f-872a21b29703.png)

By size:
![image](https://user-images.githubusercontent.com/62882347/209450691-aa12cda6-79b8-4936-a531-370708f01864.png)

By date:
![image](https://user-images.githubusercontent.com/62882347/209450695-816934d4-a716-4250-ba25-070233f4b310.png)



