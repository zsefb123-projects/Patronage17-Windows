using System;
using System.IO;

public class aplication 
{

    public static string shortPath(string path){

        string shortPath = "";
        
        if(path.Length > 0){

            char splitChar = '\\';
            int startIndex = 1;
            string separatorString = "\\..";

            if(path[0] == '/'){
                splitChar = '/';
                startIndex = 2;
                separatorString = "/..";
                shortPath = "/";
            }

            string[] splitedPath = path.Split(splitChar);
            int length = splitedPath.Length;
            
            if(length > 5){
                shortPath += splitedPath[startIndex - 1];
                for(int i = startIndex; i < (length - 2); i++)
                    shortPath += separatorString;
                shortPath += splitChar + splitedPath[length - 2] + splitChar + splitedPath[length - 1]; 

                return shortPath;
            }

        }
        
        return path;
    }

    public static string size(long length){
        int i = 1;
        string[] units = new string[] {"b","Kb","Mb","Gb"};
        long[] pows = new long[] {1, 1024, 1048576, 1073741824 }; 
        while( (length / pows[i]) > 0 ){
            i++;
            if(i > 3)
                return "Zbyt duży rozmiar pliku: " + length.ToString();
        } 

        return (length / pows[i - 1]) + "," +(length % pows[i - 1]) + " " + units[i - 1];
    }

    public static void Main(String[] args){
       
        if(args.Length > 0){
            
            try{
                string[] filePaths = Directory.GetFiles(args[0]);
                Console.WriteLine("\n Katalog {0} istnieje \n", args[0]);

                if(filePaths.Length == 0)
                    Console.WriteLine("Katalog nie zawiera plików");

                foreach (string path in filePaths){
                    Console.WriteLine("Plik: {0}", shortPath(path));
                    
                    FileInfo info = new FileInfo(path);
                    Console.WriteLine("Atrybuty: {0}", info.Attributes);
                    Console.WriteLine("Czas utworzenia pliku: {0}", info.CreationTime);
                    Console.WriteLine("Czas ostatniego dostępu: {0}", info.LastAccessTime);
                    Console.WriteLine("Czas ostatniego zapisu: {0}", info.LastWriteTime);
                    Console.WriteLine("Rozmiar pliku: {0} \n", size(info.Length));
                }

            }catch(DirectoryNotFoundException e){

                Console.WriteLine("\n Katalog {0} nie istnieje. \n {1}", args[0], e);
            }

        }else{
            Console.WriteLine("\n Nie podano ścieżki. \n");
        }
    }
}