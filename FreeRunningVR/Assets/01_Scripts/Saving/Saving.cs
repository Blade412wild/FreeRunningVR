using System;
using System.IO;
using UnityEngine;

public class Saving : MonoBehaviour
{
    public static Action OnActionDone;
    [SerializeField] private string name;
    [SerializeField] private int age;
    [SerializeField] private float height;

    [SerializeField] private Texture2D tex;
    private byte[] encodedPNG;

    private data dataTest;

    private string fullPath;

    public struct data
    {
        public string _name;
        public int _age;
        public float _height;
        public byte[] _encodedPNG;
    }

    private void Start()
    {
        dataTest = new data
        {
            _name = name,
            _age = age,
            _height = height
        };
    }

    public void OnLoadButton()
    {
        ////Debug.Log("Attempting Loading");
        //string[] options = StandaloneFileBrowser.OpenFilePanel("Open File", Application.persistentDataPath, "txt", false);
        //if (options.Length == 0) return;
        //string filepath = options[0];
        //Load(filepath);
    }

    public void OnSaveButton()
    {
        //Debug.Log("Attempting Saving");
        string filePath = "";// StandaloneFileBrowser.SaveFilePanel("Save File", Application.persistentDataPath, "Wow_Je_Saved_Een_Keer.txt", "txt");
        if (filePath.Length == 0) return;
        EncodeTexToPNG();
        Save(filePath, dataTest);
    }

    private void Save(string _filePath, data dataPackage)
    {
        StreamWriter writer = new StreamWriter(_filePath);
        writer.WriteLine(JsonUtility.ToJson(dataPackage, false));
        writer.Close();
        writer.Dispose();
        //Debug.Log("Saved File To : " + _filePath);
        OnActionDone?.Invoke();
    }

    private void Load(string _filePath)
    {
        StreamReader reader = new StreamReader(_filePath);
        data dataSetting = JsonUtility.FromJson<data>(reader.ReadToEnd());

        name = dataSetting._name;
        age = dataSetting._age;
        height = dataSetting._height;
        encodedPNG = dataSetting._encodedPNG;

        ImageConversion.LoadImage(tex, encodedPNG);
        //Debug.Log("Loaded File from : " + _filePath);
        OnActionDone?.Invoke();

    }

    private void EncodeTexToPNG()
    {
        //Debug.Log("attempting to encode " + tex.name + " to .png");
        byte[] bytes = ImageConversion.EncodeToPNG(tex);
        dataTest._encodedPNG = bytes;
        encodedPNG = bytes;
        //Debug.Log("encoded " + tex.name + " to .png");
    }

    public void OnExportButton()
    {
        //Debug.Log("Attempting toExport");
        string filePath = "";//StandaloneFileBrowser.SaveFilePanel("Export as PNG", Application.persistentDataPath, "NewPNG", "png");

        if (filePath.Length == 0) return;
        EncodeTexToPNG();
        ExportAsPNG(filePath, encodedPNG);
    }

    private void ExportAsPNG(string _filePath, byte[] dataPackage)
    {
        File.WriteAllBytes(_filePath, dataPackage);
        OnActionDone?.Invoke();
    }

}
