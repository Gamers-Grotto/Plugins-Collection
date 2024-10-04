using CustomPluginInitializer.Interfaces;

namespace CustomPluginInitializer;

public class FileCreator<T> where T : IFile
{
    public T Data { get; set; }
        
    public FileCreator(T data)
    {
        Data = data;
    }
    public void CreateFile(string path)
    {
        // Generate the content for the file
        string content = Data.GenerateFileContent();

        // Create the directory if it does not exist
        Directory.CreateDirectory(path);

        // Write the content to a file at the specified path
        File.WriteAllText(Path.Combine(path, Data.FileName), content);

        // Log the path to the created file
        Console.WriteLine($"Created {Data.FileName} at: " + path);
    }
}