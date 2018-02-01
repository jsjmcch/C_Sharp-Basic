// FileStream Read
using System;
using System.IO;

class Test
{

public static void Main()
{
    // Specify a file to read from and to create.
    string pathSource = @"c:\tests\source.txt";
    string pathNew = @"c:\tests\newfile.txt";

    try
    {

        using (FileStream fsSource = new FileStream(pathSource,
            FileMode.Open, FileAccess.Read))
        {

            // Read the source file into a byte array.
            byte[] bytes = new byte[fsSource.Length];
            int numBytesToRead = (int)fsSource.Length;
            int numBytesRead = 0;
            while (numBytesToRead > 0)
            {
                // Read may return anything from 0 to numBytesToRead.
                int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

                // Break when the end of the file is reached.
                if (n == 0)
                    break;

                numBytesRead += n;
                numBytesToRead -= n;
            }
             numBytesToRead = bytes.Length;

            // Write the byte array to the other FileStream.
            using (FileStream fsNew = new FileStream(pathNew,
                FileMode.Create, FileAccess.Write))
            {
                fsNew.Write(bytes, 0, numBytesToRead);
            }
        }
    }
    catch (FileNotFoundException ioEx)
    {
        Console.WriteLine(ioEx.Message);
    }
}
}

//FileStream Move
using System;
using System.IO;

class Test 
{
    public static void Main() 
    {
        string path = @"c:\temp\MyTest.txt";
        string path2 = @"c:\temp2\MyTest.txt";
        try 
        {
            if (!File.Exists(path)) 
            {
                // This statement ensures that the file is created,
                // but the handle is not kept.
                using (FileStream fs = File.Create(path)) {}
            }

            // Ensure that the target does not exist.
            if (File.Exists(path2))	
            File.Delete(path2);

            // Move the file.
            File.Move(path, path2);
            Console.WriteLine("{0} was moved to {1}.", path, path2);

            // See if the original exists now.
            if (File.Exists(path)) 
            {
                Console.WriteLine("The original file still exists, which is unexpected.");
            } 
            else 
            {
                Console.WriteLine("The original file no longer exists, which is expected.");
            }			

        } 
        catch (Exception e) 
        {
            Console.WriteLine("The process failed: {0}", e.ToString());
        }
    }
}
